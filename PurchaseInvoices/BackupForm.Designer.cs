namespace PurchaseInvoices
{
    partial class BackupForm
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
            this.headerPanel = new PurchaseInvoices.HeaderPanel();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.lblTarget = new System.Windows.Forms.Label();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.restoreCard = new PurchaseInvoices.CardPanel();
            this.lblRestoreIntro = new System.Windows.Forms.Label();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.lblFile = new System.Windows.Forms.Label();
            this.hostFile = new PurchaseInvoices.FieldHost();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new PurchaseInvoices.ModernButton();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.hostConfirm = new PurchaseInvoices.FieldHost();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnRestore = new PurchaseInvoices.ModernButton();
            this.backupCard = new PurchaseInvoices.CardPanel();
            this.lblBackupIntro = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.hostFolder = new PurchaseInvoices.FieldHost();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new PurchaseInvoices.ModernButton();
            this.lblBackupStatus = new System.Windows.Forms.Label();
            this.btnBackup = new PurchaseInvoices.ModernButton();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.restoreCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.hostFile.SuspendLayout();
            this.hostConfirm.SuspendLayout();
            this.backupCard.SuspendLayout();
            this.hostFolder.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(760, 64);
            this.headerPanel.Subtitle = "Keep a copy of your data, and bring it back if you need to";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Backup and restore";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblTarget);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 610);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(760, 60);
            this.footerPanel.TabIndex = 2;
            //
            // lblTarget
            //
            this.lblTarget.AutoSize = true;
            this.lblTarget.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblTarget.Location = new System.Drawing.Point(20, 21);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(0, 17);
            this.lblTarget.TabIndex = 1;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(640, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.restoreCard);
            this.contentPanel.Controls.Add(this.backupCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(760, 546);
            this.contentPanel.TabIndex = 1;
            //
            // restoreCard
            //
            this.restoreCard.Controls.Add(this.lblRestoreIntro);
            this.restoreCard.Controls.Add(this.grid);
            this.restoreCard.Controls.Add(this.lblFile);
            this.restoreCard.Controls.Add(this.hostFile);
            this.restoreCard.Controls.Add(this.btnBrowseFile);
            this.restoreCard.Controls.Add(this.lblFileInfo);
            this.restoreCard.Controls.Add(this.lblConfirm);
            this.restoreCard.Controls.Add(this.hostConfirm);
            this.restoreCard.Controls.Add(this.btnRestore);
            this.restoreCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restoreCard.Glyph = "";
            this.restoreCard.Location = new System.Drawing.Point(14, 194);
            this.restoreCard.Name = "restoreCard";
            this.restoreCard.Size = new System.Drawing.Size(732, 344);
            this.restoreCard.TabIndex = 1;
            this.restoreCard.Title = "Restore";
            //
            // lblRestoreIntro
            //
            this.lblRestoreIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRestoreIntro.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRestoreIntro.Location = new System.Drawing.Point(24, 44);
            this.lblRestoreIntro.Name = "lblRestoreIntro";
            this.lblRestoreIntro.Size = new System.Drawing.Size(684, 38);
            this.lblRestoreIntro.TabIndex = 10;
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.EmptyHint = "Backups you make appear here.";
            this.grid.EmptyTitle = "No backups in this folder yet";
            this.grid.Location = new System.Drawing.Point(24, 86);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(684, 88);
            this.grid.TabIndex = 0;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            //
            // lblFile
            //
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblFile.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFile.Location = new System.Drawing.Point(24, 182);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(68, 15);
            this.lblFile.TabIndex = 11;
            this.lblFile.Text = "Backup file";
            //
            // hostFile
            //
            this.hostFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostFile.Controls.Add(this.txtFile);
            this.hostFile.Location = new System.Drawing.Point(24, 200);
            this.hostFile.Name = "hostFile";
            this.hostFile.Placeholder = "Pick a backup above, or browse to a .bak file";
            this.hostFile.Size = new System.Drawing.Size(564, 36);
            this.hostFile.TabIndex = 1;
            //
            // txtFile
            //
            this.txtFile.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFile.Location = new System.Drawing.Point(12, 9);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(540, 18);
            this.txtFile.TabIndex = 0;
            this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
            //
            // btnBrowseFile
            //
            this.btnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnBrowseFile.Location = new System.Drawing.Point(598, 200);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(110, 36);
            this.btnBrowseFile.TabIndex = 2;
            this.btnBrowseFile.Text = "Browse…";
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            //
            // lblFileInfo
            //
            this.lblFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFileInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblFileInfo.Location = new System.Drawing.Point(24, 238);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(684, 32);
            this.lblFileInfo.TabIndex = 12;
            //
            // lblConfirm
            //
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblConfirm.Location = new System.Drawing.Point(24, 272);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(200, 15);
            this.lblConfirm.TabIndex = 13;
            this.lblConfirm.Text = "Type the database name to confirm";
            //
            // hostConfirm
            //
            this.hostConfirm.Controls.Add(this.txtConfirm);
            this.hostConfirm.Location = new System.Drawing.Point(24, 290);
            this.hostConfirm.Name = "hostConfirm";
            this.hostConfirm.Size = new System.Drawing.Size(300, 36);
            this.hostConfirm.TabIndex = 3;
            //
            // txtConfirm
            //
            this.txtConfirm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirm.Location = new System.Drawing.Point(12, 9);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(276, 18);
            this.txtConfirm.TabIndex = 0;
            this.txtConfirm.TextChanged += new System.EventHandler(this.txtConfirm_TextChanged);
            //
            // btnRestore
            //
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestore.Enabled = false;
            this.btnRestore.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnRestore.Glyph = "";
            this.btnRestore.Kind = PurchaseInvoices.ButtonKind.Danger;
            this.btnRestore.Location = new System.Drawing.Point(548, 289);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(160, 38);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            //
            // backupCard
            //
            this.backupCard.Controls.Add(this.lblBackupIntro);
            this.backupCard.Controls.Add(this.lblFolder);
            this.backupCard.Controls.Add(this.hostFolder);
            this.backupCard.Controls.Add(this.btnBrowseFolder);
            this.backupCard.Controls.Add(this.lblBackupStatus);
            this.backupCard.Controls.Add(this.btnBackup);
            this.backupCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.backupCard.Glyph = "";
            this.backupCard.Location = new System.Drawing.Point(14, 8);
            this.backupCard.Name = "backupCard";
            this.backupCard.Size = new System.Drawing.Size(732, 186);
            this.backupCard.TabIndex = 0;
            this.backupCard.Title = "Back up";
            //
            // lblBackupIntro
            //
            this.lblBackupIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackupIntro.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblBackupIntro.Location = new System.Drawing.Point(24, 44);
            this.lblBackupIntro.Name = "lblBackupIntro";
            this.lblBackupIntro.Size = new System.Drawing.Size(684, 20);
            this.lblBackupIntro.TabIndex = 10;
            //
            // lblFolder
            //
            this.lblFolder.AutoSize = true;
            this.lblFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblFolder.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFolder.Location = new System.Drawing.Point(24, 72);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(225, 15);
            this.lblFolder.TabIndex = 11;
            this.lblFolder.Text = "Backup folder (on the SQL Server computer)";
            //
            // hostFolder
            //
            this.hostFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostFolder.Controls.Add(this.txtFolder);
            this.hostFolder.Location = new System.Drawing.Point(24, 92);
            this.hostFolder.Name = "hostFolder";
            this.hostFolder.Size = new System.Drawing.Size(564, 36);
            this.hostFolder.TabIndex = 0;
            //
            // txtFolder
            //
            this.txtFolder.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFolder.Location = new System.Drawing.Point(12, 9);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(540, 18);
            this.txtFolder.TabIndex = 0;
            this.txtFolder.Leave += new System.EventHandler(this.txtFolder_Leave);
            //
            // btnBrowseFolder
            //
            this.btnBrowseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFolder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnBrowseFolder.Location = new System.Drawing.Point(598, 92);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(110, 36);
            this.btnBrowseFolder.TabIndex = 1;
            this.btnBrowseFolder.Text = "Browse…";
            this.btnBrowseFolder.Click += new System.EventHandler(this.btnBrowseFolder_Click);
            //
            // lblBackupStatus
            //
            this.lblBackupStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBackupStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblBackupStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblBackupStatus.Location = new System.Drawing.Point(24, 134);
            this.lblBackupStatus.Name = "lblBackupStatus";
            this.lblBackupStatus.Size = new System.Drawing.Size(510, 40);
            this.lblBackupStatus.TabIndex = 12;
            this.lblBackupStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnBackup
            //
            this.btnBackup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnBackup.Glyph = "";
            this.btnBackup.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnBackup.Location = new System.Drawing.Point(548, 135);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(160, 38);
            this.btnBackup.TabIndex = 2;
            this.btnBackup.Text = "Back up now";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            //
            // BackupForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(760, 670);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Backup and restore";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackupForm_FormClosing);
            this.Load += new System.EventHandler(this.BackupForm_Load);
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.restoreCard.ResumeLayout(false);
            this.restoreCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.hostFile.ResumeLayout(false);
            this.hostFile.PerformLayout();
            this.hostConfirm.ResumeLayout(false);
            this.hostConfirm.PerformLayout();
            this.backupCard.ResumeLayout(false);
            this.backupCard.PerformLayout();
            this.hostFolder.ResumeLayout(false);
            this.hostFolder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private System.Windows.Forms.Label lblTarget;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel restoreCard;
        private System.Windows.Forms.Label lblRestoreIntro;
        private PurchaseInvoices.ModernGrid grid;
        private System.Windows.Forms.Label lblFile;
        private PurchaseInvoices.FieldHost hostFile;
        private System.Windows.Forms.TextBox txtFile;
        private PurchaseInvoices.ModernButton btnBrowseFile;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.Label lblConfirm;
        private PurchaseInvoices.FieldHost hostConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private PurchaseInvoices.ModernButton btnRestore;
        private PurchaseInvoices.CardPanel backupCard;
        private System.Windows.Forms.Label lblBackupIntro;
        private System.Windows.Forms.Label lblFolder;
        private PurchaseInvoices.FieldHost hostFolder;
        private System.Windows.Forms.TextBox txtFolder;
        private PurchaseInvoices.ModernButton btnBrowseFolder;
        private System.Windows.Forms.Label lblBackupStatus;
        private PurchaseInvoices.ModernButton btnBackup;
    }
}
