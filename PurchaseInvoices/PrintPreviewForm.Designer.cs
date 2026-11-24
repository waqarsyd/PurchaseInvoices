namespace PurchaseInvoices
{
    partial class PrintPreviewForm
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
            this.btnFit = new PurchaseInvoices.ModernButton();
            this.btnActualSize = new PurchaseInvoices.ModernButton();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.btnPreviousPage = new PurchaseInvoices.ModernButton();
            this.lblPage = new System.Windows.Forms.Label();
            this.btnNextPage = new PurchaseInvoices.ModernButton();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.btnPrint = new PurchaseInvoices.ModernButton();
            this.preview = new System.Windows.Forms.PrintPreviewControl();
            this.headerPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.btnFit);
            this.headerPanel.Controls.Add(this.btnActualSize);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(900, 64);
            this.headerPanel.Subtitle = "Purchase invoice";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Print preview";
            //
            // btnFit
            //
            this.btnFit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnFit.Glyph = "";
            this.btnFit.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnFit.Location = new System.Drawing.Point(650, 14);
            this.btnFit.Name = "btnFit";
            this.btnFit.Size = new System.Drawing.Size(120, 36);
            this.btnFit.TabIndex = 0;
            this.btnFit.Text = "Fit page";
            this.btnFit.Click += new System.EventHandler(this.btnFit_Click);
            //
            // btnActualSize
            //
            this.btnActualSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualSize.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnActualSize.Glyph = "";
            this.btnActualSize.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnActualSize.Location = new System.Drawing.Point(780, 14);
            this.btnActualSize.Name = "btnActualSize";
            this.btnActualSize.Size = new System.Drawing.Size(100, 36);
            this.btnActualSize.TabIndex = 1;
            this.btnActualSize.Text = "100%";
            this.btnActualSize.Click += new System.EventHandler(this.btnActualSize_Click);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnPreviousPage);
            this.footerPanel.Controls.Add(this.lblPage);
            this.footerPanel.Controls.Add(this.btnNextPage);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Controls.Add(this.btnPrint);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 640);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(900, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnPreviousPage
            //
            this.btnPreviousPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPreviousPage.Glyph = "";
            this.btnPreviousPage.Location = new System.Drawing.Point(20, 12);
            this.btnPreviousPage.Name = "btnPreviousPage";
            this.btnPreviousPage.Size = new System.Drawing.Size(44, 36);
            this.btnPreviousPage.TabIndex = 0;
            this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
            //
            // lblPage
            //
            this.lblPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.lblPage.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPage.Location = new System.Drawing.Point(70, 12);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(110, 36);
            this.lblPage.TabIndex = 3;
            this.lblPage.Text = "Page 1 of 1";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnNextPage
            //
            this.btnNextPage.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNextPage.Glyph = "";
            this.btnNextPage.Location = new System.Drawing.Point(186, 12);
            this.btnNextPage.Name = "btnNextPage";
            this.btnNextPage.Size = new System.Drawing.Size(44, 36);
            this.btnNextPage.TabIndex = 1;
            this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(610, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 36);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // btnPrint
            //
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrint.Glyph = "";
            this.btnPrint.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnPrint.Location = new System.Drawing.Point(730, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 36);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "Print…";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // preview
            //
            this.preview.AutoZoom = true;
            this.preview.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.preview.Location = new System.Drawing.Point(0, 64);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(900, 576);
            this.preview.TabIndex = 1;
            this.preview.UseAntiAlias = true;
            //
            // PrintPreviewForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MinimizeBox = false;
            this.Name = "PrintPreviewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Print preview";
            this.headerPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.ModernButton btnFit;
        private PurchaseInvoices.ModernButton btnActualSize;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnPreviousPage;
        private System.Windows.Forms.Label lblPage;
        private PurchaseInvoices.ModernButton btnNextPage;
        private PurchaseInvoices.ModernButton btnClose;
        private PurchaseInvoices.ModernButton btnPrint;
        private System.Windows.Forms.PrintPreviewControl preview;
    }
}
