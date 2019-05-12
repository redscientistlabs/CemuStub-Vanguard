namespace CemuStub
{
    partial class CS_Core_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CS_Core_Form));
            this.label1 = new System.Windows.Forms.Label();
            this.lbCemuStatus = new System.Windows.Forms.Label();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.btnResetBackup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRestartStub = new System.Windows.Forms.Button();
            this.btnStartRpx = new System.Windows.Forms.Button();
            this.btnReconstructFakeUpdate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTargetedGame = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            // 
            // lbCemuStatus
            // 
            this.lbCemuStatus.AutoSize = true;
            this.lbCemuStatus.ForeColor = System.Drawing.Color.White;
            this.lbCemuStatus.Location = new System.Drawing.Point(12, 46);
            this.lbCemuStatus.Name = "lbCemuStatus";
            this.lbCemuStatus.Size = new System.Drawing.Size(88, 13);
            this.lbCemuStatus.TabIndex = 1;
            this.lbCemuStatus.Text = "Waiting for Cemu";
            // 
            // btnRestoreBackup
            // 
            this.btnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestoreBackup.Enabled = false;
            this.btnRestoreBackup.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRestoreBackup.FlatAppearance.BorderSize = 0;
            this.btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRestoreBackup.Location = new System.Drawing.Point(15, 73);
            this.btnRestoreBackup.Name = "btnRestoreBackup";
            this.btnRestoreBackup.Size = new System.Drawing.Size(238, 23);
            this.btnRestoreBackup.TabIndex = 2;
            this.btnRestoreBackup.Text = "Restore Backup";
            this.btnRestoreBackup.UseVisualStyleBackColor = false;
            this.btnRestoreBackup.Click += new System.EventHandler(this.BtnRestoreBackup_Click);
            // 
            // btnResetBackup
            // 
            this.btnResetBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnResetBackup.Enabled = false;
            this.btnResetBackup.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnResetBackup.FlatAppearance.BorderSize = 0;
            this.btnResetBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBackup.ForeColor = System.Drawing.Color.Firebrick;
            this.btnResetBackup.Location = new System.Drawing.Point(15, 102);
            this.btnResetBackup.Name = "btnResetBackup";
            this.btnResetBackup.Size = new System.Drawing.Size(238, 23);
            this.btnResetBackup.TabIndex = 3;
            this.btnResetBackup.Text = "Reset Backup";
            this.btnResetBackup.UseVisualStyleBackColor = false;
            this.btnResetBackup.Click += new System.EventHandler(this.BtnResetBackup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "(You need to restart the stub if you change games)";
            // 
            // btnRestartStub
            // 
            this.btnRestartStub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestartStub.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnRestartStub.FlatAppearance.BorderSize = 0;
            this.btnRestartStub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestartStub.ForeColor = System.Drawing.Color.SandyBrown;
            this.btnRestartStub.Location = new System.Drawing.Point(15, 175);
            this.btnRestartStub.Name = "btnRestartStub";
            this.btnRestartStub.Size = new System.Drawing.Size(238, 23);
            this.btnRestartStub.TabIndex = 5;
            this.btnRestartStub.Text = "Restart Stub";
            this.btnRestartStub.UseVisualStyleBackColor = false;
            this.btnRestartStub.Click += new System.EventHandler(this.BtnRestartStub_Click);
            // 
            // btnStartRpx
            // 
            this.btnStartRpx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStartRpx.Enabled = false;
            this.btnStartRpx.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnStartRpx.FlatAppearance.BorderSize = 0;
            this.btnStartRpx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartRpx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnStartRpx.Location = new System.Drawing.Point(15, 108);
            this.btnStartRpx.Name = "btnStartRpx";
            this.btnStartRpx.Size = new System.Drawing.Size(195, 23);
            this.btnStartRpx.TabIndex = 6;
            this.btnStartRpx.Text = "Start RPX";
            this.btnStartRpx.UseVisualStyleBackColor = false;
            this.btnStartRpx.Click += new System.EventHandler(this.BtnStartRpx_Click);
            // 
            // btnReconstructFakeUpdate
            // 
            this.btnReconstructFakeUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnReconstructFakeUpdate.Enabled = false;
            this.btnReconstructFakeUpdate.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnReconstructFakeUpdate.FlatAppearance.BorderSize = 0;
            this.btnReconstructFakeUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReconstructFakeUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReconstructFakeUpdate.Location = new System.Drawing.Point(15, 137);
            this.btnReconstructFakeUpdate.Name = "btnReconstructFakeUpdate";
            this.btnReconstructFakeUpdate.Size = new System.Drawing.Size(195, 23);
            this.btnReconstructFakeUpdate.TabIndex = 7;
            this.btnReconstructFakeUpdate.Text = "Reconstruct Fake Update";
            this.btnReconstructFakeUpdate.UseVisualStyleBackColor = false;
            this.btnReconstructFakeUpdate.Click += new System.EventHandler(this.BtnReconstructFakeUpdate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbTargetedGame);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnStartRpx);
            this.groupBox1.Controls.Add(this.btnReconstructFakeUpdate);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(275, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 173);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Debug tools/info";
            // 
            // lbTargetedGame
            // 
            this.lbTargetedGame.ForeColor = System.Drawing.Color.White;
            this.lbTargetedGame.Location = new System.Drawing.Point(12, 44);
            this.lbTargetedGame.Name = "lbTargetedGame";
            this.lbTargetedGame.Size = new System.Drawing.Size(198, 52);
            this.lbTargetedGame.TabIndex = 9;
            this.lbTargetedGame.Text = "_";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Targeted RPX File:";
            // 
            // CS_Core_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(505, 212);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRestartStub);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnResetBackup);
            this.Controls.Add(this.btnRestoreBackup);
            this.Controls.Add(this.lbCemuStatus);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CS_Core_Form";
            this.Text = "Cemu Stub";
            this.Load += new System.EventHandler(this.CS_Core_Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lbCemuStatus;
        public System.Windows.Forms.Button btnRestoreBackup;
        public System.Windows.Forms.Button btnResetBackup;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnRestartStub;
        public System.Windows.Forms.Button btnStartRpx;
        public System.Windows.Forms.Button btnReconstructFakeUpdate;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbTargetedGame;
        public System.Windows.Forms.Label label3;
    }
}

