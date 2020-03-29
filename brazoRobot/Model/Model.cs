using System;
using System.Drawing;
using System.Windows.Forms;

namespace brazoRobot.ModelLayer
{
    public class Model
    {
        #region Var

        private Graphics g;
        private int angle = 0;
        private int angle2 = 0;
        private int angle3 = 0;
        private int R;
        private int r = 0;
        private Bitmap bmp = new Bitmap(400, 400);
        private PictureBox pictureBox = new PictureBox();

        #endregion Var

        #region Get&Set

        public PictureBox box { get; set; }
        public int Angle { get => angle; set => angle = value; }
        public int Angle2 { get => angle2; set => angle2 = value; }
        public int Angle3 { get => angle3; set => angle3 = value; }
        public Graphics G { get => g; set => g = value; }

        #endregion Get&Set

        public Model()
        {
            r = 100;
        }

        private int[] LineCoord(int angleIn, int radius, int center) // Get any point on the circle by the angle
        {
            int[] coord = new int[2]; // Setting up the int array for return
            angleIn %= (360 * Config.Config.Accuracy);
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= (180 * Config.Config.Accuracy))
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / (180 * Config.Config.Accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * Config.Config.Accuracy)));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / (180 * Config.Config.Accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * Config.Config.Accuracy)));
            }
            return coord;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public void Render()
        {
            g = this.box.CreateGraphics();
            g.Clear(Color.White); // Clears the screen

            R = (int)Math.Sqrt((Math.Pow((Config.Config.Large / 2), 2) + Math.Pow(r, 2))); // Calculate the bigger radius
            int theta = (int)RadianToDegree(Math.Atan((double)(Config.Config.Large / 2) / r)); // Calculate the angle theta

            //obtener centro
            int xcentro = this.box.Width / 2;
            int ycentro = this.box.Height;

            // This calculates the bigger radius' lines' coordinates
            int x0 = LineCoord(angle + theta, R, xcentro)[0];
            int y0 = LineCoord(angle + theta, R, ycentro)[1];
            int x1 = LineCoord(angle2 + 360 - theta, R, x0)[0];
            int y1 = LineCoord(angle2 + 360 - theta, R, y0)[1];

            g.DrawLine(new Pen(Color.FromArgb(150, 0, 0), 30f),
                       new Point(xcentro, ycentro),
                       new Point(LineCoord(angle, r, xcentro)[0], LineCoord(angle, r, ycentro)[1])); // Draw the Handle

            g.DrawLine(new Pen(Color.FromArgb(0, 0, 230), 30f),
                       new Point(LineCoord(angle, r, xcentro)[0], LineCoord(angle, r, ycentro)[1]),
                       new Point(LineCoord(angle2, r, x0)[0], LineCoord(angle2, r, y0)[1]));

            g.DrawLine(new Pen(Color.Chocolate, 30f),
                       new Point(LineCoord(angle2, r, x0)[0], LineCoord(angle2, r, y0)[1]),
                       new Point(LineCoord(angle3, r, x1)[0], LineCoord(angle3, r, y1)[1]));

            g.Dispose();
        }
    }
}