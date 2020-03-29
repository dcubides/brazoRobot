using brazoRobot.ConfigLayer;
using System;
using System.Drawing;
using System.Threading;
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
        private int angle4 = 0;
        private int angle5 = 0;
        private bool startRender = false;
        private bool statusGripper = true;
        private int R;
        private int r = 0;
        private Bitmap bmp = new Bitmap(400, 400);

        private Thread paintThread;

        #endregion Var

        #region Get&Set

        public PictureBox box { get; set; }
        public Button btnGripper { get; set; }
        public int Angle { get => angle; set => angle = value; }
        public int Angle2 { get => angle2; set => angle2 = value; }
        public int Angle3 { get => angle3; set => angle3 = value; }
        public int Angle4 { get => angle4; set => angle4 = value; }
        public int Angle5 { get => angle5; set => angle5 = value; }
        public Graphics G { get => g; set => g = value; }
        public bool StatusGripper { get => statusGripper; set => statusGripper = value; }

        #endregion Get&Set

        public Model()
        {
            this.box = new PictureBox();
            this.btnGripper = new Button();
            this.paintThread = new Thread(new ThreadStart(Run));
            this.startRender = true;
            this.paintThread.Start();
            r = 50;
        }

        private void Run()
        {
            while (this.startRender)
            {
                try
                {
                    Thread.Sleep(10);
                }
                catch (Exception)
                {
                }

                this.Render();
            }
        }

        private int[] LineCoord(int angleIn, int radius, int center) // Get any point on the circle by the angle
        {
            int[] coord = new int[2]; // Setting up the int array for return
            angleIn %= (360 * Config.Accuracy);
            angleIn *= 1;

            if (angleIn >= 0 && angleIn <= (180 * Config.Accuracy))
            {
                coord[0] = center + (int)(radius * Math.Sin(Math.PI * angleIn / (180 * Config.Accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * Config.Accuracy)));
            }
            else
            {
                coord[0] = center - (int)(radius * -Math.Sin(Math.PI * angleIn / (180 * Config.Accuracy)));
                coord[1] = center - (int)(radius * Math.Cos(Math.PI * angleIn / (180 * Config.Accuracy)));
            }
            return coord;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public void Render()
        {
            int CentroX = this.box.Width / 2;
            int CentroY = this.box.Height / 10 * 9;

            bmp = new Bitmap(this.box.Width, this.box.Height);

            int SizeCircle = 15;
            int PositionCircle = 8;

            #region Pintar Ejes

            R = (int)Math.Sqrt((Math.Pow((Config.Large / 2), 2) + Math.Pow(r, 2))); // Calculate the bigger radius
            int theta = (int)RadianToDegree(Math.Atan((double)(Config.Large / 2) / r)); // Calculate the angle theta

            g = Graphics.FromImage(bmp);

            box.Image = bmp;
            g.Clear(Color.White);

            Pen axisPen = new Pen(Color.Black, 1f);
            g.TranslateTransform(CentroX, CentroY); //dibujar ejes x y y
            g.DrawLine(axisPen, CentroX - 1, 0, CentroX * 2, 0); //eje x     //linea horizontal
            g.DrawLine(axisPen, 0, CentroY, 0, CentroY * -1); //eje y/        //linea vertical

            #endregion Pintar Ejes

            g = Graphics.FromImage(bmp);
            box.Image = bmp;

            // This calculates the bigger radius' lines' coordinates
            int x0 = LineCoord(Angle + theta, R, CentroX)[0];
            int y0 = LineCoord(Angle + theta, R, CentroY)[1];
            int x1 = LineCoord(Angle2 + 360 - theta, R, x0)[0];
            int y1 = LineCoord(Angle2 + 360 - theta, R, y0)[1];
            int x2 = LineCoord(Angle3 + 360 - theta, R, x1)[0];
            int y2 = LineCoord(Angle3 + 360 - theta, R, y1)[1];
            int x3 = LineCoord(Angle4 + 360 - theta, R, x2)[0];
            int y3 = LineCoord(Angle4 + 360 - theta, R, y2)[1];

            //Linea Red
            Point finalPointArm1 = new Point(LineCoord(Angle, r, CentroX)[0], LineCoord(Angle, r, CentroY)[1]);
            Point finalPointArm2 = new Point(LineCoord(Angle2, r, x0)[0], LineCoord(Angle2, r, y0)[1]);
            Point finalPointArm3 = new Point(LineCoord(Angle3, r, x1)[0], LineCoord(Angle3, r, y1)[1]);
            Point finalPointArm4 = new Point(LineCoord(Angle4, r, x2)[0], LineCoord(Angle4, r, y2)[1]);
            Point finalPointArmPA = new Point(LineCoord(Angle5, r, x3)[0], LineCoord(Angle5, r, y3)[1]);
            Point finalPointArmPB = new Point(LineCoord(Angle5 + 50, r, x3)[0], LineCoord(Angle5 + 50, r, y3)[1]);

            //Base
            g.DrawRectangle(new Pen(Color.Black, Config.PenSize), new Rectangle(new Point(CentroX - (SizeCircle * 3), CentroY), new Size(SizeCircle * 6, SizeCircle / 2)));

            //Lineas de Brazos
            g.DrawLine(new Pen(Color.Red, Config.PenSize), new Point(CentroX, CentroY), finalPointArm1);   //Linea Roja Primer Brazo
            g.DrawLine(new Pen(Color.Coral, Config.PenSize), finalPointArm1, finalPointArm2);              //Linea Blue Segundo Brazo
            g.DrawLine(new Pen(Color.Blue, Config.PenSize), finalPointArm2, finalPointArm3);               //Linea Coral Tercer Brazo
            g.DrawLine(new Pen(Color.DarkOrange, Config.PenSize), finalPointArm3, finalPointArm4);         //Linea DarkOrange Cuarto Brazo

            //arm
            if (statusGripper)
            {
                finalPointArmPB = finalPointArmPA;
                //this.btnGripper.Text = "Abrir Pinza";
            }
            else
            {
                //this.btnGripper.Text = "Cerrar pinza";
            }

            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArm4, finalPointArmPA); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArm4, finalPointArmPB); // From centar to points on the Perpendicular Line

            //Circulos De Unión
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm1.X - PositionCircle, finalPointArm1.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm2.X - PositionCircle, finalPointArm2.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm3.X - PositionCircle, finalPointArm3.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm4.X - PositionCircle, finalPointArm4.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));

            g.Dispose();
        }
    }
}