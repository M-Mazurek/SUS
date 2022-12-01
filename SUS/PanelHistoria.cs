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
    public partial class PanelHistoria : Form
    {
        public PanelHistoria()
        {
            InitializeComponent();

            panelOrders.HorizontalScroll.Maximum = 0;
            panelOrders.AutoScroll = false;
            panelOrders.VerticalScroll.Visible = false;
            panelOrders.AutoScroll = true;

            CreateHistory();
            ExtensionMethods.StartAnim(this);
        }
        private void CreateHistory() 
        {
            panelOrders.Controls.Clear();

            List<HistoryEvent> historyEvents = new List<HistoryEvent>(Global.GetEvents());

            int maxOrders = historyEvents.Count;
            for (int i = 0; i < maxOrders; i++)
            {
                Based based = new Based()
                {
                    Location = new(0, 10 + (10 * i) + (50 * i)),
                    Size = new(950, 50),
                    Name = i.ToString(),
                };
                if (i == maxOrders - 1)
                    based.Size = new(based.Width, based.Height + 10);
                foreach (Control c in based.Controls)
                {
                    if (c.Name != "panel1")
                        continue;

                    c.Size = new(based.Width, 50);

                    foreach (Label l in c.Controls)
                    {
                        ExtensionMethods.SetupLabels(l, new int[] { lbActionName.Width, lbDate.Width, 0, 0 }, new int[] { lbActionName.Location.X, lbDate.Location.X, 0, 0 });
                        ExtensionMethods.ChangeName(l, new string[] { $"{historyEvents[i].Text}", $"{historyEvents[i].EventTime}", "", "" }, false); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }
    }
}
