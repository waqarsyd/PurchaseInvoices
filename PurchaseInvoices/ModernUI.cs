using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal static class Theme
    {
        public static readonly Color Canvas = Color.FromArgb(243, 245, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color SurfaceAlt = Color.FromArgb(250, 251, 254);
        public static readonly Color ReadOnlyFill = Color.FromArgb(246, 248, 251);
        public static readonly Color Border = Color.FromArgb(222, 228, 237);
        public static readonly Color BorderHover = Color.FromArgb(190, 200, 216);
        public static readonly Color CardBorder = Color.FromArgb(230, 234, 242);
        public static readonly Color Text = Color.FromArgb(15, 23, 42);
        public static readonly Color TextSecondary = Color.FromArgb(71, 85, 105);
        public static readonly Color TextMuted = Color.FromArgb(100, 116, 139);
        public static readonly Color TextFaint = Color.FromArgb(148, 163, 184);
        public static readonly Color Accent = Color.FromArgb(79, 70, 229);
        public static readonly Color AccentHover = Color.FromArgb(99, 91, 240);
        public static readonly Color AccentPressed = Color.FromArgb(67, 56, 202);
        public static readonly Color Accent2 = Color.FromArgb(124, 58, 237);
        public static readonly Color Accent2Hover = Color.FromArgb(139, 92, 246);
        public static readonly Color Accent2Pressed = Color.FromArgb(109, 40, 217);
        public static readonly Color AccentSoft = Color.FromArgb(238, 242, 255);
        public static readonly Color AccentSoftHover = Color.FromArgb(224, 231, 255);
        public static readonly Color AccentSoftPressed = Color.FromArgb(199, 210, 254);
        public static readonly Color AccentText = Color.FromArgb(55, 48, 163);
        public static readonly Color HeaderStart = Color.FromArgb(15, 23, 42);
        public static readonly Color HeaderEnd = Color.FromArgb(46, 38, 120);
        public static readonly Color Shadow = Color.FromArgb(15, 23, 42);
        public const string IconFontName = "Segoe MDL2 Assets";
        public const TextFormatFlags KeepTransform = TextFormatFlags.PreserveGraphicsTranslateTransform | TextFormatFlags.PreserveGraphicsClipping;

        private static float scale;
        private static int iconFontState;
        private static Icon appIcon;

        public static float Scale
        {
            get
            {
                if (scale <= 0)
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        scale = g.DpiX / 96f;
                    }
                }
                return scale;
            }
        }

        public static int S(float value) => (int)Math.Round(value * Scale);

        public static bool HasIconFont
        {
            get
            {
                if (iconFontState == 0)
                {
                    iconFontState = FontFamily.Families.Any(f => f.Name == IconFontName) ? 1 : 2;
                }
                return iconFontState == 1;
            }
        }

        public static Font IconFont(float points) => HasIconFont ? new Font(IconFontName, points) : null;

        public static GraphicsPath RoundedRect(RectangleF r, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = Math.Min(radius * 2, Math.Min(r.Width, r.Height));
            if (d <= 1)
            {
                path.AddRectangle(r);
                return path;
            }
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        public static void FillShadow(Graphics g, Rectangle card, float radius, int size, Color color, int strength)
        {
            if (size <= 0)
            {
                return;
            }
            int alpha = Math.Max(1, strength / size);
            for (int i = size; i >= 1; i--)
            {
                RectangleF r = new RectangleF(card.X - i, card.Y - i + S(2), card.Width + 2 * i, card.Height + 2 * i);
                using (GraphicsPath p = RoundedRect(r, radius + i))
                using (SolidBrush b = new SolidBrush(Color.FromArgb(alpha, color)))
                {
                    g.FillPath(b, p);
                }
            }
        }

        public static void DrawGlyph(Graphics g, string glyph, float points, Rectangle bounds, Color color, TextFormatFlags align)
        {
            using (Font font = IconFont(points))
            {
                if (font != null && !string.IsNullOrEmpty(glyph))
                {
                    TextRenderer.DrawText(g, glyph, font, bounds, color, align | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | KeepTransform);
                }
            }
        }

        public static void PaintParent(Control control, Graphics g, Action<Control, PaintEventArgs> paintBackground, Action<Control, PaintEventArgs> paint)
        {
            Control parent = control.Parent;
            if (parent == null)
            {
                g.Clear(Surface);
                return;
            }
            GraphicsState state = g.Save();
            g.TranslateTransform(-control.Left, -control.Top);
            PaintEventArgs pe = new PaintEventArgs(g, new Rectangle(control.Left, control.Top, control.Width, control.Height));
            paintBackground(parent, pe);
            paint(parent, pe);
            g.Restore(state);
        }

        public static void StyleWindow(Form form)
        {
            form.Icon = AppIcon;
            form.HandleCreated += (s, e) => ApplyCaptionColors((Form)s);
            form.Load += (s, e) => FitToWorkingArea((Form)s);
            if (form.IsHandleCreated)
            {
                ApplyCaptionColors(form);
            }
        }

        public static Icon AppIcon
        {
            get
            {
                if (appIcon == null)
                {
                    using (Bitmap bmp = new Bitmap(64, 64))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.SmoothingMode = SmoothingMode.AntiAlias;
                            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            using (GraphicsPath p = RoundedRect(new RectangleF(2, 2, 60, 60), 15))
                            using (LinearGradientBrush b = new LinearGradientBrush(new Rectangle(0, 0, 64, 64), Accent, Accent2, 45f))
                            {
                                g.FillPath(b, p);
                            }
                            if (HasIconFont)
                            {
                                using (Font f = new Font(IconFontName, 30f, GraphicsUnit.Pixel))
                                using (StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center })
                                {
                                    g.DrawString("", f, Brushes.White, new RectangleF(0, 2, 64, 64), sf);
                                }
                            }
                        }
                        appIcon = Icon.FromHandle(bmp.GetHicon());
                    }
                }
                return appIcon;
            }
        }

        private static void FitToWorkingArea(Form form)
        {
            Rectangle area = Screen.FromPoint(Cursor.Position).WorkingArea;
            int margin = S(12);
            int maxWidth = area.Width - margin * 2;
            int maxHeight = area.Height - margin * 2;
            form.MinimumSize = new Size(Math.Min(S(940), maxWidth), Math.Min(S(660), maxHeight));
            int width = Math.Min(form.Width, maxWidth);
            int height = Math.Min(form.Height, maxHeight);
            form.Bounds = new Rectangle(area.X + (area.Width - width) / 2, area.Y + (area.Height - height) / 2, width, height);
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

        private static void ApplyCaptionColors(Form form)
        {
            try
            {
                SetWindowAttribute(form.Handle, 20, 1);
                SetWindowAttribute(form.Handle, 34, ColorRef(HeaderStart));
                SetWindowAttribute(form.Handle, 35, ColorRef(HeaderStart));
                SetWindowAttribute(form.Handle, 36, ColorRef(Color.White));
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }

        private static void SetWindowAttribute(IntPtr handle, int attribute, int value)
        {
            DwmSetWindowAttribute(handle, attribute, ref value, sizeof(int));
        }

        private static int ColorRef(Color c) => c.R | (c.G << 8) | (c.B << 16);
    }

    public enum ButtonKind
    {
        Primary,
        Soft,
        Secondary,
        Ghost
    }

    public class ModernButton : Button
    {
        private ButtonKind kind = ButtonKind.Secondary;
        private string glyph = string.Empty;
        private bool glyphAfterText;
        private bool hovering;
        private bool pressing;

        public ModernButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
        }

        [DefaultValue(ButtonKind.Secondary)]
        public ButtonKind Kind
        {
            get { return kind; }
            set { kind = value; Invalidate(); }
        }

        [DefaultValue("")]
        public string Glyph
        {
            get { return glyph; }
            set { glyph = value ?? string.Empty; Invalidate(); }
        }

        [DefaultValue(false)]
        public bool GlyphAfterText
        {
            get { return glyphAfterText; }
            set { glyphAfterText = value; Invalidate(); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            hovering = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            hovering = false;
            pressing = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                pressing = true;
                Invalidate();
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            pressing = false;
            Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            hovering = false;
            pressing = false;
            base.OnEnabledChanged(e);
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            Theme.PaintParent(this, g, InvokePaintBackground, InvokePaint);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF r = new RectangleF(0.5f, 0.5f, Width - 1.5f, Height - 1.5f);
            Color textColor;
            using (GraphicsPath path = Theme.RoundedRect(r, Theme.S(8)))
            {
                switch (kind)
                {
                    case ButtonKind.Primary:
                        if (Enabled)
                        {
                            Color from = pressing ? Theme.AccentPressed : hovering ? Theme.AccentHover : Theme.Accent;
                            Color to = pressing ? Theme.Accent2Pressed : hovering ? Theme.Accent2Hover : Theme.Accent2;
                            using (LinearGradientBrush brush = new LinearGradientBrush(new RectangleF(0, 0, Width, Height), from, to, LinearGradientMode.Horizontal))
                            {
                                g.FillPath(brush, path);
                            }
                            textColor = Color.White;
                        }
                        else
                        {
                            Fill(g, path, Theme.AccentSoftHover);
                            textColor = Color.FromArgb(165, 180, 252);
                        }
                        break;
                    case ButtonKind.Soft:
                        Fill(g, path, !Enabled ? Theme.ReadOnlyFill : pressing ? Theme.AccentSoftPressed : hovering ? Theme.AccentSoftHover : Theme.AccentSoft);
                        textColor = Enabled ? Theme.AccentText : Theme.TextFaint;
                        break;
                    case ButtonKind.Ghost:
                        Fill(g, path, Color.FromArgb(!Enabled ? 8 : pressing ? 64 : hovering ? 44 : 24, 255, 255, 255));
                        Stroke(g, path, Color.FromArgb(Enabled ? 52 : 22, 255, 255, 255));
                        textColor = Enabled ? Color.White : Color.FromArgb(115, 128, 160);
                        break;
                    default:
                        Fill(g, path, !Enabled ? Theme.ReadOnlyFill : pressing ? Color.FromArgb(241, 245, 249) : hovering ? Theme.SurfaceAlt : Theme.Surface);
                        Stroke(g, path, Enabled && hovering ? Theme.BorderHover : Theme.Border);
                        textColor = Enabled ? Theme.Text : Theme.TextFaint;
                        break;
                }

                if (Focused && ShowFocusCues && Enabled)
                {
                    RectangleF ring = RectangleF.Inflate(r, -2.5f, -2.5f);
                    using (GraphicsPath ringPath = Theme.RoundedRect(ring, Theme.S(6)))
                    using (Pen pen = new Pen(kind == ButtonKind.Primary || kind == ButtonKind.Ghost ? Color.FromArgb(210, 255, 255, 255) : Theme.Accent, 1.5f))
                    {
                        g.DrawPath(pen, ringPath);
                    }
                }
            }

            DrawContent(g, textColor);
        }

        private static void Fill(Graphics g, GraphicsPath path, Color color)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.FillPath(brush, path);
            }
        }

        private static void Stroke(Graphics g, GraphicsPath path, Color color)
        {
            using (Pen pen = new Pen(color, 1f))
            {
                g.DrawPath(pen, path);
            }
        }

        private void DrawContent(Graphics g, Color color)
        {
            const TextFormatFlags flags = TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.NoPrefix | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
            Size textSize = TextRenderer.MeasureText(g, Text, Font, Size.Empty, flags);
            Font icon = string.IsNullOrEmpty(glyph) ? null : Theme.IconFont(Font.SizeInPoints);
            try
            {
                Size iconSize = icon == null ? Size.Empty : TextRenderer.MeasureText(g, glyph, icon, Size.Empty, flags);
                int gap = icon != null && Text.Length > 0 ? Theme.S(8) : 0;
                int x = (Width - (iconSize.Width + gap + textSize.Width)) / 2;
                int textX = glyphAfterText ? x : x + iconSize.Width + gap;
                int iconX = glyphAfterText ? x + textSize.Width + gap : x;
                TextRenderer.DrawText(g, Text, Font, new Rectangle(textX, 0, textSize.Width + 2, Height), color, flags);
                if (icon != null)
                {
                    TextRenderer.DrawText(g, glyph, icon, new Rectangle(iconX, Theme.S(1), iconSize.Width + 2, Height), color, flags);
                }
            }
            finally
            {
                icon?.Dispose();
            }
        }
    }

    public class FieldHost : Panel
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private TextBox box;
        private string placeholder = string.Empty;
        private string glyph = string.Empty;

        public FieldHost()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.IBeam;
        }

        [DefaultValue("")]
        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value ?? string.Empty; ApplyPlaceholder(); }
        }

        [DefaultValue("")]
        public string Glyph
        {
            get { return glyph; }
            set { glyph = value ?? string.Empty; PerformLayout(); Invalidate(); }
        }

        private bool IsReadOnly => box != null && (box.ReadOnly || !box.Enabled);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            TextBox textBox = e.Control as TextBox;
            if (box != null || textBox == null)
            {
                return;
            }
            box = textBox;
            box.BorderStyle = BorderStyle.None;
            box.GotFocus += BoxStateChanged;
            box.LostFocus += BoxStateChanged;
            box.ReadOnlyChanged += BoxStateChanged;
            box.EnabledChanged += BoxStateChanged;
            box.HandleCreated += (s, a) => ApplyPlaceholder();
            SyncColors();
            PerformLayout();
        }

        private void BoxStateChanged(object sender, EventArgs e)
        {
            SyncColors();
            Invalidate();
        }

        private void SyncColors()
        {
            if (box == null)
            {
                return;
            }
            box.BackColor = IsReadOnly ? Theme.ReadOnlyFill : Theme.Surface;
            box.ForeColor = Theme.Text;
        }

        private void ApplyPlaceholder()
        {
            if (box != null && box.IsHandleCreated && !box.Multiline)
            {
                SendMessage(box.Handle, EM_SETCUEBANNER, (IntPtr)1, placeholder);
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (box == null)
            {
                return;
            }
            int left = Theme.S(string.IsNullOrEmpty(glyph) ? 12 : 36);
            int right = Theme.S(12);
            if (box.Multiline)
            {
                box.SetBounds(left, Theme.S(8), Math.Max(1, Width - left - right), Math.Max(1, Height - Theme.S(14)));
            }
            else
            {
                int height = box.PreferredHeight;
                box.SetBounds(left, (Height - height) / 2, Math.Max(1, Width - left - right), height);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            box?.Focus();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Theme.PaintParent(this, g, InvokePaintBackground, InvokePaint);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            bool focused = box != null && box.Focused && !IsReadOnly;
            float penWidth = focused ? Math.Max(1.5f, 1.5f * Theme.Scale) : 1f;
            RectangleF r = new RectangleF(penWidth / 2f, penWidth / 2f, Width - penWidth - 0.5f, Height - penWidth - 0.5f);
            using (GraphicsPath path = Theme.RoundedRect(r, Theme.S(8)))
            {
                using (SolidBrush fill = new SolidBrush(IsReadOnly ? Theme.ReadOnlyFill : Theme.Surface))
                {
                    g.FillPath(fill, path);
                }
                using (Pen pen = new Pen(focused ? Theme.Accent : Theme.Border, penWidth))
                {
                    g.DrawPath(pen, path);
                }
            }

            if (!string.IsNullOrEmpty(glyph))
            {
                Theme.DrawGlyph(g, glyph, 10f, new Rectangle(Theme.S(12), 0, Theme.S(20), Height), Theme.TextFaint, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }
        }
    }

    public class ModernComboBox : ComboBox
    {
        private const int WM_PAINT = 0x000F;
        private bool hovering;
        private string placeholder = "Select…";

        public ModernComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            FlatStyle = FlatStyle.Flat;
            IntegralHeight = false;
            MaxDropDownItems = 10;
            BackColor = Theme.Surface;
            ForeColor = Theme.Text;
            Cursor = Cursors.Hand;
        }

        [DefaultValue("Select…")]
        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value ?? string.Empty; Invalidate(); }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new int ItemHeight
        {
            get { return base.ItemHeight; }
            set { base.ItemHeight = value; }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            base.ItemHeight = Math.Max(Font.Height + 4, Theme.S(36) - 6);
        }

        protected override void OnMouseEnter(EventArgs e) { hovering = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hovering = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnGotFocus(EventArgs e) { base.OnGotFocus(e); Invalidate(); }
        protected override void OnLostFocus(EventArgs e) { base.OnLostFocus(e); Invalidate(); }
        protected override void OnDropDownClosed(EventArgs e) { base.OnDropDownClosed(e); Invalidate(); }
        protected override void OnEnabledChanged(EventArgs e) { base.OnEnabledChanged(e); Invalidate(); }
        protected override void OnSelectedIndexChanged(EventArgs e) { base.OnSelectedIndexChanged(e); Invalidate(); }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || (e.State & DrawItemState.ComboBoxEdit) != 0)
            {
                return;
            }
            bool selected = (e.State & DrawItemState.Selected) != 0;
            using (SolidBrush brush = new SolidBrush(selected ? Theme.AccentSoft : Theme.Surface))
            {
                e.Graphics.FillRectangle(brush, e.Bounds);
            }
            Rectangle textBounds = new Rectangle(e.Bounds.X + Theme.S(12), e.Bounds.Y, e.Bounds.Width - Theme.S(16), e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, textBounds, selected ? Theme.AccentText : Theme.Text,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT && IsHandleCreated)
            {
                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    PaintFace(g);
                }
            }
        }

        private void PaintFace(Graphics g)
        {
            Rectangle bounds = ClientRectangle;
            if (bounds.Width <= 0 || bounds.Height <= 0)
            {
                return;
            }
            using (BufferedGraphics buffer = BufferedGraphicsManager.Current.Allocate(g, bounds))
            {
                Graphics bg = buffer.Graphics;
                bg.Clear(Parent?.BackColor ?? Theme.Surface);
                bg.SmoothingMode = SmoothingMode.AntiAlias;

                bool readOnly = !Enabled;
                bool active = !readOnly && (Focused || DroppedDown);
                float penWidth = active ? Math.Max(1.5f, 1.5f * Theme.Scale) : 1f;
                RectangleF r = new RectangleF(penWidth / 2f, penWidth / 2f, bounds.Width - penWidth - 0.5f, bounds.Height - penWidth - 0.5f);
                using (GraphicsPath path = Theme.RoundedRect(r, Theme.S(8)))
                {
                    using (SolidBrush fill = new SolidBrush(readOnly ? Theme.ReadOnlyFill : Theme.Surface))
                    {
                        bg.FillPath(fill, path);
                    }
                    using (Pen pen = new Pen(active ? Theme.Accent : hovering && !readOnly ? Theme.BorderHover : Theme.Border, penWidth))
                    {
                        bg.DrawPath(pen, path);
                    }
                }

                bool hasValue = SelectedIndex >= 0;
                string text = hasValue ? GetItemText(SelectedItem) : readOnly ? string.Empty : placeholder;
                int arrowWidth = readOnly ? 0 : Theme.S(30);
                Rectangle textBounds = new Rectangle(Theme.S(12), 0, bounds.Width - Theme.S(12) - arrowWidth - Theme.S(4), bounds.Height);
                TextRenderer.DrawText(bg, text, Font, textBounds, hasValue ? Theme.Text : Theme.TextFaint,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix | TextFormatFlags.SingleLine);

                if (!readOnly)
                {
                    Theme.DrawGlyph(bg, "", 8f, new Rectangle(bounds.Width - arrowWidth, Theme.S(1), arrowWidth - Theme.S(8), bounds.Height),
                        active ? Theme.Accent : Theme.TextMuted, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
                }
                buffer.Render(g);
            }
        }
    }

    public class CardPanel : Panel
    {
        private string title = string.Empty;
        private string glyph = string.Empty;

        public CardPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Surface;
        }

        public static int ShadowSize => Theme.S(6);
        public static float Radius => Theme.S(14);

        [DefaultValue("")]
        public string Title
        {
            get { return title; }
            set { title = value ?? string.Empty; Invalidate(); }
        }

        [DefaultValue("")]
        public string Glyph
        {
            get { return glyph; }
            set { glyph = value ?? string.Empty; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Theme.Canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int inset = ShadowSize;
            Rectangle card = new Rectangle(inset, inset, Width - inset * 2, Height - inset * 2);
            if (card.Width <= 0 || card.Height <= 0)
            {
                return;
            }
            Theme.FillShadow(g, card, Radius, inset, Theme.Shadow, 30);
            using (GraphicsPath path = Theme.RoundedRect(new RectangleF(card.X + 0.5f, card.Y + 0.5f, card.Width - 1f, card.Height - 1f), Radius))
            {
                using (SolidBrush fill = new SolidBrush(Theme.Surface))
                {
                    g.FillPath(fill, path);
                }
                using (Pen pen = new Pen(Theme.CardBorder, 1f))
                {
                    g.DrawPath(pen, path);
                }
            }

            if (title.Length > 0)
            {
                int x = card.X + Theme.S(18);
                Rectangle line = new Rectangle(x, card.Y + Theme.S(12), card.Width, Theme.S(22));
                if (glyph.Length > 0 && Theme.HasIconFont)
                {
                    Rectangle chip = new Rectangle(x, line.Y, Theme.S(22), Theme.S(22));
                    using (GraphicsPath chipPath = Theme.RoundedRect(chip, Theme.S(6)))
                    using (SolidBrush chipFill = new SolidBrush(Theme.AccentSoft))
                    {
                        g.FillPath(chipFill, chipPath);
                    }
                    Theme.DrawGlyph(g, glyph, 8.5f, new Rectangle(chip.X, chip.Y + Theme.S(1), chip.Width, chip.Height), Theme.Accent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    line.X = chip.Right + Theme.S(10);
                }
                using (Font font = new Font("Segoe UI Semibold", 9.5f))
                {
                    TextRenderer.DrawText(g, title, font, line, Theme.Text, TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | Theme.KeepTransform);
                }
            }
        }
    }

    public class HeaderPanel : Panel
    {
        private string title = string.Empty;
        private string subtitle = string.Empty;
        private string glyph = string.Empty;

        public HeaderPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.HeaderStart;
            ForeColor = Color.White;
        }

        [DefaultValue("")]
        public string Title { get { return title; } set { title = value ?? string.Empty; Invalidate(); } }

        [DefaultValue("")]
        public string Subtitle { get { return subtitle; } set { subtitle = value ?? string.Empty; Invalidate(); } }

        [DefaultValue("")]
        public string Glyph { get { return glyph; } set { glyph = value ?? string.Empty; Invalidate(); } }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (Width <= 0 || Height <= 0)
            {
                return;
            }
            using (LinearGradientBrush brush = new LinearGradientBrush(ClientRectangle, Theme.HeaderStart, Theme.HeaderEnd, LinearGradientMode.Horizontal))
            {
                g.FillRectangle(brush, ClientRectangle);
            }
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Glow(g, new Rectangle(Width - Theme.S(520), -Theme.S(170), Theme.S(560), Theme.S(340)), Color.FromArgb(80, Theme.Accent2));
            Glow(g, new Rectangle(Width / 3, Height / 2, Theme.S(420), Theme.S(200)), Color.FromArgb(36, Theme.Accent));

            int tileSize = Theme.S(40);
            Rectangle tile = new Rectangle(Theme.S(20), (Height - tileSize) / 2, tileSize, tileSize);
            using (GraphicsPath path = Theme.RoundedRect(tile, Theme.S(11)))
            using (LinearGradientBrush tileBrush = new LinearGradientBrush(tile, Theme.Accent, Theme.Accent2, 45f))
            {
                g.FillPath(tileBrush, path);
            }
            Theme.DrawGlyph(g, glyph, 14f, new Rectangle(tile.X, tile.Y + Theme.S(1), tile.Width, tile.Height), Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            int textX = tile.Right + Theme.S(14);
            using (Font titleFont = new Font("Segoe UI Semibold", 14f))
            using (Font subtitleFont = new Font("Segoe UI", 9f))
            {
                int titleHeight = TextRenderer.MeasureText(g, title, titleFont, Size.Empty, TextFormatFlags.NoPadding).Height;
                int subtitleHeight = TextRenderer.MeasureText(g, subtitle, subtitleFont, Size.Empty, TextFormatFlags.NoPadding).Height;
                int top = (Height - titleHeight - subtitleHeight) / 2;
                TextRenderer.DrawText(g, title, titleFont, new Point(textX, top), Color.White, TextFormatFlags.NoPadding | Theme.KeepTransform);
                TextRenderer.DrawText(g, subtitle, subtitleFont, new Point(textX + Theme.S(1), top + titleHeight), Color.FromArgb(165, 180, 252), TextFormatFlags.NoPadding | Theme.KeepTransform);
            }
        }

        private static void Glow(Graphics g, Rectangle bounds, Color center)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(bounds);
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    brush.CenterColor = center;
                    brush.SurroundColors = new[] { Color.FromArgb(0, center) };
                    g.FillPath(brush, path);
                }
            }
        }
    }

    public class FooterPanel : Panel
    {
        public FooterPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            BackColor = Theme.Surface;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);
            using (Pen pen = new Pen(Theme.Border, 1f))
            {
                e.Graphics.DrawLine(pen, 0, 0, Width, 0);
            }
        }
    }

    public class SummaryCard : Control
    {
        private TextBox totalSource;
        private TextBox invoiceSource;
        private TextBox customerSource;
        private DataGridView itemsSource;
        private Control modeSource;

        public SummaryCard()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
        }

        [DefaultValue(null)]
        public TextBox TotalSource
        {
            get { return totalSource; }
            set
            {
                if (totalSource != null) totalSource.TextChanged -= SourceChanged;
                totalSource = value;
                if (totalSource != null) totalSource.TextChanged += SourceChanged;
                Invalidate();
            }
        }

        [DefaultValue(null)]
        public TextBox InvoiceSource
        {
            get { return invoiceSource; }
            set
            {
                if (invoiceSource != null) invoiceSource.TextChanged -= SourceChanged;
                invoiceSource = value;
                if (invoiceSource != null) invoiceSource.TextChanged += SourceChanged;
                Invalidate();
            }
        }

        [DefaultValue(null)]
        public TextBox CustomerSource
        {
            get { return customerSource; }
            set
            {
                if (customerSource != null) customerSource.TextChanged -= SourceChanged;
                customerSource = value;
                if (customerSource != null) customerSource.TextChanged += SourceChanged;
                Invalidate();
            }
        }

        [DefaultValue(null)]
        public DataGridView ItemsSource
        {
            get { return itemsSource; }
            set
            {
                if (itemsSource != null)
                {
                    itemsSource.RowsAdded -= RowsAdded;
                    itemsSource.RowsRemoved -= RowsRemoved;
                }
                itemsSource = value;
                if (itemsSource != null)
                {
                    itemsSource.RowsAdded += RowsAdded;
                    itemsSource.RowsRemoved += RowsRemoved;
                }
                Invalidate();
            }
        }

        [DefaultValue(null)]
        public Control ModeSource
        {
            get { return modeSource; }
            set
            {
                if (modeSource != null) modeSource.EnabledChanged -= SourceChanged;
                modeSource = value;
                if (modeSource != null) modeSource.EnabledChanged += SourceChanged;
                Invalidate();
            }
        }

        private void SourceChanged(object sender, EventArgs e) => Invalidate();
        private void RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) => Invalidate();
        private void RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => Invalidate();

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent?.BackColor ?? Theme.Canvas);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int inset = CardPanel.ShadowSize;
            Rectangle card = new Rectangle(inset, inset, Width - inset * 2, Height - inset * 2);
            if (card.Width <= 0 || card.Height <= 0)
            {
                return;
            }
            Theme.FillShadow(g, card, CardPanel.Radius, inset, Theme.Accent, 60);
            using (GraphicsPath path = Theme.RoundedRect(card, CardPanel.Radius))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(card, Theme.Accent, Theme.Accent2, 35f))
                {
                    g.FillPath(brush, path);
                }
                GraphicsState state = g.Save();
                g.SetClip(path);
                using (SolidBrush ring = new SolidBrush(Color.FromArgb(26, 255, 255, 255)))
                {
                    g.FillEllipse(ring, card.Right - Theme.S(110), card.Y - Theme.S(70), Theme.S(200), Theme.S(200));
                }
                using (SolidBrush ring = new SolidBrush(Color.FromArgb(16, 255, 255, 255)))
                {
                    g.FillEllipse(ring, card.Right - Theme.S(40), card.Bottom - Theme.S(90), Theme.S(160), Theme.S(160));
                }
                g.Restore(state);
            }

            int left = card.X + Theme.S(22);
            int right = card.Right - Theme.S(20);
            using (Font caption = new Font("Segoe UI Semibold", 8.25f))
            {
                TextRenderer.DrawText(g, "INVOICE TOTAL", caption, new Point(left, card.Y + Theme.S(20)), Color.FromArgb(224, 231, 255), TextFormatFlags.NoPadding);
            }

            if (modeSource != null)
            {
                bool editing = modeSource.Enabled;
                string badge = editing ? "EDITING" : "VIEWING";
                using (Font badgeFont = new Font("Segoe UI Semibold", 7.5f))
                {
                    Size size = TextRenderer.MeasureText(g, badge, badgeFont, Size.Empty, TextFormatFlags.NoPadding);
                    Rectangle pill = new Rectangle(right - size.Width - Theme.S(18), card.Y + Theme.S(16), size.Width + Theme.S(18), Theme.S(22));
                    using (GraphicsPath pillPath = Theme.RoundedRect(pill, pill.Height / 2f))
                    using (SolidBrush pillFill = new SolidBrush(editing ? Color.FromArgb(251, 191, 36) : Color.FromArgb(46, 255, 255, 255)))
                    {
                        g.FillPath(pillFill, pillPath);
                    }
                    TextRenderer.DrawText(g, badge, badgeFont, pill, editing ? Color.FromArgb(69, 26, 3) : Color.White,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
                }
            }

            string value = FormatAmount(totalSource?.Text);
            int available = right - left;
            float points = 26f;
            Font valueFont = new Font("Segoe UI Semibold", points);
            while (points > 14f && TextRenderer.MeasureText(g, value, valueFont, Size.Empty, TextFormatFlags.NoPadding).Width > available)
            {
                valueFont.Dispose();
                points -= 1f;
                valueFont = new Font("Segoe UI Semibold", points);
            }
            using (valueFont)
            {
                TextRenderer.DrawText(g, value, valueFont, new Rectangle(left - Theme.S(2), card.Y + Theme.S(44), available + Theme.S(4), Theme.S(52)), Color.White,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
            }

            string customer = customerSource?.Text.Trim() ?? string.Empty;
            if (customer.Length > 0)
            {
                using (Font customerFont = new Font("Segoe UI", 10f))
                {
                    TextRenderer.DrawText(g, customer, customerFont, new Rectangle(left, card.Y + Theme.S(98), available, Theme.S(22)), Color.FromArgb(224, 231, 255),
                        TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
                }
            }

            int dividerY = card.Bottom - Theme.S(46);
            using (Pen divider = new Pen(Color.FromArgb(48, 255, 255, 255), 1f))
            {
                g.DrawLine(divider, left, dividerY, right, dividerY);
            }

            int items = itemsSource?.Rows.Count ?? 0;
            string invoice = invoiceSource?.Text.Trim() ?? string.Empty;
            string footer = invoice.Length > 0
                ? $"Invoice #{invoice}   ·   {items} {(items == 1 ? "item" : "items")}"
                : "No invoice loaded";
            using (Font footerFont = new Font("Segoe UI", 9f))
            {
                TextRenderer.DrawText(g, footer, footerFont, new Rectangle(left, dividerY, available, card.Bottom - dividerY), Color.FromArgb(224, 231, 255),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            }
        }

        private static string FormatAmount(string text)
        {
            decimal amount;
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0m.ToString("N2", CultureInfo.CurrentCulture);
            }
            return decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out amount)
                ? amount.ToString("N2", CultureInfo.CurrentCulture)
                : text;
        }
    }

    public class ModernGrid : DataGridView
    {
        private string emptyTitle = "No items yet";
        private string emptyHint = "Choose an item above, enter a quantity and click Add Item.";

        public ModernGrid()
        {
            DoubleBuffered = true;
            BorderStyle = BorderStyle.None;
            BackgroundColor = Theme.Surface;
            GridColor = Color.FromArgb(236, 240, 246);
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            EnableHeadersVisualStyles = false;
            RowHeadersVisible = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            MultiSelect = false;
            AllowUserToResizeRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            Padding cellPadding = new Padding(Theme.S(10), 0, Theme.S(10), 0);
            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            ColumnHeadersDefaultCellStyle.ForeColor = Theme.TextMuted;
            ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(248, 250, 252);
            ColumnHeadersDefaultCellStyle.SelectionForeColor = Theme.TextMuted;
            ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9f);
            ColumnHeadersDefaultCellStyle.Padding = cellPadding;
            ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;

            DefaultCellStyle.BackColor = Theme.Surface;
            DefaultCellStyle.ForeColor = Theme.Text;
            DefaultCellStyle.Font = new Font("Segoe UI", 9.75f);
            DefaultCellStyle.Padding = cellPadding;
            AlternatingRowsDefaultCellStyle.BackColor = Theme.SurfaceAlt;
            SetSelectionVisible(false);

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                RowTemplate.Height = Theme.S(36);
                ColumnHeadersHeight = Theme.S(40);
            }
        }

        [DefaultValue("No items yet")]
        public string EmptyTitle { get { return emptyTitle; } set { emptyTitle = value ?? string.Empty; Invalidate(); } }

        [DefaultValue("Choose an item above, enter a quantity and click Add Item.")]
        public string EmptyHint { get { return emptyHint; } set { emptyHint = value ?? string.Empty; Invalidate(); } }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
            AlignHeader(e.Column);
        }

        protected override void OnColumnDefaultCellStyleChanged(DataGridViewColumnEventArgs e)
        {
            base.OnColumnDefaultCellStyleChanged(e);
            AlignHeader(e.Column);
        }

        private static void AlignHeader(DataGridViewColumn column)
        {
            if (column.HasDefaultCellStyle && column.DefaultCellStyle.Alignment != DataGridViewContentAlignment.NotSet)
            {
                column.HeaderCell.Style.Alignment = column.DefaultCellStyle.Alignment;
            }
        }

        protected override void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            base.OnCellFormatting(e);
            string text = e.Value as string;
            decimal number;
            if (!e.FormattingApplied && text != null && !string.IsNullOrEmpty(e.CellStyle.Format)
                && decimal.TryParse(text, NumberStyles.Number, CultureInfo.CurrentCulture, out number))
            {
                e.Value = number.ToString(e.CellStyle.Format, CultureInfo.CurrentCulture);
                e.FormattingApplied = true;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            SetSelectionVisible(true);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            SetSelectionVisible(false);
        }

        private void SetSelectionVisible(bool visible)
        {
            DefaultCellStyle.SelectionBackColor = visible ? Theme.AccentSoft : Theme.Surface;
            DefaultCellStyle.SelectionForeColor = visible ? Theme.AccentText : Theme.Text;
            AlternatingRowsDefaultCellStyle.SelectionBackColor = visible ? Theme.AccentSoft : Theme.SurfaceAlt;
            AlternatingRowsDefaultCellStyle.SelectionForeColor = visible ? Theme.AccentText : Theme.Text;
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            base.OnRowsAdded(e);
            Invalidate();
        }

        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            base.OnRowsRemoved(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Rows.Count > 0)
            {
                return;
            }
            int top = ColumnHeadersVisible ? ColumnHeadersHeight : 0;
            Rectangle area = new Rectangle(0, top, Width, Height - top);
            if (area.Height < Theme.S(70))
            {
                return;
            }
            Graphics g = e.Graphics;
            int center = area.Y + area.Height / 2;
            Theme.DrawGlyph(g, "", 20f, new Rectangle(0, center - Theme.S(62), Width, Theme.S(40)), Color.FromArgb(199, 210, 254), TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            using (Font titleFont = new Font("Segoe UI Semibold", 10.5f))
            using (Font hintFont = new Font("Segoe UI", 9f))
            {
                TextRenderer.DrawText(g, emptyTitle, titleFont, new Rectangle(0, center - Theme.S(18), Width, Theme.S(24)), Theme.TextSecondary, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                TextRenderer.DrawText(g, emptyHint, hintFont, new Rectangle(0, center + Theme.S(6), Width, Theme.S(22)), Theme.TextFaint, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
