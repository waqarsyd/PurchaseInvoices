using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal enum FieldKind
    {
        Key,
        Text,
        MultilineText,
        Money
    }

    internal sealed class MasterField
    {
        public MasterField(string column, string label, FieldKind kind, int maxLength, float gridWeight)
        {
            Column = column;
            Label = label;
            Kind = kind;
            MaxLength = maxLength;
            GridWeight = gridWeight;
        }

        public string Column { get; }
        public string Label { get; }
        public FieldKind Kind { get; }
        public int MaxLength { get; }
        public float GridWeight { get; }
    }

    internal sealed class MasterDefinition
    {
        public string Table { get; private set; }
        public string Singular { get; private set; }
        public string Plural { get; private set; }
        public string Subtitle { get; private set; }
        public string Glyph { get; private set; }
        public string UsageSql { get; private set; }
        public MasterField[] Fields { get; private set; }

        public MasterField Key => Fields[0];
        public string Title => Capitalize(Plural);
        public string SingularTitle => Capitalize(Singular);

        private static string Capitalize(string text) => text.Substring(0, 1).ToUpperInvariant() + text.Substring(1);

        public static MasterDefinition Customers => new MasterDefinition
        {
            Table = "CUSTOMER_MASTER",
            Singular = "customer",
            Plural = "customers",
            Subtitle = "Add, edit and remove the customers you invoice",
            Glyph = "",
            UsageSql = "SELECT COUNT(*) FROM PURCH_HEADER WHERE CUST_ID = @key",
            Fields = new[]
            {
                new MasterField("CUSTID", "Customer ID", FieldKind.Key, 6, 23),
                new MasterField("CUST_NAME", "Name", FieldKind.Text, 50, 31),
                new MasterField("CUST_ADDRESS", "Address", FieldKind.MultilineText, 500, 46),
            }
        };

        public static MasterDefinition Items => new MasterDefinition
        {
            Table = "ITEM_MASTER",
            Singular = "item",
            Plural = "items",
            Subtitle = "Add, edit and remove items and their prices",
            Glyph = "",
            UsageSql = "SELECT COUNT(DISTINCT TRANS_ID) FROM PURCH_DETAIL WHERE ITEM_CODE = @key",
            Fields = new[]
            {
                new MasterField("ITEM_CODE", "Item code", FieldKind.Key, 6, 18),
                new MasterField("ITEM_DESCRIPTION", "Description", FieldKind.Text, 50, 52),
                new MasterField("ITEM_COST", "Cost", FieldKind.Money, 24, 30),
            }
        };
    }

    internal partial class MasterForm : Form
    {
        private const decimal MaxMoney = 9999999999999999.99m;
        private readonly string connectionString;
        private readonly MasterDefinition definition;
        private readonly TextBox[] boxes;
        private readonly Label[] labels;
        private readonly Timer statusTimer = new Timer { Interval = 4000 };
        private DataTable table = new DataTable();
        private bool loading;
        private bool dirty;
        private bool isNew;
        private bool selectionPending;
        private string currentKey;

        public MasterForm(string connectionString, MasterDefinition definition)
        {
            InitializeComponent();
            Theme.StyleWindow(this, 760, 520);
            this.connectionString = connectionString;
            this.definition = definition;
            boxes = new[] { txtField1, txtField2, txtField3 };
            labels = new[] { lblField1, lblField2, lblField3 };
            statusTimer.Tick += (s, e) => { statusTimer.Stop(); UpdateCount(); };
            FormClosed += (s, e) => statusTimer.Dispose();
            ApplyDefinition();
        }

        public bool DataChanged { get; private set; }

        private string Col(int index) => definition.Fields[index].Column;

        private void ApplyDefinition()
        {
            Text = definition.Title;
            headerPanel.Title = definition.Title;
            headerPanel.Subtitle = definition.Subtitle;
            headerPanel.Glyph = definition.Glyph;
            hostFilter.Placeholder = "Search " + definition.Plural;
            btnNew.Text = "New " + definition.Singular;
            listCard.Title = "All " + definition.Plural;
            grid.AutoGenerateColumns = false;

            for (int i = 0; i < boxes.Length; i++)
            {
                MasterField field = definition.Fields[i];
                labels[i].Text = field.Label;
                boxes[i].MaxLength = field.MaxLength;
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = field.Column,
                    HeaderText = field.Label,
                    Name = field.Column,
                    FillWeight = field.GridWeight,
                };
                if (field.Kind == FieldKind.Money)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    column.DefaultCellStyle.Format = "N2";
                    boxes[i].TextAlign = HorizontalAlignment.Right;
                }
                grid.Columns.Add(column);
            }
            boxes[0].CharacterCasing = CharacterCasing.Upper;

            bool multiline = definition.Fields[2].Kind == FieldKind.MultilineText;
            txtField3.Multiline = multiline;
            txtField3.AcceptsReturn = multiline;
            if (!multiline)
            {
                hostField3.Height = hostField1.Height;
            }
            hostField3.PerformLayout();
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {
            if (!LoadData(null))
            {
                btnNew.Enabled = false;
                btnDelete.Enabled = false;
                SetDirty(false);
                return;
            }
            if (grid.Rows.Count > 0)
            {
                SelectAndShow(grid.Rows[0].Cells[0].Value?.ToString());
                ActiveControl = txtFilter;
            }
            else
            {
                StartNew(false);
            }
        }

        private bool LoadData(string selectKey)
        {
            string columns = string.Join(", ", definition.Fields.Select(f => f.Column));
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter($"SELECT {columns} FROM {definition.Table} ORDER BY {Col(0)}", con))
                {
                    adapter.Fill(data);
                }
            }
            catch (Exception ex)
            {
                ShowError("load the " + definition.Plural, ex);
                return false;
            }

            loading = true;
            try
            {
                table = data;
                grid.DataSource = new DataView(table);
                ApplyFilter();
                SelectKey(selectKey);
            }
            finally
            {
                loading = false;
            }
            UpdateCount();
            return true;
        }

        private void ApplyFilter()
        {
            DataView view = grid.DataSource as DataView;
            if (view == null)
            {
                return;
            }
            string text = txtFilter.Text.Trim();
            view.RowFilter = text.Length == 0
                ? string.Empty
                : string.Join(" OR ", definition.Fields.Select(f => $"CONVERT([{f.Column}], 'System.String') LIKE '%{EscapeLike(text)}%'"));
            grid.EmptyTitle = text.Length == 0 ? $"No {definition.Plural} yet" : "No matches";
            grid.EmptyHint = text.Length == 0
                ? $"Click \"New {definition.Singular}\" to add the first one."
                : $"Nothing matches \"{text}\".";
        }

        private static string EscapeLike(string value)
        {
            StringBuilder escaped = new StringBuilder();
            foreach (char c in value)
            {
                if (c == '*' || c == '%' || c == '[' || c == ']')
                {
                    escaped.Append('[').Append(c).Append(']');
                }
                else if (c == '\'')
                {
                    escaped.Append("''");
                }
                else
                {
                    escaped.Append(c);
                }
            }
            return escaped.ToString();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            statusTimer.Stop();
            loading = true;
            try
            {
                ApplyFilter();
                SelectKey(isNew ? null : currentKey);
            }
            finally
            {
                loading = false;
            }
            UpdateCount();
        }

        private void SelectKey(string key)
        {
            grid.ClearSelection();
            if (key != null)
            {
                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (string.Equals(row.Cells[0].Value?.ToString(), key, StringComparison.OrdinalIgnoreCase))
                    {
                        grid.CurrentCell = row.Cells[0];
                        row.Selected = true;
                        return;
                    }
                }
            }
            grid.CurrentCell = null;
        }

        private void SelectAndShow(string key)
        {
            loading = true;
            try
            {
                SelectKey(key);
            }
            finally
            {
                loading = false;
            }
            DataRowView row = SelectedRow();
            if (row != null)
            {
                ShowRecord(row);
            }
        }

        private DataRowView SelectedRow() => grid.CurrentRow?.DataBoundItem as DataRowView;

        private void grid_SelectionChanged(object sender, EventArgs e)
        {
            if (loading || selectionPending || !IsHandleCreated)
            {
                return;
            }
            selectionPending = true;
            BeginInvoke((MethodInvoker)HandleSelection);
        }

        private void HandleSelection()
        {
            selectionPending = false;
            DataRowView row = SelectedRow();
            if (row == null)
            {
                return;
            }
            string key = row[Col(0)].ToString();
            if (!isNew && string.Equals(key, currentKey, StringComparison.OrdinalIgnoreCase))
            {
                return;
            }
            if (dirty && !ConfirmDiscard())
            {
                loading = true;
                try
                {
                    SelectKey(isNew ? null : currentKey);
                }
                finally
                {
                    loading = false;
                }
                return;
            }
            ShowRecord(row);
        }

        private void ShowRecord(DataRowView row)
        {
            loading = true;
            try
            {
                for (int i = 0; i < boxes.Length; i++)
                {
                    object value = row[definition.Fields[i].Column];
                    boxes[i].Text = value == DBNull.Value
                        ? string.Empty
                        : definition.Fields[i].Kind == FieldKind.Money
                            ? Convert.ToDecimal(value).ToString("N2", CultureInfo.CurrentCulture)
                            : value.ToString();
                }
                currentKey = boxes[0].Text;
                isNew = false;
                boxes[0].ReadOnly = true;
                editorCard.Title = definition.SingularTitle + " details";
                btnDelete.Enabled = true;
                SetDirty(false);
            }
            finally
            {
                loading = false;
            }
        }

        private void StartNew(bool confirm = true)
        {
            if (confirm && dirty && !ConfirmDiscard())
            {
                return;
            }
            loading = true;
            try
            {
                SelectKey(null);
                foreach (TextBox box in boxes)
                {
                    box.Text = string.Empty;
                }
                boxes[0].ReadOnly = false;
                isNew = true;
                currentKey = null;
                editorCard.Title = "New " + definition.Singular;
                btnDelete.Enabled = false;
                SetDirty(false);
            }
            finally
            {
                loading = false;
            }
            ActiveControl = boxes[0];
        }

        private void Field_TextChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                SetDirty(true);
            }
        }

        private void SetDirty(bool value)
        {
            dirty = value;
            btnSave.Enabled = value;
        }

        private void btnNew_Click(object sender, EventArgs e) => StartNew();

        private void btnSave_Click(object sender, EventArgs e) => Save();

        private void btnDelete_Click(object sender, EventArgs e) => Delete();

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void Save()
        {
            if (!dirty)
            {
                return;
            }
            string key = boxes[0].Text.Trim();
            string name = boxes[1].Text.Trim();
            string extra = boxes[2].Text.Trim();
            MasterField third = definition.Fields[2];

            if (key.Length == 0)
            {
                Warn($"{definition.Key.Label} is required.", boxes[0]);
                return;
            }
            if (name.Length == 0)
            {
                Warn($"{definition.Fields[1].Label} is required.", boxes[1]);
                return;
            }
            object extraValue = extra.Length == 0 ? (object)DBNull.Value : extra;
            if (third.Kind == FieldKind.Money)
            {
                decimal amount;
                if (!decimal.TryParse(extra, NumberStyles.Number, CultureInfo.CurrentCulture, out amount) || amount < 0)
                {
                    Warn($"{third.Label} must be a number of 0 or more.", boxes[2]);
                    return;
                }
                if (amount > MaxMoney)
                {
                    Warn($"{third.Label} is too large.", boxes[2]);
                    return;
                }
                extraValue = Math.Round(amount, 2, MidpointRounding.AwayFromZero);
            }

            bool creating = isNew;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = creating
                        ? $"INSERT INTO {definition.Table} ({Col(0)}, {Col(1)}, {Col(2)}) VALUES (@key, @name, @extra)"
                        : $"UPDATE {definition.Table} SET {Col(1)} = @name, {Col(2)} = @extra WHERE {Col(0)} = @key";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@key", SqlDbType.VarChar, definition.Key.MaxLength).Value = key;
                        cmd.Parameters.Add("@name", SqlDbType.VarChar, definition.Fields[1].MaxLength).Value = name;
                        SqlParameter parameter;
                        if (third.Kind == FieldKind.Money)
                        {
                            parameter = cmd.Parameters.Add("@extra", SqlDbType.Decimal);
                            parameter.Precision = 18;
                            parameter.Scale = 2;
                        }
                        else
                        {
                            parameter = cmd.Parameters.Add("@extra", SqlDbType.VarChar, third.MaxLength);
                        }
                        parameter.Value = extraValue;

                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            Warn($"{definition.SingularTitle} {key} no longer exists. Someone else may have deleted it.", null);
                            SetDirty(false);
                            LoadData(null);
                            StartNew(false);
                            return;
                        }
                    }
                }
            }
            catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
            {
                Warn($"{definition.SingularTitle} {key} already exists. Choose a different {definition.Key.Label}.", boxes[0]);
                return;
            }
            catch (Exception ex)
            {
                ShowError("save the " + definition.Singular, ex);
                return;
            }

            DataChanged = true;
            SetDirty(false);
            isNew = false;
            currentKey = key;
            LoadData(key);
            DataRowView row = SelectedRow();
            if (row != null)
            {
                ShowRecord(row);
            }
            else
            {
                boxes[0].ReadOnly = true;
                editorCard.Title = definition.SingularTitle + " details";
                btnDelete.Enabled = true;
            }
            SetStatus(creating ? $"Added {definition.Singular} {key}." : $"Saved changes to {key}.");
        }

        private void Delete()
        {
            if (isNew || currentKey == null)
            {
                return;
            }
            string key = currentKey;
            string name = boxes[1].Text.Trim();

            int usage;
            try
            {
                usage = Convert.ToInt32(Scalar(definition.UsageSql, key));
            }
            catch (Exception ex)
            {
                ShowError("check whether the " + definition.Singular + " is in use", ex);
                return;
            }
            if (usage > 0)
            {
                Warn($"{definition.SingularTitle} {key} can't be deleted because {usage} saved {(usage == 1 ? "invoice uses" : "invoices use")} it.", null);
                return;
            }
            if (MessageBox.Show(this, $"Delete {definition.Singular} {key} ({name})?\n\nThis can't be undone.", "Delete " + definition.Singular,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            int index = grid.CurrentRow?.Index ?? 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand($"DELETE FROM {definition.Table} WHERE {Col(0)} = @key", con))
                {
                    cmd.Parameters.Add("@key", SqlDbType.VarChar, definition.Key.MaxLength).Value = key;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ShowError("delete the " + definition.Singular, ex);
                return;
            }

            DataChanged = true;
            SetDirty(false);
            LoadData(null);
            if (grid.Rows.Count > 0)
            {
                SelectAndShow(grid.Rows[Math.Min(index, grid.Rows.Count - 1)].Cells[0].Value?.ToString());
            }
            else
            {
                StartNew(false);
            }
            SetStatus($"Deleted {definition.Singular} {key}.");
        }

        private object Scalar(string sql, string key)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.Add("@key", SqlDbType.VarChar, definition.Key.MaxLength).Value = key;
                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        private void UpdateCount()
        {
            if (statusTimer.Enabled)
            {
                return;
            }
            int total = table.Rows.Count;
            int shown = grid.Rows.Count;
            lblStatus.ForeColor = Theme.TextMuted;
            lblStatus.Text = shown == total
                ? $"{total} {(total == 1 ? definition.Singular : definition.Plural)}"
                : $"Showing {shown} of {total} {definition.Plural}";
        }

        private void SetStatus(string message)
        {
            statusTimer.Stop();
            lblStatus.ForeColor = Color.FromArgb(5, 150, 105);
            lblStatus.Text = "✓  " + message;
            statusTimer.Start();
        }

        private bool ConfirmDiscard()
        {
            return MessageBox.Show(this, "You have unsaved changes. Discard them?", definition.Title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        private void Warn(string message, Control focus)
        {
            MessageBox.Show(this, message, definition.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focus?.Focus();
        }

        private void ShowError(string action, Exception ex)
        {
            MessageBox.Show(this, $"Could not {action}.\n\n{ex.Message}", definition.Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MasterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty && !ConfirmDiscard())
            {
                e.Cancel = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.S:
                    if (btnSave.Enabled)
                    {
                        Save();
                    }
                    return true;
                case Keys.Control | Keys.N:
                    if (btnNew.Enabled)
                    {
                        StartNew();
                    }
                    return true;
                case Keys.Control | Keys.F:
                    txtFilter.Focus();
                    txtFilter.SelectAll();
                    return true;
                case Keys.Escape:
                    Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
