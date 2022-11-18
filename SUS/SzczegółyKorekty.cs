using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SUS
{
    public partial class SzczegółyKorekty : Form
    {
        public SzczegółyKorekty(string korektaId, string orderId, string company, string date)
        {
            InitializeComponent();

            txtCompany.Text = company;
            txtDate.Text = date;
            txtState.Text = ""; 
            txtKorektaId.Text = korektaId;

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
                    based.Size = new(based.Width, based.Height + 50 - 10);
                foreach (Control c in based.Controls)
                {
                    foreach (Label l in c.Controls)
                    {
                        ExtensionMethods.SetupLabels(l, new int[] { lbTowar.Width, lbCenaSz.Width, lbIlosc.Width, lbCena.Width }, new int[] { lbTowar.Location.X, lbCenaSz.Location.X, lbIlosc.Location.X, lbCena.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { $"Towar{i}", $"{10 + i * 20}", $"{i}", $"0" }, true); // swaps label names to correct ones
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
        private void btnBack_Click(object sender, EventArgs e)
        {
            ExtensionMethods.SwitchForm(this, new PanelKorekty());
        }
    }
}
