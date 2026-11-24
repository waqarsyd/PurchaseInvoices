namespace PurchaseInvoices
{
    partial class DatabaseSettingsForm
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
            this.btnUseDefault = new PurchaseInvoices.ModernButton();
            this.btnCancel = new PurchaseInvoices.ModernButton();
            this.btnTest = new PurchaseInvoices.ModernButton();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.connectionCard = new PurchaseInvoices.CardPanel();
            this.lblServer = new System.Windows.Forms.Label();
            this.hostServer = new PurchaseInvoices.FieldHost();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.hostDatabase = new PurchaseInvoices.FieldHost();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblAuth = new System.Windows.Forms.Label();
            this.cboAuth = new PurchaseInvoices.ModernComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.hostUser = new PurchaseInvoices.FieldHost();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.hostPassword = new PurchaseInvoices.FieldHost();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCreateTables = new PurchaseInvoices.ModernButton();
            this.lblSource = new System.Windows.Forms.Label();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.connectionCard.SuspendLayout();
            this.hostServer.SuspendLayout();
            this.hostDatabase.SuspendLayout();
            this.hostUser.SuspendLayout();
            this.hostPassword.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(640, 64);
            this.headerPanel.Subtitle = "Choose the SQL Server and database this app uses";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Database connection";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnUseDefault);
            this.footerPanel.Controls.Add(this.btnCancel);
            this.footerPanel.Controls.Add(this.btnTest);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 520);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(640, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnUseDefault
            //
            this.btnUseDefault.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnUseDefault.Location = new System.Drawing.Point(20, 12);
            this.btnUseDefault.Name = "btnUseDefault";
            this.btnUseDefault.Size = new System.Drawing.Size(150, 36);
            this.btnUseDefault.TabIndex = 3;
            this.btnUseDefault.Text = "Use App.config";
            this.btnUseDefault.Click += new System.EventHandler(this.btnUseDefault_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(230, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // btnTest
            //
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnTest.Glyph = "";
            this.btnTest.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnTest.Location = new System.Drawing.Point(340, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(160, 36);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test connection";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(510, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(110, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.connectionCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(640, 456);
            this.contentPanel.TabIndex = 1;
            //
            // connectionCard
            //
            this.connectionCard.Controls.Add(this.lblServer);
            this.connectionCard.Controls.Add(this.hostServer);
            this.connectionCard.Controls.Add(this.lblDatabase);
            this.connectionCard.Controls.Add(this.hostDatabase);
            this.connectionCard.Controls.Add(this.lblAuth);
            this.connectionCard.Controls.Add(this.cboAuth);
            this.connectionCard.Controls.Add(this.lblUser);
            this.connectionCard.Controls.Add(this.hostUser);
            this.connectionCard.Controls.Add(this.lblPassword);
            this.connectionCard.Controls.Add(this.hostPassword);
            this.connectionCard.Controls.Add(this.lblStatus);
            this.connectionCard.Controls.Add(this.btnCreateTables);
            this.connectionCard.Controls.Add(this.lblSource);
            this.connectionCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.connectionCard.Location = new System.Drawing.Point(14, 8);
            this.connectionCard.Name = "connectionCard";
            this.connectionCard.Size = new System.Drawing.Size(612, 440);
            this.connectionCard.TabIndex = 0;
            //
            // lblServer
            //
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblServer.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblServer.Location = new System.Drawing.Point(24, 18);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 15);
            this.lblServer.TabIndex = 20;
            this.lblServer.Text = "Server";
            //
            // hostServer
            //
            this.hostServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostServer.Controls.Add(this.txtServer);
            this.hostServer.Location = new System.Drawing.Point(24, 38);
            this.hostServer.Name = "hostServer";
            this.hostServer.Placeholder = "For example .\\SQLEXPRESS or SERVER\\INSTANCE";
            this.hostServer.Size = new System.Drawing.Size(564, 36);
            this.hostServer.TabIndex = 0;
            //
            // txtServer
            //
            this.txtServer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtServer.Location = new System.Drawing.Point(12, 9);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(540, 18);
            this.txtServer.TabIndex = 0;
            this.txtServer.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblDatabase
            //
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblDatabase.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblDatabase.Location = new System.Drawing.Point(24, 86);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(58, 15);
            this.lblDatabase.TabIndex = 21;
            this.lblDatabase.Text = "Database";
            //
            // hostDatabase
            //
            this.hostDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostDatabase.Controls.Add(this.txtDatabase);
            this.hostDatabase.Location = new System.Drawing.Point(24, 106);
            this.hostDatabase.Name = "hostDatabase";
            this.hostDatabase.Placeholder = "Database name";
            this.hostDatabase.Size = new System.Drawing.Size(564, 36);
            this.hostDatabase.TabIndex = 1;
            //
            // txtDatabase
            //
            this.txtDatabase.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDatabase.Location = new System.Drawing.Point(12, 9);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(540, 18);
            this.txtDatabase.TabIndex = 0;
            this.txtDatabase.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblAuth
            //
            this.lblAuth.AutoSize = true;
            this.lblAuth.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAuth.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAuth.Location = new System.Drawing.Point(24, 154);
            this.lblAuth.Name = "lblAuth";
            this.lblAuth.Size = new System.Drawing.Size(48, 15);
            this.lblAuth.TabIndex = 22;
            this.lblAuth.Text = "Sign in with";
            //
            // cboAuth
            //
            this.cboAuth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAuth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboAuth.Location = new System.Drawing.Point(24, 174);
            this.cboAuth.Name = "cboAuth";
            this.cboAuth.Size = new System.Drawing.Size(564, 36);
            this.cboAuth.TabIndex = 2;
            this.cboAuth.SelectedIndexChanged += new System.EventHandler(this.cboAuth_SelectedIndexChanged);
            //
            // lblUser
            //
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUser.Location = new System.Drawing.Point(24, 222);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(66, 15);
            this.lblUser.TabIndex = 23;
            this.lblUser.Text = "Login name";
            //
            // hostUser
            //
            this.hostUser.Controls.Add(this.txtUser);
            this.hostUser.Location = new System.Drawing.Point(24, 242);
            this.hostUser.Name = "hostUser";
            this.hostUser.Size = new System.Drawing.Size(274, 36);
            this.hostUser.TabIndex = 3;
            //
            // txtUser
            //
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUser.Location = new System.Drawing.Point(12, 9);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(250, 18);
            this.txtUser.TabIndex = 0;
            this.txtUser.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPassword.Location = new System.Drawing.Point(314, 222);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 15);
            this.lblPassword.TabIndex = 24;
            this.lblPassword.Text = "Password";
            //
            // hostPassword
            //
            this.hostPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostPassword.Controls.Add(this.txtPassword);
            this.hostPassword.Location = new System.Drawing.Point(314, 242);
            this.hostPassword.Name = "hostPassword";
            this.hostPassword.Size = new System.Drawing.Size(274, 36);
            this.hostPassword.TabIndex = 4;
            //
            // txtPassword
            //
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(12, 9);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(250, 18);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblStatus
            //
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblStatus.Location = new System.Drawing.Point(24, 296);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(564, 62);
            this.lblStatus.TabIndex = 25;
            //
            // btnCreateTables
            //
            this.btnCreateTables.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCreateTables.Glyph = "";
            this.btnCreateTables.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnCreateTables.Location = new System.Drawing.Point(24, 362);
            this.btnCreateTables.Name = "btnCreateTables";
            this.btnCreateTables.Size = new System.Drawing.Size(170, 36);
            this.btnCreateTables.TabIndex = 5;
            this.btnCreateTables.Text = "Create tables";
            this.btnCreateTables.Visible = false;
            this.btnCreateTables.Click += new System.EventHandler(this.btnCreateTables_Click);
            //
            // lblSource
            //
            this.lblSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSource.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            this.lblSource.Location = new System.Drawing.Point(24, 406);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(564, 20);
            this.lblSource.TabIndex = 26;
            //
            // DatabaseSettingsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(640, 580);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatabaseSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Database connection";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DatabaseSettingsForm_FormClosing);
            this.footerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.connectionCard.ResumeLayout(false);
            this.connectionCard.PerformLayout();
            this.hostServer.ResumeLayout(false);
            this.hostServer.PerformLayout();
            this.hostDatabase.ResumeLayout(false);
            this.hostDatabase.PerformLayout();
            this.hostUser.ResumeLayout(false);
            this.hostUser.PerformLayout();
            this.hostPassword.ResumeLayout(false);
            this.hostPassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnUseDefault;
        private PurchaseInvoices.ModernButton btnCancel;
        private PurchaseInvoices.ModernButton btnTest;
        private PurchaseInvoices.ModernButton btnSave;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel connectionCard;
        private System.Windows.Forms.Label lblServer;
        private PurchaseInvoices.FieldHost hostServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblDatabase;
        private PurchaseInvoices.FieldHost hostDatabase;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblAuth;
        private PurchaseInvoices.ModernComboBox cboAuth;
        private System.Windows.Forms.Label lblUser;
        private PurchaseInvoices.FieldHost hostUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblPassword;
        private PurchaseInvoices.FieldHost hostPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblStatus;
        private PurchaseInvoices.ModernButton btnCreateTables;
        private System.Windows.Forms.Label lblSource;
    }
}
