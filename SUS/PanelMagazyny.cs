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
            ExtensionMethods.StartAnim(this);
        }
        private void CreateOrders()
        {
            panelOrders.Controls.Clear();

            List<Ware> wares = new List<Ware>();
            List<int> currentCompanyId = new List<int>(Global.GetSellers().Select(x => x.Id).ToArray());
            //currentCompanyId.ForEach(e => MessageBox.Show($"Current Picked Id: {e}"));
            //wares = new List<Ware>(Global.GetWaresFrom(currentCompanyId.First()));
            currentCompanyId.ForEach(x => wares.AddRange(Global.GetWaresFrom(x)));

            if (!String.IsNullOrWhiteSpace(txtTowar.Text)) 
            {
                wares.ForEach(e => MessageBox.Show($"BEFORE CHANGE: {e.Name} != {txtTowar.Text}"));
                
                wares.ForEach(e => MessageBox.Show($"AFTER CHANGE: {e.Name} != {txtTowar.Text}"));
            }

            //wares.ForEach(x => MessageBox.Show($"{x}"));
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
                        ExtensionMethods.ChangeName(l, new string[] { $"{wares[i].Name}", $"{wares[i].Price} zł", "30", "0 zł" }, true); // swaps label names to correct ones
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
            // render new wares
            CreateOrders();
        }

        private void chbBrak_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
