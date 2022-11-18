using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUS
{
    public partial class PanelKorekty : Form
    {
        public PanelKorekty()
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            CreateOrders();
            ExtensionMethods.StartAnim(this);
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();
            int maxOrders = 15;
            for (int i = 0; i < maxOrders; i++)
            {
                Based based = new Based()
                {
                    Location = new(0, 10 + (10 * i) + (50 * i)),
                };
                if (i == maxOrders - 1)
                    based.Size = new(based.Width, based.Height + 10);
                foreach (Control c in based.Controls)
                {
                    foreach (Label l in c.Controls)
                    {
                        l.Click += Based_Click;
                        ExtensionMethods.SetupLabels(l, new int[] { lbNrKorekty.Width, lbKorektaZamow.Width, lbFirma.Width, lbData.Width }, new int[] { lbNrKorekty.Location.X, lbKorektaZamow.Location.X, lbFirma.Location.X, lbData.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { $"{i}", $"{i}", $"Firma{i}", $"19/10/2022" }, false); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }
        private void Based_Click(object? sender, EventArgs e)
        {
            // Open order detail
            List<string> list = new List<string>();
            foreach (Label l in ((Label)sender!).Parent.Controls.OfType<Label>().OrderBy(x => x.Name))
            {
                list.Add(l.Text);
            }
            ExtensionMethods.SwitchForm(this, new SzczegółyKorekty(list[0], list[1], list[2], list[3]));
        }

        private void txtTowar_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
