using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace PurchaseInvoices
{
    public partial class InvoiceForm : Form
    {
        private InvoiceRepository db;
        private DateTime? invoiceDateValue;
        private string invoiceTotalValue = string.Empty;
        private int? currentTransId;
        private int? editingTransId;
        private string editSnapshot = string.Empty;

        public InvoiceForm()
        {
            InitializeComponent();
            Theme.StyleWindow(this);
            cboCustomer.DropDownWidth = Theme.S(380);
        }

        private DateTime? InvoiceDate
        {
            get { return invoiceDateValue; }
            set
            {
                invoiceDateValue = value;
                summaryCard.DateText = value?.ToString("dd MMM yyyy", CultureInfo.CurrentCulture) ?? string.Empty;
            }
        }

        private string InvoiceTotal
        {
            get { return invoiceTotalValue; }
            set
            {
                invoiceTotalValue = value ?? string.Empty;
                summaryCard.TotalText = invoiceTotalValue;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    if (ActiveControl == txtItemQty)
                    {
                        int before = dgvLines.Rows.Count;
                        btnAdd.PerformClick();
                        if (dgvLines.Rows.Count > before)
                        {
                            cboItem.Focus();
                        }
                        return true;
                    }
                    if (ActiveControl == txtSearch)
                    {
                        btnSearch.PerformClick();
                        return true;
                    }
                    break;
                case Keys.Control | Keys.S:
                    btnSave.PerformClick();
                    return true;
                case Keys.Control | Keys.N:
                    btnNew.PerformClick();
                    return true;
                case Keys.Control | Keys.P:
                    btnPrint.PerformClick();
                    return true;
                case Keys.Control | Keys.E:
                    btnEditInvoice.PerformClick();
                    return true;
                case Keys.Control | Keys.F:
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    return true;
                case Keys.Control | Keys.L:
                    btnAllInvoices.PerformClick();
                    return true;
                case Keys.Alt | Keys.Left:
                    btnPrevious.PerformClick();
                    return true;
                case Keys.Alt | Keys.Right:
                    btnNext.PerformClick();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private List<string> transNumbers = new List<string>();
        private int currentIndex;
        private string connectionString;
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            this.Text = "Purchase Invoices";
            ActiveControl = txtSearch;
            SetReadOnlyMode(true);
            SetDatabaseReady(false);

            ConnectToDatabase();
        }
        private void ConnectToDatabase()
        {
            db = null;
            connectionString = AppConnection.ConnectionString;
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                MessageBox.Show("No database connection is set up yet. Click \"Database\" to choose the SQL Server and database.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                db = new InvoiceRepository(connectionString);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"The database connection settings are not valid: {ex.Message}\n\nClick \"Database\" to fix them.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadFormData();
        }
        private void LoadFormData()
        {
            SetReadOnlyMode(true);
            SetDatabaseReady(false);

            DatabaseStatus status;
            try
            {
                status = DatabaseSetup.GetStatus(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not check the database: {ex.Message}", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!status.IsReady)
            {
                string details = status.Problems.Count > 0
                    ? "These problems must be fixed by hand:\n\n- " + string.Join("\n- ", status.Problems)
                    : "It needs these changes:\n\n- " + string.Join("\n- ", status.GetPlannedSteps()) + "\n\nClick \"Database\" to set it up.";
                MessageBox.Show($"Database '{status.Database}' on '{status.Server}' is not ready for this application. {details}", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cboCustomer.Items.Clear();
            cboItem.Items.Clear();
            LoadCustomers(); // whene ever we have to called any method we can called it over here in from load form
            LoadItems();
            SetDatabaseReady(true);
            currentIndex = 0;
            transNumbers = GetTransNumbers();
            if (transNumbers != null && transNumbers.Count > 0)
            {
                transNumbers.Reverse();
                LoadRecord(transNumbers[currentIndex]);
            }
            SetReadOnlyMode(true);
        }
        private void SetDatabaseReady(bool ready)
        {
            btnNew.Enabled = ready;
            btnPrevious.Enabled = ready;
            btnNext.Enabled = ready;
            btnSearch.Enabled = ready;
            btnCustomers.Enabled = ready;
            btnItems.Enabled = ready;
            btnAllInvoices.Enabled = ready;
        }
        private void btnAllInvoices_Click(object sender, EventArgs e)
        {
            OpenInvoiceList();
        }
        private void OpenInvoiceList()
        {
            string selected;
            using (InvoiceListForm list = new InvoiceListForm(connectionString, currentTransId != null ? txtInvoiceNo.Text : null))
            {
                if (list.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                selected = list.SelectedTransNo;
            }
            GoToInvoice(selected);
        }
        private void GoToInvoice(string transNo)
        {
            if (string.IsNullOrEmpty(transNo) || !ConfirmDiscardInvoice())
            {
                return;
            }
            transNumbers = GetTransNumbers();
            transNumbers.Reverse();
            int index = transNumbers.IndexOf(transNo);
            if (index >= 0)
            {
                currentIndex = index;
            }
            LoadRecord(transNo);
        }
        private void btnCustomers_Click(object sender, EventArgs e)
        {
            OpenMaster(MasterDefinition.Customers);
        }
        private void btnItems_Click(object sender, EventArgs e)
        {
            OpenMaster(MasterDefinition.Items);
        }
        private void OpenMaster(MasterDefinition definition)
        {
            using (MasterForm form = new MasterForm(connectionString, definition))
            {
                form.ShowDialog(this);
                if (form.DataChanged)
                {
                    RefreshLookups();
                }
            }
        }
        private void RefreshLookups()
        {
            bool editing = btnSave.Enabled;
            string customer = cboCustomer.SelectedItem?.ToString();
            string item = cboItem.SelectedItem?.ToString();
            cboCustomer.Items.Clear();
            cboItem.Items.Clear();
            LoadCustomers();
            LoadItems();
            if (!editing)
            {
                if (txtInvoiceNo.Text.Length > 0)
                {
                    LoadRecord(txtInvoiceNo.Text);
                }
                return;
            }
            cboCustomer.SelectedIndex = customer == null ? -1 : cboCustomer.FindStringExact(customer);
            if (cboCustomer.SelectedIndex >= 0 && item != null)
            {
                cboItem.SelectedIndex = cboItem.FindStringExact(item);
            }
        }
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            bool reload;
            using (DatabaseSettingsForm settings = new DatabaseSettingsForm())
            {
                settings.ShowDialog(this);
                reload = settings.NeedsReload;
            }
            if (reload)
            {
                ReconnectDatabase();
            }
        }
        private void ReconnectDatabase()
        {
            ClearInvoice();
            cboCustomer.Items.Clear();
            cboItem.Items.Clear();
            transNumbers = new List<string>();
            currentIndex = 0;
            SetDatabaseReady(false);
            ConnectToDatabase();
        }
        private List<string> GetTransNumbers()
        {
            try
            {
                return db.GetInvoiceNumbers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>();
            }
        }
        private bool LoadRecord(string transNumber, bool reportMissing = true)
        {
            InvoiceRecord invoice;
            try
            {
                invoice = db.FindInvoice(transNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (invoice == null)
            {
                if (reportMissing)
                {
                    MessageBox.Show("No matching record found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return false;
            }

            InvoiceDate = invoice.Date;
            currentTransId = invoice.TransId;
            editingTransId = null;

            txtInvoiceNo.Text = invoice.TransNo;
            cboCustomer.SelectedIndex = cboCustomer.FindStringExact(invoice.CustomerId);
            InvoiceTotal = AsText(invoice.Total);

            dgvLines.Rows.Clear();
            foreach (InvoiceLine line in invoice.Lines)
            {
                dgvLines.Rows.Add(AsText(line.Slno), line.ItemCode, line.Description, AsText(line.Quantity), AsText(line.Rate), AsText(line.Amount));
            }
            SetReadOnlyMode(true);
            return true;
        }
        private static string AsText(decimal? value) => value?.ToString() ?? string.Empty;
        private void btnNew_Click(object sender, EventArgs e)
        {
            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            string nextTransNo;
            try
            {
                nextTransNo = db.GetNextInvoiceNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            currentTransId = null;
            editingTransId = null;
            txtInvoiceNo.Text = nextTransNo;
            InvoiceDate = null;
            cboCustomer.SelectedIndex = -1;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            cboItem.SelectedIndex = -1;
            txtItemRate.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtAmount.Text = string.Empty;
            InvoiceTotal = string.Empty;
            txtSearch.Text = string.Empty;
            dgvLines.Rows.Clear();
            SetReadOnlyMode(!true);
            editSnapshot = InvoiceSignature();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dgvLines.EndEdit())
            {
                return;
            }

            if (dgvLines.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool updating = editingTransId.HasValue;
            string shownTransNo = txtInvoiceNo.Text;
            SavedInvoice saved;
            try
            {
                List<InvoiceLine> lines = new List<InvoiceLine>();
                for (int i = 0; i < dgvLines.Rows.Count; i++)
                {
                    lines.Add(new InvoiceLine
                    {
                        ItemCode = dgvLines.Rows[i].Cells[1].Value.ToString(),
                        Quantity = Convert.ToDecimal(dgvLines.Rows[i].Cells[3].Value.ToString()),
                        Rate = Convert.ToDecimal(dgvLines.Rows[i].Cells[4].Value.ToString()),
                        Amount = Convert.ToDecimal(dgvLines.Rows[i].Cells[5].Value.ToString()),
                    });
                }
                saved = db.SaveInvoice(editingTransId, cboCustomer.Text, Convert.ToDecimal(InvoiceTotal), lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (saved == null)
            {
                MessageBox.Show($"Error: Invoice {shownTransNo} no longer exists. Someone else may have deleted it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentTransId = saved.TransId;
            editingTransId = null;
            InvoiceDate = saved.Date;
            txtInvoiceNo.Text = saved.TransNo;
            SetReadOnlyMode(true);

            string message = updating ? $"Invoice {saved.TransNo} updated successfully." : "Data inserted successfully!";
            if (!updating && saved.TransNo != shownTransNo)
            {
                message += $" Invoice number {shownTransNo} was taken by another save, so this invoice was saved as {saved.TransNo}.";
            }
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            transNumbers = GetTransNumbers();
            transNumbers.Reverse();
            currentIndex = Math.Max(0, transNumbers.IndexOf(txtInvoiceNo.Text));
        }
        private void SetReadOnlyMode(bool readOnly)
        {
            cboCustomer.Enabled = !readOnly;
            cboItem.Enabled = !readOnly;
            txtItemQty.ReadOnly = readOnly;
            dgvLines.ReadOnly = readOnly;
            Slno.ReadOnly = true;
            ItemCode.ReadOnly = true;
            Description.ReadOnly = true;
            Amount.ReadOnly = true;
            btnSave.Enabled = !readOnly;
            btnAdd.Enabled = !readOnly;
            btnEditInvoice.Visible = readOnly;
            btnDeleteInvoice.Visible = readOnly;
            btnCancelEdit.Visible = !readOnly;
            btnEditInvoice.Enabled = readOnly && currentTransId != null;
            btnDeleteInvoice.Enabled = readOnly && currentTransId != null;
            detailsCard.Title = readOnly ? "Invoice details" : editingTransId != null ? $"Editing invoice {txtInvoiceNo.Text}" : "New invoice";
        }
        private void btnEditInvoice_Click(object sender, EventArgs e)
        {
            if (currentTransId == null || btnSave.Enabled)
            {
                return;
            }
            editingTransId = currentTransId;
            SetReadOnlyMode(false);
            editSnapshot = InvoiceSignature();
            ActiveControl = cboItem;
        }
        private void btnCancelEdit_Click(object sender, EventArgs e)
        {
            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            bool wasEditingSaved = editingTransId != null;
            editingTransId = null;
            if (wasEditingSaved && LoadRecord(txtInvoiceNo.Text, false))
            {
                return;
            }
            if (wasEditingSaved)
            {
                transNumbers = GetTransNumbers();
                transNumbers.Reverse();
            }
            if (transNumbers.Count > 0)
            {
                currentIndex = Math.Min(currentIndex, transNumbers.Count - 1);
                if (LoadRecord(transNumbers[currentIndex]))
                {
                    return;
                }
            }
            ClearInvoice();
        }
        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            if (currentTransId == null || btnSave.Enabled)
            {
                return;
            }
            string transNo = txtInvoiceNo.Text;
            int lines = dgvLines.Rows.Count;
            decimal total;
            string totalText = decimal.TryParse(InvoiceTotal, NumberStyles.Number, CultureInfo.CurrentCulture, out total) ? total.ToString("N2", CultureInfo.CurrentCulture) : InvoiceTotal;
            string question = $"Delete invoice {transNo}?\n\n{txtCustomerName.Text}  ·  {lines} {(lines == 1 ? "line" : "lines")}  ·  total {totalText}\n\nThis can't be undone.";
            if (MessageBox.Show(question, "Delete invoice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            bool deleted;
            try
            {
                deleted = db.DeleteInvoice(currentTransId.Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not delete invoice {transNo}.\n\n{ex.Message}", "Delete invoice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!deleted)
            {
                MessageBox.Show($"Invoice {transNo} was already deleted by someone else.", "Delete invoice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            transNumbers = GetTransNumbers();
            transNumbers.Reverse();
            if (transNumbers.Count == 0)
            {
                ClearInvoice();
                return;
            }
            currentIndex = Math.Min(currentIndex, transNumbers.Count - 1);
            LoadRecord(transNumbers[currentIndex]);
        }
        private void ClearInvoice()
        {
            currentTransId = null;
            editingTransId = null;
            InvoiceDate = null;
            txtInvoiceNo.Text = string.Empty;
            cboCustomer.SelectedIndex = -1;
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            cboItem.SelectedIndex = -1;
            txtItemRate.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtAmount.Text = string.Empty;
            InvoiceTotal = string.Empty;
            dgvLines.Rows.Clear();
            SetReadOnlyMode(true);
        }
        private string InvoiceSignature()
        {
            StringBuilder signature = new StringBuilder(cboCustomer.SelectedItem?.ToString() ?? string.Empty);
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                signature.Append('|').Append(row.Cells[1].Value).Append(';').Append(row.Cells[3].Value).Append(';').Append(row.Cells[4].Value);
            }
            return signature.ToString();
        }
        private bool ConfirmDiscardInvoice()
        {
            if (!btnSave.Enabled)
            {
                return true;
            }
            dgvLines.EndEdit();
            if (InvoiceSignature() == editSnapshot)
            {
                return true;
            }
            string what = editingTransId != null ? $"your changes to invoice {txtInvoiceNo.Text}" : "this new invoice";
            return MessageBox.Show($"Discard {what}?", "Unsaved changes", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }
        private void InvoiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && !ConfirmDiscardInvoice())
            {
                e.Cancel = true;
            }
        }
        private void LoadCustomers()
        {
            Dictionary<string, string> names = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (CustomerRecord customer in db.GetCustomers())
            {
                cboCustomer.Items.Add(customer.Id);
                names[customer.Id] = customer.Name;
            }
            cboCustomer.Descriptions = names;
        }
        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCustomerName.Text = string.Empty;
            txtCustomerAddress.Text = string.Empty;
            if (cboCustomer.SelectedItem != null)
            {
                CustomerRecord customer;
                try
                {
                    customer = db.FindCustomer(cboCustomer.SelectedItem.ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (customer != null)
                {
                    txtCustomerName.Text = customer.Name;
                    txtCustomerAddress.Text = customer.Address;
                }
            }
        }
        private void LoadItems()
        {
            Dictionary<string, string> descriptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            cboItem.Descriptions = descriptions;
            foreach (ItemRecord item in db.GetItems())
            {
                descriptions[item.Code] = item.Description;
                cboItem.Items.Add(item.Code);
            }
        }

        private void cboItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboItem.SelectedItem == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInvoiceNo.Text) ||
                string.IsNullOrWhiteSpace(cboCustomer.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                string.IsNullOrWhiteSpace(txtCustomerAddress.Text))
            {
                MessageBox.Show("Please fill the customer details before select the item code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ItemRecord item;
            try
            {
                item = db.FindItem(cboItem.SelectedItem.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (item != null)
            {
                txtItemRate.Text = item.Cost?.ToString("N2", CultureInfo.CurrentCulture) ?? string.Empty;
            }
            txtItemQty.Text = string.Empty;
            txtAmount.Text = string.Empty;
        }
        private void txtItemQty_TextChanged(object sender, EventArgs e)
        {
            double itemRate, itemQunatity, totalTxtAmout;

            double.TryParse(txtItemRate.Text, out itemRate);
            double.TryParse(txtItemQty.Text, out itemQunatity);

            totalTxtAmout = itemRate * itemQunatity;
            txtAmount.Text = totalTxtAmout.ToString("N2");


        }

        private void txtItemQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) /*&& e.KeyChar != '.')*/
            {
                e.Handled = true;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                MessageBox.Show("Please fill in all fields before adding.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String itemcode = cboItem.Text;

            if (IsItemCodeExists(itemcode))
            {
                MessageBox.Show("Item code already exists in the Table.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string description = GetDescription(itemcode);
            dgvLines.Rows.Insert(0, new object[]
            {
                    dgvLines.Rows.Count+1,
                    itemcode,
                    description,
                    txtItemQty.Text,
                    txtItemRate.Text,
                    txtAmount.Text,
            });

            UpdateTotalSum();
            UpdateSlno();

        }

        private bool ValidateInput()
        {
            return
                !string.IsNullOrWhiteSpace(cboCustomer.Text) &&
                !string.IsNullOrWhiteSpace(txtCustomerName.Text) &&
                !string.IsNullOrWhiteSpace(txtCustomerAddress.Text) &&
                !string.IsNullOrWhiteSpace(cboItem.Text) &&
                !string.IsNullOrWhiteSpace(txtItemQty.Text) &&
                !string.IsNullOrWhiteSpace(txtAmount.Text) &&
                IsValidQty(txtItemQty.Text) &&
                double.TryParse(txtItemRate.Text, out _) &&
                double.TryParse(txtAmount.Text, out _);
        }

        private string GetDescription(string itemcode)
        {
            return db.FindItem(itemcode)?.Description ?? string.Empty;
        }
        private void UpdateSlno()
        {
            for (int i = 0; i < dgvLines.Rows.Count; i++)
            {
                dgvLines.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void UpdateTotalSum()
        {
            double totalTxtAmout = 0;
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                double cellValue;
                if (double.TryParse(row.Cells[5].Value?.ToString(), out cellValue))
                {
                    totalTxtAmout += cellValue;
                }
            }
            InvoiceTotal = totalTxtAmout.ToString();
        }

        private void dgvLines_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                DataGridViewRow row = dgvLines.Rows[e.RowIndex];
                double qty, rate;
                double.TryParse(row.Cells[3].Value?.ToString(), out qty);
                double.TryParse(row.Cells[4].Value?.ToString(), out rate);
                row.Cells[5].Value = (qty * rate).ToString();
            }
            UpdateTotalSum();
        }

        private static bool IsValidQty(string value)
        {
            int qty;
            return int.TryParse(value, NumberStyles.None, CultureInfo.CurrentCulture, out qty) && qty <= 999999;
        }

        private void dgvLines_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!dgvLines.IsCurrentCellInEditMode)
            {
                return;
            }

            string value = Convert.ToString(e.FormattedValue);
            if (e.ColumnIndex == 3)
            {
                if (!IsValidQty(value))
                {
                    MessageBox.Show("Quantity must be a whole number of up to 6 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == 4)
            {
                decimal rate;
                if (!decimal.TryParse(value, out rate) || rate < 0)
                {
                    MessageBox.Show("Rate must be a number of 0 or more.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
        }

        private void dgvLines_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateSlno();
            UpdateTotalSum();
        }

        private bool IsItemCodeExists(string itemcode)
        {
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == itemcode)
                {
                    return true;
                }

            }
            return false;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInvoiceNum = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchInvoiceNum))
            {
                OpenInvoiceList();
                return;
            }

            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            int index = transNumbers.IndexOf(searchInvoiceNum);
            if (index >= 0)
            {
                currentIndex = index;
            }
            LoadRecord(searchInvoiceNum);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            if (currentIndex < transNumbers.Count - 1)
            {
                currentIndex++;
                LoadRecord(transNumbers[currentIndex]);
            }
            else
            {
                MessageBox.Show("No previous records available.", "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (!ConfirmDiscardInvoice())
            {
                return;
            }
            if (currentIndex > 0)
            {
                currentIndex--;
                LoadRecord(transNumbers[currentIndex]);
            }
            else
            {
                MessageBox.Show("No more records available.", "Navigation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (!dgvLines.EndEdit())
            {
                return;
            }
            if (dgvLines.Rows.Count == 0)
            {
                MessageBox.Show("There are no items to print. Add at least one item first.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (PrinterSettings.InstalledPrinters.Count == 0)
            {
                MessageBox.Show("No printer is installed on this PC. Install a printer, or \"Microsoft Print to PDF\", to preview and print invoices.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (InvoicePrintDocument document = new InvoicePrintDocument(BuildPrintData()))
            using (PrintPreviewForm preview = new PrintPreviewForm(document))
            {
                preview.ShowDialog(this);
            }
        }
        private InvoicePrintData BuildPrintData()
        {
            bool draft = btnSave.Enabled;
            InvoicePrintData data = new InvoicePrintData
            {
                InvoiceNo = txtInvoiceNo.Text.Trim(),
                Date = InvoiceDate ?? DateTime.Now,
                IsDraft = draft,
                CustomerId = cboCustomer.SelectedItem?.ToString() ?? string.Empty,
                CustomerName = txtCustomerName.Text.Trim(),
                CustomerAddress = txtCustomerAddress.Text.Trim(),
            };
            decimal sum = 0;
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                decimal amount = ParseAmount(row.Cells[5].Value);
                sum += amount;
                data.Lines.Add(new InvoicePrintLine(
                    row.Cells[0].Value?.ToString(),
                    row.Cells[1].Value?.ToString(),
                    row.Cells[2].Value?.ToString(),
                    ParseAmount(row.Cells[3].Value),
                    ParseAmount(row.Cells[4].Value),
                    amount));
            }
            decimal total;
            data.Total = decimal.TryParse(InvoiceTotal, NumberStyles.Number, CultureInfo.CurrentCulture, out total) ? total : sum;
            return data;
        }
        private static decimal ParseAmount(object value)
        {
            decimal number;
            return decimal.TryParse(Convert.ToString(value), NumberStyles.Number, CultureInfo.CurrentCulture, out number) ? number : 0m;
        }
    }
}