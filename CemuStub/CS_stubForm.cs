using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void BtnStartRpx_Click(object sender, EventArgs e)
        {
            CemuWatch.StartRpx();
        }

        private void BtnReconstructFakeUpdate_Click(object sender, EventArgs e)
        {
            CemuWatch.PrepareUpdateFolder(true);
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

            CemuWatch.SelectGame(selected);

            if(!VanguardCore.vanguardStarted)
                VanguardCore.Start();
            else
                CemuWatch.UpdateDomains();

        }

        private void BtnUnmodSelectedGame_Click(object sender, EventArgs e)
        {
            CemuWatch.UnmodGame();
        }
    }
}
