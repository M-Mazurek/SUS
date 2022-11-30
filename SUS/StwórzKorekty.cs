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
    public partial class StwórzKorekty : Form
    {
        private CustomCombo customComboCompanies;
        private List<Order>? orders;
        private List<int>? sellerIds;
        public StwórzKorekty()
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            customComboCompanies = new CustomCombo()
            {
                Name = "companies",
                Location = new(10, 45),
                Size = new(180, 23),
                BackColor = Color.FromArgb(60, 60, 60),
                OuterBorderColor = Color.DimGray,
                InnerBorderColor = BackColor,
                ArrowBackgroundColor = Color.FromArgb(36, 36, 36),
                ForeColor = Color.White,
            };
            customComboCompanies.Items.AddRange(Global.GetSellers().Select(x => x.Name).ToArray()); // viable companies

            sellerIds = new List<int>(Global.GetSellers().Select(x => x.Id).ToArray());

            customComboCompanies.SelectedIndexChanged += CustomComboCompanies_SelectedIndexChanged;
            panelFilters.Controls.Add(customComboCompanies);

            CreateOrders();
            ExtensionMethods.StartAnim(this);
        }
        private void CustomComboCompanies_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CreateOrders();
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();

            if (customComboCompanies!.SelectedIndex == -1)
                return;

            orders = new List<Order>(Global.GetOrders(sellerIds![customComboCompanies!.SelectedIndex]));

            int maxOrders = orders.Count;
            for (int i = 0; i < maxOrders; i++)
            {
                Based based = new Based()
                {
                    Location = new(0, 10 + (10 * i) + (50 * i)),
                    Name = i.ToString(),
                };
                if (i == maxOrders - 1)
                    based.Size = new(based.Width, based.Height + 50 - 10);
                foreach (Control c in based.Controls)
                {
                    foreach (Label l in c.Controls)
                    {
                        l.Click += Based_Click;
                        //orders[i].Wares.ToList().Select(x => x.Ware.Price);
                        ExtensionMethods.SetupLabels(l, new int[] { lbTowar.Width, lbCenaSz.Width, lbIlosc.Width, lbCena.Width }, new int[] { lbTowar.Location.X, lbCenaSz.Location.X, lbIlosc.Location.X, lbCena.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { $"{String.Join(", ", orders[i].Wares.ToList().Select(x => x.Ware.Name))}", $"{orders[i].Id.ToString().PadLeft(4, '0')}", $"{ExtensionMethods.SetupStatus(orders[i].Status)}", $"{orders[i].CreationTime}" }, false); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }
        private void Based_Click(object? sender, EventArgs e)
        {
            ExtensionMethods.SwitchForm(this, new DetaleKorekty(orders![Int32.Parse(((Label)sender!).Parent.Parent.Name)]));
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            ExtensionMethods.SwitchForm(this, new PanelKorekty());
        }
    }
}
