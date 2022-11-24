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
        private Order order;
        public SzczegółyZamówienia(Order order)
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            this.order = order;

            txtCompany.Text = Global.GetSellerName(order.SellerId);
            txtDate.Text = order.CreationTime.ToString();
            txtState.Text = ExtensionMethods.SetupStatus(order.Status);
            txtOrderId.Text = order.Id.ToString().PadLeft(4, '0');

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

            List<WareStack> wareStack = new List<WareStack>(order.Wares);
            //wareStack.ForEach(x => MessageBox.Show($"{x.Ware.Name}, {x.Amount}"));

            int maxOrders = wareStack.Count;
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
                        ExtensionMethods.ChangeName(l, new string[] { $"{wareStack[i].Ware.Name}", $"{wareStack[i].Ware.Price} zł", $"{wareStack[i].Amount}", $"0 zł" }, true); // swaps label names to correct ones
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
            Global.ConfirmOrder(order.Id);
            ExtensionMethods.SwitchForm(this, new PanelZamówienia());
        }
    }
}
