namespace PurchaseInvoices
{
    partial class ChangePasswordForm
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
            this.btnCancel = new PurchaseInvoices.ModernButton();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.passwordCard = new PurchaseInvoices.CardPanel();
            this.lblIntro = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.hostCurrent = new PurchaseInvoices.FieldHost();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.hostNew = new PurchaseInvoices.FieldHost();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.hostConfirm = new PurchaseInvoices.FieldHost();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblRule = new System.Windows.Forms.Label();
            this.chkMustChange = new System.Windows.Forms.CheckBox();
            this.lblError = new System.Windows.Forms.Label();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.passwordCard.SuspendLayout();
            this.hostCurrent.SuspendLayout();
            this.hostNew.SuspendLayout();
            this.hostConfirm.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(520, 64);
            this.headerPanel.Subtitle = "Keep your account safe";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Change password";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnCancel);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 420);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(520, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnCancel
            //
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCancel.Location = new System.Drawing.Point(240, 12);
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
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(350, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 36);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save password";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.passwordCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(520, 356);
            this.contentPanel.TabIndex = 1;
            //
            // passwordCard
            //
            this.passwordCard.Controls.Add(this.lblIntro);
            this.passwordCard.Controls.Add(this.lblCurrent);
            this.passwordCard.Controls.Add(this.hostCurrent);
            this.passwordCard.Controls.Add(this.lblNew);
            this.passwordCard.Controls.Add(this.hostNew);
            this.passwordCard.Controls.Add(this.lblConfirm);
            this.passwordCard.Controls.Add(this.hostConfirm);
            this.passwordCard.Controls.Add(this.lblRule);
            this.passwordCard.Controls.Add(this.chkMustChange);
            this.passwordCard.Controls.Add(this.lblError);
            this.passwordCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passwordCard.Location = new System.Drawing.Point(14, 8);
            this.passwordCard.Name = "passwordCard";
            this.passwordCard.Size = new System.Drawing.Size(492, 340);
            this.passwordCard.TabIndex = 0;
            //
            // lblIntro
            //
            this.lblIntro.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblIntro.Location = new System.Drawing.Point(24, 18);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(444, 40);
            this.lblIntro.TabIndex = 10;
            //
            // lblCurrent
            //
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCurrent.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCurrent.Location = new System.Drawing.Point(24, 64);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(101, 15);
            this.lblCurrent.TabIndex = 11;
            this.lblCurrent.Text = "Current password";
            //
            // hostCurrent
            //
            this.hostCurrent.Controls.Add(this.txtCurrent);
            this.hostCurrent.Glyph = "";
            this.hostCurrent.Location = new System.Drawing.Point(24, 84);
            this.hostCurrent.Name = "hostCurrent";
            this.hostCurrent.Size = new System.Drawing.Size(444, 38);
            this.hostCurrent.TabIndex = 0;
            //
            // txtCurrent
            //
            this.txtCurrent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrent.Location = new System.Drawing.Point(36, 10);
            this.txtCurrent.MaxLength = 128;
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(396, 18);
            this.txtCurrent.TabIndex = 0;
            this.txtCurrent.UseSystemPasswordChar = true;
            this.txtCurrent.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblNew
            //
            this.lblNew.AutoSize = true;
            this.lblNew.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblNew.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblNew.Location = new System.Drawing.Point(24, 132);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(84, 15);
            this.lblNew.TabIndex = 12;
            this.lblNew.Text = "New password";
            //
            // hostNew
            //
            this.hostNew.Controls.Add(this.txtNew);
            this.hostNew.Glyph = "";
            this.hostNew.Location = new System.Drawing.Point(24, 152);
            this.hostNew.Name = "hostNew";
            this.hostNew.Size = new System.Drawing.Size(444, 38);
            this.hostNew.TabIndex = 1;
            //
            // txtNew
            //
            this.txtNew.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNew.Location = new System.Drawing.Point(36, 10);
            this.txtNew.MaxLength = 128;
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(396, 18);
            this.txtNew.TabIndex = 0;
            this.txtNew.UseSystemPasswordChar = true;
            this.txtNew.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblConfirm
            //
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblConfirm.Location = new System.Drawing.Point(24, 200);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(130, 15);
            this.lblConfirm.TabIndex = 13;
            this.lblConfirm.Text = "Confirm new password";
            //
            // hostConfirm
            //
            this.hostConfirm.Controls.Add(this.txtConfirm);
            this.hostConfirm.Glyph = "";
            this.hostConfirm.Location = new System.Drawing.Point(24, 220);
            this.hostConfirm.Name = "hostConfirm";
            this.hostConfirm.Size = new System.Drawing.Size(444, 38);
            this.hostConfirm.TabIndex = 2;
            //
            // txtConfirm
            //
            this.txtConfirm.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirm.Location = new System.Drawing.Point(36, 10);
            this.txtConfirm.MaxLength = 128;
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(396, 18);
            this.txtConfirm.TabIndex = 0;
            this.txtConfirm.UseSystemPasswordChar = true;
            this.txtConfirm.TextChanged += new System.EventHandler(this.Field_TextChanged);
            //
            // lblRule
            //
            this.lblRule.AutoSize = true;
            this.lblRule.Font = new System.Drawing.Font("Segoe UI", 8.75F);
            this.lblRule.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblRule.Location = new System.Drawing.Point(24, 264);
            this.lblRule.Name = "lblRule";
            this.lblRule.Size = new System.Drawing.Size(282, 15);
            this.lblRule.TabIndex = 14;
            this.lblRule.Text = "At least 8 characters, with both letters and numbers.";
            //
            // chkMustChange
            //
            this.chkMustChange.AutoSize = true;
            this.chkMustChange.Checked = true;
            this.chkMustChange.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMustChange.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.chkMustChange.Location = new System.Drawing.Point(25, 284);
            this.chkMustChange.Name = "chkMustChange";
            this.chkMustChange.Size = new System.Drawing.Size(346, 21);
            this.chkMustChange.TabIndex = 3;
            this.chkMustChange.Text = "Ask them to choose a new password when they sign in";
            this.chkMustChange.UseVisualStyleBackColor = true;
            this.chkMustChange.Visible = false;
            //
            // lblError
            //
            this.lblError.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblError.ForeColor = System.Drawing.Color.FromArgb(185, 28, 28);
            this.lblError.Location = new System.Drawing.Point(24, 290);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(444, 36);
            this.lblError.TabIndex = 15;
            //
            // ChangePasswordForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(520, 480);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePasswordForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Change password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChangePasswordForm_FormClosing);
            this.footerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.passwordCard.ResumeLayout(false);
            this.passwordCard.PerformLayout();
            this.hostCurrent.ResumeLayout(false);
            this.hostCurrent.PerformLayout();
            this.hostNew.ResumeLayout(false);
            this.hostNew.PerformLayout();
            this.hostConfirm.ResumeLayout(false);
            this.hostConfirm.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnCancel;
        private PurchaseInvoices.ModernButton btnSave;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel passwordCard;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label lblCurrent;
        private PurchaseInvoices.FieldHost hostCurrent;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.Label lblNew;
        private PurchaseInvoices.FieldHost hostNew;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Label lblConfirm;
        private PurchaseInvoices.FieldHost hostConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblRule;
        private System.Windows.Forms.CheckBox chkMustChange;
        private System.Windows.Forms.Label lblError;
    }
}
