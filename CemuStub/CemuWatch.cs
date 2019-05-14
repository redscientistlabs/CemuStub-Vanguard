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
using CemuStub;

namespace CemuStub
{
    public static class CemuWatch
    {
        static Timer watch = null;
        public static string expectedCemuTitle = "Cemu 1.15.6c";
        public static string currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static Dictionary<string, CemuGameInfo> knownGamesDico = new Dictionary<string, CemuGameInfo>();
        public static CemuGameInfo currentGameInfo = new CemuGameInfo();

        public static bool DontSelectGame = false;

        static CemuState state = CemuState.UNFOUND;

        static Process cemuProcess = null;
        internal static bool writeCopyMode = false;

        static FileInterface rpxInterface;

        //File management
        public static Dictionary<String, String> CompositeFilenameDico = null;
        public static string CemuStubVersion = "0.02";


        public static void Start()
        {
            if (watch != null)
            {
                watch.Stop();
                watch = null;

            }

            CemuWatch.currentGameInfo = new CemuGameInfo();

            S.GET<CS_Core_Form>().lbCemuStatus.Text = "Waiting for Cemu";
            S.GET<CS_Core_Form>().lbTargetedGameId.Text = "";
            S.GET<CS_Core_Form>().lbTargetedGameRpx.Text = "No game selected. Cemu Stub will auto-detect and prepare any game you load in Cemu.";

            DisableButtons();



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

            watch = new Timer();
            watch.Interval = 1500;
            watch.Tick += Watch_Tick;
            watch.Start();
        }

        internal static void UnmodGame()
        {
            KillCemuProcess();

            //remove item from known games and go back to autodetect
            var lastRef = CemuWatch.currentGameInfo;
            CompositeFilenameDico.Remove(lastRef.gameName);
            knownGamesDico.Remove(lastRef.gameName);
            SaveKnownGames();
            S.GET<CS_Core_Form>().cbSelectedGame.SelectedIndex = 0;
            S.GET<CS_Core_Form>().cbSelectedGame.Items.Remove(lastRef.gameName);

            //remove fake update from game
            if(Directory.Exists(lastRef.updateRpxPath))
                Directory.Delete(lastRef.updateRpxPath, true);
        }

        internal static void SelectGame(string selected = null)
        {
            if (selected != null)
                currentGameInfo = knownGamesDico[selected];

            string rpxFullPath = currentGameInfo.gameRpxFileInfo.FullName;
            if (!File.Exists(rpxFullPath))
            {
                string message = "Cemu Stub couldn't find the Rpx file for this game. Would you like to remove this entry?";
                var result = MessageBox.Show(message, "Error finding game", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if(result == DialogResult.Yes)
                    UnmodGame();

                S.GET<CS_Core_Form>().cbSelectedGame.SelectedIndex = 0;

                return;
            }

            LoadRpxFileInterface();

            state = CemuState.READY;
            S.GET<CS_Core_Form>().lbCemuStatus.Text = "Ready for corrupting";
            S.GET<CS_Core_Form>().lbTargetedGameRpx.Text = currentGameInfo.gameRpxFileInfo.FullName;
            S.GET<CS_Core_Form>().lbTargetedGameId.Text = "Game ID: " + currentGameInfo.FirstID + "-" + currentGameInfo.SecondID;
            EnableButtons();


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

                knownGamesDico[currentGameInfo.gameName] = currentGameInfo;

                SelectGame();

                DontSelectGame = true;
                S.GET<CS_Core_Form>().cbSelectedGame.Items.Add(currentGameInfo.gameName);
                S.GET<CS_Core_Form>().cbSelectedGame.SelectedIndex = S.GET<CS_Core_Form>().cbSelectedGame.Items.Count - 1;
                DontSelectGame = false;

                SaveKnownGames();

                VanguardCore.Start();

            }

        }

        public static bool LoadKnownGames()
        {
            JsonSerializer serializer = new JsonSerializer();
            string path = Path.Combine(CemuWatch.currentDir + "\\PARAMS\\" + "knowngames.json");
            if (!File.Exists(path))
            {
                knownGamesDico = new Dictionary<string, CemuGameInfo>();
                return true;
            }
            try
            {

                using (StreamReader sw = new StreamReader(path))
                using (JsonTextReader reader = new JsonTextReader(sw))
                {
                    knownGamesDico = serializer.Deserialize<Dictionary<string, CemuGameInfo>>(reader);
                }

                foreach (var key in knownGamesDico.Keys)
                    S.GET<CS_Core_Form>().cbSelectedGame.Items.Add(key);
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the filemap! Figure out what's locking it and then restart the WGH.\n" + e.ToString());
                return false;
            }
            return true;
        }
        public static bool SaveKnownGames()
        {
            JsonSerializer serializer = new JsonSerializer();
            var path = CemuWatch.currentDir + "\\PARAMS\\" + "knowngames.json";
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, knownGamesDico);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Unable to access the known games!\n" + e.ToString());
                return false;
            }
            return true;
        }

        private static void LoadRpxFileInterface()
        {
            currentGameInfo.fileInterfaceTargetId = "File|" + currentGameInfo.updateRpxLocation;
            rpxInterface = new FileInterface(currentGameInfo.fileInterfaceTargetId);
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

            currentGameInfo.FirstID = TitleNumberPart.Split('-')[0].Trim();
            currentGameInfo.SecondID = TitleNumberPart.Split('-')[1].Trim();
            currentGameInfo.CemuExeLocation = cemuProcess.MainModule.FileName;
            currentGameInfo.cemuExeFile = new FileInfo(currentGameInfo.CemuExeLocation);

            currentGameInfo.gameName = TitleGameNamePart.Trim();

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
                psi.Arguments = $"/F /IM {currentGameInfo.cemuExeFile.Name} /T";
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

            string[] logTxt = File.ReadAllLines(Path.Combine(currentGameInfo.cemuExeFile.DirectoryName, "log.txt"));
            //string[] settingsXml = File.ReadAllLines(Path.Combine(cemuExeFile.DirectoryName, "settings.xml"));
            byte[] settingsBin = File.ReadAllBytes(Path.Combine(currentGameInfo.cemuExeFile.DirectoryName, "settings.bin"));



            //getting rpx filename from log.txt
            string logLoadingLine = logTxt.FirstOrDefault(it => it.Contains("Loading") && it.Contains(".rpx"));
            string[] logLoadingLineParts = logLoadingLine.Split(' ');
            currentGameInfo.rpxFile = logLoadingLineParts[logLoadingLineParts.Length - 1];

            //Getting rpx path from settings.bin
            byte[] rpx = { 0x2E, 0x00, 0x72, 0x00, 0x70, 0x00, 0x78, 0x00 }; //".rpx" encoded as utf-16
            int startOffset = 0xB7;
            var endOffset = settingsBin.IndexOf(rpx) + rpx.Length;

            byte[] tmp = new byte[endOffset - startOffset];
            Array.Copy(settingsBin, startOffset, tmp, 0, endOffset - startOffset);
            var gamePath = Encoding.Unicode.GetString(tmp);



            currentGameInfo.gameRpxPath = gamePath;
            currentGameInfo.gameRpxFileInfo = new FileInfo(currentGameInfo.gameRpxPath);
            currentGameInfo.updateRpxPath = Path.Combine(currentGameInfo.cemuExeFile.DirectoryName, "mlc01", "usr", "title", currentGameInfo.FirstID, currentGameInfo.SecondID);

            currentGameInfo.updateCodePath = Path.Combine(currentGameInfo.updateRpxPath, "code");
            currentGameInfo.updateMetaPath = Path.Combine(currentGameInfo.updateRpxPath, "meta");



            currentGameInfo.updateRpxLocation = Path.Combine(currentGameInfo.updateCodePath, currentGameInfo.rpxFile);
            currentGameInfo.updateRpxCompressed = Path.Combine(currentGameInfo.updateCodePath, "compressed_" + currentGameInfo.rpxFile);
            currentGameInfo.updateRpxBackup = Path.Combine(currentGameInfo.updateCodePath, "backup_" + currentGameInfo.rpxFile);


        }

        public static void UpdateDomains()
        {
            try
            {


                PartialSpec gameDone = new PartialSpec("VanguardSpec");
                gameDone[VSPEC.SYSTEM] = "Wii U";
                gameDone[VSPEC.GAMENAME] = CemuWatch.currentGameInfo.gameName;
                gameDone[VSPEC.SYSTEMPREFIX] = "Cemu";
                gameDone[VSPEC.SYSTEMCORE] = "Cemu";
                //gameDone[VSPEC.SYNCSETTINGS] = BIZHAWK_GETSET_SYNCSETTINGS;
                gameDone[VSPEC.OPENROMFILENAME] = currentGameInfo.gameRpxFileInfo.FullName;
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
                if (Directory.Exists(currentGameInfo.updateRpxPath))
                    Directory.Delete(currentGameInfo.updateRpxPath,true);


            //Creating fake update if update doesn't already exist
            if (!Directory.Exists(currentGameInfo.updateRpxPath))
            {
                Directory.CreateDirectory(currentGameInfo.updateRpxPath);
                Directory.CreateDirectory(currentGameInfo.updateCodePath);
                Directory.CreateDirectory(currentGameInfo.updateMetaPath);

                foreach (var file in currentGameInfo.gameRpxFileInfo.Directory.GetFiles())
                    File.Copy(file.FullName, Path.Combine(currentGameInfo.updateCodePath, file.Name));

                DirectoryInfo gameDirectoryInfo = currentGameInfo.gameRpxFileInfo.Directory.Parent;
                DirectoryInfo metaDirectoryInfo = new DirectoryInfo(currentGameInfo.updateMetaPath);

                foreach (var file in metaDirectoryInfo.GetFiles())
                    File.Copy(file.FullName, currentGameInfo.updateMetaPath);

            }

            //Uncompress update rpx if it isn't already

            DirectoryInfo updateCodeDirectoryInfo = new DirectoryInfo(currentGameInfo.updateCodePath);
            currentGameInfo.updateCodeFiles = updateCodeDirectoryInfo.GetFiles();

            if (currentGameInfo.updateCodeFiles.FirstOrDefault(it => it.Name == "UNCOMPRESSED.txt") == null)
            {
                File.Move(currentGameInfo.updateRpxLocation, currentGameInfo.updateRpxCompressed);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = Path.Combine(currentDir, "wiiurpxtool.exe");
                psi.WorkingDirectory = currentDir;
                psi.Arguments = $"-d \"{currentGameInfo.updateRpxCompressed}\" \"{currentGameInfo.updateRpxLocation}\"";
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                var p = Process.Start(psi);

                p.WaitForExit();

                File.WriteAllText(Path.Combine(currentGameInfo.updateCodePath, "UNCOMPRESSED.txt"), "DONE");
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
                if (File.Exists(currentGameInfo.updateRpxBackup))
                    File.Delete(currentGameInfo.updateRpxBackup);

            if (!File.Exists(currentGameInfo.updateRpxBackup))
            {
                File.Copy(currentGameInfo.updateRpxLocation, currentGameInfo.updateRpxBackup);
            }
        }

        internal static void StartRpx()
        {
            rpxInterface.ApplyWorkingFile();

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = currentGameInfo.CemuExeLocation;
            psi.WorkingDirectory = new FileInfo(currentGameInfo.CemuExeLocation).DirectoryName;
            psi.Arguments = $"-g \"{currentGameInfo.gameRpxPath}\"";
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

            if (File.Exists(currentGameInfo.updateRpxBackup))
            {
                if (File.Exists(currentGameInfo.updateRpxLocation))
                    File.Delete(currentGameInfo.updateRpxLocation);
                File.Copy(currentGameInfo.updateRpxBackup, currentGameInfo.updateRpxLocation);
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
                cemuProcess = null;
                Console.WriteLine(e);
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
            S.GET<CS_Core_Form>().btnUnmodSelectedGame.Enabled = true;
        }

        public static void DisableButtons()
        {
            S.GET<CS_Core_Form>().btnResetBackup.Enabled = false;
            S.GET<CS_Core_Form>().btnRestoreBackup.Enabled = false;
            S.GET<CS_Core_Form>().btnStartRpx.Enabled = false;
            S.GET<CS_Core_Form>().btnReconstructFakeUpdate.Enabled = false;
            S.GET<CS_Core_Form>().btnUnmodSelectedGame.Enabled = false;
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
