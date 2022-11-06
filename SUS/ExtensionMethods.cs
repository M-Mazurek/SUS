using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUS
{
    public static class ExtensionMethods
    {
        public static Point Add(this Point p1, Point p2) 
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static object CreateObjectInstance(string? str) 
        {
            Type? t = Type.GetType(str!);
            if (t == null)
                throw new Exception("Couldnt find control of this type");

            return Activator.CreateInstance(t);
        }
        public static void SwitchForm(this Form form, Form newForm) 
        {
            MenuStripFunctions.currentForm = newForm;
            form.Hide();
            newForm.ShowDialog();
            form.Close();
        }
    }
}
