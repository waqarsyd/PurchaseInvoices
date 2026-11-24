namespace PurchaseInvoices
{
    partial class InvoiceForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.btnNew = new PurchaseInvoices.ModernButton();
            this.btnPrint = new PurchaseInvoices.ModernButton();
            this.btnEditInvoice = new PurchaseInvoices.ModernButton();
            this.btnDeleteInvoice = new PurchaseInvoices.ModernButton();
            this.btnCancelEdit = new PurchaseInvoices.ModernButton();
            this.btnPrevious = new PurchaseInvoices.ModernButton();
            this.btnNext = new PurchaseInvoices.ModernButton();
            this.btnSearch = new PurchaseInvoices.ModernButton();
            this.btnAllInvoices = new PurchaseInvoices.ModernButton();
            this.btnAdd = new PurchaseInvoices.ModernButton();
            this.btnCustomers = new PurchaseInvoices.ModernButton();
            this.btnItems = new PurchaseInvoices.ModernButton();
            this.btnReports = new PurchaseInvoices.ModernButton();
            this.btnAccount = new PurchaseInvoices.AccountButton();
            this.headerPanel = new PurchaseInvoices.HeaderPanel();
            this.hostSearch = new PurchaseInvoices.FieldHost();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.gridCard = new PurchaseInvoices.CardPanel();
            this.dgvLines = new PurchaseInvoices.ModernGrid();
            this.lblAudit = new System.Windows.Forms.Label();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCard = new PurchaseInvoices.CardPanel();
            this.lblItem = new System.Windows.Forms.Label();
            this.cboItem = new PurchaseInvoices.ModernComboBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.hostRate = new PurchaseInvoices.FieldHost();
            this.txtItemRate = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.hostQty = new PurchaseInvoices.FieldHost();
            this.txtItemQty = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.hostAmount = new PurchaseInvoices.FieldHost();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.topRow = new System.Windows.Forms.Panel();
            this.detailsCard = new PurchaseInvoices.CardPanel();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.hostInvoiceNo = new PurchaseInvoices.FieldHost();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.lblCustomerId = new System.Windows.Forms.Label();
            this.cboCustomer = new PurchaseInvoices.ModernComboBox();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.hostCustomerName = new PurchaseInvoices.FieldHost();
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.hostCustomerAddress = new PurchaseInvoices.FieldHost();
            this.txtCustomerAddress = new System.Windows.Forms.TextBox();
            this.summaryCard = new PurchaseInvoices.SummaryCard();
            this.headerPanel.SuspendLayout();
            this.hostSearch.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            this.addCard.SuspendLayout();
            this.hostRate.SuspendLayout();
            this.hostQty.SuspendLayout();
            this.hostAmount.SuspendLayout();
            this.topRow.SuspendLayout();
            this.detailsCard.SuspendLayout();
            this.hostInvoiceNo.SuspendLayout();
            this.hostCustomerName.SuspendLayout();
            this.hostCustomerAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(940, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 36);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save Invoice";
            this.toolTip.SetToolTip(this.btnSave, "Save this invoice (Ctrl+S)");
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNew.Glyph = "";
            this.btnNew.Location = new System.Drawing.Point(780, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(150, 36);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New Invoice";
            this.toolTip.SetToolTip(this.btnNew, "Start a new invoice (Ctrl+N)");
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrint.Glyph = "";
            this.btnPrint.Location = new System.Drawing.Point(660, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 36);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.toolTip.SetToolTip(this.btnPrint, "Preview and print this invoice (Ctrl+P)");
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnEditInvoice
            // 
            this.btnEditInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditInvoice.BackColor = System.Drawing.Color.Transparent;
            this.btnEditInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditInvoice.Enabled = false;
            this.btnEditInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnEditInvoice.Glyph = "";
            this.btnEditInvoice.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnEditInvoice.Location = new System.Drawing.Point(544, 13);
            this.btnEditInvoice.Name = "btnEditInvoice";
            this.btnEditInvoice.Size = new System.Drawing.Size(92, 30);
            this.btnEditInvoice.TabIndex = 4;
            this.btnEditInvoice.Text = "Edit";
            this.toolTip.SetToolTip(this.btnEditInvoice, "Edit this invoice (Ctrl+E)");
            this.btnEditInvoice.UseVisualStyleBackColor = false;
            this.btnEditInvoice.Click += new System.EventHandler(this.btnEditInvoice_Click);
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteInvoice.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteInvoice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteInvoice.Enabled = false;
            this.btnDeleteInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnDeleteInvoice.Glyph = "";
            this.btnDeleteInvoice.Kind = PurchaseInvoices.ButtonKind.Danger;
            this.btnDeleteInvoice.Location = new System.Drawing.Point(644, 13);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(104, 30);
            this.btnDeleteInvoice.TabIndex = 5;
            this.btnDeleteInvoice.Text = "Delete";
            this.toolTip.SetToolTip(this.btnDeleteInvoice, "Delete this invoice");
            this.btnDeleteInvoice.UseVisualStyleBackColor = false;
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // btnCancelEdit
            // 
            this.btnCancelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCancelEdit.Glyph = "";
            this.btnCancelEdit.Location = new System.Drawing.Point(644, 13);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(104, 30);
            this.btnCancelEdit.TabIndex = 6;
            this.btnCancelEdit.Text = "Cancel";
            this.toolTip.SetToolTip(this.btnCancelEdit, "Discard the changes and stop editing");
            this.btnCancelEdit.UseVisualStyleBackColor = false;
            this.btnCancelEdit.Visible = false;
            this.btnCancelEdit.Click += new System.EventHandler(this.btnCancelEdit_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.BackColor = System.Drawing.Color.Transparent;
            this.btnPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrevious.Glyph = "";
            this.btnPrevious.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnPrevious.Location = new System.Drawing.Point(942, 14);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(44, 36);
            this.btnPrevious.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnPrevious, "Older invoice (Alt+Left)");
            this.btnPrevious.UseVisualStyleBackColor = false;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNext.Glyph = "";
            this.btnNext.GlyphAfterText = true;
            this.btnNext.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnNext.Location = new System.Drawing.Point(992, 14);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(44, 36);
            this.btnNext.TabIndex = 4;
            this.toolTip.SetToolTip(this.btnNext, "Newer invoice (Alt+Right)");
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSearch.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnSearch.Location = new System.Drawing.Point(674, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 36);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.toolTip.SetToolTip(this.btnSearch, "Find an invoice by number (Enter in the search box, Ctrl+F to jump there)");
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            //
            // btnAllInvoices
            //
            this.btnAllInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAllInvoices.BackColor = System.Drawing.Color.Transparent;
            this.btnAllInvoices.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllInvoices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllInvoices.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnAllInvoices.Glyph = "";
            this.btnAllInvoices.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnAllInvoices.Location = new System.Drawing.Point(790, 14);
            this.btnAllInvoices.Name = "btnAllInvoices";
            this.btnAllInvoices.Size = new System.Drawing.Size(140, 36);
            this.btnAllInvoices.TabIndex = 2;
            this.btnAllInvoices.Text = "All invoices";
            this.toolTip.SetToolTip(this.btnAllInvoices, "Browse, search and filter all invoices (Ctrl+L)");
            this.btnAllInvoices.UseVisualStyleBackColor = false;
            this.btnAllInvoices.Click += new System.EventHandler(this.btnAllInvoices_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnAdd.Glyph = "";
            this.btnAdd.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnAdd.Location = new System.Drawing.Point(918, 37);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 36);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add Item";
            this.toolTip.SetToolTip(this.btnAdd, "Add this item to the invoice (Enter in Quantity)");
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCustomers.Glyph = "";
            this.btnCustomers.Location = new System.Drawing.Point(190, 12);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(140, 36);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "Customers";
            this.toolTip.SetToolTip(this.btnCustomers, "Add, edit and remove customers");
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnItems
            // 
            this.btnItems.BackColor = System.Drawing.Color.Transparent;
            this.btnItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnItems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnItems.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnItems.Glyph = "";
            this.btnItems.Location = new System.Drawing.Point(340, 12);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(110, 36);
            this.btnItems.TabIndex = 2;
            this.btnItems.Text = "Items";
            this.toolTip.SetToolTip(this.btnItems, "Add, edit and remove items and prices");
            this.btnItems.UseVisualStyleBackColor = false;
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.Transparent;
            this.btnReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnReports.Glyph = "\uE9F9";
            this.btnReports.Location = new System.Drawing.Point(20, 12);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(160, 36);
            this.btnReports.TabIndex = 0;
            this.btnReports.Text = "Reports";
            this.btnReports.Visible = false;
            this.toolTip.SetToolTip(this.btnReports, "Purchase reports by customer, item and month (Ctrl+R)");
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnAccount.Location = new System.Drawing.Point(1044, 14);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(56, 36);
            this.btnAccount.TabIndex = 5;
            this.toolTip.SetToolTip(this.btnAccount, "Your account");
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // headerPanel
            //             this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.headerPanel.Controls.Add(this.hostSearch);
            this.headerPanel.Controls.Add(this.btnSearch);
            this.headerPanel.Controls.Add(this.btnAllInvoices);
            this.headerPanel.Controls.Add(this.btnPrevious);
            this.headerPanel.Controls.Add(this.btnNext);
            this.headerPanel.Controls.Add(this.btnAccount);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.ForeColor = System.Drawing.Color.White;
            this.headerPanel.ShowLogo = true;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1120, 64);
            this.headerPanel.Subtitle = "Create, find and print purchase invoices";
            this.headerPanel.TabIndex = 0;
            this.headerPanel.Title = "Purchase Invoices";
            // 
            // hostSearch
            // 
            this.hostSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostSearch.BackColor = System.Drawing.Color.Transparent;
            this.hostSearch.Controls.Add(this.txtSearch);
            this.hostSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostSearch.Glyph = "";
            this.hostSearch.Location = new System.Drawing.Point(466, 14);
            this.hostSearch.Name = "hostSearch";
            this.hostSearch.Placeholder = "Search invoice no.";
            this.hostSearch.Size = new System.Drawing.Size(200, 36);
            this.hostSearch.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtSearch.Location = new System.Drawing.Point(36, 9);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(152, 18);
            this.txtSearch.TabIndex = 0;
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.White;
            this.footerPanel.Controls.Add(this.btnReports);
            this.footerPanel.Controls.Add(this.btnCustomers);
            this.footerPanel.Controls.Add(this.btnItems);
            this.footerPanel.Controls.Add(this.btnPrint);
            this.footerPanel.Controls.Add(this.btnNew);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 640);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1120, 60);
            this.footerPanel.TabIndex = 2;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.contentPanel.Controls.Add(this.gridCard);
            this.contentPanel.Controls.Add(this.addCard);
            this.contentPanel.Controls.Add(this.topRow);
            this.contentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentPanel.Location = new System.Drawing.Point(0, 64);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.contentPanel.Size = new System.Drawing.Size(1120, 576);
            this.contentPanel.TabIndex = 1;
            // 
            // gridCard
            // 
            this.gridCard.BackColor = System.Drawing.Color.White;
            this.gridCard.Controls.Add(this.dgvLines);
            this.gridCard.Controls.Add(this.lblAudit);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCard.Glyph = "";
            this.gridCard.Location = new System.Drawing.Point(14, 294);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(1092, 274);
            this.gridCard.TabIndex = 2;
            this.gridCard.Title = "Line items";
            // 
            // lblAudit
            // 
            this.lblAudit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAudit.AutoEllipsis = true;
            this.lblAudit.BackColor = System.Drawing.Color.White;
            this.lblAudit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAudit.ForeColor = System.Drawing.Color.FromArgb(100, 116, 139);
            this.lblAudit.Location = new System.Drawing.Point(352, 16);
            this.lblAudit.Name = "lblAudit";
            this.lblAudit.Size = new System.Drawing.Size(720, 20);
            this.lblAudit.TabIndex = 1;
            this.lblAudit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvLines
            // 
            this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.dgvLines.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLines.BackgroundColor = System.Drawing.Color.White;
            this.dgvLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLines.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLines.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLines.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvLines.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.ItemCode,
            this.Description,
            this.Quantity,
            this.Rate,
            this.Amount});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLines.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLines.EnableHeadersVisualStyles = false;
            this.dgvLines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(246)))));
            this.dgvLines.Location = new System.Drawing.Point(20, 46);
            this.dgvLines.MultiSelect = false;
            this.dgvLines.Name = "dgvLines";
            this.dgvLines.RowHeadersVisible = false;
            this.dgvLines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLines.Size = new System.Drawing.Size(1052, 210);
            this.dgvLines.TabIndex = 0;
            this.dgvLines.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLines_CellEndEdit);
            this.dgvLines.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvLines_CellValidating);
            this.dgvLines.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvLines_UserDeletedRow);
            // 
            // Slno
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Slno.DefaultCellStyle = dataGridViewCellStyle3;
            this.Slno.FillWeight = 7F;
            this.Slno.HeaderText = "#";
            this.Slno.Name = "Slno";
            this.Slno.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ItemCode
            // 
            this.ItemCode.FillWeight = 15F;
            this.ItemCode.HeaderText = "Item code";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            this.Description.FillWeight = 38F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Quantity
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle4;
            this.Quantity.FillWeight = 11F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            this.Quantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Rate
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            this.Rate.DefaultCellStyle = dataGridViewCellStyle5;
            this.Rate.FillWeight = 13F;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Amount
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            dataGridViewCellStyle6.Format = "N2";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle6;
            this.Amount.FillWeight = 16F;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // addCard
            // 
            this.addCard.BackColor = System.Drawing.Color.White;
            this.addCard.Controls.Add(this.lblItem);
            this.addCard.Controls.Add(this.cboItem);
            this.addCard.Controls.Add(this.lblRate);
            this.addCard.Controls.Add(this.hostRate);
            this.addCard.Controls.Add(this.lblQuantity);
            this.addCard.Controls.Add(this.hostQty);
            this.addCard.Controls.Add(this.lblAmount);
            this.addCard.Controls.Add(this.hostAmount);
            this.addCard.Controls.Add(this.btnAdd);
            this.addCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.addCard.Location = new System.Drawing.Point(14, 196);
            this.addCard.Name = "addCard";
            this.addCard.Size = new System.Drawing.Size(1092, 98);
            this.addCard.TabIndex = 1;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblItem.Location = new System.Drawing.Point(24, 17);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(32, 15);
            this.lblItem.TabIndex = 10;
            this.lblItem.Text = "Item";
            // 
            // cboItem
            // 
            this.cboItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboItem.BackColor = System.Drawing.Color.White;
            this.cboItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboItem.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboItem.IntegralHeight = false;
            this.cboItem.Location = new System.Drawing.Point(24, 37);
            this.cboItem.MaxDropDownItems = 10;
            this.cboItem.Name = "cboItem";
            this.cboItem.Placeholder = "Select an item";
            this.cboItem.Size = new System.Drawing.Size(430, 36);
            this.cboItem.TabIndex = 0;
            this.cboItem.SelectedIndexChanged += new System.EventHandler(this.cboItem_SelectedIndexChanged);
            // 
            // lblRate
            // 
            this.lblRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblRate.Location = new System.Drawing.Point(470, 17);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(30, 15);
            this.lblRate.TabIndex = 11;
            this.lblRate.Text = "Rate";
            // 
            // hostRate
            // 
            this.hostRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostRate.BackColor = System.Drawing.Color.Transparent;
            this.hostRate.Controls.Add(this.txtItemRate);
            this.hostRate.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostRate.Location = new System.Drawing.Point(470, 37);
            this.hostRate.Name = "hostRate";
            this.hostRate.Size = new System.Drawing.Size(130, 36);
            this.hostRate.TabIndex = 1;
            // 
            // txtItemRate
            // 
            this.txtItemRate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtItemRate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemRate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtItemRate.Location = new System.Drawing.Point(12, 9);
            this.txtItemRate.Name = "txtItemRate";
            this.txtItemRate.ReadOnly = true;
            this.txtItemRate.Size = new System.Drawing.Size(106, 18);
            this.txtItemRate.TabIndex = 0;
            this.txtItemRate.TabStop = false;
            this.txtItemRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblQuantity.Location = new System.Drawing.Point(616, 17);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(53, 15);
            this.lblQuantity.TabIndex = 12;
            this.lblQuantity.Text = "Quantity";
            // 
            // hostQty
            // 
            this.hostQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostQty.BackColor = System.Drawing.Color.Transparent;
            this.hostQty.Controls.Add(this.txtItemQty);
            this.hostQty.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostQty.Location = new System.Drawing.Point(616, 37);
            this.hostQty.Name = "hostQty";
            this.hostQty.Size = new System.Drawing.Size(120, 36);
            this.hostQty.TabIndex = 2;
            // 
            // txtItemQty
            // 
            this.txtItemQty.BackColor = System.Drawing.Color.White;
            this.txtItemQty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtItemQty.Location = new System.Drawing.Point(12, 9);
            this.txtItemQty.MaxLength = 6;
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Size = new System.Drawing.Size(96, 18);
            this.txtItemQty.TabIndex = 0;
            this.txtItemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtItemQty.TextChanged += new System.EventHandler(this.txtItemQty_TextChanged);
            this.txtItemQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemQty_KeyPress);
            // 
            // lblAmount
            // 
            this.lblAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAmount.Location = new System.Drawing.Point(752, 17);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(51, 15);
            this.lblAmount.TabIndex = 13;
            this.lblAmount.Text = "Amount";
            // 
            // hostAmount
            // 
            this.hostAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostAmount.BackColor = System.Drawing.Color.Transparent;
            this.hostAmount.Controls.Add(this.txtAmount);
            this.hostAmount.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostAmount.Location = new System.Drawing.Point(752, 37);
            this.hostAmount.Name = "hostAmount";
            this.hostAmount.Size = new System.Drawing.Size(150, 36);
            this.hostAmount.TabIndex = 3;
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtAmount.Location = new System.Drawing.Point(12, 9);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(126, 18);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.TabStop = false;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // topRow
            // 
            this.topRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.topRow.Controls.Add(this.detailsCard);
            this.topRow.Controls.Add(this.summaryCard);
            this.topRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.topRow.Location = new System.Drawing.Point(14, 8);
            this.topRow.Name = "topRow";
            this.topRow.Size = new System.Drawing.Size(1092, 188);
            this.topRow.TabIndex = 0;
            // 
            // detailsCard
            // 
            this.detailsCard.BackColor = System.Drawing.Color.White;
            this.detailsCard.Controls.Add(this.lblInvoiceNo);
            this.detailsCard.Controls.Add(this.hostInvoiceNo);
            this.detailsCard.Controls.Add(this.lblCustomerId);
            this.detailsCard.Controls.Add(this.cboCustomer);
            this.detailsCard.Controls.Add(this.lblCustomerName);
            this.detailsCard.Controls.Add(this.hostCustomerName);
            this.detailsCard.Controls.Add(this.lblAddress);
            this.detailsCard.Controls.Add(this.hostCustomerAddress);
            this.detailsCard.Controls.Add(this.btnEditInvoice);
            this.detailsCard.Controls.Add(this.btnDeleteInvoice);
            this.detailsCard.Controls.Add(this.btnCancelEdit);
            this.detailsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailsCard.Glyph = "";
            this.detailsCard.Location = new System.Drawing.Point(0, 0);
            this.detailsCard.Name = "detailsCard";
            this.detailsCard.Size = new System.Drawing.Size(772, 188);
            this.detailsCard.TabIndex = 0;
            this.detailsCard.Title = "Invoice details";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblInvoiceNo.Location = new System.Drawing.Point(24, 48);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(66, 15);
            this.lblInvoiceNo.TabIndex = 10;
            this.lblInvoiceNo.Text = "Invoice no.";
            // 
            // hostInvoiceNo
            // 
            this.hostInvoiceNo.BackColor = System.Drawing.Color.Transparent;
            this.hostInvoiceNo.Controls.Add(this.txtInvoiceNo);
            this.hostInvoiceNo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostInvoiceNo.Location = new System.Drawing.Point(24, 68);
            this.hostInvoiceNo.Name = "hostInvoiceNo";
            this.hostInvoiceNo.Size = new System.Drawing.Size(144, 36);
            this.hostInvoiceNo.TabIndex = 0;
            // 
            // txtInvoiceNo
            // 
            this.txtInvoiceNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInvoiceNo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtInvoiceNo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtInvoiceNo.Location = new System.Drawing.Point(12, 9);
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            this.txtInvoiceNo.ReadOnly = true;
            this.txtInvoiceNo.Size = new System.Drawing.Size(120, 18);
            this.txtInvoiceNo.TabIndex = 0;
            this.txtInvoiceNo.TabStop = false;
            // 
            // lblCustomerId
            // 
            this.lblCustomerId.AutoSize = true;
            this.lblCustomerId.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCustomerId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustomerId.Location = new System.Drawing.Point(184, 48);
            this.lblCustomerId.Name = "lblCustomerId";
            this.lblCustomerId.Size = new System.Drawing.Size(74, 15);
            this.lblCustomerId.TabIndex = 11;
            this.lblCustomerId.Text = "Customer ID";
            // 
            // cboCustomer
            // 
            this.cboCustomer.BackColor = System.Drawing.Color.White;
            this.cboCustomer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCustomer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.cboCustomer.IntegralHeight = false;
            this.cboCustomer.Location = new System.Drawing.Point(184, 68);
            this.cboCustomer.MaxDropDownItems = 10;
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Placeholder = "Select customer";
            this.cboCustomer.ShowDescriptionInFace = false;
            this.cboCustomer.Size = new System.Drawing.Size(204, 36);
            this.cboCustomer.TabIndex = 1;
            this.cboCustomer.SelectedIndexChanged += new System.EventHandler(this.cboCustomer_SelectedIndexChanged);
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblCustomerName.Location = new System.Drawing.Point(404, 48);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(91, 15);
            this.lblCustomerName.TabIndex = 12;
            this.lblCustomerName.Text = "Customer name";
            // 
            // hostCustomerName
            // 
            this.hostCustomerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostCustomerName.BackColor = System.Drawing.Color.Transparent;
            this.hostCustomerName.Controls.Add(this.txtCustomerName);
            this.hostCustomerName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostCustomerName.Location = new System.Drawing.Point(404, 68);
            this.hostCustomerName.Name = "hostCustomerName";
            this.hostCustomerName.Size = new System.Drawing.Size(344, 36);
            this.hostCustomerName.TabIndex = 2;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtCustomerName.Location = new System.Drawing.Point(12, 9);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.ReadOnly = true;
            this.txtCustomerName.Size = new System.Drawing.Size(320, 18);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.TabStop = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblAddress.Location = new System.Drawing.Point(24, 112);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(49, 15);
            this.lblAddress.TabIndex = 13;
            this.lblAddress.Text = "Address";
            // 
            // hostCustomerAddress
            // 
            this.hostCustomerAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostCustomerAddress.BackColor = System.Drawing.Color.Transparent;
            this.hostCustomerAddress.Controls.Add(this.txtCustomerAddress);
            this.hostCustomerAddress.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.hostCustomerAddress.Location = new System.Drawing.Point(24, 132);
            this.hostCustomerAddress.Name = "hostCustomerAddress";
            this.hostCustomerAddress.Size = new System.Drawing.Size(724, 38);
            this.hostCustomerAddress.TabIndex = 3;
            // 
            // txtCustomerAddress
            // 
            this.txtCustomerAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(251)))));
            this.txtCustomerAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCustomerAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCustomerAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.txtCustomerAddress.Location = new System.Drawing.Point(12, 8);
            this.txtCustomerAddress.Multiline = true;
            this.txtCustomerAddress.Name = "txtCustomerAddress";
            this.txtCustomerAddress.ReadOnly = true;
            this.txtCustomerAddress.Size = new System.Drawing.Size(700, 24);
            this.txtCustomerAddress.TabIndex = 0;
            this.txtCustomerAddress.TabStop = false;
            // 
            // summaryCard
            // 
            this.summaryCard.CustomerSource = this.txtCustomerName;
            this.summaryCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.summaryCard.InvoiceSource = this.txtInvoiceNo;
            this.summaryCard.ItemsSource = this.dgvLines;
            this.summaryCard.Location = new System.Drawing.Point(772, 0);
            this.summaryCard.ModeSource = this.btnSave;
            this.summaryCard.Name = "summaryCard";
            this.summaryCard.Size = new System.Drawing.Size(320, 188);
            this.summaryCard.TabIndex = 1;
            this.summaryCard.TabStop = false;
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Purchase Invoices";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InvoiceForm_FormClosing);
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.hostSearch.ResumeLayout(false);
            this.hostSearch.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.contentPanel.ResumeLayout(false);
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            this.addCard.ResumeLayout(false);
            this.addCard.PerformLayout();
            this.hostRate.ResumeLayout(false);
            this.hostRate.PerformLayout();
            this.hostQty.ResumeLayout(false);
            this.hostQty.PerformLayout();
            this.hostAmount.ResumeLayout(false);
            this.hostAmount.PerformLayout();
            this.topRow.ResumeLayout(false);
            this.detailsCard.ResumeLayout(false);
            this.detailsCard.PerformLayout();
            this.hostInvoiceNo.ResumeLayout(false);
            this.hostInvoiceNo.PerformLayout();
            this.hostCustomerName.ResumeLayout(false);
            this.hostCustomerName.PerformLayout();
            this.hostCustomerAddress.ResumeLayout(false);
            this.hostCustomerAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FieldHost hostSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private PurchaseInvoices.ModernButton btnSearch;
        private PurchaseInvoices.ModernButton btnAllInvoices;
        private PurchaseInvoices.ModernButton btnPrevious;
        private PurchaseInvoices.ModernButton btnNext;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnReports;
        private PurchaseInvoices.AccountButton btnAccount;
        private System.Windows.Forms.Label lblAudit;
        private PurchaseInvoices.ModernButton btnCustomers;
        private PurchaseInvoices.ModernButton btnItems;
        private PurchaseInvoices.ModernButton btnPrint;
        private PurchaseInvoices.ModernButton btnNew;
        private PurchaseInvoices.ModernButton btnSave;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel gridCard;
        private PurchaseInvoices.ModernGrid dgvLines;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private PurchaseInvoices.CardPanel addCard;
        private System.Windows.Forms.Label lblItem;
        private PurchaseInvoices.ModernComboBox cboItem;
        private System.Windows.Forms.Label lblRate;
        private PurchaseInvoices.FieldHost hostRate;
        private System.Windows.Forms.TextBox txtItemRate;
        private System.Windows.Forms.Label lblQuantity;
        private PurchaseInvoices.FieldHost hostQty;
        private System.Windows.Forms.TextBox txtItemQty;
        private System.Windows.Forms.Label lblAmount;
        private PurchaseInvoices.FieldHost hostAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private PurchaseInvoices.ModernButton btnAdd;
        private System.Windows.Forms.Panel topRow;
        private PurchaseInvoices.CardPanel detailsCard;
        private PurchaseInvoices.ModernButton btnEditInvoice;
        private PurchaseInvoices.ModernButton btnDeleteInvoice;
        private PurchaseInvoices.ModernButton btnCancelEdit;
        private System.Windows.Forms.Label lblInvoiceNo;
        private PurchaseInvoices.FieldHost hostInvoiceNo;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label lblCustomerId;
        private PurchaseInvoices.ModernComboBox cboCustomer;
        private System.Windows.Forms.Label lblCustomerName;
        private PurchaseInvoices.FieldHost hostCustomerName;
        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.Label lblAddress;
        private PurchaseInvoices.FieldHost hostCustomerAddress;
        private System.Windows.Forms.TextBox txtCustomerAddress;
        private PurchaseInvoices.SummaryCard summaryCard;
    }
}
