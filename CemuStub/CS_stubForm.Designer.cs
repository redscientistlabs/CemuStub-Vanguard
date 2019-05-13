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
            this.btnStartRpx = new System.Windows.Forms.Button();
            this.btnReconstructFakeUpdate = new System.Windows.Forms.Button();
            this.lbTargetedGameRpx = new System.Windows.Forms.Label();
            this.pnSideBar = new System.Windows.Forms.Panel();
            this.pnGlitchHarvesterOpen = new System.Windows.Forms.Panel();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.btnResetBackup = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnUnmodSelectedGame = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTargetedGameId = new System.Windows.Forms.Label();
            this.cbSelectedGame = new System.Windows.Forms.ComboBox();
            this.pnSideBar.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Status";
            // 
            // lbCemuStatus
            // 
            this.lbCemuStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lbCemuStatus.ForeColor = System.Drawing.Color.White;
            this.lbCemuStatus.Location = new System.Drawing.Point(10, 38);
            this.lbCemuStatus.Name = "lbCemuStatus";
            this.lbCemuStatus.Size = new System.Drawing.Size(110, 44);
            this.lbCemuStatus.TabIndex = 1;
            this.lbCemuStatus.Text = "Waiting for Cemu";
            // 
            // btnStartRpx
            // 
            this.btnStartRpx.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnStartRpx.Enabled = false;
            this.btnStartRpx.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnStartRpx.FlatAppearance.BorderSize = 0;
            this.btnStartRpx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartRpx.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnStartRpx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnStartRpx.Location = new System.Drawing.Point(8, 13);
            this.btnStartRpx.Name = "btnStartRpx";
            this.btnStartRpx.Size = new System.Drawing.Size(147, 24);
            this.btnStartRpx.TabIndex = 6;
            this.btnStartRpx.Text = "Manually Start RPX";
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
            this.btnReconstructFakeUpdate.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnReconstructFakeUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnReconstructFakeUpdate.Location = new System.Drawing.Point(8, 42);
            this.btnReconstructFakeUpdate.Name = "btnReconstructFakeUpdate";
            this.btnReconstructFakeUpdate.Size = new System.Drawing.Size(147, 24);
            this.btnReconstructFakeUpdate.TabIndex = 7;
            this.btnReconstructFakeUpdate.Text = "Reconstruct Fake Update";
            this.btnReconstructFakeUpdate.UseVisualStyleBackColor = false;
            this.btnReconstructFakeUpdate.Click += new System.EventHandler(this.BtnReconstructFakeUpdate_Click);
            // 
            // lbTargetedGameRpx
            // 
            this.lbTargetedGameRpx.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbTargetedGameRpx.ForeColor = System.Drawing.Color.White;
            this.lbTargetedGameRpx.Location = new System.Drawing.Point(10, 56);
            this.lbTargetedGameRpx.Name = "lbTargetedGameRpx";
            this.lbTargetedGameRpx.Size = new System.Drawing.Size(164, 64);
            this.lbTargetedGameRpx.TabIndex = 9;
            this.lbTargetedGameRpx.Text = "No game selected. Cemu Stub will auto-detect and prepare any game you load in Cem" +
    "u.";
            // 
            // pnSideBar
            // 
            this.pnSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnSideBar.Controls.Add(this.pnGlitchHarvesterOpen);
            this.pnSideBar.Controls.Add(this.lbCemuStatus);
            this.pnSideBar.Controls.Add(this.label1);
            this.pnSideBar.Controls.Add(this.btnRestoreBackup);
            this.pnSideBar.Controls.Add(this.btnResetBackup);
            this.pnSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnSideBar.Name = "pnSideBar";
            this.pnSideBar.Size = new System.Drawing.Size(130, 179);
            this.pnSideBar.TabIndex = 10;
            this.pnSideBar.Tag = "color:dark3";
            // 
            // pnGlitchHarvesterOpen
            // 
            this.pnGlitchHarvesterOpen.BackColor = System.Drawing.Color.Gray;
            this.pnGlitchHarvesterOpen.Location = new System.Drawing.Point(-19, 188);
            this.pnGlitchHarvesterOpen.Name = "pnGlitchHarvesterOpen";
            this.pnGlitchHarvesterOpen.Size = new System.Drawing.Size(23, 25);
            this.pnGlitchHarvesterOpen.TabIndex = 8;
            this.pnGlitchHarvesterOpen.Tag = "color:light1";
            this.pnGlitchHarvesterOpen.Visible = false;
            // 
            // btnRestoreBackup
            // 
            this.btnRestoreBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnRestoreBackup.Enabled = false;
            this.btnRestoreBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnRestoreBackup.FlatAppearance.BorderSize = 0;
            this.btnRestoreBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnRestoreBackup.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnRestoreBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestoreBackup.Location = new System.Drawing.Point(0, 89);
            this.btnRestoreBackup.Name = "btnRestoreBackup";
            this.btnRestoreBackup.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnRestoreBackup.Size = new System.Drawing.Size(133, 34);
            this.btnRestoreBackup.TabIndex = 119;
            this.btnRestoreBackup.TabStop = false;
            this.btnRestoreBackup.Tag = "color:dark3";
            this.btnRestoreBackup.Text = "Restore Backup";
            this.btnRestoreBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestoreBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRestoreBackup.UseVisualStyleBackColor = false;
            this.btnRestoreBackup.Click += new System.EventHandler(this.BtnRestoreBackup_Click);
            // 
            // btnResetBackup
            // 
            this.btnResetBackup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.btnResetBackup.Enabled = false;
            this.btnResetBackup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnResetBackup.FlatAppearance.BorderSize = 0;
            this.btnResetBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResetBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnResetBackup.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnResetBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetBackup.Location = new System.Drawing.Point(0, 127);
            this.btnResetBackup.Name = "btnResetBackup";
            this.btnResetBackup.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btnResetBackup.Size = new System.Drawing.Size(133, 34);
            this.btnResetBackup.TabIndex = 120;
            this.btnResetBackup.TabStop = false;
            this.btnResetBackup.Tag = "color:dark3";
            this.btnResetBackup.Text = "Reset Backup";
            this.btnResetBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResetBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnResetBackup.UseVisualStyleBackColor = false;
            this.btnResetBackup.Click += new System.EventHandler(this.BtnResetBackup_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnUnmodSelectedGame);
            this.panel1.Controls.Add(this.btnStartRpx);
            this.panel1.Controls.Add(this.btnReconstructFakeUpdate);
            this.panel1.Location = new System.Drawing.Point(342, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(163, 124);
            this.panel1.TabIndex = 11;
            // 
            // btnUnmodSelectedGame
            // 
            this.btnUnmodSelectedGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.btnUnmodSelectedGame.Enabled = false;
            this.btnUnmodSelectedGame.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnUnmodSelectedGame.FlatAppearance.BorderSize = 0;
            this.btnUnmodSelectedGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUnmodSelectedGame.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnUnmodSelectedGame.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnUnmodSelectedGame.Location = new System.Drawing.Point(8, 88);
            this.btnUnmodSelectedGame.Name = "btnUnmodSelectedGame";
            this.btnUnmodSelectedGame.Size = new System.Drawing.Size(147, 24);
            this.btnUnmodSelectedGame.TabIndex = 8;
            this.btnUnmodSelectedGame.Text = "Unmod selected game";
            this.btnUnmodSelectedGame.UseVisualStyleBackColor = false;
            this.btnUnmodSelectedGame.Click += new System.EventHandler(this.BtnUnmodSelectedGame_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(347, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Settings and params";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(148, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Targeted Game";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel2.Controls.Add(this.lbTargetedGameRpx);
            this.panel2.Controls.Add(this.lbTargetedGameId);
            this.panel2.Controls.Add(this.cbSelectedGame);
            this.panel2.Location = new System.Drawing.Point(144, 38);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 124);
            this.panel2.TabIndex = 13;
            // 
            // lbTargetedGameId
            // 
            this.lbTargetedGameId.Font = new System.Drawing.Font("Segoe UI", 7F);
            this.lbTargetedGameId.ForeColor = System.Drawing.Color.White;
            this.lbTargetedGameId.Location = new System.Drawing.Point(9, 41);
            this.lbTargetedGameId.Name = "lbTargetedGameId";
            this.lbTargetedGameId.Size = new System.Drawing.Size(164, 15);
            this.lbTargetedGameId.TabIndex = 140;
            // 
            // cbSelectedGame
            // 
            this.cbSelectedGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.cbSelectedGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedGame.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSelectedGame.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.cbSelectedGame.ForeColor = System.Drawing.Color.White;
            this.cbSelectedGame.FormattingEnabled = true;
            this.cbSelectedGame.Items.AddRange(new object[] {
            "Autodetect"});
            this.cbSelectedGame.Location = new System.Drawing.Point(12, 13);
            this.cbSelectedGame.Name = "cbSelectedGame";
            this.cbSelectedGame.Size = new System.Drawing.Size(161, 21);
            this.cbSelectedGame.TabIndex = 139;
            this.cbSelectedGame.Tag = "color:normal";
            this.cbSelectedGame.SelectedIndexChanged += new System.EventHandler(this.CbSelectedGame_SelectedIndexChanged);
            // 
            // CS_Core_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(518, 179);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnSideBar);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CS_Core_Form";
            this.Text = "Cemu Stub ";
            this.Load += new System.EventHandler(this.CS_Core_Form_Load);
            this.pnSideBar.ResumeLayout(false);
            this.pnSideBar.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lbCemuStatus;
        public System.Windows.Forms.Button btnStartRpx;
        public System.Windows.Forms.Button btnReconstructFakeUpdate;
        public System.Windows.Forms.Label lbTargetedGameRpx;
        private System.Windows.Forms.Panel pnSideBar;
        internal System.Windows.Forms.Panel pnGlitchHarvesterOpen;
        public System.Windows.Forms.Button btnRestoreBackup;
        public System.Windows.Forms.Button btnResetBackup;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.ComboBox cbSelectedGame;
        public System.Windows.Forms.Label lbTargetedGameId;
        public System.Windows.Forms.Button btnUnmodSelectedGame;
    }
}

