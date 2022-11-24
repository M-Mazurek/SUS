using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SUS
{
    public static class ExtensionMethods
    {
        private static Point windowFrameMargin = new(16, 38); // window frame margins (8 on left, right and bottom, 30 on top) K.Y.S.
        private static Point windowSize = Point.Empty; // already calculated window size (margins etc not included)

        private static Point picturePosition = Point.Empty; // current picture position
        private static Point pictureAdjustments = new(1, 1); // values added to location to achieve moving object

        private static (bool goingRight, bool goingDown) movementChecks = new(true, true); // bools needed for movement logic

        private static PictureBox? backgroundPicture;
        private static bool isLoopOpened = false; // ghetto fix? // we need only one async loop 
        public static void StartAnim(Form frm)
        {
            if (MenuStripFunctions.currentForm!.GetType() == frm!.GetType())
                return;

            backgroundPicture = new PictureBox()
            {
                BackgroundImage = new Bitmap(Image.FromFile(Path.Combine(Global.DIR, "logo.png"))),
                SizeMode = PictureBoxSizeMode.AutoSize,
                Size = new(46, 54),
            };

            frm.Controls.Add(backgroundPicture);
            backgroundPicture.SendToBack();

            windowSize = new(frm.Width - backgroundPicture.Width - windowFrameMargin.X, frm.Height - backgroundPicture.Height - windowFrameMargin.Y);
            Random random = new Random();
            backgroundPicture.Location = new(random.Next(windowSize.X), random.Next(windowSize.Y));
            if (!isLoopOpened)
                BackgroundLoop();

            isLoopOpened = true;
        }
        private static async void BackgroundLoop()
        {
            while (true)
            {
                picturePosition = new(backgroundPicture!.Location.X, backgroundPicture.Location.Y);
                movementLogic();

                backgroundPicture.Location = picturePosition.Add(pictureAdjustments);

                await Task.Delay(1);
            }
        }
        private static void movementLogic()
        {
            if (backgroundPicture!.Location.X < windowSize.X && movementChecks.goingRight == true || backgroundPicture.Location.X < 0)
            {
                pictureAdjustments.X = 1;
                movementChecks.goingRight = true;
            }
            else
            {
                pictureAdjustments.X = -1;
                movementChecks.goingRight = false;
            }
            if (backgroundPicture.Location.Y < windowSize.Y && movementChecks.goingDown == true || backgroundPicture.Location.Y < 0)
            {
                pictureAdjustments.Y = 1;
                movementChecks.goingDown = true;
            }
            else
            {
                pictureAdjustments.Y = -1;
                movementChecks.goingDown = false;
            }
            /*pictureAdjustments.X = backgroundPicture.Location.X < windowSize.X ? 1 : -1;
            pictureAdjustments.Y = backgroundPicture.Location.Y < windowSize.Y ? 1 : -1;*/
        }
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
            "label4" => label.Text = needCalc ? $"{Math.Round(float.Parse(newText[1].Replace(" zł", "")) * float.Parse(newText[2]), 2)} zł" : newText[3],
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
        public static float GetSum(Panel startPanel)
        {
            float sum = 0;
            int ilosc = 0;
            float cena = 0;
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
                            cena = float.Parse(label.Text.Replace(" zł", ""));
                    }
                }
                sum += ilosc * cena;
            }
            return (float)Math.Round(sum, 2);
        }
        public static void GetPrice(Panel startPanel)
        {
            float pricePerItem = 0;
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
                            pricePerItem = float.Parse(label.Text.Replace(" zł", ""));

                        if (label.Name == "label3" && label.Text != "")
                            itemCount = Int32.Parse(label.Text);
                        else if (label.Name == "label3" && label.Controls.Count >= 1)
                            itemCount = (int)((CustomNumericUpDown)label.Controls[0]).Value;

                        if (label.Name == "label4")
                            label.Text = $"{Math.Round(pricePerItem * itemCount, 2)} zł";
                    }
                }
            }
        }
        public static string SetupStatus(ORDER_STATUS os) => os switch
        {
            ORDER_STATUS.PENDING => "Oczekujące",
            ORDER_STATUS.CONFIRMED => "Potwierdzone",
            _ => ""
        };
    }
}
