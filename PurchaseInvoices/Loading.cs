using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PurchaseInvoices
{
    internal static class SpinnerArt
    {
        public const float DegreesPerTick = 7.5f;

        public static void Draw(Graphics g, RectangleF bounds, float angle, float thickness, Color track, Color from, Color to)
        {
            SmoothingMode smoothing = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            RectangleF ring = RectangleF.Inflate(bounds, -thickness / 2f, -thickness / 2f);
            if (ring.Width > 1 && ring.Height > 1)
            {
                using (Pen trackPen = new Pen(track, thickness))
                {
                    g.DrawEllipse(trackPen, ring);
                }
                using (LinearGradientBrush brush = new LinearGradientBrush(bounds, from, to, angle + 45f))
                using (Pen arc = new Pen(brush, thickness) { StartCap = LineCap.Round, EndCap = LineCap.Round })
                {
                    g.DrawArc(arc, ring, angle, 110f);
                }
            }
            g.SmoothingMode = smoothing;
        }
    }

    public class Spinner : Control
    {
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
        private float angle;

        public Spinner()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Selectable, false);
            TabStop = false;
            BackColor = Color.Transparent;
            timer.Tick += (s, e) =>
            {
                angle = (angle + SpinnerArt.DegreesPerTick) % 360f;
                Invalidate();
            };
        }

        protected override Size DefaultSize => new Size(36, 36);

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            timer.Enabled = Visible && !DesignMode;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            float thickness = Math.Max(2f, Math.Min(Width, Height) / 9f);
            SpinnerArt.Draw(e.Graphics, new RectangleF(1, 1, Math.Min(Width, Height) - 2, Math.Min(Width, Height) - 2), angle, thickness, Theme.AccentSoft, Theme.Accent, Theme.Accent2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    internal sealed class BusyIndicator : IDisposable
    {
        public const int DefaultDelay = 250;

        [ThreadStatic]
        private static int active;

        private readonly object sync = new object();
        private BusyWindow window;
        private bool done;

        private BusyIndicator()
        {
        }

        private BusyIndicator(Rectangle area, string title, string detail, int delay)
        {
            Thread thread = new Thread(() => Run(area, title, detail, delay)) { IsBackground = true, Name = "Busy indicator" };
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public static bool IsActive => active > 0;

        public static BusyIndicator Start(Control near, string title, string detail = null, int delay = DefaultDelay)
        {
            active++;
            return active > 1 ? new BusyIndicator() : new BusyIndicator(AreaFor(near), title, detail ?? "This only takes a moment.", delay);
        }

        private static Rectangle AreaFor(Control near)
        {
            Form form = near as Form ?? near?.FindForm();
            Form target = form != null && form.Visible ? form : form?.Owner ?? Form.ActiveForm;
            if (target != null && target.Visible && target.WindowState != FormWindowState.Minimized && target.IsHandleCreated)
            {
                return target.RectangleToScreen(target.ClientRectangle);
            }
            return Screen.FromPoint(Cursor.Position).WorkingArea;
        }

        private void Run(Rectangle area, string title, string detail, int delay)
        {
            lock (sync)
            {
                if (delay > 0 && !done)
                {
                    Monitor.Wait(sync, delay);
                }
                if (done)
                {
                    return;
                }
            }
            BusyWindow busy = new BusyWindow(area, title, detail);
            lock (sync)
            {
                if (done || busy.Handle == IntPtr.Zero)
                {
                    busy.Dispose();
                    return;
                }
                window = busy;
            }
            Application.Run(busy);
            busy.Dispose();
        }

        public void Dispose()
        {
            BusyWindow busy;
            lock (sync)
            {
                if (done)
                {
                    return;
                }
                done = true;
                active = Math.Max(0, active - 1);
                Monitor.PulseAll(sync);
                busy = window;
            }
            if (busy != null)
            {
                try
                {
                    busy.BeginInvoke((MethodInvoker)busy.Close);
                }
                catch (InvalidOperationException)
                {
                }
            }
        }
    }

    internal sealed class BusyWindow : Form
    {
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int CS_DROPSHADOW = 0x20000;
        private static readonly IntPtr HWND_TOP = IntPtr.Zero;
        private static readonly uint ProcessId = (uint)Process.GetCurrentProcess().Id;
        private const uint SWP_NOSIZE = 0x1, SWP_NOMOVE = 0x2, SWP_NOACTIVATE = 0x10;

        private readonly string title;
        private readonly string detail;
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = 16 };
        private readonly Stopwatch clock = new Stopwatch();
        private float angle;
        private int ticks;

        [DllImport("user32.dll")] private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")] private static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)] private static extern int GetClassName(IntPtr hwnd, StringBuilder name, int count);
        [DllImport("user32.dll")] private static extern bool SetWindowPos(IntPtr hwnd, IntPtr after, int x, int y, int cx, int cy, uint flags);
        [DllImport("dwmapi.dll")] private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attribute, ref int value, int size);

        public BusyWindow(Rectangle area, string title, string detail)
        {
            this.title = title ?? string.Empty;
            this.detail = detail ?? string.Empty;
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            BackColor = Theme.Surface;
            DoubleBuffered = true;
            Opacity = 0;
            Size size = new Size(Theme.S(340), Theme.S(84));
            Bounds = new Rectangle(area.X + (area.Width - size.Width) / 2, area.Y + (area.Height - size.Height) / 2, size.Width, size.Height);
            timer.Tick += Timer_Tick;
        }

        protected override bool ShowWithoutActivation => true;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            try
            {
                int round = 2;
                DwmSetWindowAttribute(Handle, 33, ref round, sizeof(int));
            }
            catch (DllNotFoundException)
            {
            }
            catch (EntryPointNotFoundException)
            {
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            KeepPlace();
            clock.Start();
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            angle = (angle + SpinnerArt.DegreesPerTick) % 360f;
            if (Opacity < 1)
            {
                Opacity = Math.Min(1.0, clock.ElapsedMilliseconds / 140.0);
            }
            if (++ticks % 5 == 0)
            {
                KeepPlace();
            }
            Invalidate();
        }

        private void KeepPlace()
        {
            IntPtr foreground = GetForegroundWindow();
            uint process;
            GetWindowThreadProcessId(foreground, out process);
            if (foreground == IntPtr.Zero || foreground == Handle || process != ProcessId)
            {
                return;
            }
            StringBuilder name = new StringBuilder(64);
            GetClassName(foreground, name, name.Capacity);
            IntPtr after = name.ToString() == "#32770" ? foreground : HWND_TOP;
            SetWindowPos(Handle, after, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Theme.Surface);
            using (Pen border = new Pen(Theme.Border))
            {
                g.DrawRectangle(border, 0, 0, Width - 1, Height - 1);
            }
            int size = Theme.S(36);
            RectangleF spinner = new RectangleF(Theme.S(22), (Height - size) / 2f, size, size);
            SpinnerArt.Draw(g, spinner, angle, Theme.S(4), Theme.AccentSoft, Theme.Accent, Theme.Accent2);

            int textX = (int)spinner.Right + Theme.S(16);
            int textWidth = Width - textX - Theme.S(18);
            using (Font titleFont = new Font("Segoe UI Semibold", 10.5f))
            using (Font detailFont = new Font("Segoe UI", 9f))
            {
                int titleHeight = TextRenderer.MeasureText(g, title, titleFont, Size.Empty, TextFormatFlags.NoPadding).Height;
                int detailHeight = detail.Length > 0 ? TextRenderer.MeasureText(g, detail, detailFont, Size.Empty, TextFormatFlags.NoPadding).Height + Theme.S(2) : 0;
                int top = (Height - titleHeight - detailHeight) / 2;
                TextRenderer.DrawText(g, title, titleFont, new Rectangle(textX, top, textWidth, titleHeight), Theme.Text,
                    TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                if (detail.Length > 0)
                {
                    TextRenderer.DrawText(g, detail, detailFont, new Rectangle(textX, top + titleHeight + Theme.S(2), textWidth, detailHeight), Theme.TextMuted,
                        TextFormatFlags.NoPadding | TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPrefix);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                timer.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
