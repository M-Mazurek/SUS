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
        private Correction correction;
        public SzczegółyKorekty(Correction correction)
        {
            InitializeComponent();

            this.correction = correction;

            txtCompany.Text = Global.GetSellerName(Global.GetOrderById(correction.OrderId).FirstOrDefault().SellerId);
            txtDate.Text = correction.CreationTime.ToString();
            txtState.Text = ExtensionMethods.SetupStatus(Global.GetOrderById(correction.OrderId).FirstOrDefault().Status); 
            txtKorektaId.Text = correction.OrderId.ToString().PadLeft(4, '0');

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

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
            int maxOrders = correction.InexactWares.Count();
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
                        ExtensionMethods.ChangeName(l, new string[] { $"{correction.InexactWares[i].Ware.Name}", $"{correction.InexactWares[i].Ware.Price}", $"{correction.InexactWares[i].Amount}", $"0" }, true); // swaps label names to correct ones
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Global.ConfirmOrder(Global.GetOrderById(correction.OrderId).FirstOrDefault());
            ExtensionMethods.SwitchForm(this, new PanelKorekty());
        }
    }
}
