namespace PurchaseInvoices
{
    partial class CompanyForm
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
            this.btnPreview = new PurchaseInvoices.ModernButton();
            this.btnCancel = new PurchaseInvoices.ModernButton();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.profileCard = new PurchaseInvoices.CardPanel();
            this.lblIntro = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.hostName = new PurchaseInvoices.FieldHost();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.hostAddress = new PurchaseInvoices.FieldHost();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.hostPhone = new PurchaseInvoices.FieldHost();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.hostEmail = new PurchaseInvoices.FieldHost();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblTax = new System.Windows.Forms.Label();
            this.hostTax = new PurchaseInvoices.FieldHost();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.profileCard.SuspendLayout();
            this.hostName.SuspendLayout();
            this.hostAddress.SuspendLayout();
            this.hostPhone.SuspendLayout();
            this.hostEmail.SuspendLayout();
            this.hostTax.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(640, 64);
            this.headerPanel.Subtitle = "Printed on every invoice and report";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Company profile";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnPreview);
            this.footerPanel.Controls.Add(this.btnCancel);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 500);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(640, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnPreview
            //
            this.btnPreview.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPreview.Glyph = "";
            this.btnPreview.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnPreview.Location = new System.Drawing.Point(20, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(200, 36);
            this.btnPreview.TabIndex = 2;
            this.btnPreview.Text = "Preview on an invoice";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(400, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 36);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.contentPanel.Controls.Add(this.profileCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(640, 436);
            this.contentPanel.TabIndex = 1;
            //
            // profileCard
            //
            this.profileCard.Controls.Add(this.lblIntro);
            this.profileCard.Controls.Add(this.lblName);
            this.profileCard.Controls.Add(this.hostName);
            this.profileCard.Controls.Add(this.lblAddress);
            this.profileCard.Controls.Add(this.hostAddress);
            this.profileCard.Controls.Add(this.lblPhone);
            this.profileCard.Controls.Add(this.hostPhone);
            this.profileCard.Controls.Add(this.lblEmail);
            this.profileCard.Controls.Add(this.hostEmail);
            this.profileCard.Controls.Add(this.lblTax);
            this.profileCard.Controls.Add(this.hostTax);
            this.profileCard.Controls.Add(this.lblError);
            this.profileCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.profileCard.Location = new System.Drawing.Point(14, 8);
            this.profileCard.Name = "profileCard";
            this.profileCard.Size = new System.Drawing.Size(612, 420);
            this.profileCard.TabIndex = 0;
            //
            // lblIntro
            //
            this.lblIntro.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblIntro.Location = new System.Drawing.Point(24, 18);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(564, 36);
            this.lblIntro.TabIndex = 10;
            this.lblIntro.Text = "These details print at the top of every invoice and report, next to your logo. Only the company name is required.";
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblName.Location = new System.Drawing.Point(24, 62);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(88, 15);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "Company name";
            //
            // hostName
            //
            this.hostName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostName.Controls.Add(this.txtName);
            this.hostName.Location = new System.Drawing.Point(24, 82);
            this.hostName.Name = "hostName";
            this.hostName.Size = new System.Drawing.Size(564, 36);
            this.hostName.TabIndex = 0;
            //
            // txtName
            //
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.Location = new System.Drawing.Point(12, 9);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(540, 18);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblAddress
            //
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblAddress.Location = new System.Drawing.Point(24, 128);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(51, 15);
            this.lblAddress.TabIndex = 12;
            this.lblAddress.Text = "Address";
            //
            // hostAddress
            //
            this.hostAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostAddress.Controls.Add(this.txtAddress);
            this.hostAddress.Location = new System.Drawing.Point(24, 148);
            this.hostAddress.Name = "hostAddress";
            this.hostAddress.Size = new System.Drawing.Size(564, 72);
            this.hostAddress.TabIndex = 1;
            //
            // txtAddress
            //
            this.txtAddress.AcceptsReturn = true;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location = new System.Drawing.Point(12, 8);
            this.txtAddress.MaxLength = 500;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(540, 58);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblPhone
            //
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPhone.Location = new System.Drawing.Point(24, 230);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(40, 15);
            this.lblPhone.TabIndex = 13;
            this.lblPhone.Text = "Phone";
            //
            // hostPhone
            //
            this.hostPhone.Controls.Add(this.txtPhone);
            this.hostPhone.Location = new System.Drawing.Point(24, 250);
            this.hostPhone.Name = "hostPhone";
            this.hostPhone.Size = new System.Drawing.Size(274, 36);
            this.hostPhone.TabIndex = 2;
            //
            // txtPhone
            //
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(12, 9);
            this.txtPhone.MaxLength = 30;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(250, 18);
            this.txtPhone.TabIndex = 0;
            this.txtPhone.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblEmail.Location = new System.Drawing.Point(314, 230);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(36, 15);
            this.lblEmail.TabIndex = 14;
            this.lblEmail.Text = "Email";
            //
            // hostEmail
            //
            this.hostEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostEmail.Controls.Add(this.txtEmail);
            this.hostEmail.Location = new System.Drawing.Point(314, 250);
            this.hostEmail.Name = "hostEmail";
            this.hostEmail.Size = new System.Drawing.Size(274, 36);
            this.hostEmail.TabIndex = 3;
            //
            // txtEmail
            //
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(12, 9);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 18);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblTax
            //
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblTax.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTax.Location = new System.Drawing.Point(24, 296);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(142, 15);
            this.lblTax.TabIndex = 15;
            this.lblTax.Text = "Tax number (GST, VAT…)";
            //
            // hostTax
            //
            this.hostTax.Controls.Add(this.txtTax);
            this.hostTax.Location = new System.Drawing.Point(24, 316);
            this.hostTax.Name = "hostTax";
            this.hostTax.Size = new System.Drawing.Size(274, 36);
            this.hostTax.TabIndex = 4;
            //
            // txtTax
            //
            this.txtTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTax.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTax.Location = new System.Drawing.Point(12, 9);
            this.txtTax.MaxLength = 30;
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(250, 18);
            this.txtTax.TabIndex = 0;
            this.txtTax.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblError
            //
            this.lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblError.Location = new System.Drawing.Point(24, 364);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(564, 36);
            this.lblError.TabIndex = 16;
            //
            // CompanyForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(640, 560);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CompanyForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Company profile";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CompanyForm_FormClosing);
            this.Load += new System.EventHandler(this.CompanyForm_Load);
            this.footerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.profileCard.ResumeLayout(false);
            this.profileCard.PerformLayout();
            this.hostName.ResumeLayout(false);
            this.hostName.PerformLayout();
            this.hostAddress.ResumeLayout(false);
            this.hostAddress.PerformLayout();
            this.hostPhone.ResumeLayout(false);
            this.hostPhone.PerformLayout();
            this.hostEmail.ResumeLayout(false);
            this.hostEmail.PerformLayout();
            this.hostTax.ResumeLayout(false);
            this.hostTax.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnPreview;
        private PurchaseInvoices.ModernButton btnCancel;
        private PurchaseInvoices.ModernButton btnSave;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel profileCard;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label lblName;
        private PurchaseInvoices.FieldHost hostName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblAddress;
        private PurchaseInvoices.FieldHost hostAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPhone;
        private PurchaseInvoices.FieldHost hostPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private PurchaseInvoices.FieldHost hostEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblTax;
        private PurchaseInvoices.FieldHost hostTax;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label lblError;
    }
}
