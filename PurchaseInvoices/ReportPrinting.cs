using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;

namespace PurchaseInvoices
{
    internal abstract class PagedPrintDocument : PrintDocument
    {
        public abstract int TotalPages { get; }
    }

    internal sealed class ReportPrintDocument : PagedPrintDocument
    {
        private const float FirstHeaderHeight = 150f;
        private const float ContinuedHeaderHeight = 44f;
        private const float TableHeaderHeight = 28f;
        private const float RowHeight = 24f;
        private const float TotalsHeight = 28f;
        private const float FooterHeight = 34f;
        private const float CellPadding = 7f;

        private readonly ReportResult report;
        private readonly CompanyProfile company;
        private readonly string printedBy;
        private readonly List<string[]> rows;
        private readonly string[] totals;
        private readonly Font titleFont = new Font("Segoe UI Semibold", 15f);
        private readonly Font companyFont = new Font("Segoe UI Semibold", 13f);
        private readonly Font headingFont = new Font("Segoe UI Semibold", 11f);
        private readonly Font labelFont = new Font("Segoe UI Semibold", 7f);
        private readonly Font statFont = new Font("Segoe UI Semibold", 11.5f);
        private readonly Font bodyFont = new Font("Segoe UI", 9f);
        private readonly Font bodyBoldFont = new Font("Segoe UI Semibold", 9f);
        private readonly Font smallFont = new Font("Segoe UI", 7.5f);
        private readonly Font tableHeadFont = new Font("Segoe UI Semibold", 7.5f);
        private List<PagePlan> plan = new List<PagePlan>();
        private int pageIndex;
        private DateTime printedAt;

        public ReportPrintDocument(ReportResult report, CompanyProfile company, string printedBy)
        {
            this.report = report;
            this.company = company ?? new CompanyProfile();
            this.printedBy = printedBy ?? string.Empty;
            rows = report.FormattedRows();
            totals = report.FormattedTotals();
            DocumentName = report.Title;
        }

        public override int TotalPages => plan.Count;

        private struct PagePlan
        {
            public int First;
            public int Count;
            public bool Totals;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);
            pageIndex = 0;
            printedAt = DateTime.Now;
            Rectangle bounds = DefaultPageSettings.Bounds;
            Margins margins = DefaultPageSettings.Margins;
            plan = BuildPlan(new RectangleF(margins.Left, margins.Top, bounds.Width - margins.Left - margins.Right, bounds.Height - margins.Top - margins.Bottom));
        }

        private List<PagePlan> BuildPlan(RectangleF area)
        {
            List<PagePlan> pages = new List<PagePlan>();
            float bottom = area.Bottom - FooterHeight;
            float totalsHeight = totals != null ? TotalsHeight : 0f;
            int index = 0;
            bool first = true;
            while (true)
            {
                float tableTop = area.Top + (first ? FirstHeaderHeight : ContinuedHeaderHeight) + TableHeaderHeight;
                int fit = Math.Max(1, (int)((bottom - tableTop) / RowHeight));
                int remaining = rows.Count - index;
                if (remaining <= fit)
                {
                    bool totalsFit = tableTop + remaining * RowHeight + totalsHeight <= bottom;
                    pages.Add(new PagePlan { First = index, Count = remaining, Totals = totals != null && totalsFit });
                    if (totals != null && !totalsFit)
                    {
                        pages.Add(new PagePlan { First = rows.Count, Count = 0, Totals = true });
                    }
                    return pages;
                }
                pages.Add(new PagePlan { First = index, Count = fit });
                index += fit;
                first = false;
            }
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            RectangleF area = e.MarginBounds;
            PagePlan page = plan[pageIndex];

            float y = pageIndex == 0 ? DrawFirstHeader(g, area) : DrawContinuedHeader(g, area);
            y = DrawTableHeader(g, area, y);
            if (rows.Count == 0 && pageIndex == 0)
            {
                using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
                using (StringFormat center = SingleLine(StringAlignment.Center))
                {
                    g.DrawString("Nothing to show for this period.", bodyFont, muted, new RectangleF(area.Left, y + 8f, area.Width, RowHeight), center);
                }
            }
            for (int i = 0; i < page.Count; i++)
            {
                y = DrawRow(g, area, y, rows[page.First + i], page.First + i, false);
            }
            if (page.Totals)
            {
                DrawRow(g, area, y, totals, -1, true);
            }
            DrawFooter(g, area);
            pageIndex++;
            e.HasMorePages = pageIndex < plan.Count;
        }

        private float DrawFirstHeader(Graphics g, RectangleF area)
        {
            float top = area.Top;
            Accent(g, new RectangleF(area.Left, top, area.Width, 6f));
            float logo = 40f;
            InterpolationMode interpolation = g.InterpolationMode;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(Theme.Logo, area.Left, top + 20f, logo, logo);
            g.InterpolationMode = interpolation;

            string name = string.IsNullOrWhiteSpace(company.Name) ? AppInfo.ProductName : company.Name.Trim();
            float leftWidth = area.Width * 0.5f - logo - 10f;
            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (StringFormat left = SingleLine(StringAlignment.Near))
            using (StringFormat right = SingleLine(StringAlignment.Far))
            {
                g.DrawString(name, companyFont, text, new RectangleF(area.Left + logo + 10f, top + 18f, leftWidth, 24f), left);
                string contact = string.Join("  ·  ", new[] { company.Phone, company.Email, company.TaxNumber.Length > 0 ? "Tax no. " + company.TaxNumber : string.Empty }
                    .Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim()));
                g.DrawString(OneLine(company.Address), smallFont, muted, new RectangleF(area.Left + logo + 10f, top + 42f, leftWidth, 15f), left);
                g.DrawString(contact, smallFont, muted, new RectangleF(area.Left + logo + 10f, top + 57f, leftWidth, 15f), left);

                RectangleF rightBox = new RectangleF(area.Left + area.Width * 0.45f, top + 16f, area.Width * 0.55f, 28f);
                g.DrawString(report.Title, titleFont, text, rightBox, right);
                g.DrawString(report.Description, smallFont, muted, new RectangleF(rightBox.X, top + 46f, rightBox.Width, 16f), right);
                string generated = "Generated " + report.GeneratedAt.ToString("dd MMM yyyy, h:mm tt", CultureInfo.CurrentCulture) + (printedBy.Length > 0 ? " by " + printedBy : string.Empty);
                g.DrawString(generated, smallFont, muted, new RectangleF(rightBox.X, top + 62f, rightBox.Width, 16f), right);

                float statsTop = top + 90f;
                using (SolidBrush fill = new SolidBrush(Theme.SurfaceAlt))
                using (Pen border = new Pen(Theme.Border, 1f))
                {
                    g.FillRectangle(fill, area.Left, statsTop, area.Width, 46f);
                    g.DrawRectangle(border, area.Left, statsTop, area.Width, 46f);
                }
                string[] labels = { "TOTAL PURCHASES", "INVOICES", "AVERAGE INVOICE", report.HighlightLabel ?? string.Empty };
                string[] values =
                {
                    report.Total.ToString("N2", CultureInfo.CurrentCulture),
                    report.Invoices.ToString("N0", CultureInfo.CurrentCulture),
                    report.Average.ToString("N2", CultureInfo.CurrentCulture),
                    report.HighlightValue ?? string.Empty,
                };
                float cell = area.Width / 4f;
                for (int i = 0; i < 4; i++)
                {
                    float x = area.Left + cell * i + 12f;
                    g.DrawString(labels[i], labelFont, muted, new RectangleF(x, statsTop + 7f, cell - 20f, 12f), left);
                    g.DrawString(values[i], statFont, text, new RectangleF(x, statsTop + 20f, cell - 20f, 22f), left);
                }
            }
            return top + FirstHeaderHeight;
        }

        private float DrawContinuedHeader(Graphics g, RectangleF area)
        {
            float top = area.Top;
            Accent(g, new RectangleF(area.Left, top, area.Width, 4f));
            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (StringFormat left = SingleLine(StringAlignment.Near))
            using (StringFormat right = SingleLine(StringAlignment.Far))
            {
                g.DrawString(report.Title + " (continued)", headingFont, text, new RectangleF(area.Left, top + 12f, area.Width * 0.6f, 22f), left);
                g.DrawString(report.Description, smallFont, muted, new RectangleF(area.Left + area.Width * 0.4f, top + 15f, area.Width * 0.6f, 18f), right);
            }
            return top + ContinuedHeaderHeight;
        }

        private float DrawTableHeader(Graphics g, RectangleF area, float y)
        {
            using (SolidBrush fill = new SolidBrush(Theme.AccentSoft))
            {
                g.FillRectangle(fill, area.Left, y, area.Width, TableHeaderHeight);
            }
            using (SolidBrush text = new SolidBrush(Theme.AccentText))
            {
                for (int c = 0; c < report.Columns.Count; c++)
                {
                    Cell(g, report.Columns[c].Header.ToUpper(CultureInfo.CurrentCulture), tableHeadFont, text, area, c, y, TableHeaderHeight);
                }
            }
            return y + TableHeaderHeight;
        }

        private float DrawRow(Graphics g, RectangleF area, float y, string[] values, int index, bool isTotal)
        {
            float height = isTotal ? TotalsHeight : RowHeight;
            if (isTotal || index % 2 == 1)
            {
                using (SolidBrush shade = new SolidBrush(isTotal ? Theme.AccentSoft : Theme.SurfaceAlt))
                {
                    g.FillRectangle(shade, area.Left, y, area.Width, height);
                }
            }
            using (SolidBrush text = new SolidBrush(isTotal ? Theme.AccentText : Theme.Text))
            {
                for (int c = 0; c < report.Columns.Count; c++)
                {
                    Cell(g, values[c], isTotal || report.Columns[c].Name == "TOTAL" ? bodyBoldFont : bodyFont, text, area, c, y, height);
                }
            }
            using (Pen rule = new Pen(isTotal ? Theme.Accent : Theme.Border, isTotal ? 1.5f : 1f))
            {
                g.DrawLine(rule, area.Left, isTotal ? y : y + height, area.Right, isTotal ? y : y + height);
            }
            return y + height;
        }

        private void DrawFooter(Graphics g, RectangleF area)
        {
            float y = area.Bottom - FooterHeight + 10f;
            using (Pen rule = new Pen(Theme.Border, 1f))
            {
                g.DrawLine(rule, area.Left, y, area.Right, y);
            }
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (StringFormat left = SingleLine(StringAlignment.Near))
            using (StringFormat right = SingleLine(StringAlignment.Far))
            {
                RectangleF line = new RectangleF(area.Left, y + 5f, area.Width, 16f);
                string printed = "Printed " + printedAt.ToString("dd MMM yyyy, h:mm tt", CultureInfo.CurrentCulture) + (printedBy.Length > 0 ? " by " + printedBy : string.Empty);
                g.DrawString(printed, smallFont, muted, line, left);
                g.DrawString($"Page {pageIndex + 1} of {plan.Count}", smallFont, muted, line, right);
            }
        }

        private void Cell(Graphics g, string text, Font font, Brush brush, RectangleF area, int column, float y, float height)
        {
            float totalWeight = report.Columns.Sum(c => c.Weight);
            float left = area.Left;
            for (int c = 0; c < column; c++)
            {
                left += area.Width * report.Columns[c].Weight / totalWeight;
            }
            float width = area.Width * report.Columns[column].Weight / totalWeight;
            RectangleF box = new RectangleF(left + CellPadding, y, Math.Max(1f, width - CellPadding * 2), height);
            using (StringFormat format = SingleLine(report.Columns[column].RightAligned ? StringAlignment.Far : StringAlignment.Near))
            {
                g.DrawString(text ?? string.Empty, font, brush, box, format);
            }
        }

        private static void Accent(Graphics g, RectangleF bar)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(bar, Theme.Accent, Theme.Accent2, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, bar);
            }
        }

        private static string OneLine(string value) => string.Join(", ", (value ?? string.Empty).Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).Where(s => s.Length > 0));

        private static StringFormat SingleLine(StringAlignment alignment)
        {
            StringFormat format = new StringFormat(StringFormat.GenericTypographic)
            {
                Alignment = alignment,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter,
            };
            format.FormatFlags |= StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
            return format;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (Font font in new[] { titleFont, companyFont, headingFont, labelFont, statFont, bodyFont, bodyBoldFont, smallFont, tableHeadFont })
                {
                    font.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
