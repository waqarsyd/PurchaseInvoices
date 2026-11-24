namespace PurchaseInvoices
{
    partial class AboutForm
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
            this.btnCopy = new PurchaseInvoices.ModernButton();
            this.lblCopied = new System.Windows.Forms.Label();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.shortcutsCard = new PurchaseInvoices.CardPanel();
            this.detailsCard = new PurchaseInvoices.CardPanel();
            this.detailsList = new PurchaseInvoices.InfoList();
            this.shortcutsList = new PurchaseInvoices.InfoList();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.shortcutsCard.SuspendLayout();
            this.detailsCard.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.ShowLogo = true;
            this.headerPanel.Size = new System.Drawing.Size(640, 64);
            this.headerPanel.Subtitle = "Version";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Purchase Invoices";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnCopy);
            this.footerPanel.Controls.Add(this.lblCopied);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 600);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(640, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnCopy
            //
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCopy.Glyph = "";
            this.btnCopy.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnCopy.Location = new System.Drawing.Point(20, 12);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 36);
            this.btnCopy.TabIndex = 1;
            this.btnCopy.Text = "Copy details";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            //
            // lblCopied
            //
            this.lblCopied.AutoSize = true;
            this.lblCopied.ForeColor = System.Drawing.Color.FromArgb(4, 120, 87);
            this.lblCopied.Location = new System.Drawing.Point(182, 21);
            this.lblCopied.Name = "lblCopied";
            this.lblCopied.Size = new System.Drawing.Size(0, 17);
            this.lblCopied.TabIndex = 2;
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnClose.Location = new System.Drawing.Point(520, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.shortcutsCard);
            this.contentPanel.Controls.Add(this.detailsCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(640, 536);
            this.contentPanel.TabIndex = 1;
            //
            // shortcutsCard
            //
            this.shortcutsCard.Controls.Add(this.shortcutsList);
            this.shortcutsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shortcutsCard.Glyph = "";
            this.shortcutsCard.Location = new System.Drawing.Point(14, 300);
            this.shortcutsCard.Name = "shortcutsCard";
            this.shortcutsCard.Size = new System.Drawing.Size(612, 228);
            this.shortcutsCard.TabIndex = 1;
            this.shortcutsCard.Title = "Keyboard shortcuts";
            //
            // shortcutsList
            //
            this.shortcutsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutsList.Columns = 2;
            this.shortcutsList.Keycaps = true;
            this.shortcutsList.Location = new System.Drawing.Point(24, 46);
            this.shortcutsList.Name = "shortcutsList";
            this.shortcutsList.Size = new System.Drawing.Size(564, 166);
            this.shortcutsList.TabIndex = 0;
            //
            // detailsCard
            //
            this.detailsCard.Controls.Add(this.detailsList);
            this.detailsCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailsCard.Glyph = "";
            this.detailsCard.Location = new System.Drawing.Point(14, 8);
            this.detailsCard.Name = "detailsCard";
            this.detailsCard.Size = new System.Drawing.Size(612, 310);
            this.detailsCard.TabIndex = 0;
            this.detailsCard.Title = "About this copy";
            //
            // detailsList
            //
            this.detailsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.detailsList.Location = new System.Drawing.Point(24, 46);
            this.detailsList.Name = "detailsList";
            this.detailsList.Size = new System.Drawing.Size(564, 250);
            this.detailsList.TabIndex = 0;
            //
            // AboutForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(640, 680);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "About Purchase Invoices";
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.shortcutsCard.ResumeLayout(false);
            this.detailsCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnCopy;
        private System.Windows.Forms.Label lblCopied;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel shortcutsCard;
        private PurchaseInvoices.CardPanel detailsCard;
        private PurchaseInvoices.InfoList detailsList;
        private PurchaseInvoices.InfoList shortcutsList;
    }
}
