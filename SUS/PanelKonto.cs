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
    public partial class PanelKonto : Form
    {
        public PanelKonto()
        {
            InitializeComponent();
            menu.Renderer = new Renderer(); //override menuStrip with our functions
            MenuStripFunctions.CreateEvents(menu); //creates onclick events on menu strip items
            MenuStripFunctions.currentForm = this;
        }
    }
}