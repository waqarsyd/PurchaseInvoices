namespace PurchaseInvoices
{
    partial class ReportsForm
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
            this.lblSummary = new System.Windows.Forms.Label();
            this.btnExport = new PurchaseInvoices.ModernButton();
            this.btnPrint = new PurchaseInvoices.ModernButton();
            this.btnClose = new PurchaseInvoices.ModernButton();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.gridCard = new PurchaseInvoices.CardPanel();
            this.grid = new PurchaseInvoices.ModernGrid();
            this.statsPanel = new System.Windows.Forms.Panel();
            this.tileTotal = new PurchaseInvoices.StatTile();
            this.tileInvoices = new PurchaseInvoices.StatTile();
            this.tileAverage = new PurchaseInvoices.StatTile();
            this.tileHighlight = new PurchaseInvoices.StatTile();
            this.filterCard = new PurchaseInvoices.CardPanel();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.comboPeriod = new PurchaseInvoices.ModernComboBox();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.comboCustomer = new PurchaseInvoices.ModernComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.navCard = new PurchaseInvoices.CardPanel();
            this.btnByCustomer = new PurchaseInvoices.ModernButton();
            this.btnByItem = new PurchaseInvoices.ModernButton();
            this.btnByMonth = new PurchaseInvoices.ModernButton();
            this.btnRegister = new PurchaseInvoices.ModernButton();
            this.lblReportHint = new System.Windows.Forms.Label();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.statsPanel.SuspendLayout();
            this.filterCard.SuspendLayout();
            this.navCard.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1120, 64);
            this.headerPanel.Subtitle = "See where your purchases go";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Reports";
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.lblSummary);
            this.footerPanel.Controls.Add(this.btnExport);
            this.footerPanel.Controls.Add(this.btnPrint);
            this.footerPanel.Controls.Add(this.btnClose);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 660);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1120, 60);
            this.footerPanel.TabIndex = 2;
            //
            // lblSummary
            //
            this.lblSummary.AutoSize = true;
            this.lblSummary.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblSummary.Location = new System.Drawing.Point(20, 21);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(0, 17);
            this.lblSummary.TabIndex = 3;
            //
            // btnExport
            //
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnExport.Glyph = "";
            this.btnExport.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnExport.Location = new System.Drawing.Point(700, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(140, 36);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "Export CSV";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            //
            // btnPrint
            //
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrint.Glyph = "";
            this.btnPrint.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnPrint.Location = new System.Drawing.Point(850, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 36);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print preview";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // btnClose
            //
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnClose.Location = new System.Drawing.Point(1010, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 36);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.contentPanel.Controls.Add(this.mainPanel);
            this.contentPanel.Controls.Add(this.navCard);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(1120, 596);
            this.contentPanel.TabIndex = 1;
            //
            // mainPanel
            //
            this.mainPanel.Controls.Add(this.gridCard);
            this.mainPanel.Controls.Add(this.statsPanel);
            this.mainPanel.Controls.Add(this.filterCard);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(254, 8);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(852, 580);
            this.mainPanel.TabIndex = 1;
            //
            // gridCard
            //
            this.gridCard.Controls.Add(this.grid);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCard.Glyph = "";
            this.gridCard.Location = new System.Drawing.Point(0, 206);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(852, 374);
            this.gridCard.TabIndex = 2;
            this.gridCard.Title = "Report";
            //
            // grid
            //
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.EmptyHint = "Try a different period or customer.";
            this.grid.EmptyTitle = "Nothing in this period";
            this.grid.Location = new System.Drawing.Point(20, 46);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.Size = new System.Drawing.Size(812, 310);
            this.grid.TabIndex = 0;
            //
            // statsPanel
            //
            this.statsPanel.Controls.Add(this.tileTotal);
            this.statsPanel.Controls.Add(this.tileInvoices);
            this.statsPanel.Controls.Add(this.tileAverage);
            this.statsPanel.Controls.Add(this.tileHighlight);
            this.statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.statsPanel.Location = new System.Drawing.Point(0, 96);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new System.Drawing.Size(852, 110);
            this.statsPanel.TabIndex = 1;
            this.statsPanel.Layout += new System.Windows.Forms.LayoutEventHandler(this.statsPanel_Layout);
            //
            // tileTotal
            //
            this.tileTotal.Caption = "TOTAL PURCHASES";
            this.tileTotal.Highlight = true;
            this.tileTotal.Location = new System.Drawing.Point(0, 0);
            this.tileTotal.Name = "tileTotal";
            this.tileTotal.Size = new System.Drawing.Size(213, 110);
            this.tileTotal.TabIndex = 0;
            //
            // tileInvoices
            //
            this.tileInvoices.Caption = "INVOICES";
            this.tileInvoices.Location = new System.Drawing.Point(213, 0);
            this.tileInvoices.Name = "tileInvoices";
            this.tileInvoices.Size = new System.Drawing.Size(213, 110);
            this.tileInvoices.TabIndex = 1;
            //
            // tileAverage
            //
            this.tileAverage.Caption = "AVERAGE INVOICE";
            this.tileAverage.Location = new System.Drawing.Point(426, 0);
            this.tileAverage.Name = "tileAverage";
            this.tileAverage.Size = new System.Drawing.Size(213, 110);
            this.tileAverage.TabIndex = 2;
            //
            // tileHighlight
            //
            this.tileHighlight.Location = new System.Drawing.Point(639, 0);
            this.tileHighlight.Name = "tileHighlight";
            this.tileHighlight.Size = new System.Drawing.Size(213, 110);
            this.tileHighlight.TabIndex = 3;
            //
            // filterCard
            //
            this.filterCard.Controls.Add(this.lblPeriod);
            this.filterCard.Controls.Add(this.comboPeriod);
            this.filterCard.Controls.Add(this.lblCustomer);
            this.filterCard.Controls.Add(this.comboCustomer);
            this.filterCard.Controls.Add(this.lblFrom);
            this.filterCard.Controls.Add(this.dtFrom);
            this.filterCard.Controls.Add(this.lblTo);
            this.filterCard.Controls.Add(this.dtTo);
            this.filterCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterCard.Location = new System.Drawing.Point(0, 0);
            this.filterCard.Name = "filterCard";
            this.filterCard.Size = new System.Drawing.Size(852, 96);
            this.filterCard.TabIndex = 0;
            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblPeriod.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblPeriod.Location = new System.Drawing.Point(24, 16);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(41, 15);
            this.lblPeriod.TabIndex = 10;
            this.lblPeriod.Text = "Period";
            //
            // comboPeriod
            //
            this.comboPeriod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboPeriod.Location = new System.Drawing.Point(24, 36);
            this.comboPeriod.Name = "comboPeriod";
            this.comboPeriod.Size = new System.Drawing.Size(200, 36);
            this.comboPeriod.TabIndex = 0;
            this.comboPeriod.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblCustomer
            //
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblCustomer.Location = new System.Drawing.Point(240, 16);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(60, 15);
            this.lblCustomer.TabIndex = 11;
            this.lblCustomer.Text = "Customer";
            //
            // comboCustomer
            //
            this.comboCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboCustomer.Location = new System.Drawing.Point(240, 36);
            this.comboCustomer.Name = "comboCustomer";
            this.comboCustomer.Placeholder = "All customers";
            this.comboCustomer.Size = new System.Drawing.Size(240, 36);
            this.comboCustomer.TabIndex = 1;
            this.comboCustomer.SelectedIndexChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblFrom
            //
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblFrom.Location = new System.Drawing.Point(496, 16);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(35, 15);
            this.lblFrom.TabIndex = 12;
            this.lblFrom.Text = "From";
            this.lblFrom.Visible = false;
            //
            // dtFrom
            //
            this.dtFrom.CustomFormat = "dd MMM yyyy";
            this.dtFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(496, 40);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(140, 25);
            this.dtFrom.TabIndex = 2;
            this.dtFrom.Visible = false;
            this.dtFrom.ValueChanged += new System.EventHandler(this.FilterChanged);
            //
            // lblTo
            //
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.lblTo.Location = new System.Drawing.Point(652, 16);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(19, 15);
            this.lblTo.TabIndex = 13;
            this.lblTo.Text = "To";
            this.lblTo.Visible = false;
            //
            // dtTo
            //
            this.dtTo.CustomFormat = "dd MMM yyyy";
            this.dtTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(652, 40);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(140, 25);
            this.dtTo.TabIndex = 3;
            this.dtTo.Visible = false;
            this.dtTo.ValueChanged += new System.EventHandler(this.FilterChanged);
            //
            // navCard
            //
            this.navCard.Controls.Add(this.btnByCustomer);
            this.navCard.Controls.Add(this.btnByItem);
            this.navCard.Controls.Add(this.btnByMonth);
            this.navCard.Controls.Add(this.btnRegister);
            this.navCard.Controls.Add(this.lblReportHint);
            this.navCard.Dock = System.Windows.Forms.DockStyle.Left;
            this.navCard.Glyph = "";
            this.navCard.Location = new System.Drawing.Point(14, 8);
            this.navCard.Name = "navCard";
            this.navCard.Size = new System.Drawing.Size(240, 580);
            this.navCard.TabIndex = 0;
            this.navCard.Title = "Reports";
            //
            // btnByCustomer
            //
            this.btnByCustomer.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnByCustomer.Glyph = "";
            this.btnByCustomer.Location = new System.Drawing.Point(20, 50);
            this.btnByCustomer.Name = "btnByCustomer";
            this.btnByCustomer.Size = new System.Drawing.Size(200, 42);
            this.btnByCustomer.TabIndex = 0;
            this.btnByCustomer.Text = "By customer";
            this.btnByCustomer.Click += new System.EventHandler(this.btnByCustomer_Click);
            //
            // btnByItem
            //
            this.btnByItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnByItem.Glyph = "";
            this.btnByItem.Location = new System.Drawing.Point(20, 100);
            this.btnByItem.Name = "btnByItem";
            this.btnByItem.Size = new System.Drawing.Size(200, 42);
            this.btnByItem.TabIndex = 1;
            this.btnByItem.Text = "By item";
            this.btnByItem.Click += new System.EventHandler(this.btnByItem_Click);
            //
            // btnByMonth
            //
            this.btnByMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnByMonth.Glyph = "";
            this.btnByMonth.Location = new System.Drawing.Point(20, 150);
            this.btnByMonth.Name = "btnByMonth";
            this.btnByMonth.Size = new System.Drawing.Size(200, 42);
            this.btnByMonth.TabIndex = 2;
            this.btnByMonth.Text = "By month";
            this.btnByMonth.Click += new System.EventHandler(this.btnByMonth_Click);
            //
            // btnRegister
            //
            this.btnRegister.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnRegister.Glyph = "";
            this.btnRegister.Location = new System.Drawing.Point(20, 200);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(200, 42);
            this.btnRegister.TabIndex = 3;
            this.btnRegister.Text = "Invoice register";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            //
            // lblReportHint
            //
            this.lblReportHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblReportHint.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblReportHint.Location = new System.Drawing.Point(22, 258);
            this.lblReportHint.Name = "lblReportHint";
            this.lblReportHint.Size = new System.Drawing.Size(196, 80);
            this.lblReportHint.TabIndex = 4;
            //
            // ReportsForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(1120, 720);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.MinimizeBox = false;
            this.Name = "ReportsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Reports";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.mainPanel.ResumeLayout(false);
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.statsPanel.ResumeLayout(false);
            this.filterCard.ResumeLayout(false);
            this.filterCard.PerformLayout();
            this.navCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FooterPanel footerPanel;
        private System.Windows.Forms.Label lblSummary;
        private PurchaseInvoices.ModernButton btnExport;
        private PurchaseInvoices.ModernButton btnPrint;
        private PurchaseInvoices.ModernButton btnClose;
        private System.Windows.Forms.Panel contentPanel;
        private System.Windows.Forms.Panel mainPanel;
        private PurchaseInvoices.CardPanel gridCard;
        private PurchaseInvoices.ModernGrid grid;
        private System.Windows.Forms.Panel statsPanel;
        private PurchaseInvoices.StatTile tileTotal;
        private PurchaseInvoices.StatTile tileInvoices;
        private PurchaseInvoices.StatTile tileAverage;
        private PurchaseInvoices.StatTile tileHighlight;
        private PurchaseInvoices.CardPanel filterCard;
        private System.Windows.Forms.Label lblPeriod;
        private PurchaseInvoices.ModernComboBox comboPeriod;
        private System.Windows.Forms.Label lblCustomer;
        private PurchaseInvoices.ModernComboBox comboCustomer;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.DateTimePicker dtTo;
        private PurchaseInvoices.CardPanel navCard;
        private PurchaseInvoices.ModernButton btnByCustomer;
        private PurchaseInvoices.ModernButton btnByItem;
        private PurchaseInvoices.ModernButton btnByMonth;
        private PurchaseInvoices.ModernButton btnRegister;
        private System.Windows.Forms.Label lblReportHint;
    }
}
