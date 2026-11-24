namespace PurchaseInvoices
{
    partial class SignInForm
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
            this.brandPanel = new PurchaseInvoices.BrandPanel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.pageSignIn = new System.Windows.Forms.Panel();
            this.lblSignInTitle = new System.Windows.Forms.Label();
            this.lblSignInHint = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.hostUserName = new PurchaseInvoices.FieldHost();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.hostPassword = new PurchaseInvoices.FieldHost();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.lblSignInError = new System.Windows.Forms.Label();
            this.btnSignIn = new PurchaseInvoices.ModernButton();
            this.lblConnection = new System.Windows.Forms.Label();
            this.btnDatabaseSettings = new PurchaseInvoices.ModernButton();
            this.pageSetup = new System.Windows.Forms.Panel();
            this.lblSetupTitle = new System.Windows.Forms.Label();
            this.lblSetupHint = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.hostCompany = new PurchaseInvoices.FieldHost();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.hostFullName = new PurchaseInvoices.FieldHost();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblNewUserName = new System.Windows.Forms.Label();
            this.hostNewUserName = new PurchaseInvoices.FieldHost();
            this.txtNewUserName = new System.Windows.Forms.TextBox();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.hostNewPassword = new PurchaseInvoices.FieldHost();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.hostConfirmPassword = new PurchaseInvoices.FieldHost();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.lblPasswordRule = new System.Windows.Forms.Label();
            this.lblSetupError = new System.Windows.Forms.Label();
            this.btnCreateAccount = new PurchaseInvoices.ModernButton();
            this.pageMessage = new System.Windows.Forms.Panel();
            this.spinner = new PurchaseInvoices.Spinner();
            this.lblMessageTitle = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnMessagePrimary = new PurchaseInvoices.ModernButton();
            this.btnMessageSecondary = new PurchaseInvoices.ModernButton();
            this.contentPanel.SuspendLayout();
            this.pageSignIn.SuspendLayout();
            this.hostUserName.SuspendLayout();
            this.hostPassword.SuspendLayout();
            this.pageSetup.SuspendLayout();
            this.hostCompany.SuspendLayout();
            this.hostFullName.SuspendLayout();
            this.hostNewUserName.SuspendLayout();
            this.hostNewPassword.SuspendLayout();
            this.hostConfirmPassword.SuspendLayout();
            this.pageMessage.SuspendLayout();
            this.SuspendLayout();
            //
            // brandPanel
            //
            this.brandPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.brandPanel.Location = new System.Drawing.Point(0, 0);
            this.brandPanel.Name = "brandPanel";
            this.brandPanel.Size = new System.Drawing.Size(380, 580);
            this.brandPanel.TabIndex = 1;
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Controls.Add(this.pageSignIn);
            this.contentPanel.Controls.Add(this.pageSetup);
            this.contentPanel.Controls.Add(this.pageMessage);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(380, 0);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(520, 580);
            this.contentPanel.TabIndex = 0;
            //
            // pageSignIn
            //
            this.pageSignIn.Controls.Add(this.lblSignInTitle);
            this.pageSignIn.Controls.Add(this.lblSignInHint);
            this.pageSignIn.Controls.Add(this.lblUserName);
            this.pageSignIn.Controls.Add(this.hostUserName);
            this.pageSignIn.Controls.Add(this.lblPassword);
            this.pageSignIn.Controls.Add(this.hostPassword);
            this.pageSignIn.Controls.Add(this.chkRemember);
            this.pageSignIn.Controls.Add(this.chkShowPassword);
            this.pageSignIn.Controls.Add(this.lblSignInError);
            this.pageSignIn.Controls.Add(this.btnSignIn);
            this.pageSignIn.Controls.Add(this.lblConnection);
            this.pageSignIn.Controls.Add(this.btnDatabaseSettings);
            this.pageSignIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageSignIn.Location = new System.Drawing.Point(0, 0);
            this.pageSignIn.Name = "pageSignIn";
            this.pageSignIn.Size = new System.Drawing.Size(520, 580);
            this.pageSignIn.TabIndex = 0;
            this.pageSignIn.Visible = false;
            //
            // lblSignInTitle
            //
            this.lblSignInTitle.AutoSize = true;
            this.lblSignInTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.lblSignInTitle.Location = new System.Drawing.Point(56, 80);
            this.lblSignInTitle.Name = "lblSignInTitle";
            this.lblSignInTitle.Size = new System.Drawing.Size(88, 32);
            this.lblSignInTitle.TabIndex = 10;
            this.lblSignInTitle.Text = "Sign in";
            //
            // lblSignInHint
            //
            this.lblSignInHint.AutoSize = true;
            this.lblSignInHint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSignInHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSignInHint.Location = new System.Drawing.Point(59, 122);
            this.lblSignInHint.Name = "lblSignInHint";
            this.lblSignInHint.Size = new System.Drawing.Size(232, 19);
            this.lblSignInHint.TabIndex = 11;
            this.lblSignInHint.Text = "Welcome back. Sign in to continue.";
            //
            // lblUserName
            //
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblUserName.Location = new System.Drawing.Point(60, 170);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(65, 15);
            this.lblUserName.TabIndex = 12;
            this.lblUserName.Text = "User name";
            //
            // hostUserName
            //
            this.hostUserName.Controls.Add(this.txtUserName);
            this.hostUserName.Glyph = "";
            this.hostUserName.Location = new System.Drawing.Point(60, 190);
            this.hostUserName.Name = "hostUserName";
            this.hostUserName.Placeholder = "Your user name";
            this.hostUserName.Size = new System.Drawing.Size(400, 40);
            this.hostUserName.TabIndex = 0;
            //
            // txtUserName
            //
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtUserName.Location = new System.Drawing.Point(36, 10);
            this.txtUserName.MaxLength = 30;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(352, 19);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextChanged += new System.EventHandler(this.SignInField_TextChanged);
            //
            // lblPassword
            //
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPassword.Location = new System.Drawing.Point(60, 244);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(59, 15);
            this.lblPassword.TabIndex = 13;
            this.lblPassword.Text = "Password";
            //
            // hostPassword
            //
            this.hostPassword.Controls.Add(this.txtPassword);
            this.hostPassword.Glyph = "";
            this.hostPassword.Location = new System.Drawing.Point(60, 264);
            this.hostPassword.Name = "hostPassword";
            this.hostPassword.Placeholder = "Your password";
            this.hostPassword.Size = new System.Drawing.Size(400, 40);
            this.hostPassword.TabIndex = 1;
            //
            // txtPassword
            //
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtPassword.Location = new System.Drawing.Point(36, 10);
            this.txtPassword.MaxLength = 128;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(352, 19);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.SignInField_TextChanged);
            //
            // chkRemember
            //
            this.chkRemember.AutoSize = true;
            this.chkRemember.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkRemember.Location = new System.Drawing.Point(61, 316);
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.Size = new System.Drawing.Size(108, 21);
            this.chkRemember.TabIndex = 2;
            this.chkRemember.Text = "Remember me";
            this.chkRemember.UseVisualStyleBackColor = true;
            //
            // chkShowPassword
            //
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkShowPassword.Location = new System.Drawing.Point(338, 316);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(119, 21);
            this.chkShowPassword.TabIndex = 3;
            this.chkShowPassword.Text = "Show password";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            //
            // lblSignInError
            //
            this.lblSignInError.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSignInError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblSignInError.Location = new System.Drawing.Point(60, 346);
            this.lblSignInError.Name = "lblSignInError";
            this.lblSignInError.Size = new System.Drawing.Size(400, 40);
            this.lblSignInError.TabIndex = 14;
            //
            // btnSignIn
            //
            this.btnSignIn.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnSignIn.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSignIn.Location = new System.Drawing.Point(60, 392);
            this.btnSignIn.Name = "btnSignIn";
            this.btnSignIn.Size = new System.Drawing.Size(400, 44);
            this.btnSignIn.TabIndex = 4;
            this.btnSignIn.Text = "Sign in";
            this.btnSignIn.BusyText = "Signing in…";
            this.btnSignIn.Click += new System.EventHandler(this.btnSignIn_Click);
            //
            // lblConnection
            //
            this.lblConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblConnection.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblConnection.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblConnection.Location = new System.Drawing.Point(60, 516);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(262, 34);
            this.lblConnection.TabIndex = 15;
            this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnDatabaseSettings
            //
            this.btnDatabaseSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDatabaseSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnDatabaseSettings.Glyph = "";
            this.btnDatabaseSettings.Location = new System.Drawing.Point(330, 516);
            this.btnDatabaseSettings.Name = "btnDatabaseSettings";
            this.btnDatabaseSettings.Size = new System.Drawing.Size(130, 34);
            this.btnDatabaseSettings.TabIndex = 5;
            this.btnDatabaseSettings.Text = "Database";
            this.btnDatabaseSettings.Click += new System.EventHandler(this.btnDatabaseSettings_Click);
            //
            // pageSetup
            //
            this.pageSetup.Controls.Add(this.lblSetupTitle);
            this.pageSetup.Controls.Add(this.lblSetupHint);
            this.pageSetup.Controls.Add(this.lblCompany);
            this.pageSetup.Controls.Add(this.hostCompany);
            this.pageSetup.Controls.Add(this.lblFullName);
            this.pageSetup.Controls.Add(this.hostFullName);
            this.pageSetup.Controls.Add(this.lblNewUserName);
            this.pageSetup.Controls.Add(this.hostNewUserName);
            this.pageSetup.Controls.Add(this.lblNewPassword);
            this.pageSetup.Controls.Add(this.hostNewPassword);
            this.pageSetup.Controls.Add(this.lblConfirmPassword);
            this.pageSetup.Controls.Add(this.hostConfirmPassword);
            this.pageSetup.Controls.Add(this.lblPasswordRule);
            this.pageSetup.Controls.Add(this.lblSetupError);
            this.pageSetup.Controls.Add(this.btnCreateAccount);
            this.pageSetup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageSetup.Location = new System.Drawing.Point(0, 0);
            this.pageSetup.Name = "pageSetup";
            this.pageSetup.Size = new System.Drawing.Size(520, 580);
            this.pageSetup.TabIndex = 1;
            this.pageSetup.Visible = false;
            //
            // lblSetupTitle
            //
            this.lblSetupTitle.AutoSize = true;
            this.lblSetupTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 18F);
            this.lblSetupTitle.Location = new System.Drawing.Point(56, 36);
            this.lblSetupTitle.Name = "lblSetupTitle";
            this.lblSetupTitle.Size = new System.Drawing.Size(296, 32);
            this.lblSetupTitle.TabIndex = 20;
            this.lblSetupTitle.Text = "Welcome! Let\'s get started";
            //
            // lblSetupHint
            //
            this.lblSetupHint.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSetupHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSetupHint.Location = new System.Drawing.Point(59, 76);
            this.lblSetupHint.Name = "lblSetupHint";
            this.lblSetupHint.Size = new System.Drawing.Size(400, 40);
            this.lblSetupHint.TabIndex = 21;
            this.lblSetupHint.Text = "Create the administrator account for your company. You can add more people later.";
            //
            // lblCompany
            //
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCompany.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCompany.Location = new System.Drawing.Point(60, 126);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(88, 15);
            this.lblCompany.TabIndex = 22;
            this.lblCompany.Text = "Company name";
            //
            // hostCompany
            //
            this.hostCompany.Controls.Add(this.txtCompany);
            this.hostCompany.Location = new System.Drawing.Point(60, 146);
            this.hostCompany.Name = "hostCompany";
            this.hostCompany.Placeholder = "Shown on printed invoices and reports";
            this.hostCompany.Size = new System.Drawing.Size(400, 38);
            this.hostCompany.TabIndex = 0;
            //
            // txtCompany
            //
            this.txtCompany.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCompany.Location = new System.Drawing.Point(12, 10);
            this.txtCompany.MaxLength = 100;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(376, 18);
            this.txtCompany.TabIndex = 0;
            this.txtCompany.TextChanged += new System.EventHandler(this.SetupField_TextChanged);
            //
            // lblFullName
            //
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFullName.Location = new System.Drawing.Point(60, 194);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(86, 15);
            this.lblFullName.TabIndex = 23;
            this.lblFullName.Text = "Your full name";
            //
            // hostFullName
            //
            this.hostFullName.Controls.Add(this.txtFullName);
            this.hostFullName.Location = new System.Drawing.Point(60, 214);
            this.hostFullName.Name = "hostFullName";
            this.hostFullName.Size = new System.Drawing.Size(400, 38);
            this.hostFullName.TabIndex = 1;
            //
            // txtFullName
            //
            this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFullName.Location = new System.Drawing.Point(12, 10);
            this.txtFullName.MaxLength = 60;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(376, 18);
            this.txtFullName.TabIndex = 0;
            this.txtFullName.TextChanged += new System.EventHandler(this.SetupField_TextChanged);
            //
            // lblNewUserName
            //
            this.lblNewUserName.AutoSize = true;
            this.lblNewUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblNewUserName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNewUserName.Location = new System.Drawing.Point(60, 262);
            this.lblNewUserName.Name = "lblNewUserName";
            this.lblNewUserName.Size = new System.Drawing.Size(65, 15);
            this.lblNewUserName.TabIndex = 24;
            this.lblNewUserName.Text = "User name";
            //
            // hostNewUserName
            //
            this.hostNewUserName.Controls.Add(this.txtNewUserName);
            this.hostNewUserName.Glyph = "";
            this.hostNewUserName.Location = new System.Drawing.Point(60, 282);
            this.hostNewUserName.Name = "hostNewUserName";
            this.hostNewUserName.Placeholder = "You will sign in with this, for example asha";
            this.hostNewUserName.Size = new System.Drawing.Size(400, 38);
            this.hostNewUserName.TabIndex = 2;
            //
            // txtNewUserName
            //
            this.txtNewUserName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewUserName.Location = new System.Drawing.Point(36, 10);
            this.txtNewUserName.MaxLength = 30;
            this.txtNewUserName.Name = "txtNewUserName";
            this.txtNewUserName.Size = new System.Drawing.Size(352, 18);
            this.txtNewUserName.TabIndex = 0;
            this.txtNewUserName.TextChanged += new System.EventHandler(this.SetupField_TextChanged);
            //
            // lblNewPassword
            //
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNewPassword.Location = new System.Drawing.Point(60, 330);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(59, 15);
            this.lblNewPassword.TabIndex = 25;
            this.lblNewPassword.Text = "Password";
            //
            // hostNewPassword
            //
            this.hostNewPassword.Controls.Add(this.txtNewPassword);
            this.hostNewPassword.Glyph = "";
            this.hostNewPassword.Location = new System.Drawing.Point(60, 350);
            this.hostNewPassword.Name = "hostNewPassword";
            this.hostNewPassword.Size = new System.Drawing.Size(194, 38);
            this.hostNewPassword.TabIndex = 3;
            //
            // txtNewPassword
            //
            this.txtNewPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPassword.Location = new System.Drawing.Point(36, 10);
            this.txtNewPassword.MaxLength = 128;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(146, 18);
            this.txtNewPassword.TabIndex = 0;
            this.txtNewPassword.UseSystemPasswordChar = true;
            this.txtNewPassword.TextChanged += new System.EventHandler(this.SetupField_TextChanged);
            //
            // lblConfirmPassword
            //
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblConfirmPassword.Location = new System.Drawing.Point(266, 330);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(105, 15);
            this.lblConfirmPassword.TabIndex = 26;
            this.lblConfirmPassword.Text = "Confirm password";
            //
            // hostConfirmPassword
            //
            this.hostConfirmPassword.Controls.Add(this.txtConfirmPassword);
            this.hostConfirmPassword.Glyph = "";
            this.hostConfirmPassword.Location = new System.Drawing.Point(266, 350);
            this.hostConfirmPassword.Name = "hostConfirmPassword";
            this.hostConfirmPassword.Size = new System.Drawing.Size(194, 38);
            this.hostConfirmPassword.TabIndex = 4;
            //
            // txtConfirmPassword
            //
            this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassword.Location = new System.Drawing.Point(36, 10);
            this.txtConfirmPassword.MaxLength = 128;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(146, 18);
            this.txtConfirmPassword.TabIndex = 0;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            this.txtConfirmPassword.TextChanged += new System.EventHandler(this.SetupField_TextChanged);
            //
            // lblPasswordRule
            //
            this.lblPasswordRule.AutoSize = true;
            this.lblPasswordRule.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblPasswordRule.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblPasswordRule.Location = new System.Drawing.Point(60, 394);
            this.lblPasswordRule.Name = "lblPasswordRule";
            this.lblPasswordRule.Size = new System.Drawing.Size(282, 15);
            this.lblPasswordRule.TabIndex = 27;
            this.lblPasswordRule.Text = "At least 8 characters, with both letters and numbers.";
            //
            // lblSetupError
            //
            this.lblSetupError.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSetupError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblSetupError.Location = new System.Drawing.Point(60, 416);
            this.lblSetupError.Name = "lblSetupError";
            this.lblSetupError.Size = new System.Drawing.Size(400, 40);
            this.lblSetupError.TabIndex = 28;
            //
            // btnCreateAccount
            //
            this.btnCreateAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.btnCreateAccount.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnCreateAccount.Location = new System.Drawing.Point(60, 462);
            this.btnCreateAccount.Name = "btnCreateAccount";
            this.btnCreateAccount.Size = new System.Drawing.Size(400, 44);
            this.btnCreateAccount.TabIndex = 5;
            this.btnCreateAccount.Text = "Create account and sign in";
            this.btnCreateAccount.BusyText = "Creating your account…";
            this.btnCreateAccount.Click += new System.EventHandler(this.btnCreateAccount_Click);
            //
            // pageMessage
            //
            this.pageMessage.Controls.Add(this.spinner);
            this.pageMessage.Controls.Add(this.lblMessageTitle);
            this.pageMessage.Controls.Add(this.lblMessage);
            this.pageMessage.Controls.Add(this.btnMessagePrimary);
            this.pageMessage.Controls.Add(this.btnMessageSecondary);
            this.pageMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageMessage.Location = new System.Drawing.Point(0, 0);
            this.pageMessage.Name = "pageMessage";
            this.pageMessage.Size = new System.Drawing.Size(520, 580);
            this.pageMessage.TabIndex = 2;
            //
            // spinner
            //
            this.spinner.Location = new System.Drawing.Point(60, 46);
            this.spinner.Name = "spinner";
            this.spinner.Size = new System.Drawing.Size(40, 40);
            this.spinner.TabIndex = 32;
            this.spinner.Visible = false;
            //
            // lblMessageTitle
            //
            this.lblMessageTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 16F);
            this.lblMessageTitle.Location = new System.Drawing.Point(56, 96);
            this.lblMessageTitle.Name = "lblMessageTitle";
            this.lblMessageTitle.Size = new System.Drawing.Size(404, 36);
            this.lblMessageTitle.TabIndex = 30;
            //
            // lblMessage
            //
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblMessage.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblMessage.Location = new System.Drawing.Point(59, 142);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(400, 250);
            this.lblMessage.TabIndex = 31;
            //
            // btnMessagePrimary
            //
            this.btnMessagePrimary.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnMessagePrimary.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnMessagePrimary.Location = new System.Drawing.Point(60, 404);
            this.btnMessagePrimary.Name = "btnMessagePrimary";
            this.btnMessagePrimary.Size = new System.Drawing.Size(194, 40);
            this.btnMessagePrimary.TabIndex = 0;
            this.btnMessagePrimary.Visible = false;
            this.btnMessagePrimary.Click += new System.EventHandler(this.btnMessagePrimary_Click);
            //
            // btnMessageSecondary
            //
            this.btnMessageSecondary.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnMessageSecondary.Glyph = "";
            this.btnMessageSecondary.Location = new System.Drawing.Point(266, 404);
            this.btnMessageSecondary.Name = "btnMessageSecondary";
            this.btnMessageSecondary.Size = new System.Drawing.Size(194, 40);
            this.btnMessageSecondary.TabIndex = 1;
            this.btnMessageSecondary.Visible = false;
            this.btnMessageSecondary.Click += new System.EventHandler(this.btnMessageSecondary_Click);
            //
            // SignInForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 580);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.brandPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SignInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Sign in - Purchase Invoices";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SignInForm_FormClosing);
            this.contentPanel.ResumeLayout(false);
            this.pageSignIn.ResumeLayout(false);
            this.pageSignIn.PerformLayout();
            this.hostUserName.ResumeLayout(false);
            this.hostUserName.PerformLayout();
            this.hostPassword.ResumeLayout(false);
            this.hostPassword.PerformLayout();
            this.pageSetup.ResumeLayout(false);
            this.pageSetup.PerformLayout();
            this.hostCompany.ResumeLayout(false);
            this.hostCompany.PerformLayout();
            this.hostFullName.ResumeLayout(false);
            this.hostFullName.PerformLayout();
            this.hostNewUserName.ResumeLayout(false);
            this.hostNewUserName.PerformLayout();
            this.hostNewPassword.ResumeLayout(false);
            this.hostNewPassword.PerformLayout();
            this.hostConfirmPassword.ResumeLayout(false);
            this.hostConfirmPassword.PerformLayout();
            this.pageMessage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.BrandPanel brandPanel;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel pageSignIn;
        private System.Windows.Forms.Label lblSignInTitle;
        private System.Windows.Forms.Label lblSignInHint;
        private System.Windows.Forms.Label lblUserName;
        private PurchaseInvoices.FieldHost hostUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblPassword;
        private PurchaseInvoices.FieldHost hostPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Label lblSignInError;
        private PurchaseInvoices.ModernButton btnSignIn;
        private System.Windows.Forms.Label lblConnection;
        private PurchaseInvoices.ModernButton btnDatabaseSettings;
        private System.Windows.Forms.Panel pageSetup;
        private System.Windows.Forms.Label lblSetupTitle;
        private System.Windows.Forms.Label lblSetupHint;
        private System.Windows.Forms.Label lblCompany;
        private PurchaseInvoices.FieldHost hostCompany;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label lblFullName;
        private PurchaseInvoices.FieldHost hostFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblNewUserName;
        private PurchaseInvoices.FieldHost hostNewUserName;
        private System.Windows.Forms.TextBox txtNewUserName;
        private System.Windows.Forms.Label lblNewPassword;
        private PurchaseInvoices.FieldHost hostNewPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private PurchaseInvoices.FieldHost hostConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Label lblPasswordRule;
        private System.Windows.Forms.Label lblSetupError;
        private PurchaseInvoices.ModernButton btnCreateAccount;
        private System.Windows.Forms.Panel pageMessage;
        private PurchaseInvoices.Spinner spinner;
        private System.Windows.Forms.Label lblMessageTitle;
        private System.Windows.Forms.Label lblMessage;
        private PurchaseInvoices.ModernButton btnMessagePrimary;
        private PurchaseInvoices.ModernButton btnMessageSecondary;
    }
}
