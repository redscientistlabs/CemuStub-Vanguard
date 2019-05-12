using Newtonsoft.Json;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.NetCore.StaticTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;
using WindowsFilesVanguard;

namespace CemuStub
{
    public static class CemuWatch
    {
        static Timer watch = new Timer();
        public static string expectedCemuTitle = "Cemu 1.15.6c";
        public static string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);


        static FileInfo gameRpxFileInfo = null;
        public static FileInfo cemuExeFile = null;
        static FileInfo[] updateCodeFiles = null;

        static string rpxFile = null;
        static string gameRpxPath = null;
        static string updateRpxPath = null;
        static string updateCodePath = null;
        static string updateMetaPath = null;
        static string updateRpxLocation = null;
        static string updateRpxCompressed = null;
        static string updateRpxBackup = null;
        static string CemuExeLocation;
        static string FirstID = null;
        static string SecondID = null;
        static string fileInterfaceTargetId = null;


        static CemuState state = CemuState.UNFOUND;

        static Process cemuProcess = null;
        internal static bool writeCopyMode = false;

        static FileInterface rpxInterface;

        //File management
        public static Dictionary<String, String> CompositeFilenameDico = null;
        private static string CemuStubVersion = "0.01";
        private static string gameName = null;

        public static void Start()
        {
            if (!Directory.Exists(CemuWatch.currentDir + "\\TEMP\\"))
                Directory.CreateDirectory(CemuWatch.currentDir + "\\TEMP\\");

            if (!Directory.Exists(CemuWatch.currentDir + "\\TEMP2\\"))
                Directory.CreateDirectory(CemuWatch.currentDir + "\\TEMP2\\");

            if (!Directory.Exists(CemuWatch.currentDir + "\\PARAMS\\"))
                Directory.CreateDirectory(CemuWatch.currentDir + "\\PARAMS\\");


            if (File.Exists(currentDir + "\\LICENSES\\DISCLAIMER.TXT") && !File.Exists(currentDir + "\\PARAMS\\DISCLAIMERREAD"))
            {
                MessageBox.Show(File.ReadAllText(currentDir + "\\LICENSES\\DISCLAIMER.TXT").Replace("[ver]", CemuWatch.CemuStubVersion), "Cemu Stub", MessageBoxButtons.OK, MessageBoxIcon.Information);
                File.Create(currentDir + "\\PARAMS\\DISCLAIMERREAD");
            }

            //If we can't load the dictionary, quit the wgh to prevent the loss of backups
            if (!LoadCompositeFilenameDico())
                Application.Exit();

            watch.Interval = 420;
            watch.Tick += Watch_Tick;
            watch.Start();
        }

        private static void Watch_Tick(object sender, EventArgs e)
        {
            ScanCemu();

            if (state == CemuState.RUNNING && cemuProcess.MainWindowTitle.Contains("[TitleId:"))
                state = CemuState.GAMELOADED;

            if(state == CemuState.GAMELOADED)
            {
                state = CemuState.PREPARING; // this prevents the ticker to call this method again

                //Game is loaded in cemu, let's gather all the info we need

                FetchBaseInfoFromCemuProcess();
                KillCemuProcess();
                LoadDataFromCemuFiles();

                // Prepare fake update and backup
                PrepareUpdateFolder();
                CreateRpxBackup();

                LoadRpxFileInterface();

                state = CemuState.READY;
                S.GET<CS_Core_Form>().lbCemuStatus.Text = "Ready for corrupting";
                S.GET<CS_Core_Form>().lbTargetedGame.Text = fileInterfaceTargetId;
                EnableButtons();

                VanguardCore.Start();

            }

        }

        private static void LoadRpxFileInterface()
        {
            fileInterfaceTargetId = "File|" + updateRpxLocation;
            rpxInterface = new FileInterface(fileInterfaceTargetId);
        }

        private static void FetchBaseInfoFromCemuProcess()
        {
            ///
            ///Fetching Game info from cemu process window title
            ///

            string windowTitle = cemuProcess.MainWindowTitle;
            string TitleIdPart = windowTitle.Split('[').FirstOrDefault(it => it.Contains("TitleId:"));
            string TitleNumberPartLong = TitleIdPart.Split(':')[1];
            string TitleNumberPart = TitleNumberPartLong.Split(']')[0];
            string TitleGameNamePart = TitleNumberPartLong.Split(']')[1];

            FirstID = TitleNumberPart.Split('-')[0].Trim();
            SecondID = TitleNumberPart.Split('-')[1].Trim();
            CemuExeLocation = cemuProcess.MainModule.FileName;
            cemuExeFile = new FileInfo(CemuExeLocation);

            gameName = TitleGameNamePart.Trim();

        }

        internal static void KillCemuProcess()
        {
            if (cemuProcess == null)
                return;

            var p = cemuProcess;
            //killing cemu
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "taskkill";
                psi.Arguments = $"/F /IM {cemuExeFile.Name} /T";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                Process.Start(psi);
            }

            p.WaitForExit();
        }

        public static int IndexOf<T>(this T[] haystack, T[] needle)
        {
            if ((needle != null) && (haystack.Length >= needle.Length))
            {
                for (int l = 0; l < haystack.Length - needle.Length + 1; l++)
                {
                    if (!needle.Where((data, index) => !haystack[l + index].Equals(data)).Any())
                    {
                        return l;
                    }
                }
            }

            return -1;
        }

        private static void LoadDataFromCemuFiles()
        {
            ///
            ///gathering data from log.txt and settings.xml files
            ///

            string[] logTxt = File.ReadAllLines(Path.Combine(cemuExeFile.DirectoryName, "log.txt"));
            //string[] settingsXml = File.ReadAllLines(Path.Combine(cemuExeFile.DirectoryName, "settings.xml"));
            byte[] settingsBin = File.ReadAllBytes(Path.Combine(cemuExeFile.DirectoryName, "settings.bin"));



            //getting rpx filename from log.txt
            string logLoadingLine = logTxt.FirstOrDefault(it => it.Contains("Loading") && it.Contains(".rpx"));
            string[] logLoadingLineParts = logLoadingLine.Split(' ');
            rpxFile = logLoadingLineParts[logLoadingLineParts.Length - 1];

            //Getting rpx path from settings.bin
            byte[] rpx = { 0x2E, 0x00, 0x72, 0x00, 0x70, 0x00, 0x78 }; //".rpx" with the extra characters
            int startOffset = 0xB7;
            var endOffset = settingsBin.IndexOf(rpx) + rpx.Length;

            byte[] tmp = new byte[endOffset - startOffset];
            Array.Copy(settingsBin, startOffset, tmp, 0, endOffset - startOffset);
            var chars = Encoding.UTF8.GetChars(tmp);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                if (i % 2 == 0)
                    sb.Append(chars[i]);
            }



            gameRpxPath = sb.ToString();
            gameRpxFileInfo = new FileInfo(gameRpxPath);
            updateRpxPath = Path.Combine(cemuExeFile.DirectoryName, "mlc01", "usr", "title", FirstID, SecondID);

            updateCodePath = Path.Combine(updateRpxPath, "code");
            updateMetaPath = Path.Combine(updateRpxPath, "meta");



            updateRpxLocation = Path.Combine(updateCodePath, rpxFile);
            updateRpxCompressed = Path.Combine(updateCodePath, "compressed_" + rpxFile);
            updateRpxBackup = Path.Combine(updateCodePath, "backup_" + rpxFile);


        }

        public static void UpdateDomains()
        {
            try
            {


                PartialSpec gameDone = new PartialSpec("VanguardSpec");
                gameDone[VSPEC.SYSTEM] = "Wii U";
                gameDone[VSPEC.GAMENAME] = CemuWatch.gameName;
                gameDone[VSPEC.SYSTEMPREFIX] = "Cemu";
                gameDone[VSPEC.SYSTEMCORE] = "Cemu";
                //gameDone[VSPEC.SYNCSETTINGS] = BIZHAWK_GETSET_SYNCSETTINGS;
                gameDone[VSPEC.OPENROMFILENAME] = gameRpxFileInfo.FullName;
                gameDone[VSPEC.MEMORYDOMAINS_BLACKLISTEDDOMAINS] = new string[0];
                gameDone[VSPEC.MEMORYDOMAINS_INTERFACES] = GetInterfaces();
                gameDone[VSPEC.CORE_DISKBASED] = false;
                AllSpec.VanguardSpec.Update(gameDone);

                //This is local. If the domains changed it propgates over netcore
                LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_EVENT_DOMAINSUPDATED, true, true);

                //Asks RTC to restrict any features unsupported by the stub
                LocalNetCoreRouter.Route(NetcoreCommands.CORRUPTCORE, NetcoreCommands.REMOTE_EVENT_RESTRICTFEATURES, true, true);

            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();
            }
        }


        public static MemoryDomainProxy[] GetInterfaces()
        {
            try
            {
                Console.WriteLine($" getInterfaces()");

                List<MemoryDomainProxy> interfaces = new List<MemoryDomainProxy>();

                interfaces.Add(new MemoryDomainProxy(rpxInterface));

                return interfaces.ToArray();
            }
            catch (Exception ex)
            {
                if (VanguardCore.ShowErrorDialog(ex, true) == DialogResult.Abort)
                    throw new RTCV.NetCore.AbortEverythingException();

                return new MemoryDomainProxy[] { };
            }

        }


        internal static void PrepareUpdateFolder(bool overwrite = false)
        {
            if (overwrite)
                if (Directory.Exists(updateRpxPath))
                    Directory.Delete(updateRpxPath,true);


            //Creating fake update if update doesn't already exist
            if (!Directory.Exists(updateRpxPath))
            {
                Directory.CreateDirectory(updateRpxPath);
                Directory.CreateDirectory(updateCodePath);
                Directory.CreateDirectory(updateMetaPath);

                foreach (var file in gameRpxFileInfo.Directory.GetFiles())
                    File.Copy(file.FullName, Path.Combine(updateCodePath, file.Name));

                DirectoryInfo gameDirectoryInfo = gameRpxFileInfo.Directory.Parent;
                DirectoryInfo metaDirectoryInfo = new DirectoryInfo(updateMetaPath);

                foreach (var file in metaDirectoryInfo.GetFiles())
                    File.Copy(file.FullName, updateMetaPath);

            }

            //Uncompress update rpx if it isn't already

            DirectoryInfo updateCodeDirectoryInfo = new DirectoryInfo(updateCodePath);
            updateCodeFiles = updateCodeDirectoryInfo.GetFiles();

            if (updateCodeFiles.FirstOrDefault(it => it.Name == "UNCOMPRESSED.txt") == null)
            {
                File.Move(updateRpxLocation, updateRpxCompressed);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Path.Combine(currentDir, "wiiurpxtool.exe");
                psi.WorkingDirectory = currentDir;
                psi.Arguments = $"-d \"{updateRpxCompressed}\" \"{updateRpxLocation}\"";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                var p = Process.Start(psi);

                p.WaitForExit();

                File.WriteAllText(Path.Combine(updateCodePath, "UNCOMPRESSED.txt"), "DONE");
            }
        }

        public static bool LoadCompositeFilenameDico()
        {
            JsonSerializer serializer = new JsonSerializer();
            string filemapPath = Path.Combine(CemuWatch.currentDir + "\\TEMP\\" + "filemap.json");
            if (!File.Exists(filemapPath))
            {
                CompositeFilenameDico = new Dictionary<string, string>();
                return true;
            }
            try
            {

                using (StreamReader sw = new StreamReader(filemapPath))
                using (JsonTextReader reader = new JsonTextReader(sw))
                {
                    CompositeFilenameDico = serializer.Deserialize<Dictionary<string, string>>(reader);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the filemap! Figure out what's locking it and then restart the WGH.\n" + e.ToString());
                return false;
            }
            return true;
        }

        public static bool SaveCompositeFilenameDico()
        {
            JsonSerializer serializer = new JsonSerializer();
            var path = CemuWatch.currentDir + "\\TEMP\\" + "filemap.json";
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, CompositeFilenameDico);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the filemap!\n" + e.ToString());
                return false;
            }
            return true;
        }


        internal static void ResetBackup() => CreateRpxBackup(true);
        private static void CreateRpxBackup(bool Recreate = false)
        {



            if (Recreate)
                if (File.Exists(updateRpxBackup))
                    File.Delete(updateRpxBackup);

            if (!File.Exists(updateRpxBackup))
            {
                File.Copy(updateRpxLocation, updateRpxBackup);
            }
        }

        internal static void StartRpx()
        {
            rpxInterface.ApplyWorkingFile();

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = CemuExeLocation;
            psi.WorkingDirectory = new FileInfo(CemuExeLocation).DirectoryName;
            psi.Arguments = $"-g \"{gameRpxPath}\"";
            //psi.RedirectStandardOutput = true;
            //psi.RedirectStandardError = true;
            //psi.UseShellExecute = false;
            //psi.CreateNoWindow = true;

            Process.Start(psi);
        }

        private static void ScanCemu()
        {
            Process p = getCemuProcess();

            if (state == CemuState.UNFOUND && p != null)
            {
                S.GET<CS_Core_Form>().lbCemuStatus.Text = "Cemu detected, waiting for a loaded game";
                state = CemuState.RUNNING;
            }
            else if (
                state != CemuState.UNFOUND && 
                state != CemuState.GAMELOADED && 
                state != CemuState.READY && 
                p == null)
            {
                S.GET<CS_Core_Form>().lbCemuStatus.Text = "Waiting for Cemu";
                state = CemuState.UNFOUND;
                DisableButtons();
            }

        }

        internal static void RestoreBackup()
        {
            rpxInterface.CloseStream();

            if (File.Exists(updateRpxBackup))
            {
                if (File.Exists(updateRpxLocation))
                    File.Delete(updateRpxLocation);
                File.Copy(updateRpxBackup, updateRpxLocation);
            }
            else
                MessageBox.Show("Backup could not be found");
        }

        private static Process getCemuProcess()
        {
            if (cemuProcess == null)
            {
                RefreshCemuProcess();
            }
            //Get a new process object from then pid we have. 
            try
            {
                cemuProcess = Process.GetProcessById(cemuProcess?.Id ?? -1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                cemuProcess = null;
            }
            //If the title is still expectedCemuTitle, we know something else didn't eat the pid 
            if (!(cemuProcess?.MainWindowTitle.Contains(expectedCemuTitle) ?? false))
                RefreshCemuProcess();

            return cemuProcess;
        }

        public static void RefreshCemuProcess(Process p = null)
        {
            if(p == null)
                p = Process.GetProcesses().FirstOrDefault(it => it?.MainWindowTitle?.Contains(expectedCemuTitle) ?? false);

            cemuProcess = p;

            if (cemuProcess != null)
            {
                cemuProcess.EnableRaisingEvents = true;
                cemuProcess.Exited += (o, e) =>
                {
                    cemuProcess = null;
                };
            }
        }


        public static void EnableButtons()
        {
            S.GET<CS_Core_Form>().btnResetBackup.Enabled = true;
            S.GET<CS_Core_Form>().btnRestoreBackup.Enabled = true;
            S.GET<CS_Core_Form>().btnStartRpx.Enabled = true;
            S.GET<CS_Core_Form>().btnReconstructFakeUpdate.Enabled = true;
        }

        public static void DisableButtons()
        {
            S.GET<CS_Core_Form>().btnResetBackup.Enabled = false;
            S.GET<CS_Core_Form>().btnRestoreBackup.Enabled = false;
            S.GET<CS_Core_Form>().btnStartRpx.Enabled = false;
            S.GET<CS_Core_Form>().btnReconstructFakeUpdate.Enabled = false;
        }

    }

    enum CemuState
    {
        UNFOUND,
        RUNNING,
        GAMELOADED,
        PREPARING,
        READY
    }
}
