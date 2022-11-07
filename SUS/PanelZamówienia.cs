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
        private CustomCombo? customComboDateStart;
        private CustomCombo? customComboDateEnd;
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
                Items = { "Firma1", "Firma2", "Firma3" }, // viable companies
            };
            panelFilters.Controls.Add(customComboCompanies);
            customComboDateStart = new CustomCombo()
            {
                Name = "dateStart",
                Location = new(10, 145),
                Size = new(180, 23),
                BackColor = Color.FromArgb(60, 60, 60),
                OuterBorderColor = Color.DimGray,
                InnerBorderColor = BackColor,
                ArrowBackgroundColor = Color.FromArgb(36, 36, 36),
                ForeColor = Color.White,
                Items = { "19.10.2022", "20.10.2022", "21.10.2022" }, // viable dates
            };
            panelFilters.Controls.Add(customComboDateStart);
            customComboDateEnd = new CustomCombo()
            {
                Name = "dateEnd",
                Location = new(10, 245),
                Size = new(180, 23),
                BackColor = Color.FromArgb(60, 60, 60),
                OuterBorderColor = Color.DimGray,
                InnerBorderColor = BackColor,
                ArrowBackgroundColor = Color.FromArgb(36, 36, 36),
                ForeColor = Color.White,
                Items = { "19.10.2022", "20.10.2022", "21.10.2022" }, // viable dates
            };
            panelFilters.Controls.Add(customComboDateEnd);
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
            int maxOrders = 20;
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
                        ExtensionMethods.ChangeName(l, new string[] { "nr_zam" + i.ToString(), "firma", "data", "status" }); // swaps label names to correct ones
                    }
                }
                panelOrders.Controls.Add(based);
            }
        }

        private void Based_Click(object? sender, EventArgs e)
        {
            // Open order detail
            ExtensionMethods.SwitchForm(this, new SzczegółyZamówienia());
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //Filter or smth

            // call paint func
            CreateOrders();
        }

        private void btnCreateOffer_Click(object sender, EventArgs e)
        {

        }
    }
}
