using System.Numerics;

namespace SUS
{
    public partial class Logowanie : Form
    {
        private Point windowFrameMargin = new(16, 38); // window frame margins (8 on left, right and bottom, 30 on top) K.Y.S.
        private Point windowSize = Point.Empty; // already calculated window size (margins etc not included)

        private Point picturePosition = Point.Empty; // current picture position
        private Point pictureAdjustments = new(1,1); // values added to location to achieve moving object

        private (bool goingRight, bool goingDown) movementChecks = new(true, true); // bools needed for movement logic
        async void BackgroundLoop() 
        {
            while (true) 
            {
                picturePosition = new(backgroundPicture.Location.X, backgroundPicture.Location.Y);
                movementLogic();

                backgroundPicture.Location = picturePosition.Add(pictureAdjustments);

                await Task.Delay(1);
            }
        }

        private void movementLogic()
        {
            if (backgroundPicture.Location.X < windowSize.X && movementChecks.goingRight == true || backgroundPicture.Location.X < 0)
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

        public Logowanie()
        {
            InitializeComponent();
            wrongCredentials.Visible = false;
            wrongCredentials.ForeColor = Color.FromArgb(255, 255, 50 ,0);
            windowSize = new(Size.Width - backgroundPicture.Width - windowFrameMargin.X, Size.Height - backgroundPicture.Height - windowFrameMargin.Y);
            //new(538, 308) // bottom-right
            Random random = new Random();
            backgroundPicture.Location = new(random.Next(windowSize.X), random.Next(windowSize.Y));
            BackgroundLoop();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // credentials check /w database
            if (txtLogin.Text != "admin" && txtPassword.Text != "admin")
            {
                wrongCredentials.Visible = true;
                return;
            }

            wrongCredentials.Visible = false;

            ExtensionMethods.SwitchForm(this, new PanelKonto());
        }
    }
}