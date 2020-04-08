﻿using brazoRobot.ConfigLayer;
using brazoRobot.ModelLayer.Business;
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

        private int CentroX = 0;
        private int CentroY = 0;

        private OperationArm _orchestrator = null;

        private bool showPoint = false;
        private Thread paintThread;

        #endregion Var

        #region Get&Set

        public PictureBox box { get; set; }
        public Button btnGripper { get; set; }

        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                this.LblAxis1.Text = angle.ToString();
            }
        }

        public int Angle2
        {
            get => angle2;
            set
            {
                angle2 = value;
                this.LblAxis2.Text = angle2.ToString();
            }
        }

        public int Angle3
        {
            get => angle3;
            set
            {
                angle3 = value;
                this.LblAxis3.Text = angle3.ToString();
            }
        }

        public int Angle4
        {
            get => angle4;
            set
            {
                angle4 = value;
                this.LblAxis4.Text = angle4.ToString();
            }
        }

        public int Angle5
        {
            get => angle5;
            set
            {
                angle5 = value;
                this.LblAxis5.Text = angle5.ToString();
            }
        }

        public Graphics G { get => g; set => g = value; }
        public bool StatusGripper { get => statusGripper; set => statusGripper = value; }
        public bool StartRender { get => startRender; set => startRender = value; }
        public bool ShowPoint { get => showPoint; set => showPoint = value; }
        public Label LblAxis1 { get; set; }
        public Label LblAxis2 { get; set; }
        public Label LblAxis3 { get; set; }
        public Label LblAxis4 { get; set; }
        public Label LblAxis5 { get; set; }

        public OperationArm Orchestrator
        {
            get
            {
                if (_orchestrator == null)
                {
                    _orchestrator = new OperationArm(new Point(this.CentroX, this.CentroY));
                }
                return _orchestrator;
            }
        }

        #endregion Get&Set

        public Model()
        {
            this.box = new PictureBox();
            this.box.Size = new Size(530, 420);
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
                catch (Exception ex)
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
            this.CentroX = this.box.Width / 2;
            this.CentroY = this.box.Height / 10 * 9;

            bmp = new Bitmap(this.box.Width, this.box.Height);

            int SizeCircle = 15;
            int PositionCircle = 8;

            #region Pintar Ejes

            this.g = Graphics.FromImage(bmp);
            this.g.TranslateTransform(CentroX, CentroY); //dibujar ejes x y y
            this.g.ScaleTransform(1, -1);

            Pen axisPen = new Pen(Color.Black, 2);
            this.g.DrawLine(axisPen, CentroX * -1, 0, CentroX * 2, 0); //eje x     //linea horizontal
            this.g.DrawLine(axisPen, 0, CentroY, 0, CentroY * -1);  //eje y/        //linea vertical

            #endregion Pintar Ejes

            this.Orchestrator.Angle = this.angle;
            this.Orchestrator.Angle2 = this.angle2;
            this.Orchestrator.Angle3 = this.angle3;
            this.Orchestrator.Angle4 = this.angle4;
            this.Orchestrator.Angle5 = this.angle5;
            this.Orchestrator.StatusGripper = this.StatusGripper;

            this.Orchestrator.ManipulateArm();

            this.g = Graphics.FromImage(bmp);

            //Base
            g.DrawRectangle(new Pen(Color.Black, Config.PenSize), new Rectangle(new Point(CentroX - (SizeCircle * 3), CentroY), new Size(SizeCircle * 6, SizeCircle / 2)));

            //Lineas de Brazos
            g.DrawLine(new Pen(Color.Red, Config.PenSize), Orchestrator.ActualArm.Joints[0].InitialPoint, Orchestrator.ActualArm.Joints[0].FinalPoint);   //Linea Roja Primer Brazo
            g.DrawLine(new Pen(Color.Coral, Config.PenSize), Orchestrator.ActualArm.Joints[1].InitialPoint, Orchestrator.ActualArm.Joints[1].FinalPoint);              //Linea Blue Segundo Brazo
            g.DrawLine(new Pen(Color.Blue, Config.PenSize), Orchestrator.ActualArm.Joints[2].InitialPoint, Orchestrator.ActualArm.Joints[2].FinalPoint);               //Linea Coral Tercer Brazo
            g.DrawLine(new Pen(Color.DarkOrange, Config.PenSize), Orchestrator.ActualArm.Joints[3].InitialPoint, Orchestrator.ActualArm.Joints[3].FinalPoint);         //Linea DarkOrange Cuarto Brazo

            //Fill Poligon 1
            g.FillPolygon(new SolidBrush(Color.DarkBlue), Orchestrator.ActualArm.Joints[0].PolygonPoints);

            //Fill Polygon 2
            g.FillPolygon(new SolidBrush(Color.PaleGreen), Orchestrator.ActualArm.Joints[1].PolygonPoints);

            //Fill Polygon 3
            g.FillPolygon(new SolidBrush(Color.Olive), Orchestrator.ActualArm.Joints[2].PolygonPoints);

            //Fill Polygon 4
            g.FillPolygon(new SolidBrush(Color.YellowGreen), Orchestrator.ActualArm.Joints[3].PolygonPoints);

            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), Orchestrator.ActualArm.Gripper.BaseGripper, Orchestrator.ActualArm.Gripper.BaseGripperA); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), Orchestrator.ActualArm.Gripper.BaseGripper, Orchestrator.ActualArm.Gripper.BaseGripperB); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), Orchestrator.ActualArm.Gripper.BaseGripperA, Orchestrator.ActualArm.Gripper.BaseGripperC); // From centar to points on the Perpendicular Line
            g.DrawLine(new Pen(Color.FromArgb(100, 100, 0), Config.PenSize), Orchestrator.ActualArm.Gripper.BaseGripperB, Orchestrator.ActualArm.Gripper.BaseGripperD); // From centar to points on the Perpendicular Line

            //Circulos De Unión
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[0].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[0].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[1].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[1].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[2].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[2].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[3].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[3].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));

            if (showPoint)
            {
                Font drawFont = new Font("Arial", 8);

                string formatBase = "     ({0},{1})";

                g.DrawString(string.Format(formatBase, CentroX, CentroY), drawFont, Brushes.Red, (float)CentroX, (float)CentroY);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Joints[0].FinalPoint.X, Orchestrator.ActualArm.Joints[0].FinalPoint.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Joints[0].FinalPoint.X, Orchestrator.ActualArm.Joints[0].FinalPoint.Y);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Joints[1].FinalPoint.X, Orchestrator.ActualArm.Joints[1].FinalPoint.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Joints[1].FinalPoint.X, Orchestrator.ActualArm.Joints[1].FinalPoint.Y);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Joints[2].FinalPoint.X, Orchestrator.ActualArm.Joints[2].FinalPoint.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Joints[2].FinalPoint.X, Orchestrator.ActualArm.Joints[2].FinalPoint.Y);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Joints[3].FinalPoint.X, Orchestrator.ActualArm.Joints[3].FinalPoint.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Joints[3].FinalPoint.X - 80, Orchestrator.ActualArm.Joints[3].FinalPoint.Y);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Gripper.BaseGripperC.X, Orchestrator.ActualArm.Gripper.BaseGripperC.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Gripper.BaseGripperC.X - 80, Orchestrator.ActualArm.Gripper.BaseGripperC.Y);
                g.DrawString(string.Format(formatBase, Orchestrator.ActualArm.Gripper.BaseGripperD.X, Orchestrator.ActualArm.Gripper.BaseGripperD.Y), drawFont, Brushes.Red, Orchestrator.ActualArm.Gripper.BaseGripperD.X, Orchestrator.ActualArm.Gripper.BaseGripperD.Y);
            }

            this.box.Image = bmp;
            g.Dispose();
        }
    }
}