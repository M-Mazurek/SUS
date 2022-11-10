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
    public partial class PanelMagazyny : Form
    {
        public PanelMagazyny()
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            CreateOrders();
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();
            int maxOrders = 20;
            for (int i = 0; i < maxOrders; i++)
            {
                Based based = new Based()
                {
                    Location = new(0, 10 + (10 * i) + (50 * i)),
                };
                if (i == maxOrders - 1)
                    based.Size = new(based.Width, based.Height + 50 - 10);
                foreach (Control c in based.Controls)
                {
                    foreach (Label l in c.Controls)
                    {
                        ExtensionMethods.SetupLabels(l, new int[] { lbTowar.Width, lbCenaSz.Width, lbIlosc.Width, lbCena.Width }, new int[] { lbTowar.Location.X, lbCenaSz.Location.X, lbIlosc.Location.X, lbCena.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { "towar" + i.ToString(), $"{40 * (i + 1)} zł", "30", "0 zł" }, true); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
            UpdateSum();
        }
        private void UpdateSum()
        {
            lbSuma.Text = $"Suma: {ExtensionMethods.GetSum(panelOrders)} zł";
        }

        private void txtTowar_TextChanged(object sender, EventArgs e)
        {
            // render new goods
        }

        private void chbBrak_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
