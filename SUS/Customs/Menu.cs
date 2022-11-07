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
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
            menuStrip1.Renderer = new Renderer(); //override menuStrip with our functions
            MenuStripFunctions.CreateEvents(menuStrip1); //creates onclick events on menu strip items
        }
    }
}