using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Globalization;

namespace PurchaseInvoices
{
    internal sealed class InvoicePrintLine
    {
        public InvoicePrintLine(string number, string code, string description, decimal quantity, decimal rate, decimal amount)
        {
            Number = number ?? string.Empty;
            Code = code ?? string.Empty;
            Description = description ?? string.Empty;
            Quantity = quantity;
            Rate = rate;
            Amount = amount;
        }

        public string Number { get; }
        public string Code { get; }
        public string Description { get; }
        public decimal Quantity { get; }
        public decimal Rate { get; }
        public decimal Amount { get; }
    }

    internal sealed class InvoicePrintData
    {
        public string InvoiceNo { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsDraft { get; set; }
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public List<InvoicePrintLine> Lines { get; } = new List<InvoicePrintLine>();
    }

    internal sealed class InvoicePrintDocument : PrintDocument
    {
        private const float FirstHeaderHeight = 240f;
        private const float ContinuedHeaderHeight = 62f;
        private const float TableHeaderHeight = 30f;
        private const float RowHeight = 26f;
        private const float TotalsGap = 16f;
        private const float TotalsHeight = 92f;
        private const float FooterHeight = 36f;
        private const float CellPadding = 8f;
        private static readonly float[] ColumnWeights = { 6f, 14f, 44f, 9f, 12f, 15f };
        private static readonly string[] ColumnTitles = { "#", "ITEM CODE", "DESCRIPTION", "QTY", "RATE", "AMOUNT" };
        private static readonly bool[] RightAligned = { false, false, false, true, true, true };

        private readonly InvoicePrintData data;
        private readonly Font titleFont = new Font("Segoe UI Semibold", 20f);
        private readonly Font headingFont = new Font("Segoe UI Semibold", 12f);
        private readonly Font metaValueFont = new Font("Segoe UI Semibold", 12.5f);
        private readonly Font labelFont = new Font("Segoe UI Semibold", 7.5f);
        private readonly Font bodyFont = new Font("Segoe UI", 9.5f);
        private readonly Font bodyBoldFont = new Font("Segoe UI Semibold", 9.5f);
        private readonly Font smallFont = new Font("Segoe UI", 8f);
        private readonly Font tableHeadFont = new Font("Segoe UI Semibold", 8f);
        private readonly Font totalLabelFont = new Font("Segoe UI Semibold", 11f);
        private readonly Font totalValueFont = new Font("Segoe UI Semibold", 15f);
        private List<PagePlan> plan = new List<PagePlan>();
        private int pageIndex;
        private DateTime printedAt;

        public InvoicePrintDocument(InvoicePrintData data)
        {
            this.data = data;
            DocumentName = data.InvoiceNo.Length > 0 ? "Purchase invoice " + data.InvoiceNo : "Purchase invoice";
        }

        public int TotalPages => plan.Count;

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
            PageSettings settings = DefaultPageSettings;
            Rectangle bounds = settings.Bounds;
            Margins margins = settings.Margins;
            plan = BuildPlan(new RectangleF(margins.Left, margins.Top, bounds.Width - margins.Left - margins.Right, bounds.Height - margins.Top - margins.Bottom));
        }

        private List<PagePlan> BuildPlan(RectangleF area)
        {
            List<PagePlan> pages = new List<PagePlan>();
            float bottom = area.Bottom - FooterHeight;
            int total = data.Lines.Count;
            int index = 0;
            bool first = true;
            while (true)
            {
                float tableTop = area.Top + (first ? FirstHeaderHeight : ContinuedHeaderHeight) + TableHeaderHeight;
                int fit = Math.Max(1, (int)((bottom - tableTop) / RowHeight));
                int remaining = total - index;
                if (remaining <= fit)
                {
                    bool totalsFit = tableTop + remaining * RowHeight + TotalsGap + TotalsHeight <= bottom;
                    pages.Add(new PagePlan { First = index, Count = remaining, Totals = totalsFit });
                    if (!totalsFit)
                    {
                        pages.Add(new PagePlan { First = total, Count = 0, Totals = true });
                    }
                    return pages;
                }
                pages.Add(new PagePlan { First = index, Count = fit, Totals = false });
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
            if (page.Count > 0)
            {
                y = DrawTableHeader(g, area, y);
                for (int i = 0; i < page.Count; i++)
                {
                    y = DrawRow(g, area, y, data.Lines[page.First + i], page.First + i);
                }
            }
            if (page.Totals)
            {
                DrawTotals(g, area, y + TotalsGap);
            }
            DrawFooter(g, area);

            pageIndex++;
            e.HasMorePages = pageIndex < plan.Count;
        }

        private float DrawFirstHeader(Graphics g, RectangleF area)
        {
            float top = area.Top;
            DrawAccentBar(g, new RectangleF(area.Left, top, area.Width, 7f));

            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            {
                g.DrawString("PURCHASE INVOICE", titleFont, text, area.Left - 2f, top + 24f);
                if (data.IsDraft)
                {
                    DrawBadge(g, "DRAFT · NOT SAVED", area.Left, top + 72f);
                }

                DrawMeta(g, "INVOICE NO.", data.InvoiceNo.Length > 0 ? data.InvoiceNo : "—", area.Right, top + 24f);
                DrawMeta(g, "DATE", data.Date.ToString("dd MMM yyyy", CultureInfo.CurrentCulture), area.Right, top + 70f);

                using (Pen divider = new Pen(Theme.Border, 1.5f))
                {
                    g.DrawLine(divider, area.Left, top + 122f, area.Right, top + 122f);
                }

                g.DrawString("CUSTOMER", labelFont, muted, area.Left, top + 138f);
                RectangleF nameBox = new RectangleF(area.Left, top + 154f, area.Width * 0.62f, 24f);
                g.DrawString(data.CustomerName.Length > 0 ? data.CustomerName : "—", headingFont, text, nameBox, SingleLine(StringAlignment.Near));
                if (data.CustomerId.Length > 0)
                {
                    g.DrawString("Customer ID: " + data.CustomerId, smallFont, muted, area.Left, top + 180f);
                }
                if (data.CustomerAddress.Length > 0)
                {
                    using (StringFormat wrap = new StringFormat { Trimming = StringTrimming.EllipsisWord, FormatFlags = StringFormatFlags.LineLimit })
                    {
                        g.DrawString(data.CustomerAddress.Replace("\r\n", ", ").Replace("\n", ", "), bodyFont, text, new RectangleF(area.Left, top + 198f, area.Width * 0.62f, 36f), wrap);
                    }
                }
            }
            return top + FirstHeaderHeight;
        }

        private float DrawContinuedHeader(Graphics g, RectangleF area)
        {
            float top = area.Top;
            DrawAccentBar(g, new RectangleF(area.Left, top, area.Width, 4f));
            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (StringFormat right = SingleLine(StringAlignment.Far))
            {
                string title = data.InvoiceNo.Length > 0 ? "Purchase invoice " + data.InvoiceNo + " (continued)" : "Purchase invoice (continued)";
                g.DrawString(title, headingFont, text, area.Left, top + 16f);
                g.DrawString(data.Date.ToString("dd MMM yyyy", CultureInfo.CurrentCulture), bodyFont, muted, new RectangleF(area.Left, top + 18f, area.Width, 20f), right);
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
                for (int c = 0; c < ColumnTitles.Length; c++)
                {
                    DrawCell(g, ColumnTitles[c], tableHeadFont, text, area, c, y, TableHeaderHeight);
                }
            }
            return y + TableHeaderHeight;
        }

        private float DrawRow(Graphics g, RectangleF area, float y, InvoicePrintLine line, int index)
        {
            if (index % 2 == 1)
            {
                using (SolidBrush shade = new SolidBrush(Theme.SurfaceAlt))
                {
                    g.FillRectangle(shade, area.Left, y, area.Width, RowHeight);
                }
            }
            using (SolidBrush text = new SolidBrush(Theme.Text))
            {
                DrawCell(g, line.Number, bodyFont, text, area, 0, y, RowHeight);
                DrawCell(g, line.Code, bodyFont, text, area, 1, y, RowHeight);
                DrawCell(g, line.Description, bodyFont, text, area, 2, y, RowHeight);
                DrawCell(g, line.Quantity.ToString("0.##", CultureInfo.CurrentCulture), bodyFont, text, area, 3, y, RowHeight);
                DrawCell(g, Money(line.Rate), bodyFont, text, area, 4, y, RowHeight);
                DrawCell(g, Money(line.Amount), bodyBoldFont, text, area, 5, y, RowHeight);
            }
            using (Pen line1 = new Pen(Theme.Border, 1f))
            {
                g.DrawLine(line1, area.Left, y + RowHeight, area.Right, y + RowHeight);
            }
            return y + RowHeight;
        }

        private void DrawTotals(Graphics g, RectangleF area, float y)
        {
            float width = area.Width * 0.42f;
            float left = area.Right - width;
            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (SolidBrush accent = new SolidBrush(Theme.Accent))
            using (StringFormat leftFormat = SingleLine(StringAlignment.Near))
            using (StringFormat rightFormat = SingleLine(StringAlignment.Far))
            {
                int count = data.Lines.Count;
                g.DrawString("Line items", bodyFont, muted, new RectangleF(left, y, width, 22f), leftFormat);
                g.DrawString(count.ToString(CultureInfo.CurrentCulture), bodyFont, text, new RectangleF(left, y, width, 22f), rightFormat);

                using (Pen rule = new Pen(Theme.Accent, 1.5f))
                {
                    g.DrawLine(rule, left, y + 30f, area.Right, y + 30f);
                }
                g.DrawString("Total", totalLabelFont, text, new RectangleF(left, y + 38f, width, 34f), leftFormat);
                g.DrawString(Money(data.Total), totalValueFont, accent, new RectangleF(left, y + 36f, width, 36f), rightFormat);
            }
        }

        private void DrawFooter(Graphics g, RectangleF area)
        {
            float y = area.Bottom - FooterHeight + 10f;
            using (Pen rule = new Pen(Theme.Border, 1f))
            {
                g.DrawLine(rule, area.Left, y, area.Right, y);
            }
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (StringFormat leftFormat = SingleLine(StringAlignment.Near))
            using (StringFormat rightFormat = SingleLine(StringAlignment.Far))
            {
                RectangleF line = new RectangleF(area.Left, y + 6f, area.Width, 18f);
                g.DrawString("Printed " + printedAt.ToString("dd MMM yyyy, h:mm tt", CultureInfo.CurrentCulture), smallFont, muted, line, leftFormat);
                g.DrawString($"Page {pageIndex + 1} of {plan.Count}", smallFont, muted, line, rightFormat);
            }
        }

        private void DrawMeta(Graphics g, string label, string value, float right, float y)
        {
            using (SolidBrush muted = new SolidBrush(Theme.TextMuted))
            using (SolidBrush text = new SolidBrush(Theme.Text))
            using (StringFormat format = SingleLine(StringAlignment.Far))
            {
                g.DrawString(label, labelFont, muted, new RectangleF(right - 300f, y, 300f, 16f), format);
                g.DrawString(value, metaValueFont, text, new RectangleF(right - 300f, y + 14f, 300f, 26f), format);
            }
        }

        private void DrawBadge(Graphics g, string text, float x, float y)
        {
            SizeF size = g.MeasureString(text, labelFont);
            RectangleF pill = new RectangleF(x, y, size.Width + 18f, 20f);
            using (GraphicsPath path = Theme.RoundedRect(pill, 10f))
            using (SolidBrush fill = new SolidBrush(Color.FromArgb(254, 243, 199)))
            using (SolidBrush ink = new SolidBrush(Color.FromArgb(146, 64, 14)))
            using (StringFormat center = SingleLine(StringAlignment.Center))
            {
                g.FillPath(fill, path);
                g.DrawString(text, labelFont, ink, pill, center);
            }
        }

        private static void DrawAccentBar(Graphics g, RectangleF bar)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(bar, Theme.Accent, Theme.Accent2, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, bar);
            }
        }

        private static void DrawCell(Graphics g, string text, Font font, Brush brush, RectangleF area, int column, float y, float height)
        {
            float left = area.Left;
            for (int c = 0; c < column; c++)
            {
                left += area.Width * ColumnWeights[c] / 100f;
            }
            float width = area.Width * ColumnWeights[column] / 100f;
            RectangleF cell = new RectangleF(left + CellPadding, y, Math.Max(1f, width - CellPadding * 2), height);
            using (StringFormat format = SingleLine(RightAligned[column] ? StringAlignment.Far : StringAlignment.Near))
            {
                g.DrawString(text, font, brush, cell, format);
            }
        }

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

        private static string Money(decimal value) => value.ToString("N2", CultureInfo.CurrentCulture);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                titleFont.Dispose();
                headingFont.Dispose();
                metaValueFont.Dispose();
                labelFont.Dispose();
                bodyFont.Dispose();
                bodyBoldFont.Dispose();
                smallFont.Dispose();
                tableHeadFont.Dispose();
                totalLabelFont.Dispose();
                totalValueFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
