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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.headerPanel = new PurchaseInvoices.HeaderPanel();
            this.hostSearch = new PurchaseInvoices.FieldHost();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.button1 = new PurchaseInvoices.ModernButton();
            this.btnPrevious = new PurchaseInvoices.ModernButton();
            this.btnNext = new PurchaseInvoices.ModernButton();
            this.footerPanel = new PurchaseInvoices.FooterPanel();
            this.btnCreateTables = new PurchaseInvoices.ModernButton();
            this.btnCustomers = new PurchaseInvoices.ModernButton();
            this.btnItems = new PurchaseInvoices.ModernButton();
            this.btnPrint = new PurchaseInvoices.ModernButton();
            this.btnNew = new PurchaseInvoices.ModernButton();
            this.btnSave = new PurchaseInvoices.ModernButton();
            this.txtTotalSum = new System.Windows.Forms.TextBox();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.gridCard = new PurchaseInvoices.CardPanel();
            this.dgv_sales = new PurchaseInvoices.ModernGrid();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addCard = new PurchaseInvoices.CardPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.combo_Itemcode = new PurchaseInvoices.ModernComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.hostRate = new PurchaseInvoices.FieldHost();
            this.txtItemRate = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.hostQty = new PurchaseInvoices.FieldHost();
            this.txtItemQty = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.hostAmount = new PurchaseInvoices.FieldHost();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnAdd = new PurchaseInvoices.ModernButton();
            this.topRow = new System.Windows.Forms.Panel();
            this.detailsCard = new PurchaseInvoices.CardPanel();
            this.btnEditInvoice = new PurchaseInvoices.ModernButton();
            this.btnDeleteInvoice = new PurchaseInvoices.ModernButton();
            this.btnCancelEdit = new PurchaseInvoices.ModernButton();
            this.label1 = new System.Windows.Forms.Label();
            this.hostTransNo = new PurchaseInvoices.FieldHost();
            this.txtTransNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboCustId = new PurchaseInvoices.ModernComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostCustName = new PurchaseInvoices.FieldHost();
            this.txtcustname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.hostCustAddress = new PurchaseInvoices.FieldHost();
            this.txtcustaddresss = new System.Windows.Forms.TextBox();
            this.summaryCard = new PurchaseInvoices.SummaryCard();
            this.headerPanel.SuspendLayout();
            this.hostSearch.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sales)).BeginInit();
            this.addCard.SuspendLayout();
            this.hostRate.SuspendLayout();
            this.hostQty.SuspendLayout();
            this.hostAmount.SuspendLayout();
            this.topRow.SuspendLayout();
            this.detailsCard.SuspendLayout();
            this.hostTransNo.SuspendLayout();
            this.hostCustName.SuspendLayout();
            this.hostCustAddress.SuspendLayout();
            this.SuspendLayout();
            //
            // headerPanel
            //
            this.headerPanel.Controls.Add(this.hostSearch);
            this.headerPanel.Controls.Add(this.button1);
            this.headerPanel.Controls.Add(this.btnPrevious);
            this.headerPanel.Controls.Add(this.btnNext);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Glyph = "";
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
            this.hostSearch.Controls.Add(this.txtSearch);
            this.hostSearch.Glyph = "";
            this.hostSearch.Location = new System.Drawing.Point(548, 14);
            this.hostSearch.Name = "hostSearch";
            this.hostSearch.Placeholder = "Search invoice no.";
            this.hostSearch.Size = new System.Drawing.Size(200, 36);
            this.hostSearch.TabIndex = 0;
            //
            // txtSearch
            //
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(36, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(152, 18);
            this.txtSearch.TabIndex = 0;
            //
            // button1
            //
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.button1.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.button1.Location = new System.Drawing.Point(756, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            //
            // btnPrevious
            //
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrevious.Glyph = "";
            this.btnPrevious.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnPrevious.Location = new System.Drawing.Point(872, 14);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(116, 36);
            this.btnPrevious.TabIndex = 2;
            this.btnPrevious.Text = "Previous";
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            //
            // btnNext
            //
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNext.Glyph = "";
            this.btnNext.GlyphAfterText = true;
            this.btnNext.Kind = PurchaseInvoices.ButtonKind.Ghost;
            this.btnNext.Location = new System.Drawing.Point(996, 14);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(104, 36);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "Next";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            //
            // footerPanel
            //
            this.footerPanel.Controls.Add(this.btnCreateTables);
            this.footerPanel.Controls.Add(this.btnCustomers);
            this.footerPanel.Controls.Add(this.btnItems);
            this.footerPanel.Controls.Add(this.btnPrint);
            this.footerPanel.Controls.Add(this.btnNew);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Controls.Add(this.txtTotalSum);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.footerPanel.Location = new System.Drawing.Point(0, 640);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(1120, 60);
            this.footerPanel.TabIndex = 2;
            //
            // btnCreateTables
            //
            this.btnCreateTables.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCreateTables.Glyph = "";
            this.btnCreateTables.Location = new System.Drawing.Point(20, 12);
            this.btnCreateTables.Name = "btnCreateTables";
            this.btnCreateTables.Size = new System.Drawing.Size(160, 36);
            this.btnCreateTables.TabIndex = 0;
            this.btnCreateTables.Text = "Create Tables";
            this.btnCreateTables.Click += new System.EventHandler(this.btnCreateTables_Click);
            //
            // btnCustomers
            //
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCustomers.Glyph = "";
            this.btnCustomers.Location = new System.Drawing.Point(190, 12);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(140, 36);
            this.btnCustomers.TabIndex = 1;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            //
            // btnItems
            //
            this.btnItems.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnItems.Glyph = "";
            this.btnItems.Location = new System.Drawing.Point(340, 12);
            this.btnItems.Name = "btnItems";
            this.btnItems.Size = new System.Drawing.Size(110, 36);
            this.btnItems.TabIndex = 2;
            this.btnItems.Text = "Items";
            this.btnItems.Click += new System.EventHandler(this.btnItems_Click);
            //
            // btnPrint
            //
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnPrint.Glyph = "";
            this.btnPrint.Location = new System.Drawing.Point(660, 12);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(110, 36);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            //
            // btnNew
            //
            this.btnNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNew.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnNew.Glyph = "";
            this.btnNew.Location = new System.Drawing.Point(780, 12);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(150, 36);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "New Invoice";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // btnSave
            //
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnSave.Glyph = "";
            this.btnSave.Kind = PurchaseInvoices.ButtonKind.Primary;
            this.btnSave.Location = new System.Drawing.Point(940, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(160, 36);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save Invoice";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //
            // txtTotalSum
            //
            this.txtTotalSum.Location = new System.Drawing.Point(400, 20);
            this.txtTotalSum.Name = "txtTotalSum";
            this.txtTotalSum.ReadOnly = true;
            this.txtTotalSum.Size = new System.Drawing.Size(100, 25);
            this.txtTotalSum.TabIndex = 6;
            this.txtTotalSum.TabStop = false;
            this.txtTotalSum.Visible = false;
            //
            // contentPanel
            //
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
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
            this.gridCard.Controls.Add(this.dgv_sales);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCard.Glyph = "";
            this.gridCard.Location = new System.Drawing.Point(14, 294);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(1092, 274);
            this.gridCard.TabIndex = 2;
            this.gridCard.Title = "Line items";
            //
            // dgv_sales
            //
            this.dgv_sales.AllowUserToAddRows = false;
            this.dgv_sales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_sales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.ItemCode,
            this.Description,
            this.Quantity,
            this.Rate,
            this.Amount});
            this.dgv_sales.Location = new System.Drawing.Point(20, 46);
            this.dgv_sales.Name = "dgv_sales";
            this.dgv_sales.Size = new System.Drawing.Size(1052, 210);
            this.dgv_sales.TabIndex = 0;
            this.dgv_sales.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_sales_CellEndEdit);
            this.dgv_sales.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgv_sales_CellValidating);
            this.dgv_sales.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_sales_UserDeletedRow);
            //
            // Slno
            //
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Slno.DefaultCellStyle = dataGridViewCellStyle1;
            this.Slno.FillWeight = 7F;
            this.Slno.HeaderText = "#";
            this.Slno.Name = "Slno";
            //
            // ItemCode
            //
            this.ItemCode.FillWeight = 15F;
            this.ItemCode.HeaderText = "Item code";
            this.ItemCode.Name = "ItemCode";
            //
            // Description
            //
            this.Description.FillWeight = 38F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            //
            // Quantity
            //
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle2;
            this.Quantity.FillWeight = 11F;
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            //
            // Rate
            //
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            this.Rate.DefaultCellStyle = dataGridViewCellStyle3;
            this.Rate.FillWeight = 13F;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            //
            // Amount
            //
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            dataGridViewCellStyle4.Format = "N2";
            this.Amount.DefaultCellStyle = dataGridViewCellStyle4;
            this.Amount.FillWeight = 16F;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            //
            // addCard
            //
            this.addCard.Controls.Add(this.label4);
            this.addCard.Controls.Add(this.combo_Itemcode);
            this.addCard.Controls.Add(this.label5);
            this.addCard.Controls.Add(this.hostRate);
            this.addCard.Controls.Add(this.label6);
            this.addCard.Controls.Add(this.hostQty);
            this.addCard.Controls.Add(this.label7);
            this.addCard.Controls.Add(this.hostAmount);
            this.addCard.Controls.Add(this.btnAdd);
            this.addCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.addCard.Location = new System.Drawing.Point(14, 196);
            this.addCard.Name = "addCard";
            this.addCard.Size = new System.Drawing.Size(1092, 98);
            this.addCard.TabIndex = 1;
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label4.Location = new System.Drawing.Point(24, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Item";
            //
            // combo_Itemcode
            //
            this.combo_Itemcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_Itemcode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.combo_Itemcode.Location = new System.Drawing.Point(24, 37);
            this.combo_Itemcode.Name = "combo_Itemcode";
            this.combo_Itemcode.Placeholder = "Select an item";
            this.combo_Itemcode.Size = new System.Drawing.Size(430, 36);
            this.combo_Itemcode.TabIndex = 0;
            this.combo_Itemcode.SelectedIndexChanged += new System.EventHandler(this.combo_Itemcode_SelectedIndexChanged);
            //
            // label5
            //
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label5.Location = new System.Drawing.Point(470, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Rate";
            //
            // hostRate
            //
            this.hostRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostRate.Controls.Add(this.txtItemRate);
            this.hostRate.Location = new System.Drawing.Point(470, 37);
            this.hostRate.Name = "hostRate";
            this.hostRate.Size = new System.Drawing.Size(130, 36);
            this.hostRate.TabIndex = 1;
            //
            // txtItemRate
            //
            this.txtItemRate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemRate.Location = new System.Drawing.Point(12, 8);
            this.txtItemRate.Name = "txtItemRate";
            this.txtItemRate.ReadOnly = true;
            this.txtItemRate.Size = new System.Drawing.Size(106, 18);
            this.txtItemRate.TabIndex = 0;
            this.txtItemRate.TabStop = false;
            this.txtItemRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // label6
            //
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label6.Location = new System.Drawing.Point(616, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 15);
            this.label6.TabIndex = 12;
            this.label6.Text = "Quantity";
            //
            // hostQty
            //
            this.hostQty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostQty.Controls.Add(this.txtItemQty);
            this.hostQty.Location = new System.Drawing.Point(616, 37);
            this.hostQty.Name = "hostQty";
            this.hostQty.Size = new System.Drawing.Size(120, 36);
            this.hostQty.TabIndex = 2;
            //
            // txtItemQty
            //
            this.txtItemQty.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtItemQty.Location = new System.Drawing.Point(12, 8);
            this.txtItemQty.MaxLength = 6;
            this.txtItemQty.Name = "txtItemQty";
            this.txtItemQty.Size = new System.Drawing.Size(96, 18);
            this.txtItemQty.TabIndex = 0;
            this.txtItemQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtItemQty.TextChanged += new System.EventHandler(this.txtItemQty_TextChanged);
            this.txtItemQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtItemOrderQty);
            //
            // label7
            //
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label7.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label7.Location = new System.Drawing.Point(752, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Amount";
            //
            // hostAmount
            //
            this.hostAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hostAmount.Controls.Add(this.txtAmount);
            this.hostAmount.Location = new System.Drawing.Point(752, 37);
            this.hostAmount.Name = "hostAmount";
            this.hostAmount.Size = new System.Drawing.Size(150, 36);
            this.hostAmount.TabIndex = 3;
            //
            // txtAmount
            //
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtAmount.Location = new System.Drawing.Point(12, 8);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(126, 18);
            this.txtAmount.TabIndex = 0;
            this.txtAmount.TabStop = false;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // btnAdd
            //
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnAdd.Glyph = "";
            this.btnAdd.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnAdd.Location = new System.Drawing.Point(918, 37);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(150, 36);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add Item";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            //
            // topRow
            //
            this.topRow.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
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
            this.detailsCard.Controls.Add(this.label1);
            this.detailsCard.Controls.Add(this.hostTransNo);
            this.detailsCard.Controls.Add(this.label9);
            this.detailsCard.Controls.Add(this.comboCustId);
            this.detailsCard.Controls.Add(this.label2);
            this.detailsCard.Controls.Add(this.hostCustName);
            this.detailsCard.Controls.Add(this.label3);
            this.detailsCard.Controls.Add(this.hostCustAddress);
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
            // btnEditInvoice
            //
            this.btnEditInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditInvoice.Enabled = false;
            this.btnEditInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnEditInvoice.Glyph = "";
            this.btnEditInvoice.Kind = PurchaseInvoices.ButtonKind.Soft;
            this.btnEditInvoice.Location = new System.Drawing.Point(544, 13);
            this.btnEditInvoice.Name = "btnEditInvoice";
            this.btnEditInvoice.Size = new System.Drawing.Size(92, 30);
            this.btnEditInvoice.TabIndex = 4;
            this.btnEditInvoice.Text = "Edit";
            this.btnEditInvoice.Click += new System.EventHandler(this.btnEditInvoice_Click);
            //
            // btnDeleteInvoice
            //
            this.btnDeleteInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteInvoice.Enabled = false;
            this.btnDeleteInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnDeleteInvoice.Glyph = "";
            this.btnDeleteInvoice.Kind = PurchaseInvoices.ButtonKind.Danger;
            this.btnDeleteInvoice.Location = new System.Drawing.Point(644, 13);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(104, 30);
            this.btnDeleteInvoice.TabIndex = 5;
            this.btnDeleteInvoice.Text = "Delete";
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            //
            // btnCancelEdit
            //
            this.btnCancelEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.btnCancelEdit.Glyph = "";
            this.btnCancelEdit.Location = new System.Drawing.Point(644, 13);
            this.btnCancelEdit.Name = "btnCancelEdit";
            this.btnCancelEdit.Size = new System.Drawing.Size(104, 30);
            this.btnCancelEdit.TabIndex = 6;
            this.btnCancelEdit.Text = "Cancel";
            this.btnCancelEdit.Visible = false;
            this.btnCancelEdit.Click += new System.EventHandler(this.btnCancelEdit_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label1.Location = new System.Drawing.Point(24, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Invoice no.";
            //
            // hostTransNo
            //
            this.hostTransNo.Controls.Add(this.txtTransNo);
            this.hostTransNo.Location = new System.Drawing.Point(24, 68);
            this.hostTransNo.Name = "hostTransNo";
            this.hostTransNo.Size = new System.Drawing.Size(144, 36);
            this.hostTransNo.TabIndex = 0;
            //
            // txtTransNo
            //
            this.txtTransNo.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtTransNo.Location = new System.Drawing.Point(12, 8);
            this.txtTransNo.Name = "txtTransNo";
            this.txtTransNo.ReadOnly = true;
            this.txtTransNo.Size = new System.Drawing.Size(120, 18);
            this.txtTransNo.TabIndex = 0;
            this.txtTransNo.TabStop = false;
            //
            // label9
            //
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label9.Location = new System.Drawing.Point(184, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 15);
            this.label9.TabIndex = 11;
            this.label9.Text = "Customer ID";
            //
            // comboCustId
            //
            this.comboCustId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboCustId.Location = new System.Drawing.Point(184, 68);
            this.comboCustId.Name = "comboCustId";
            this.comboCustId.Placeholder = "Select customer";
            this.comboCustId.ShowDescriptionInFace = false;
            this.comboCustId.Size = new System.Drawing.Size(204, 36);
            this.comboCustId.TabIndex = 1;
            this.comboCustId.SelectedIndexChanged += new System.EventHandler(this.comboCustId_SelectedIndexChanged);
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label2.Location = new System.Drawing.Point(404, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Customer name";
            //
            // hostCustName
            //
            this.hostCustName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostCustName.Controls.Add(this.txtcustname);
            this.hostCustName.Location = new System.Drawing.Point(404, 68);
            this.hostCustName.Name = "hostCustName";
            this.hostCustName.Size = new System.Drawing.Size(344, 36);
            this.hostCustName.TabIndex = 2;
            //
            // txtcustname
            //
            this.txtcustname.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtcustname.Location = new System.Drawing.Point(12, 8);
            this.txtcustname.Name = "txtcustname";
            this.txtcustname.ReadOnly = true;
            this.txtcustname.Size = new System.Drawing.Size(320, 18);
            this.txtcustname.TabIndex = 0;
            this.txtcustname.TabStop = false;
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(71, 85, 105);
            this.label3.Location = new System.Drawing.Point(24, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Address";
            //
            // hostCustAddress
            //
            this.hostCustAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hostCustAddress.Controls.Add(this.txtcustaddresss);
            this.hostCustAddress.Location = new System.Drawing.Point(24, 132);
            this.hostCustAddress.Name = "hostCustAddress";
            this.hostCustAddress.Size = new System.Drawing.Size(724, 38);
            this.hostCustAddress.TabIndex = 3;
            //
            // txtcustaddresss
            //
            this.txtcustaddresss.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtcustaddresss.Location = new System.Drawing.Point(12, 8);
            this.txtcustaddresss.Multiline = true;
            this.txtcustaddresss.Name = "txtcustaddresss";
            this.txtcustaddresss.ReadOnly = true;
            this.txtcustaddresss.Size = new System.Drawing.Size(700, 24);
            this.txtcustaddresss.TabIndex = 0;
            this.txtcustaddresss.TabStop = false;
            //
            // summaryCard
            //
            this.summaryCard.CustomerSource = this.txtcustname;
            this.summaryCard.Dock = System.Windows.Forms.DockStyle.Right;
            this.summaryCard.InvoiceSource = this.txtTransNo;
            this.summaryCard.ItemsSource = this.dgv_sales;
            this.summaryCard.Location = new System.Drawing.Point(772, 0);
            this.summaryCard.ModeSource = this.btnSave;
            this.summaryCard.Name = "summaryCard";
            this.summaryCard.Size = new System.Drawing.Size(320, 188);
            this.summaryCard.TabIndex = 1;
            this.summaryCard.TotalSource = this.txtTotalSum;
            //
            // InvoiceForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(243, 245, 250);
            this.ClientSize = new System.Drawing.Size(1120, 700);
            this.Controls.Add(this.contentPanel);
            this.Controls.Add(this.footerPanel);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Purchase Invoices";
            this.toolTip.SetToolTip(this.btnSave, "Save this invoice (Ctrl+S)");
            this.toolTip.SetToolTip(this.btnNew, "Start a new invoice (Ctrl+N)");
            this.toolTip.SetToolTip(this.btnPrint, "Preview and print this invoice (Ctrl+P)");
            this.toolTip.SetToolTip(this.btnEditInvoice, "Edit this invoice (Ctrl+E)");
            this.toolTip.SetToolTip(this.btnDeleteInvoice, "Delete this invoice");
            this.toolTip.SetToolTip(this.btnCancelEdit, "Discard the changes and stop editing");
            this.toolTip.SetToolTip(this.btnPrevious, "Older invoice (Alt+Left)");
            this.toolTip.SetToolTip(this.btnNext, "Newer invoice (Alt+Right)");
            this.toolTip.SetToolTip(this.button1, "Find an invoice by number (Enter in the search box, Ctrl+F to jump there)");
            this.toolTip.SetToolTip(this.btnAdd, "Add this item to the invoice (Enter in Quantity)");
            this.toolTip.SetToolTip(this.btnCustomers, "Add, edit and remove customers");
            this.toolTip.SetToolTip(this.btnItems, "Add, edit and remove items and prices");
            this.toolTip.SetToolTip(this.btnCreateTables, "Create the database tables if any are missing");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InvoiceForm_FormClosing);
            this.Load += new System.EventHandler(this.InvoiceForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.hostSearch.ResumeLayout(false);
            this.hostSearch.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_sales)).EndInit();
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
            this.hostTransNo.ResumeLayout(false);
            this.hostTransNo.PerformLayout();
            this.hostCustName.ResumeLayout(false);
            this.hostCustName.PerformLayout();
            this.hostCustAddress.ResumeLayout(false);
            this.hostCustAddress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private PurchaseInvoices.HeaderPanel headerPanel;
        private PurchaseInvoices.FieldHost hostSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private PurchaseInvoices.ModernButton button1;
        private PurchaseInvoices.ModernButton btnPrevious;
        private PurchaseInvoices.ModernButton btnNext;
        private PurchaseInvoices.FooterPanel footerPanel;
        private PurchaseInvoices.ModernButton btnCreateTables;
        private PurchaseInvoices.ModernButton btnCustomers;
        private PurchaseInvoices.ModernButton btnItems;
        private PurchaseInvoices.ModernButton btnPrint;
        private PurchaseInvoices.ModernButton btnNew;
        private PurchaseInvoices.ModernButton btnSave;
        private System.Windows.Forms.TextBox txtTotalSum;
        private System.Windows.Forms.Panel contentPanel;
        private PurchaseInvoices.CardPanel gridCard;
        private PurchaseInvoices.ModernGrid dgv_sales;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private PurchaseInvoices.CardPanel addCard;
        private System.Windows.Forms.Label label4;
        private PurchaseInvoices.ModernComboBox combo_Itemcode;
        private System.Windows.Forms.Label label5;
        private PurchaseInvoices.FieldHost hostRate;
        private System.Windows.Forms.TextBox txtItemRate;
        private System.Windows.Forms.Label label6;
        private PurchaseInvoices.FieldHost hostQty;
        private System.Windows.Forms.TextBox txtItemQty;
        private System.Windows.Forms.Label label7;
        private PurchaseInvoices.FieldHost hostAmount;
        private System.Windows.Forms.TextBox txtAmount;
        private PurchaseInvoices.ModernButton btnAdd;
        private System.Windows.Forms.Panel topRow;
        private PurchaseInvoices.CardPanel detailsCard;
        private PurchaseInvoices.ModernButton btnEditInvoice;
        private PurchaseInvoices.ModernButton btnDeleteInvoice;
        private PurchaseInvoices.ModernButton btnCancelEdit;
        private System.Windows.Forms.Label label1;
        private PurchaseInvoices.FieldHost hostTransNo;
        private System.Windows.Forms.TextBox txtTransNo;
        private System.Windows.Forms.Label label9;
        private PurchaseInvoices.ModernComboBox comboCustId;
        private System.Windows.Forms.Label label2;
        private PurchaseInvoices.FieldHost hostCustName;
        private System.Windows.Forms.TextBox txtcustname;
        private System.Windows.Forms.Label label3;
        private PurchaseInvoices.FieldHost hostCustAddress;
        private System.Windows.Forms.TextBox txtcustaddresss;
        private PurchaseInvoices.SummaryCard summaryCard;
    }
}
