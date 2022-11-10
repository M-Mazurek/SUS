﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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

            return Activator.CreateInstance(t)!;
        }
        public static void SwitchForm(this Form form, Form newForm) 
        {
            MenuStripFunctions.currentForm = newForm;
            form.Hide();
            newForm.ShowDialog();
            form.Close();
        }
        public static string ChangeName(Label label, string[] newText, bool needCalc) => label.Name switch 
        {
            "label1" => label.Text = newText[0],
            "label2" => label.Text = newText[1],
            "label3" => label.Text = newText[2],
            "label4" => label.Text = needCalc ? $"{Int32.Parse(newText[1].Replace(" zł", "")) * Int32.Parse(newText[2])} zł" : newText[3],
            _ => label.Text = "no suitable label found",
        };
        public static void SetupLabels(Label label, int[] width, int[] location) 
        {
            ChangeSize(label, width);
            ChangeLocation(label, location);
        }
        private static Size ChangeSize(Label label, int[] width) => label.Name switch
        {
            "label1" => label.Size = new(width[0], label.Height),
            "label2" => label.Size = new(width[1], label.Height),
            "label3" => label.Size = new(width[2], label.Height),
            "label4" => label.Size = new(width[3], label.Height),
            _ => label.Size = new(label.Width, label.Height),
        };
        private static Point ChangeLocation(Label label, int[] location) => label.Name switch
        {
            "label1" => label.Location = new(location[0], 0),
            "label2" => label.Location = new(location[1], 0),
            "label3" => label.Location = new(location[2], 0),
            "label4" => label.Location = new(location[3], 0),
            _ => label.Location = label.Location,
        };
        public static int GetSum(Panel startPanel) 
        {
            int sum = 0;
            int ilosc = 0;
            int cena = 0;
            foreach (Control based in startPanel.Controls)
            {
                if (based.GetType() != typeof(Based))
                    continue;

                foreach (Control panel in ((Based)based).Controls)
                {
                    if (panel.GetType() != typeof(Panel))
                        continue;

                    foreach (Control label in ((Panel)panel).Controls.OfType<Label>().OrderBy(x => x.Name))
                    {
                        if (label.GetType() != typeof(Label))
                            continue;

                        if (label.Name == "label3" && label.Text != "")
                            ilosc = Int32.Parse(label.Text);
                        else if (label.Name == "label3" && label.Controls.Count >= 1)
                            ilosc = (int)((CustomNumericUpDown)label.Controls[0]).Value;

                        if (label.Name == "label2")
                            cena = Int32.Parse(label.Text.Replace(" zł", ""));
                    }
                }
                sum += ilosc * cena;
            }
            return sum;
        }
        public static void GetPrice(Panel startPanel)
        {
            int pricePerItem = 0;
            int itemCount = 0;

            foreach (Control based in startPanel.Controls)
            {
                if (based.GetType() != typeof(Based))
                    continue;

                foreach (Control panel in ((Based)based).Controls)
                {
                    if (panel.GetType() != typeof(Panel))
                        continue;

                    foreach (Control label in ((Panel)panel).Controls.OfType<Label>().OrderBy(x => x.Name))
                    {
                        if (label.GetType() != typeof(Label))
                            continue;

                        if (label.Name == "label2")
                            pricePerItem = Int32.Parse(label.Text.Replace(" zł", ""));

                        if (label.Name == "label3" && label.Text != "")
                            itemCount = Int32.Parse(label.Text);
                        else if (label.Name == "label3" && label.Controls.Count >= 1)
                            itemCount = (int)((CustomNumericUpDown)label.Controls[0]).Value;

                        if (label.Name == "label4")
                            label.Text = $"{pricePerItem * itemCount} zł";
                    }
                }
            }
        }
    }
}
