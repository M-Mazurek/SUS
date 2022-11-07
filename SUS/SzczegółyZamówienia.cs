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
    public partial class SzczegółyZamówienia : Form
    {
        public SzczegółyZamówienia()
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            txtCompany.Text = "Firma";
            txtDate.Text = "20.11.2022";
            txtState.Text = "Oczekujace";
            txtOrderId.Text = "00002";

            lbSuma.Text = "Suma: 420000 zł";

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
                    Location = new(10, 10 + (10 * i) + (50 * i)),
                };
                if (i == maxOrders - 1)
                    based.Size = new(based.Width, based.Height + 50 - 10);
                foreach (Control c in based.Controls)
                {
                    foreach (Label l in c.Controls)
                    {
                        ExtensionMethods.SetupLabels(l, new int[] { lbTowar.Width, lbCenaSz.Width, lbIlosc.Width, lbCena.Width }, new int[] { lbTowar.Location.X, lbCenaSz.Location.X, lbIlosc.Location.X, lbCena.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { "towar" + i.ToString(), "420 zł", "999", "99999 zł" }); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }
    }
}
