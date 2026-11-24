using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal static class CsvExport
    {
        public static string FromGrid(DataGridView grid)
        {
            List<DataGridViewColumn> columns = grid.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).OrderBy(c => c.DisplayIndex).ToList();
            StringBuilder csv = new StringBuilder();
            AppendLine(csv, columns.Select(c => c.HeaderText));
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (!row.IsNewRow)
                {
                    AppendLine(csv, columns.Select(c => Convert.ToString(row.Cells[c.Index].FormattedValue)));
                }
            }
            return csv.ToString();
        }

        public static string FromRows(IEnumerable<string> headers, IEnumerable<IEnumerable<string>> rows)
        {
            StringBuilder csv = new StringBuilder();
            AppendLine(csv, headers);
            foreach (IEnumerable<string> row in rows)
            {
                AppendLine(csv, row);
            }
            return csv.ToString();
        }

        public static string Field(string value)
        {
            value = value ?? string.Empty;
            if (value.Length > 0 && "=+-@\t\r".IndexOf(value[0]) >= 0 && !IsNumber(value))
            {
                value = "'" + value;
            }
            bool quote = value.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0 || value != value.Trim();
            return quote ? "\"" + value.Replace("\"", "\"\"") + "\"" : value;
        }

        public static void Write(string path, string csv)
        {
            File.WriteAllText(path, csv, new UTF8Encoding(true));
        }

        public static bool SaveWithDialog(IWin32Window owner, string suggestedName, string csv)
        {
            string path;
            using (SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Export to CSV",
                Filter = "CSV file (opens in Excel) (*.csv)|*.csv",
                FileName = Clean(suggestedName),
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                OverwritePrompt = true,
            })
            {
                if (dialog.ShowDialog(owner) != DialogResult.OK)
                {
                    return false;
                }
                path = dialog.FileName;
            }
            try
            {
                Write(path, csv);
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                MessageBox.Show(owner, "Could not save the file.\n\n" + ex.Message, "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            ActivityLog.Record("Exported to CSV", Path.GetFileName(path));
            if (MessageBox.Show(owner, $"Saved {Path.GetFileName(path)}.\n\nOpen it now?", "Export to CSV", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                }
                catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is FileNotFoundException)
                {
                    MessageBox.Show(owner, "There's no app on this PC that opens CSV files.\n\n" + ex.Message, "Export to CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            return true;
        }

        private static void AppendLine(StringBuilder csv, IEnumerable<string> values)
        {
            csv.Append(string.Join(",", values.Select(Field))).Append("\r\n");
        }

        private static bool IsNumber(string value)
        {
            decimal number;
            return decimal.TryParse(value, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out number);
        }

        private static string Clean(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '-');
            }
            return name;
        }
    }
}
