using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SUS
{
    public partial class StwórzZamówienie : Form
    {
        private CustomCombo customComboCompanies;
        private CustomNumericUpDown? cn;
        private List<Ware>? wares;
        public StwórzZamówienie()
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

            wares = new List<Ware>(Global.GetWaresFrom(customComboCompanies!.SelectedIndex + 1));
            //wares.ForEach(x => MessageBox.Show($"{x.Name}"));

            int maxOrders = wares.Count;
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
                        ExtensionMethods.ChangeName(l, new string[] { $"{wares[i].Name}", $"{wares[i].Price} zł", $"0", $"0 zł" }, true); // swaps label names to correct ones
                        if (l.Name == "label3")
                        {
                            //l.BackColor = Color.FromArgb(50, 99, 212, 113);
                            l.Text = "";
                            cn = new CustomNumericUpDown()
                            {
                                Size = l.Size,
                                Location = new(0, 14),
                                TextAlign = HorizontalAlignment.Center,
                                BackColor = Color.FromArgb(60, 60, 60),
                                ForeColor = Color.White,
                                Font = l.Font,
                                BorderStyle = BorderStyle.None,
                                Maximum = 999999999,
                            };
                            cn.ValueChanged += Cn_ValueChanged;
                            l.Controls.Add(cn);
                        }
                    }
                }
                panelOrders.Controls.Add(based);
            }
            UpdateSum();
        }

        private void Cn_ValueChanged(object? sender, EventArgs e)
        {
            ExtensionMethods.GetPrice(panelOrders);
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (customComboCompanies!.SelectedIndex == -1)
                return;
            List<WareStack> wareStacks = new List<WareStack>();
            // do smth
            //CustomNumericUpDown.GetValues(panelOrders).ForEach(x => MessageBox.Show(x.ToString()));
            for (int i = 0; i < wares!.Count; i++) 
            {
                //MessageBox.Show($"{CustomNumericUpDown.GetValues(panelOrders)[i]}");
                decimal wareCount = CustomNumericUpDown.GetValues(panelOrders)[i];
                if (wareCount <= 0)
                    continue;

                Ware ware = new(wares[i].Id, wares[i].Name, wares[i].SellerId, wares[i].Price);
                WareStack wareStack = new(ware, (int)wareCount);
                wareStacks.Add(wareStack);
            }
            Global.NewOrder(wares[0].SellerId, ORDER_STATUS.PENDING, wareStacks.ToArray(), out string err);
            ExtensionMethods.SwitchForm(this, new PanelZamówienia());
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CustomNumericUpDown.SetValue(panelOrders, 0);
        }
    }
}
