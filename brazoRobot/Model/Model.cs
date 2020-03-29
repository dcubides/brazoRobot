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
        public bool StartRender { get => startRender; set => startRender = value; }

        #endregion Get&Set

        public Model()
        {
            this.box = new PictureBox();
            this.btnGripper = new Button();
            this.paintThread = new Thread(new ThreadStart(Run));
            this.StartRender = true;
            this.paintThread.Start();
            r = 50;
        }

        public void StopThread()
        {
            this.paintThread.Abort();
        }

        private void Run()
        {
            while (this.StartRender)
            {
                try
                {
                    Thread.Sleep(10);
                    this.Render();
                }
                catch (Exception)
                {
                }
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

            this.g = Graphics.FromImage(bmp);
            this.g.TranslateTransform(CentroX, CentroY); //dibujar ejes x y y
            this.g.ScaleTransform(1, -1);

            Pen axisPen = new Pen(Color.Black, 2);
            this.g.DrawLine(axisPen, CentroX * -1, 0, CentroX * 2, 0); //eje x     //linea horizontal
            this.g.DrawLine(axisPen, 0, CentroY, 0, CentroY * -1);  //eje y/        //linea vertical

            #endregion Pintar Ejes

            this.g = Graphics.FromImage(bmp);
            this.box.Image = bmp;

            // This calculates the bigger radius' lines' coordinates

            // Center p1
            int x0 = LineCoord(Angle + theta, R, CentroX)[0];
            int y0 = LineCoord(Angle + theta, R, CentroY)[1];
            // Center P2
            int x1 = LineCoord(Angle2 + 360 - theta, R, x0)[0];
            int y1 = LineCoord(Angle2 + 360 - theta, R, y0)[1];
            //Center P3
            int x2 = LineCoord(Angle3 + 360 - theta, R, x1)[0];
            int y2 = LineCoord(Angle3 + 360 - theta, R, y1)[1];
            //Center P4
            int x3 = LineCoord(Angle4 + 360 - theta, R, x2)[0];
            int y3 = LineCoord(Angle4 + 360 - theta, R, y2)[1];

            //Center P5
            int x4 = LineCoord(Angle4 + 360 - theta, R, x3)[0];
            int y4 = LineCoord(Angle4 + 360 - theta, R, y3)[1];

            //Center P6
            int x5 = LineCoord(Angle4 + 360 - theta, R, x3)[0];
            int y5 = LineCoord(Angle4 + 360 - theta, R, y3)[1];

            Point finalPointArm1 = new Point(LineCoord(Angle, r, CentroX)[0], LineCoord(Angle, r, CentroY)[1]);
            Point finalPointArm2 = new Point(LineCoord(Angle2, r, x0)[0], LineCoord(Angle2, r, y0)[1]);
            Point finalPointArm3 = new Point(LineCoord(Angle3, r, x1)[0], LineCoord(Angle3, r, y1)[1]);
            Point finalPointArm4 = new Point(LineCoord(Angle4, r, x2)[0], LineCoord(Angle4, r, y2)[1]);
            Point finalPointArmPA = new Point(LineCoord(Angle5, r, x3)[0], LineCoord(Angle5, r, y3)[1]);
            Point finalPointArmPB = new Point(LineCoord(Angle5 + 50, (r / 10) * 8, x3)[0], LineCoord(Angle5 + 50, (r / 10) * 8, y3)[1]);
            Point finalPointArmPC = new Point(LineCoord(Angle5 - 50, (r / 10) * 8, x3)[0], LineCoord(Angle5 - 50, (r / 10) * 8, y3)[1]);
            Point finalPointArmPD = new Point(LineCoord(Angle5 - 50, (r / 10) * 1, x4)[0], LineCoord(Angle5 - 50, (r / 10) * 1, y4)[1]);
            Point finalPointArmPE = new Point(LineCoord(Angle5 + 50, (r / 10) * 1, x5)[0], LineCoord(Angle5 + 50, (r / 10) * 1, y5)[1]);

            //Rule 1
            Point basePointPolygonRule = new Point(CentroX, CentroY);
            Point leftPointPolygonRule = new Point(LineCoord(Angle - 90, r, basePointPolygonRule.X)[0], LineCoord(Angle - 90, r, basePointPolygonRule.Y)[1]);
            Point leftMiddlePointPolygonRule = new Point(LineCoord(Angle - 45, r, basePointPolygonRule.X)[0], LineCoord(Angle - 45, r, basePointPolygonRule.Y)[1]);
            Point upperPointPolygonRule = finalPointArm1;
            Point rigthMiddlePointPolygonRule = new Point(LineCoord(Angle + 45, r, basePointPolygonRule.X)[0], LineCoord(Angle + 45, r, basePointPolygonRule.Y)[1]);
            Point rigthPointPolygonRule = new Point(LineCoord(Angle + 90, r, basePointPolygonRule.X)[0], LineCoord(Angle + 90, r, basePointPolygonRule.Y)[1]);

            Point[] pointsRule = new Point[] { basePointPolygonRule,
                                               leftPointPolygonRule,
                                               leftMiddlePointPolygonRule,
                                               upperPointPolygonRule,
                                               rigthMiddlePointPolygonRule,
                                               rigthPointPolygonRule
            };

            //Arm 1
            Point basePointPolygonArm1 = new Point(CentroX, CentroY);
            Point leftPointPolygonArm1 = new Point(LineCoord(Angle - 90, (r / 10) * 5, basePointPolygonArm1.X)[0], LineCoord(Angle - 90, (r / 10) * 5, basePointPolygonArm1.Y)[1]);
            Point leftMiddlePointPolygonArm1 = new Point(LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm1.X)[0], LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm1.Y)[1]);
            Point leftMiddleUpperPointPolygonArm1 = new Point(LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm1.X)[0], LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm1.Y)[1]);
            Point upperPointPolygonArm1 = finalPointArm1;
            Point rigthMiddleUpperPointPolygonArm1 = new Point(LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm1.X)[0], LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm1.Y)[1]);
            Point rigthMiddlePointPolygonArm1 = new Point(LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm1.X)[0], LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm1.Y)[1]);
            Point rigthPointPolygonArm1 = new Point(LineCoord(Angle + 90, (r / 10) * 5, basePointPolygonArm1.X)[0], LineCoord(Angle + 90, (r / 10) * 5, basePointPolygonArm1.Y)[1]);

            Point[] pointsArm1 = new Point[] { basePointPolygonArm1,
                                               leftPointPolygonArm1,
                                               leftMiddlePointPolygonArm1,
                                               leftMiddleUpperPointPolygonArm1,
                                               upperPointPolygonArm1,
                                               rigthMiddleUpperPointPolygonArm1,
                                               rigthMiddlePointPolygonArm1,
                                               rigthPointPolygonArm1
            };

            //Rule 2
            Point basePointPolygonRule2 = finalPointArm1;
            Point leftPointPolygonRule2 = new Point(LineCoord(Angle2 - 90, r, finalPointArm1.X)[0], LineCoord(Angle2 - 90, r, finalPointArm1.Y)[1]);
            Point leftMiddlePointPolygonRule2 = new Point(LineCoord(Angle2 - 45, r, finalPointArm1.X)[0], LineCoord(Angle2 - 45, r, finalPointArm1.Y)[1]);
            Point upperPointPolygonRule2 = finalPointArm2;
            Point rigthMiddlePointPolygonRule2 = new Point(LineCoord(Angle2 + 45, r, finalPointArm1.X)[0], LineCoord(Angle2 + 45, r, finalPointArm1.Y)[1]);
            Point rigthPointPolygonRule2 = new Point(LineCoord(Angle2 + 90, r, finalPointArm1.X)[0], LineCoord(Angle2 + 90, r, finalPointArm1.Y)[1]);

            Point[] pointsRule2 = new Point[] { basePointPolygonRule2,
                                               leftPointPolygonRule2,
                                               leftMiddlePointPolygonRule2,
                                               upperPointPolygonRule2,
                                               rigthMiddlePointPolygonRule2,
                                               rigthPointPolygonRule2
            };

            //Arm 2
            Point basePointPolygonArm2 = finalPointArm1;
            Point leftPointPolygonArm2 = new Point(LineCoord(Angle2 - 90, (r / 10) * 5, x0)[0], LineCoord(Angle2 - 90, (r / 10) * 5, y0)[1]);
            Point leftMiddlePointPolygonArm2 = new Point(LineCoord(Angle2 - 45, (r / 10) * 7, x0)[0], LineCoord(Angle2 - 45, (r / 10) * 7, y0)[1]);
            Point leftMiddleUpperPointPolygonArm2 = new Point(LineCoord(Angle2 - 45, (r / 10) * 7, x0)[0], LineCoord(Angle2 - 45, (r / 10) * 7, y0)[1]);
            Point upperPointPolygonArm2 = finalPointArm2;
            Point rigthMiddleUpperPointPolygonArm2 = new Point(LineCoord(Angle2 + 45, (r / 10) * 7, x0)[0], LineCoord(Angle2 + 45, (r / 10) * 7, y0)[1]);
            Point rigthMiddlePointPolygonArm2 = new Point(LineCoord(Angle2 + 45, (r / 10) * 7, x0)[0], LineCoord(Angle2 + 45, (r / 10) * 7, y0)[1]);
            Point rigthPointPolygonArm2 = new Point(LineCoord(Angle2 + 90, (r / 10) * 5, x0)[0], LineCoord(Angle2 + 90, (r / 10) * 5, y0)[1]);

            Point[] pointsArm2 = new Point[] { basePointPolygonArm2,
                                               leftPointPolygonArm2,
                                               leftMiddlePointPolygonArm2,
                                               leftMiddleUpperPointPolygonArm2,
                                               upperPointPolygonArm2,
                                               rigthMiddleUpperPointPolygonArm2,
                                               rigthMiddlePointPolygonArm2,
                                               rigthPointPolygonArm2
            };

            //Rule 3
            Point basePointPolygonRule3 = finalPointArm2;
            Point leftPointPolygonRule3 = new Point(LineCoord(Angle3 - 90, r, basePointPolygonRule3.X)[0], LineCoord(Angle3 - 90, r, basePointPolygonRule3.Y)[1]);
            Point leftMiddlePointPolygonRule3 = new Point(LineCoord(Angle3 - 45, r, basePointPolygonRule3.X)[0], LineCoord(Angle3 - 45, r, basePointPolygonRule3.Y)[1]);
            Point upperPointPolygonRule3 = finalPointArm3;
            Point rigthMiddlePointPolygonRule3 = new Point(LineCoord(Angle3 + 45, r, basePointPolygonRule3.X)[0], LineCoord(Angle3 + 45, r, basePointPolygonRule3.Y)[1]);
            Point rigthPointPolygonRule3 = new Point(LineCoord(Angle3 + 90, r, basePointPolygonRule3.X)[0], LineCoord(Angle3 + 90, r, basePointPolygonRule3.Y)[1]);

            Point[] pointsRule3 = new Point[] { basePointPolygonRule3,
                                               leftPointPolygonRule3,
                                               leftMiddlePointPolygonRule3,
                                               upperPointPolygonRule3,
                                               rigthMiddlePointPolygonRule3,
                                               rigthPointPolygonRule3
            };

            //Arm 3
            Point basePointPolygonArm3 = finalPointArm2;
            Point leftPointPolygonArm3 = new Point(LineCoord(Angle3 - 90, (r / 10) * 5, basePointPolygonArm3.X)[0], LineCoord(Angle3 - 90, (r / 10) * 5, basePointPolygonArm3.Y)[1]);
            Point leftMiddlePointPolygonArm3 = new Point(LineCoord(Angle3 - 45, (r / 10) * 7, basePointPolygonArm3.X)[0], LineCoord(Angle3 - 45, (r / 10) * 7, basePointPolygonArm3.Y)[1]);
            Point leftMiddleUpperPointPolygonArm3 = new Point(LineCoord(Angle3 - 45, (r / 10) * 7, basePointPolygonArm3.X)[0], LineCoord(Angle3 - 45, (r / 10) * 7, basePointPolygonArm3.Y)[1]);
            Point upperPointPolygonArm3 = finalPointArm3;
            Point rigthMiddleUpperPointPolygonArm3 = new Point(LineCoord(Angle3 + 45, (r / 10) * 7, basePointPolygonArm3.X)[0], LineCoord(Angle3 + 45, (r / 10) * 7, basePointPolygonArm3.Y)[1]);
            Point rigthMiddlePointPolygonArm3 = new Point(LineCoord(Angle3 + 45, (r / 10) * 7, basePointPolygonArm3.X)[0], LineCoord(Angle3 + 45, (r / 10) * 7, basePointPolygonArm3.Y)[1]);
            Point rigthPointPolygonArm3 = new Point(LineCoord(Angle3 + 90, (r / 10) * 5, basePointPolygonArm3.X)[0], LineCoord(Angle3 + 90, (r / 10) * 5, basePointPolygonArm3.Y)[1]);

            Point[] pointsArm3 = new Point[] { basePointPolygonArm3,
                                               leftPointPolygonArm3,
                                               leftMiddlePointPolygonArm3,
                                               leftMiddleUpperPointPolygonArm3,
                                               upperPointPolygonArm3,
                                               rigthMiddleUpperPointPolygonArm3,
                                               rigthMiddlePointPolygonArm3,
                                               rigthPointPolygonArm3
            };

            //Rule 3
            Point basePointPolygonRule4 = finalPointArm3;
            Point leftPointPolygonRule4 = new Point(LineCoord(Angle4 - 90, r, basePointPolygonRule4.X)[0], LineCoord(Angle4 - 90, r, basePointPolygonRule4.Y)[1]);
            Point leftMiddlePointPolygonRule4 = new Point(LineCoord(Angle4 - 45, r, basePointPolygonRule4.X)[0], LineCoord(Angle4 - 45, r, basePointPolygonRule4.Y)[1]);
            Point upperPointPolygonRule4 = finalPointArm4;
            Point rigthMiddlePointPolygonRule4 = new Point(LineCoord(Angle3 + 45, r, basePointPolygonRule4.X)[0], LineCoord(Angle4 + 45, r, basePointPolygonRule4.Y)[1]);
            Point rigthPointPolygonRule4 = new Point(LineCoord(Angle3 + 90, r, basePointPolygonRule4.X)[0], LineCoord(Angle4 + 90, r, basePointPolygonRule4.Y)[1]);

            Point[] pointsRule4 = new Point[] { basePointPolygonRule4,
                                               leftPointPolygonRule4,
                                               leftMiddlePointPolygonRule4,
                                               upperPointPolygonRule4,
                                               rigthMiddlePointPolygonRule4,
                                               rigthPointPolygonRule4
            };

            //Arm 4
            Point basePointPolygonArm4 = finalPointArm3;
            Point leftPointPolygonArm4 = new Point(LineCoord(Angle4 - 90, (r / 10) * 3, basePointPolygonArm4.X)[0], LineCoord(Angle4 - 90, (r / 10) * 3, basePointPolygonArm4.Y)[1]);
            Point leftMiddlePointPolygonArm4 = new Point(LineCoord(Angle4 - 45, (r / 10) * 4, basePointPolygonArm4.X)[0], LineCoord(Angle4 - 45, (r / 10) * 4, basePointPolygonArm4.Y)[1]);
            Point leftMiddleUpperPointPolygonArm4 = new Point(LineCoord(Angle4 - 45, (r / 10) * 5, basePointPolygonArm4.X)[0], LineCoord(Angle4 - 45, (r / 10) * 5, basePointPolygonArm4.Y)[1]);
            Point upperPointPolygonArm4 = finalPointArm4;
            Point rigthMiddleUpperPointPolygonArm4 = new Point(LineCoord(Angle4 + 60, (r / 10) * 5, basePointPolygonArm4.X)[0], LineCoord(Angle4 + 60, (r / 10) * 5, basePointPolygonArm4.Y)[1]);
            Point rigthMiddlePointPolygonArm4 = new Point(LineCoord(Angle4 + 45, (r / 10) * 4, basePointPolygonArm4.X)[0], LineCoord(Angle4 + 45, (r / 10) * 4, basePointPolygonArm4.Y)[1]);
            Point rigthPointPolygonArm4 = new Point(LineCoord(Angle4 + 90, (r / 10) * 3, basePointPolygonArm4.X)[0], LineCoord(Angle4 + 90, (r / 10) * 3, basePointPolygonArm4.Y)[1]);

            Point[] pointsArm4 = new Point[] { basePointPolygonArm4,
                                               leftPointPolygonArm4,
                                               leftMiddlePointPolygonArm4,
                                               leftMiddleUpperPointPolygonArm4,
                                               upperPointPolygonArm4,
                                               rigthMiddleUpperPointPolygonArm4,
                                               rigthMiddlePointPolygonArm4,
                                               rigthPointPolygonArm4
            };

            //Base
            g.DrawRectangle(new Pen(Color.Black, Config.PenSize), new Rectangle(new Point(CentroX - (SizeCircle * 3), CentroY), new Size(SizeCircle * 6, SizeCircle / 2)));

            //Lineas de Brazos
            g.DrawLine(new Pen(Color.Red, Config.PenSize), new Point(CentroX, CentroY), finalPointArm1);   //Linea Roja Primer Brazo
            g.DrawLine(new Pen(Color.Coral, Config.PenSize), finalPointArm1, finalPointArm2);              //Linea Blue Segundo Brazo
            g.DrawLine(new Pen(Color.Blue, Config.PenSize), finalPointArm2, finalPointArm3);               //Linea Coral Tercer Brazo
            g.DrawLine(new Pen(Color.DarkOrange, Config.PenSize), finalPointArm3, finalPointArm4);         //Linea DarkOrange Cuarto Brazo

            //Fill Poligon 1
            g.FillPolygon(new SolidBrush(Color.DarkBlue), pointsArm1);

            //Draw Ruler 1
            //foreach (var item in pointsRule)
            //{
            //    g.DrawEllipse(new Pen(Color.BlueViolet, Config.PenSize), item.X, item.Y, 5, 5);
            //}

            //Fill Polygon 2
            g.FillPolygon(new SolidBrush(Color.PaleGreen), pointsArm2);

            //Draw Ruler 2
            //foreach (var item in pointsRule2)
            //{
            //    g.DrawEllipse(new Pen(Color.MediumBlue, Config.PenSize), item.X, item.Y, 5, 5);
            //}

            //Fill Polygon 3
            g.FillPolygon(new SolidBrush(Color.Olive), pointsArm3);

            //Draw Ruler 3
            //foreach (var item in pointsRule3)
            //{
            //    g.DrawEllipse(new Pen(Color.Firebrick, Config.PenSize), item.X, item.Y, 5, 5);
            //}

            //Fill Polygon 4
            g.FillPolygon(new SolidBrush(Color.YellowGreen), pointsArm4);

            //Draw Ruler 4
            //foreach (var item in pointsRule4)
            //{
            //    g.DrawEllipse(new Pen(Color.Firebrick, Config.PenSize), item.X, item.Y, 5, 5);
            //}

            //arm
            //if (statusGripper)
            //{
            //    finalPointArmPB = finalPointArmPA;
            //    //this.btnGripper.Text = "Abrir Pinza";
            //}
            //else
            //{
            //    //this.btnGripper.Text = "Cerrar pinza";
            //}

            //g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArm4, finalPointArmPA); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArm4, finalPointArmPB); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArm4, finalPointArmPC); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArmPB, finalPointArmPA); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), finalPointArmPC, finalPointArmPA); // From centar to points on the Perpendicular Line

            //Circulos De Unión
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm1.X - PositionCircle, finalPointArm1.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm2.X - PositionCircle, finalPointArm2.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm3.X - PositionCircle, finalPointArm3.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(finalPointArm4.X - PositionCircle, finalPointArm4.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));

            g.Dispose();
        }
    }
}