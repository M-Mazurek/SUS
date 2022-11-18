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

        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            //clear B)
        }

        private void btnOrderHistory_Click(object sender, EventArgs e)
        {

        }

        private void btnActionHistory_Click(object sender, EventArgs e)
        {

        }
    }
}
