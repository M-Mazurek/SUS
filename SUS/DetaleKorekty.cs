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
    public partial class DetaleKorekty : Form
    {
        private CustomNumericUpDown? cn;
        private List<WareStack>? wares;
        private Order order;
        public DetaleKorekty(Order order)
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            this.order = order;
            txtCompany.Text = order.Id.ToString().PadLeft(4, '0');

            CreateOrders();
            ExtensionMethods.StartAnim(this);
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();

            wares = new List<WareStack>(order.Wares.ToList());

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
                        //orders[i].Wares.ToList().Select(x => x.Ware.Price);
                        ExtensionMethods.SetupLabels(l, new int[] { lbTowar.Width, lbCenaSz.Width, lbIlosc.Width, lbCena.Width }, new int[] { lbTowar.Location.X, lbCenaSz.Location.X, lbIlosc.Location.X, lbCena.Location.X });
                        ExtensionMethods.ChangeName(l, new string[] { $"{wares[i].Ware.Name}", $"{wares[i].Ware.Price} zł", "0", $"0 zł" }, true); // swaps label names to correct ones
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
                            cn.Value = wares[i].Amount;
                            cn.ValueChanged += Cn_ValueChanged;
                            l.Controls.Add(cn);
                        }

                    }
                }
                panelOrders.Controls.Add(based);
            }
            ExtensionMethods.GetPrice(panelOrders);
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

        private void btnConfirm_Click_1(object sender, EventArgs e)
        {
            // add
            List<WareStack> wareStacks = new List<WareStack>();
            // do smth
            //CustomNumericUpDown.GetValues(panelOrders).ForEach(x => MessageBox.Show(x.ToString()));
            for (int i = 0; i < wares!.Count; i++)
            {
                //MessageBox.Show($"{CustomNumericUpDown.GetValues(panelOrders)[i]}");
                decimal wareCount = CustomNumericUpDown.GetValues(panelOrders)[i];
                if (wareCount <= 0)
                    continue;

                WareStack wareStack = new(wares[i].Ware, (int)wareCount);
                wareStacks.Add(wareStack);
            }
            Global.NewCorrection(order.Id, wareStacks.ToArray(), out string err);
            ExtensionMethods.SwitchForm(this, new PanelKorekty());
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            CustomNumericUpDown.SetValue(panelOrders, 0);
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            ExtensionMethods.SwitchForm(this, new StwórzKorekty());
        }
    }
}
