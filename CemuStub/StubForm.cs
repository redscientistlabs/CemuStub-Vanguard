﻿using RTCV.CorruptCore;
using RTCV.UI;
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
    public partial class StubForm : Form
    {

        public StubForm()
        {
            InitializeComponent();
            Text += CemuWatch.CemuStubVersion;
            tbExpectedVersion.Text = CemuWatch.expectedCemuVersion;
            tbExpectedVersion.TextChanged += TbExpectedVersion_TextChanged;
        }

        private void TbExpectedVersion_TextChanged(object sender, EventArgs e)
        {
            CemuWatch.expectedCemuVersion = tbExpectedVersion.Text;
        }

        private void BtnRestoreBackup_Click(object sender, EventArgs e)
        {
            CemuWatch.RestoreBackup();
        }

        private void BtnResetBackup_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Resetting the backup will take the current rpx and promote it to backup. Do you want to continue?", "Reset Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                CemuWatch.ResetBackup();

            FileInterface.CompositeFilenameDico = new Dictionary<string, string>();
            FileInterface.SaveCompositeFilenameDico();
        }

        private void StubForm_Load(object sender, EventArgs e)
        {
            cbSelectedGame.SelectedIndex = 0;
            CemuWatch.LoadKnownGames();

            Colors.SetRTCColor(Color.LightSteelBlue, this);
            lbTargetVersion.Focus();
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

            if (CemuWatch.currentGameInfo != null && CemuWatch.currentGameInfo.gameName != "Autodetect")
                cemuExeFile = CemuWatch.currentGameInfo.cemuExeFile;
            else if (CemuWatch.knownGamesDico.Values.Count > 0)
                cemuExeFile = CemuWatch.knownGamesDico.Values.First().cemuExeFile;

            ContextMenuStrip loadMenuItems = new ContextMenuStrip();

            loadMenuItems.Items.Add("Start Cemu", null, new EventHandler((ob, ev) =>
            {
                CemuWatch.StartCemu();
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

            loadMenuItems.Items.Add(new ToolStripSeparator());
            loadMenuItems.Items.Add("Toggle Cemu version override", null, new EventHandler((ob, ev) =>
            {
                lbTargetVersion.Visible = !lbTargetVersion.Visible;
                tbExpectedVersion.Visible = !tbExpectedVersion.Visible;
            }));

            loadMenuItems.Show(this, locate);
        }
    }
}
