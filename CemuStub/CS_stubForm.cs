using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CemuStub
{
    public partial class CS_Core_Form : Form
    {

        public CS_Core_Form()
        {
            InitializeComponent();
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
            CemuWatch.Start();
        }
    }
}
