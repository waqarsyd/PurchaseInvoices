namespace PurchaseInvoices
{
    partial class UsersForm
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
            this.btnNewUser = new PurchaseInvoices.ModernButton();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.listCard = new PurchaseInvoices.CardPanel();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.editorCard = new PurchaseInvoices.CardPanel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.hostUserName = new PurchaseInvoices.FieldHost();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.hostFullName = new PurchaseInvoices.FieldHost();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.cboRole = new PurchaseInvoices.ModernComboBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.hostPassword = new PurchaseInvoices.FieldHost();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.hostConfirm = new PurchaseInvoices.FieldHost();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.chkMustChange = new System.Windows.Forms.CheckBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.btnResetPassword = new PurchaseInvoices.ModernButton();
            this.lblError = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.listCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.editorCard.SuspendLayout();
            this.hostUserName.SuspendLayout();
            this.hostFullName.SuspendLayout();
            this.hostPassword.SuspendLayout();
            this.hostConfirm.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.btnNewUser);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(980, 64);
            this.headerPanel.Subtitle = "Add people, choose what they can do, and turn accounts on or off";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Users";
            //
            // btnNewUser
            //
            this.btnNewUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNewUser.Glyph = "";
            this.btnNewUser.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnNewUser.Location = new System.Drawing.Point(840, 14);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(120, 36);
            this.btnNewUser.TabIndex = 0;
            this.btnNewUser.Text = "New user";
            this.btnNewUser.Click += new System.EventHandler(this.btnNewUser_Click);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblStatus);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 560);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(980, 60);
            this.footerPanel.TabIndex = 2;
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblStatus.Location = new System.Drawing.Point(20, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 1;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(860, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.listCard);
            this.contentPanel.Controls.Add(this.editorCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(980, 496);
            this.contentPanel.TabIndex = 1;
            //
            // listCard
            //
            this.listCard.Controls.Add(this.grid);
            this.listCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listCard.Glyph = "";
            this.listCard.Location = new System.Drawing.Point(14, 8);
            this.listCard.Name = "listCard";
            this.listCard.Size = new System.Drawing.Size(572, 480);
            this.listCard.TabIndex = 0;
            this.listCard.Title = "People";
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AlwaysShowSelection = true;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.EmptyHint = "Click New user to add someone.";
            this.grid.EmptyTitle = "No users yet";
            this.grid.Location = new System.Drawing.Point(20, 46);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(532, 416);
            this.grid.TabIndex = 0;
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            //
            // editorCard
            //
            this.editorCard.Controls.Add(this.lblUserName);
            this.editorCard.Controls.Add(this.hostUserName);
            this.editorCard.Controls.Add(this.lblFullName);
            this.editorCard.Controls.Add(this.hostFullName);
            this.editorCard.Controls.Add(this.lblRole);
            this.editorCard.Controls.Add(this.cboRole);
            this.editorCard.Controls.Add(this.chkActive);
            this.editorCard.Controls.Add(this.lblPassword);
            this.editorCard.Controls.Add(this.hostPassword);
            this.editorCard.Controls.Add(this.lblConfirm);
            this.editorCard.Controls.Add(this.hostConfirm);
            this.editorCard.Controls.Add(this.chkMustChange);
            this.editorCard.Controls.Add(this.lblInfo);
            this.editorCard.Controls.Add(this.btnSave);
            this.editorCard.Controls.Add(this.btnResetPassword);
            this.editorCard.Controls.Add(this.lblError);
            this.editorCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.editorCard.Glyph = "";
            this.editorCard.Location = new System.Drawing.Point(586, 8);
            this.editorCard.Name = "editorCard";
            this.editorCard.Size = new System.Drawing.Size(380, 480);
            this.editorCard.TabIndex = 1;
            this.editorCard.Title = "User details";
            //
            // lblUserName
            //
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUserName.Location = new System.Drawing.Point(24, 52);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(65, 15);
            this.lblUserName.TabIndex = 20;
            this.lblUserName.Text = "User name";
            //
            // hostUserName
            //
            this.hostUserName.Controls.Add(this.txtUserName);
            this.hostUserName.Location = new System.Drawing.Point(24, 72);
            this.hostUserName.Name = "hostUserName";
            this.hostUserName.Placeholder = "They sign in with this";
            this.hostUserName.Size = new System.Drawing.Size(332, 36);
            this.hostUserName.TabIndex = 0;
            //
            // txtUserName
            //
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUserName.Location = new System.Drawing.Point(12, 9);
            this.txtUserName.MaxLength = 30;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(308, 18);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextChanged += new System.EventHandler(this.Field_Changed);
            //
            // lblFullName
            //
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFullName.Location = new System.Drawing.Point(24, 118);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(61, 15);
            this.lblFullName.TabIndex = 21;
            this.lblFullName.Text = "Full name";
            //
            // hostFullName
            //
            this.hostFullName.Controls.Add(this.txtFullName);
            this.hostFullName.Location = new System.Drawing.Point(24, 138);
            this.hostFullName.Name = "hostFullName";
            this.hostFullName.Size = new System.Drawing.Size(332, 36);
            this.hostFullName.TabIndex = 1;
            //
            // txtFullName
            //
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Location = new System.Drawing.Point(12, 9);
            this.txtFullName.MaxLength = 60;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(308, 18);
            this.txtFullName.TabIndex = 0;
            this.txtFullName.TextChanged += new System.EventHandler(this.Field_Changed);
            //
            // lblRole
            //
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblRole.Location = new System.Drawing.Point(24, 184);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(30, 15);
            this.lblRole.TabIndex = 22;
            this.lblRole.Text = "Role";
            //
            // cboRole
            //
            this.cboRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboRole.Location = new System.Drawing.Point(24, 204);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(332, 36);
            this.cboRole.TabIndex = 2;
            this.cboRole.SelectedIndexChanged += new System.EventHandler(this.Field_Changed);
            //
            // chkActive
            //
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkActive.Location = new System.Drawing.Point(25, 250);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(266, 21);
            this.chkActive.TabIndex = 3;
            this.chkActive.Text = "Account is turned on (they can sign in)";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.Field_Changed);
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPassword.Location = new System.Drawing.Point(24, 284);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(117, 15);
            this.lblPassword.TabIndex = 23;
            this.lblPassword.Text = "Temporary password";
            //
            // hostPassword
            //
            this.hostPassword.Controls.Add(this.txtPassword);
            this.hostPassword.Location = new System.Drawing.Point(24, 304);
            this.hostPassword.Name = "hostPassword";
            this.hostPassword.Size = new System.Drawing.Size(160, 36);
            this.hostPassword.TabIndex = 4;
            //
            // txtPassword
            //
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Location = new System.Drawing.Point(12, 9);
            this.txtPassword.MaxLength = 128;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(136, 18);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.Field_Changed);
            //
            // lblConfirm
            //
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblConfirm.Location = new System.Drawing.Point(196, 284);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(51, 15);
            this.lblConfirm.TabIndex = 24;
            this.lblConfirm.Text = "Confirm";
            //
            // hostConfirm
            //
            this.hostConfirm.Controls.Add(this.txtConfirm);
            this.hostConfirm.Location = new System.Drawing.Point(196, 304);
            this.hostConfirm.Name = "hostConfirm";
            this.hostConfirm.Size = new System.Drawing.Size(160, 36);
            this.hostConfirm.TabIndex = 5;
            //
            // txtConfirm
            //
            this.txtConfirm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirm.Location = new System.Drawing.Point(12, 9);
            this.txtConfirm.MaxLength = 128;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(136, 18);
            this.txtConfirm.TabIndex = 0;
            this.txtConfirm.UseSystemPasswordChar = true;
            this.txtConfirm.TextChanged += new System.EventHandler(this.Field_Changed);
            //
            // chkMustChange
            //
            this.chkMustChange.AutoSize = true;
            this.chkMustChange.Checked = true;
            this.chkMustChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMustChange.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkMustChange.Location = new System.Drawing.Point(25, 348);
            this.chkMustChange.Name = "chkMustChange";
            this.chkMustChange.Size = new System.Drawing.Size(268, 21);
            this.chkMustChange.TabIndex = 6;
            this.chkMustChange.Text = "Ask for a new password at first sign-in";
            this.chkMustChange.UseVisualStyleBackColor = true;
            //
            // lblInfo
            //
            this.lblInfo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblInfo.Location = new System.Drawing.Point(24, 284);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(332, 88);
            this.lblInfo.TabIndex = 25;
            this.lblInfo.Visible = false;
            //
            // btnSave
            //
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(24, 384);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 38);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // btnResetPassword
            //
            this.btnResetPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnResetPassword.Glyph = "";
            this.btnResetPassword.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnResetPassword.Location = new System.Drawing.Point(196, 384);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(160, 38);
            this.btnResetPassword.TabIndex = 8;
            this.btnResetPassword.Text = "Reset password";
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
            //
            // lblError
            //
            this.lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblError.Location = new System.Drawing.Point(24, 428);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(332, 40);
            this.lblError.TabIndex = 26;
            //
            // UsersForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(980, 620);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MinimizeBox = false;
            this.Name = "UsersForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Users";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsersForm_FormClosing);
            this.Load += new System.EventHandler(this.UsersForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.listCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.editorCard.ResumeLayout(false);
            this.editorCard.PerformLayout();
            this.hostUserName.ResumeLayout(false);
            this.hostUserName.PerformLayout();
            this.hostFullName.ResumeLayout(false);
            this.hostFullName.PerformLayout();
            this.hostPassword.ResumeLayout(false);
            this.hostPassword.PerformLayout();
            this.hostConfirm.ResumeLayout(false);
            this.hostConfirm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.ModernButton btnNewUser;
        private PurchaseInvoices.FooterPanel footerPanel;
        private System.Windows.Forms.Label lblStatus;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel listCard;
        private PurchaseInvoices.ModernGrid grid;
        private PurchaseInvoices.CardPanel editorCard;
        private System.Windows.Forms.Label lblUserName;
        private PurchaseInvoices.FieldHost hostUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblFullName;
        private PurchaseInvoices.FieldHost hostFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblRole;
        private PurchaseInvoices.ModernComboBox cboRole;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblPassword;
        private PurchaseInvoices.FieldHost hostPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirm;
        private PurchaseInvoices.FieldHost hostConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.CheckBox chkMustChange;
        private System.Windows.Forms.Label lblInfo;
        private PurchaseInvoices.ModernButton btnSave;
        private PurchaseInvoices.ModernButton btnResetPassword;
        private System.Windows.Forms.Label lblError;
    }
}
