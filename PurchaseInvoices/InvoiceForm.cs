using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        SqlConnection con; //--- first step...
        private DateTime? invoiceDate;

        public InvoiceForm()
        {
            InitializeComponent();
            Theme.StyleWindow(this);
        }


        private List<string> transNumbers = new List<string>();
        private int currentIndex;
        private string connectionString;
        private void InvoiceForm_Load(object sender, EventArgs e)
        {
            this.Text = "Purchase Invoices";
            ActiveControl = txtSearch;
            setControlsReadOnnly(true);
            SetDatabaseReady(false);

            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["MYCON"];
            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {
                btnCreateTables.Enabled = false;
                MessageBox.Show("App.config has no connection string named MYCON.", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connectionString = setting.ConnectionString;
                con = new SqlConnection(connectionString); //-- combination of second or third step...
            }
            catch (ArgumentException ex)
            {
                btnCreateTables.Enabled = false;
                MessageBox.Show($"The MYCON connection string in App.config is not valid: {ex.Message}", "Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadFormData();
        }
        private void LoadFormData()
        {
            setControlsReadOnnly(true);
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
                    : "It needs these changes:\n\n- " + string.Join("\n- ", status.GetPlannedSteps()) + "\n\nClick \"Create Tables\" to make them.";
                MessageBox.Show($"Database '{status.Database}' on '{status.Server}' is not ready for this application. {details}", "Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            con.Open();  //--fourth step...
            con.Close();  //--fifth step...
            comboCustId.Items.Clear();
            combo_Itemcode.Items.Clear();
            bindComboBox(); // whene ever we have to called any method we can called it over here in from load form
            bindComboBox_1();
            SetDatabaseReady(true);
            currentIndex = 0;
            transNumbers = GetTransNumbers();
            if (transNumbers != null && transNumbers.Count > 0)
            {
                transNumbers.Reverse();
                LoadRecord(transNumbers[currentIndex]);
            }
            setControlsReadOnnly(true);
        }
        private void SetDatabaseReady(bool ready)
        {
            btnNew.Enabled = ready;
            btnPrevious.Enabled = ready;
            btnNext.Enabled = ready;
            button1.Enabled = ready;
            btnCustomers.Enabled = ready;
            btnItems.Enabled = ready;
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
            string customer = comboCustId.SelectedItem?.ToString();
            string item = combo_Itemcode.SelectedItem?.ToString();
            comboCustId.Items.Clear();
            combo_Itemcode.Items.Clear();
            bindComboBox();
            bindComboBox_1();
            if (!editing)
            {
                if (txtTransNo.Text.Length > 0)
                {
                    LoadRecord(txtTransNo.Text);
                }
                return;
            }
            comboCustId.SelectedIndex = customer == null ? -1 : comboCustId.FindStringExact(customer);
            if (comboCustId.SelectedIndex >= 0 && item != null)
            {
                combo_Itemcode.SelectedIndex = combo_Itemcode.FindStringExact(item);
            }
        }
        private void btnCreateTables_Click(object sender, EventArgs e)
        {
            DatabaseStatus status;
            try
            {
                status = DatabaseSetup.GetStatus(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not check the database: {ex.Message}", "Create Tables", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string target = $"database '{status.Database}' on server '{status.Server}'";
            if (status.Problems.Count > 0)
            {
                MessageBox.Show($"Nothing was changed. These problems in {target} must be fixed by hand first:\n\n- " + string.Join("\n- ", status.Problems), "Create Tables", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (status.IsReady)
            {
                MessageBox.Show($"All tables already exist in {target}. Nothing to create.", "Create Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string plan = "- " + string.Join("\n- ", status.GetPlannedSteps());
            if (MessageBox.Show($"This will make these changes in {target}:\n\n{plan}\n\nExisting tables and data are not changed. Continue?", "Create Tables", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            bool databaseCreated = false;
            try
            {
                if (!status.DatabaseExists)
                {
                    DatabaseSetup.CreateDatabase(connectionString);
                    databaseCreated = true;
                }
                DatabaseSetup.ApplyTableChanges(connectionString, status);
            }
            catch (Exception ex)
            {
                string note = databaseCreated ? $"\n\nDatabase '{status.Database}' was created but is still empty." : string.Empty;
                MessageBox.Show($"Setup failed: {ex.Message}\n\nNo tables were created or changed.{note}", "Create Tables", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Done. Changes made in {target}:\n\n{plan}", "Create Tables", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadFormData();
        }
        private List<string> GetTransNumbers()
        {
            List<string> numbers = new List<string>();
            try
            {
                con.Open();

                string fetchTrnsNumQry = "SELECT TRANS_NO FROM PURCH_HEADER ORDER BY TRANS_ID";
                using (SqlCommand cmd = new SqlCommand(fetchTrnsNumQry, con))
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        numbers.Add(sdr["TRANS_NO"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            return numbers;
        }
        private void LoadRecord(string transNumber)
        {
            try
            {
                con.Open();
                string searchHeaderQuery = "SELECT * FROM PURCH_HEADER WHERE TRANS_NO = @invoiceNum"; ;
                string searchDetailQuery = "SELECT * FROM PURCH_DETAIL WHERE TRANS_ID IN (SELECT h.TRANS_ID FROM PURCH_HEADER h WHERE h.TRANS_NO = @invoiceNum)";

                using (SqlCommand cmdHeader = new SqlCommand(searchHeaderQuery, con))
                using (SqlCommand cmdDetail = new SqlCommand(searchDetailQuery, con))
                {
                    cmdHeader.Parameters.AddWithValue("@invoiceNum", transNumber);
                    cmdDetail.Parameters.AddWithValue("@invoiceNum", transNumber);

                    using (SqlDataReader sdrHeader = cmdHeader.ExecuteReader())
                    {
                        if (sdrHeader.Read())
                        {
                            string TRANS_NO = sdrHeader["TRANS_NO"].ToString();
                            string CUST_ID = sdrHeader["CUST_ID"].ToString();
                            string TOTAL_AMOUNT = sdrHeader["TOTAL_AMOUNT"].ToString();
                            invoiceDate = sdrHeader["TRANS_DATE"] as DateTime?;

                            txtTransNo.Text = TRANS_NO;
                            comboCustId.SelectedIndex = comboCustId.FindStringExact(CUST_ID);
                            txtTotalSum.Text = TOTAL_AMOUNT;

                            dgv_sales.Rows.Clear();
                            using (SqlDataReader sdrDetail = cmdDetail.ExecuteReader())
                            {
                                while (sdrDetail.Read())
                                {
                                    string SLNO = sdrDetail["SLNO"].ToString();
                                    string ITEM_CODE = sdrDetail["ITEM_CODE"].ToString();
                                    string QTY = sdrDetail["QTY"].ToString();
                                    string RATE = sdrDetail["RATE"].ToString();
                                    string AMOUNT = sdrDetail["AMOUNT"].ToString();

                                    string ITEM_DESCRIPTION = GetDescription(ITEM_CODE);

                                    dgv_sales.Rows.Add(SLNO, ITEM_CODE, ITEM_DESCRIPTION, QTY, RATE, AMOUNT);
                                }
                            }
                            setControlsReadOnnly(true);
                        }
                        else
                        {
                            MessageBox.Show("No matching record found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            string qry = "Select isnull(max(TRANS_NO),0) + 1 from PURCH_HEADER";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            object result = cmd.ExecuteScalar();
            txtTransNo.Text = result.ToString();
            con.Close();
            invoiceDate = null;
            comboCustId.SelectedIndex = -1;
            txtcustname.Text = string.Empty;
            txtcustaddresss.Text = string.Empty;
            combo_Itemcode.SelectedIndex = -1;
            txtItemRate.Text = string.Empty;
            txtItemQty.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtTotalSum.Text = string.Empty;
            txtSearch.Text = string.Empty;
            dgv_sales.Rows.Clear();
            setControlsReadOnnly(!true);
        }

        private bool isDataSaved = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!dgv_sales.EndEdit())
            {
                return;
            }

            if (dgv_sales.Rows.Count == 0)
            {
                MessageBox.Show("Please add at least one item before saving.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            isDataSaved = false;
            string shownTransNo = txtTransNo.Text;
            string savedTransNo;
            try
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string headerInsertQuery = "insert into PURCH_HEADER (TRANS_DATE, TRANS_NO, CUST_ID, TOTAL_AMOUNT) ";
                    headerInsertQuery += "select GETDATE(), isnull(max(TRANS_NO),0) + 1, @CUST_ID, @TOTAL_AMOUNT from PURCH_HEADER with (updlock, holdlock); ";
                    headerInsertQuery += "select TRANS_ID, TRANS_NO, TRANS_DATE from PURCH_HEADER where TRANS_ID = SCOPE_IDENTITY();";

                    int lastTransId;
                    using (SqlCommand headerCmd = new SqlCommand(headerInsertQuery, con, transaction))
                    {
                        headerCmd.Parameters.AddWithValue("@CUST_ID", comboCustId.Text);
                        headerCmd.Parameters.AddWithValue("@TOTAL_AMOUNT", Convert.ToDecimal(txtTotalSum.Text));
                        using (SqlDataReader sdr = headerCmd.ExecuteReader())
                        {
                            sdr.Read();
                            lastTransId = Convert.ToInt32(sdr["TRANS_ID"]);
                            savedTransNo = sdr["TRANS_NO"].ToString();
                            invoiceDate = sdr["TRANS_DATE"] as DateTime?;
                        }
                    }

                    string detailInsertQuery = "insert into PURCH_DETAIL(TRANS_ID,SLNO, ITEM_CODE, QTY, RATE, AMOUNT) values";
                    List<SqlParameter> detailParams = new List<SqlParameter>();
                    detailParams.Add(new SqlParameter("@TRANS_ID", lastTransId));

                    for (int i = 0; i < dgv_sales.Rows.Count; i++)
                    {
                        string itemCode = dgv_sales.Rows[i].Cells[1].Value.ToString();
                        string qty = dgv_sales.Rows[i].Cells[3].Value.ToString();
                        string rate = dgv_sales.Rows[i].Cells[4].Value.ToString();
                        string amount = dgv_sales.Rows[i].Cells[5].Value.ToString();

                        detailInsertQuery += $"(@TRANS_ID, @SLNO{i}, @ITEM_CODE{i}, @QTY{i}, @RATE{i}, @AMOUNT{i})";
                        detailParams.Add(new SqlParameter($"@SLNO{i}", i + 1));
                        detailParams.Add(new SqlParameter($"@ITEM_CODE{i}", itemCode));
                        detailParams.Add(new SqlParameter($"@QTY{i}", Convert.ToDecimal(qty)));
                        detailParams.Add(new SqlParameter($"@RATE{i}", Convert.ToDecimal(rate)));
                        detailParams.Add(new SqlParameter($"@AMOUNT{i}", Convert.ToDecimal(amount)));
                        if (i < dgv_sales.Rows.Count - 1)
                        {
                            detailInsertQuery += ", ";
                        }
                    }
                    using (SqlCommand detailCmd = new SqlCommand(detailInsertQuery, con, transaction))  //.Open();
                    {
                        detailCmd.Parameters.AddRange(detailParams.ToArray());
                        detailCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                isDataSaved = true;
                txtTransNo.Text = savedTransNo;
                setControlsReadOnnly(true);

                string message = "Data inserted successfully!";
                if (savedTransNo != shownTransNo)
                {
                    message += $" Invoice number {shownTransNo} was taken by another save, so this invoice was saved as {savedTransNo}.";
                }
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }

            if (isDataSaved)
            {
                transNumbers = GetTransNumbers();
                transNumbers.Reverse();
                currentIndex = Math.Max(0, transNumbers.IndexOf(txtTransNo.Text));
            }
        }
        private void setControlsReadOnnly(bool readOnly)
        {
            comboCustId.Enabled = !readOnly;
            combo_Itemcode.Enabled = !readOnly;
            txtItemQty.ReadOnly = readOnly;
            dgv_sales.ReadOnly = readOnly;
            Slno.ReadOnly = true;
            ItemCode.ReadOnly = true;
            Description.ReadOnly = true;
            Amount.ReadOnly = true;
            btnSave.Enabled = !readOnly;
            btnAdd.Enabled = !readOnly;
        }
        private void bindComboBox()
        {
            String qry = "select * from CUSTOMER_MASTER order by CUSTID";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string CUSTID = dr.GetString(0);
                comboCustId.Items.Add(CUSTID);
            }
            con.Close();
        }
        private void comboCustId_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtcustname.Text = string.Empty;
            txtcustaddresss.Text = string.Empty;
            if (comboCustId.SelectedItem != null)
            {
                string CUSTID = Convert.ToString(comboCustId.SelectedItem.ToString());
                String qry = "select * from CUSTOMER_MASTER where CUSTID = @CUSTID";
                SqlDataAdapter sda = new SqlDataAdapter(qry, con);
                sda.SelectCommand.Parameters.AddWithValue("@CUSTID", CUSTID);
                DataTable data = new DataTable();
                sda.Fill(data);
                if (data.Rows.Count > 0)
                {
                    txtcustname.Text = data.Rows[0]["CUST_NAME"].ToString();
                    txtcustaddresss.Text = data.Rows[0]["CUST_ADDRESS"].ToString();
                }
            }
        }
        private void bindComboBox_1()
        {
            string qry = "select * from ITEM_MASTER order by ITEM_CODE";
            SqlCommand cmd = new SqlCommand(qry, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string itmcode = sdr.GetString(0);
                combo_Itemcode.Items.Add(itmcode);
            }
            con.Close();
        }

        private void combo_Itemcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Itemcode.SelectedItem == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTransNo.Text) ||
                string.IsNullOrWhiteSpace(comboCustId.Text) ||
                string.IsNullOrWhiteSpace(txtcustname.Text) ||
                string.IsNullOrWhiteSpace(txtcustaddresss.Text))
            {
                MessageBox.Show("Please fill the customer details before select the item code.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string itmcode = Convert.ToString(combo_Itemcode.SelectedItem.ToString());
            string qury = "select * from ITEM_MASTER where ITEM_CODE = @ITEM_CODE";
            SqlDataAdapter sda = new SqlDataAdapter(qury, con);
            sda.SelectCommand.Parameters.AddWithValue("@ITEM_CODE", itmcode);
            DataTable data = new DataTable();
            sda.Fill(data);
            if (data.Rows.Count > 0)
            {
                txtItemRate.Text = data.Rows[0]["ITEM_COST"].ToString();
            }
            con.Close();
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

        private void txtItemOrderQty(object sender, KeyPressEventArgs e)
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
            String itemcode = combo_Itemcode.Text;

            if (IsItemCodeExists(itemcode))
            {
                MessageBox.Show("Item code already exists in the Table.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string description = GetDescription(itemcode);
            dgv_sales.Rows.Insert(0, new object[]
            {
                    dgv_sales.Rows.Count+1,
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
                !string.IsNullOrWhiteSpace(comboCustId.Text) &&
                !string.IsNullOrWhiteSpace(txtcustname.Text) &&
                !string.IsNullOrWhiteSpace(txtcustaddresss.Text) &&
                !string.IsNullOrWhiteSpace(combo_Itemcode.Text) &&
                !string.IsNullOrWhiteSpace(txtItemQty.Text) &&
                !string.IsNullOrWhiteSpace(txtAmount.Text) &&
                IsValidQty(txtItemQty.Text) &&
                double.TryParse(txtItemRate.Text, out _) &&
                double.TryParse(txtAmount.Text, out _);
        }

        private string GetDescription(string itemcode)
        {
            string description = string.Empty;
            bool openedHere = con.State == ConnectionState.Closed;

            string qry = "SELECT ITEM_DESCRIPTION FROM ITEM_MASTER WHERE ITEM_CODE = @ITEM_CODE";
            using (SqlCommand cmd = new SqlCommand(qry, con))
            {
                cmd.Parameters.AddWithValue("@ITEM_CODE", itemcode);
                try
                {
                    if (openedHere)
                    {
                        con.Open();
                    }
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        description = result.ToString();
                    }
                }
                finally
                {
                    if (openedHere)
                    {
                        con.Close();
                    }
                }
            }

            return description;
        }
        private void UpdateSlno()
        {
            for (int i = 0; i < dgv_sales.Rows.Count; i++)
            {
                dgv_sales.Rows[i].Cells[0].Value = i + 1;
            }
        }
        private void UpdateTotalSum()
        {
            double totalTxtAmout = 0;
            foreach (DataGridViewRow row in dgv_sales.Rows)
            {
                double cellValue;
                if (double.TryParse(row.Cells[5].Value?.ToString(), out cellValue))
                {
                    totalTxtAmout += cellValue;
                }
            }
            txtTotalSum.Text = totalTxtAmout.ToString();
        }

        private void dgv_sales_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                DataGridViewRow row = dgv_sales.Rows[e.RowIndex];
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

        private void dgv_sales_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (!dgv_sales.IsCurrentCellInEditMode)
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

        private void dgv_sales_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateSlno();
            UpdateTotalSum();
        }

        private bool IsItemCodeExists(string itemcode)
        {
            foreach (DataGridViewRow row in dgv_sales.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == itemcode)
                {
                    return true;
                }

            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string searchInvoiceNum = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchInvoiceNum))
            {
                MessageBox.Show("Please enter an invoice number for search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (!dgv_sales.EndEdit())
            {
                return;
            }
            if (dgv_sales.Rows.Count == 0)
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
                InvoiceNo = txtTransNo.Text.Trim(),
                Date = draft || invoiceDate == null ? DateTime.Now : invoiceDate.Value,
                IsDraft = draft,
                CustomerId = comboCustId.SelectedItem?.ToString() ?? string.Empty,
                CustomerName = txtcustname.Text.Trim(),
                CustomerAddress = txtcustaddresss.Text.Trim(),
            };
            decimal sum = 0;
            foreach (DataGridViewRow row in dgv_sales.Rows)
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
            data.Total = decimal.TryParse(txtTotalSum.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out total) ? total : sum;
            return data;
        }
        private static decimal ParseAmount(object value)
        {
            decimal number;
            return decimal.TryParse(Convert.ToString(value), NumberStyles.Number, CultureInfo.CurrentCulture, out number) ? number : 0m;
        }
    }
}