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
            CreateCustomCombos();
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

        private void btnFilter_Click(object sender, EventArgs e)
        {
            //Filter or smth
            
            // call paint func
            // 
        }
    }
}
