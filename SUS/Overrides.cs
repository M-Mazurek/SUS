using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SUS
{
    class MenuStripFunctions 
    {
        public static Form? currentForm = new Form();
        public static void CreateEvents(MenuStrip menu) 
        {
            foreach (ToolStripItem item in menu.Items) 
            {
                item.Click += Item_Click;
            }
        }

        private static void Item_Click(object? sender, EventArgs e)
        {
            ToolStripItem? item = sender as ToolStripItem;
            if (item == null)
                return;

            Form? form = ExtensionMethods.CreateObjectInstance("SUS.Panel" + item.Text) as Form; // create object of our new form

            // trying to open same form = skip)
            if (currentForm!.GetType() == form!.GetType())
                return;

            ExtensionMethods.SwitchForm(currentForm!, form!); // open new form
        }
    }
    class Renderer : ToolStripProfessionalRenderer 
    {
        public Renderer() : base(new Colors()) { }
    }
    class Colors : ProfessionalColorTable
    {
        public override Color MenuItemSelectedGradientBegin
        {
            // hover gradient begin color 
            get { return Color.FromArgb(50, 60, 60, 60); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            // hover gradient end color 
            get { return Color.FromArgb(50, 60, 60, 60); }
        }
        public override Color MenuItemBorder
        {
            // hovered item border color
            get { return Color.FromArgb(255, 60, 60, 60); }
        }
        /*public override Color MenuItemPressedGradientBegin
        {
            // current selected menu
            get { return Color.FromArgb(50, 60, 60, 60); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            // current selected menu
            get { return Color.FromArgb(50, 60, 60, 60); }
        }*/
    }
    class CustomCombo : ComboBox 
    {
        // windows hooks etc
        #region imports&structs
        private const int WM_PAINT = 0xF;
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int L, T, R, B;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public int rcPaint_left;
            public int rcPaint_top;
            public int rcPaint_right;
            public int rcPaint_bottom;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hWnd,
            [In, Out] ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);

        [DllImport("user32.dll")]
        public static extern int GetUpdateRgn(IntPtr hwnd, IntPtr hrgn, bool fErase);
        public enum RegionFlags
        {
            ERROR = 0,
            NULLREGION = 1,
            SIMPLEREGION = 2,
            COMPLEXREGION = 3,
        }
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);
        #endregion

        #region colors
        Color innerBorderColor = Color.Black;
        Color outerBorderColor = Color.Black;
        Color arrowBackgroundColor = Color.Black;
        public Color InnerBorderColor
        {
            get { return innerBorderColor; }
            set { innerBorderColor = value; Invalidate(); }
        }
        public Color OuterBorderColor
        {
            get { return outerBorderColor; }
            set { outerBorderColor = value; Invalidate(); }
        }
        public Color ArrowBackgroundColor
        {
            get { return arrowBackgroundColor; }
            set { arrowBackgroundColor = value; Invalidate(); }
        }
        #endregion
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT && DropDownStyle != ComboBoxStyle.Simple)
            {
                // size calculations etc
                var clientRect = ClientRectangle;
                var dropDownButtonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
                var outerBorder = new Rectangle(clientRect.Location,
                                                new Size(clientRect.Width - 1, clientRect.Height - 1));
                var innerBorder = new Rectangle(outerBorder.X + 1, outerBorder.Y + 1,
                                                outerBorder.Width - dropDownButtonWidth - 2, outerBorder.Height - 2);
                var innerInnerBorder = new Rectangle(innerBorder.X + 1, innerBorder.Y + 1,
                                                    innerBorder.Width - 2, innerBorder.Height - 2);
                var dropDownRect = new Rectangle(innerBorder.Right + 1, innerBorder.Y,
                                                dropDownButtonWidth, innerBorder.Height + 1);
                var middle = new Point(dropDownRect.Left + dropDownRect.Width / 2,
                                        dropDownRect.Top + dropDownRect.Height / 2);
                var arrow = new Point[]
                {
                        new Point(middle.X - 3, middle.Y - 2),
                        new Point(middle.X + 4, middle.Y - 2),
                        new Point(middle.X, middle.Y + 2)
                };
                // size calculations etc

                // windows stuff?
                var ps = new PAINTSTRUCT();
                bool shouldEndPaint = false;
                IntPtr dc;
                if (m.WParam == IntPtr.Zero)
                {
                    dc = BeginPaint(Handle, ref ps);
                    m.WParam = dc;
                    shouldEndPaint = true;
                }
                else
                {
                    dc = m.WParam;
                }

                var rgn = CreateRectRgn(innerInnerBorder.Left, innerInnerBorder.Top,
                                        innerInnerBorder.Right, innerInnerBorder.Bottom);
                SelectClipRgn(dc, rgn);
                DefWndProc(ref m);
                DeleteObject(rgn);
                rgn = CreateRectRgn(clientRect.Left, clientRect.Top,
                    clientRect.Right, clientRect.Bottom);
                SelectClipRgn(dc, rgn);
                // windows stuff?

                // paint funcs
                using (var g = Graphics.FromHwnd(Handle))
                {
                    using (var b = new SolidBrush(ArrowBackgroundColor)) // arrow background
                    {
                        g.FillRectangle(b, dropDownRect);
                    }
                    using (var b = new SolidBrush(Color.FromArgb(255,255,255))) // arrow
                    {
                        g.FillPolygon(b, arrow);
                    }
                    using (var p = new Pen(InnerBorderColor)) // innerborder
                    {
                        g.DrawRectangle(p, innerBorder);
                        g.DrawRectangle(p, innerInnerBorder);
                    }
                    using (var p = new Pen(OuterBorderColor)) // outerborder
                    {
                        g.DrawRectangle(p, outerBorder);
                    }
                }
               
                // paint funcs

                if (shouldEndPaint)
                    EndPaint(Handle, ref ps);
                DeleteObject(rgn);
            }
            else
                base.WndProc(ref m);
        }
    }
    class CustomNumericUpDown : NumericUpDown 
    {
        public CustomNumericUpDown() 
        {
            Controls[0].Hide();
        }
        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            Controls[1].Width = Width + 4;
            //base.OnTextBoxResize(source, e);
        }
        public static void SetValue(Panel startPanel, int value) 
        {
            foreach (Control based in startPanel.Controls)
            {
                if (based.GetType() != typeof(Based))
                    continue;

                foreach (Control panel in ((Based)based).Controls)
                {
                    if (panel.GetType() != typeof(Panel))
                        continue;

                    foreach (Control label in ((Panel)panel).Controls)
                    {
                        if (label.GetType() != typeof(Label))
                            continue;

                        if (label.Name != "label3")
                            continue;

                        foreach (CustomNumericUpDown cn in ((Label)label).Controls)
                        {
                            cn.Value = value;
                        }
                    }
                }
            }
        }
        public static List<decimal> GetValues(Panel startPanel)
        {
            List<decimal> temp = new List<decimal>();
            foreach (Control based in startPanel.Controls)
            {
                if (based.GetType() != typeof(Based))
                    continue;

                foreach (Control panel in ((Based)based).Controls)
                {
                    if (panel.GetType() != typeof(Panel))
                        continue;

                    foreach (Control label in ((Panel)panel).Controls)
                    {
                        if (label.GetType() != typeof(Label))
                            continue;

                        if (label.Name != "label3")
                            continue;

                        foreach (CustomNumericUpDown cn in ((Label)label).Controls)
                        {
                            temp.Add(cn.Value);
                        }
                    }
                }
            }
            return temp;
        }
    }
}
