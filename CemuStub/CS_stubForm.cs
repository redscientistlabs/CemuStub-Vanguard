using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vanguard;

namespace CemuStub
{
    public partial class CS_Core_Form : Form
    {

        public CS_Core_Form()
        {
            InitializeComponent();
            Text += CemuWatch.CemuStubVersion;
        }

        private void BtnRestartStub_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            CemuWatch.RestoreBackup();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Resetting the backup will take the current rpx and promote it to backup. Do you want to continue?", "Reset Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                CemuWatch.ResetBackup();

            CemuWatch.CompositeFilenameDico = new Dictionary<string, string>();
            CemuWatch.SaveCompositeFilenameDico();
        }


        private void CS_Core_Form_Load(object sender, EventArgs e)
        {
            cbSelectedGame.SelectedIndex = 0;
            CemuWatch.LoadKnownGames();
        }

        private void CbSelectedGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CemuWatch.DontSelectGame)
                return;

            string selected = cbSelectedGame.SelectedItem.ToString();
            if (selected == "Autodetect")
            { 


                CemuWatch.Start();
                return;
            }

            if(!CemuWatch.SelectGame(selected))
            {
                cbSelectedGame.SelectedIndex = 0;
                return;
            }

            if(!VanguardCore.vanguardStarted)
                VanguardCore.Start();
            else if (VanguardCore.vanguardConnected)
                CemuWatch.UpdateDomains();

        }

        private void BtnSettings_MouseDown(object sender, MouseEventArgs e)
        {

            Point locate = new Point(((Control)sender).Location.X + e.Location.X, ((Control)sender).Location.Y + e.Location.Y);

            FileInfo cemuExeFile = null;

            if (CemuWatch.currentGameInfo != null && CemuWatch.currentGameInfo.gameName != "No game")
                cemuExeFile = CemuWatch.currentGameInfo.cemuExeFile;
            else if (CemuWatch.knownGamesDico.Values.Count > 0)
                cemuExeFile = CemuWatch.knownGamesDico.Values.First().cemuExeFile;

            ContextMenuStrip loadMenuItems = new ContextMenuStrip();

            loadMenuItems.Items.Add("Start Cemu", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.StartRpx();
            })).Visible = (cemuExeFile != null);

            var startRpxItem = loadMenuItems.Items.Add("Manually start Rpx", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.StartRpx();
            }));

            startRpxItem.Visible = (cemuExeFile != null);
            startRpxItem.Enabled = CemuWatch.InterfaceEnabled;


            loadMenuItems.Items.Add("Reconstruct fake update", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.PrepareUpdateFolder(true);
            })).Enabled = CemuWatch.InterfaceEnabled;

            loadMenuItems.Items.Add("Change Cemu location", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.ChangeCemuLocation();
            })).Enabled = CemuWatch.InterfaceEnabled;

            loadMenuItems.Items.Add(new ToolStripSeparator());

            loadMenuItems.Items.Add("Unmod selected Game", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.UnmodGame();
            })).Enabled = CemuWatch.InterfaceEnabled;

            loadMenuItems.Show(this, locate);
        }
    }
}
