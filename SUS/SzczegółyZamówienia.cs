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
        public SzczegółyZamówienia(string company, string date, string state, string orderId)
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            txtCompany.Text = company;
            txtDate.Text = date;
            txtState.Text = state;
            txtOrderId.Text = orderId;

            lbSuma.Text = "Suma: 0 zł";

            CreateOrders();

            if (txtState.Text == "Oczekujące") 
            {
                btnConfirm.Visible = true;
                txtState.Location = new(txtState.Location.X, 248);
            }
            ExtensionMethods.StartAnim(this);
        }

        private void CreateOrders() 
        {
            panelOrders.Controls.Clear();
            int maxOrders = 5;
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
                        ExtensionMethods.ChangeName(l, new string[] { "towar" + i.ToString(), "10 zł", $"{2 * (i + 1)}", $"0 zł" }, true); // swaps label names to correct ones
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
            ExtensionMethods.SwitchForm(this, new PanelZamówienia());
        }

        private void btnPobierz_Click(object sender, EventArgs e)
        {

        }

        private void btnPodglad_Click(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // change order state to ready B)
            ExtensionMethods.SwitchForm(this, new PanelZamówienia());
        }
    }
}
