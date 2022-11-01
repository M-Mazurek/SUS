using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS
{
    class MenuStripFunctions 
    {
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
            // Open Tab
            MessageBox.Show(item.Text);
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
}
