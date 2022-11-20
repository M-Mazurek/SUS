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
    public partial class PanelZamówienia : Form
    {
        private CustomCombo? customComboCompanies;
        private CustomCombo? customComboState;
        public PanelZamówienia()
        {
            InitializeComponent();
            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;
            CreateCustomCombos();
            CreateOrders();
            ExtensionMethods.StartAnim(this);
        }
        private void CreateCustomCombos() 
        {
            // not able to multiselect
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
            panelFilters.Controls.Add(customComboCompanies);

            customComboState = new CustomCombo()
            {
                Name = "orderState",
                Location = new(10, 345),
                Size = new(180, 23),
                BackColor = Color.FromArgb(60, 60, 60),
                OuterBorderColor = Color.DimGray,
                InnerBorderColor = BackColor,
                ArrowBackgroundColor = Color.FromArgb(36, 36, 36),
                ForeColor = Color.White,
                Items = { "Potwierdzone", "Oczekujące" }, // viable states
            };
            panelFilters.Controls.Add(customComboState);
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();

            //list is ghetto B)
            List<Order> orders = new List<Order>();
            int currentId = Global.GetSellers().Where(x => x.Name == (string)customComboCompanies!.SelectedItem).Select(x => x.Id).FirstOrDefault();
            //MessageBox.Show($"CurrentID: {currentId}");
            orders = new List<Order>(Global.GetOrders(currentId)); //dtpStart.Value, dtpEnd.Value, (ORDER_STATUS)customComboState!.SelectedIndex
            //orders.ForEach(x => MessageBox.Show($"ORDERS: {x}"));

            int maxOrders = orders.Count;
            for(int i = 0; i < maxOrders; i++) 
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
                        ExtensionMethods.ChangeName(l, new string[] { $"{orders[i].Id.ToString().PadLeft(4, '0')}", $"", $"{orders[i].CreationTime}", $"{ExtensionMethods.SetupStatus(orders[i].Status)}" }, false); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }

        private void Based_Click(object? sender, EventArgs e)
        {
            List<string> list = new List<string>();
            // Open order detail
            foreach (Label l in ((Label)sender!).Parent.Controls.OfType<Label>().OrderBy(x => x.Name))
            {
                list.Add(l.Text);
            }
            ExtensionMethods.SwitchForm(this, new SzczegółyZamówienia(list[0], list[1], list[2], list[3]));
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //Filter or smth

            // call paint func
            CreateOrders();
        }

        private void btnCreateOffer_Click(object sender, EventArgs e)
        {
            ExtensionMethods.SwitchForm(this, new StwórzZamówienie());
        }
    }
}
