using brazoRobot.Business;
using CommonLibrary.ConfigLayer;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace brazoRobot.ModelLayer
{
    /// <summary>
    /// Model of MVC
    /// </summary>
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

        private ConnectionSocket _orchestrator = null;

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
                Orchestrator.controlObject.Angle = angle;
                Orchestrator.SendRequest();
                this.LblAxis1.Text = Orchestrator.ActualArm.Joints[0].Angle.ToString();
            }
        }

        public int Angle2
        {
            get => angle2;
            set
            {
                angle2 = value;
                Orchestrator.controlObject.Angle2 = angle2;
                Orchestrator.SendRequest();
                this.LblAxis2.Text = Orchestrator.ActualArm.Joints[1].Angle.ToString();
            }
        }

        public int Angle3
        {
            get => angle3;
            set
            {
                angle3 = value;
                Orchestrator.controlObject.Angle3 = angle3;
                Orchestrator.SendRequest();
                this.LblAxis3.Text = Orchestrator.ActualArm.Joints[2].Angle.ToString();
            }
        }

        public int Angle4
        {
            get => angle4;
            set
            {
                angle4 = value;
                Orchestrator.controlObject.Angle4 = angle4;
                Orchestrator.SendRequest();
                this.LblAxis4.Text = Orchestrator.ActualArm.Joints[3].Angle.ToString();
            }
        }

        public int Angle5
        {
            get => angle5;
            set
            {
                angle5 = value;
                Orchestrator.controlObject.Angle5 = angle5;
                Orchestrator.SendRequest();
                this.LblAxis5.Text = Orchestrator.ActualArm.Joints[4].Angle.ToString();
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

        public ConnectionSocket Orchestrator
        {
            get
            {
                if (_orchestrator == null)
                {
                    _orchestrator = new ConnectionSocket();
                }
                return _orchestrator;
            }
        }

        #endregion Get&Set

        #region ctor

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

        #endregion ctor

        #region Thread

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

        #endregion Thread

        #region PublicMethods

        public void Render()
        {
            this.CentroX = this.box.Width / 2;
            this.CentroY = this.box.Height / 10 * 9;

            bmp = new Bitmap(this.box.Width, this.box.Height);

            int SizeCircle = 15;
            int PositionCircle = 8;

            DrawAxisMap();

            #region Alter Angles Arm

            //this.Orchestrator.StatusGripper = this.StatusGripper;

            #endregion Alter Angles Arm

            //this.Orchestrator.ManipulateArm();

            this.g = Graphics.FromImage(bmp);

            DrawArm(SizeCircle, PositionCircle);

            this.box.Image = bmp;

            this.g.Dispose();
        }

        #endregion PublicMethods

        #region PrivateMethods

        /// <summary>
        /// Manage the fases of the draw
        /// </summary>
        /// <param name="SizeCircle"></param>
        /// <param name="PositionCircle"></param>
        private void DrawArm(int SizeCircle, int PositionCircle)
        {
            DrawArmParts(SizeCircle);

            DrawUnions(SizeCircle, PositionCircle);

            ShowPointsInGraphic();
        }

        /// <summary>
        /// Method for draw all the joints and grapple of arm
        /// </summary>
        /// <param name="SizeCircle"></param>
        private void DrawArmParts(int SizeCircle)
        {
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
        }

        /// <summary>
        /// Method for draw the axis x and y inside the picture box
        /// </summary>
        private void DrawAxisMap()
        {
            this.g = Graphics.FromImage(bmp);
            this.g.TranslateTransform(CentroX, CentroY); //dibujar ejes x y y
            this.g.ScaleTransform(1, -1);

            Pen axisPen = new Pen(Color.Black, 2);
            this.g.DrawLine(axisPen, CentroX * -1, 0, CentroX * 2, 0); //eje x     //linea horizontal
            this.g.DrawLine(axisPen, 0, CentroY, 0, CentroY * -1);  //eje y/        //linea vertical
        }

        /// <summary>
        /// Method for put the point inside of the graphic
        /// </summary>
        private void ShowPointsInGraphic()
        {
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
        }

        /// <summary>
        /// Method for draw the union points
        /// </summary>
        /// <param name="SizeCircle"></param>
        /// <param name="PositionCircle"></param>
        private void DrawUnions(int SizeCircle, int PositionCircle)
        {
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[0].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[0].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[1].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[1].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[2].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[2].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(Orchestrator.ActualArm.Joints[3].FinalPoint.X - PositionCircle, Orchestrator.ActualArm.Joints[3].FinalPoint.Y - PositionCircle), new Size(SizeCircle, SizeCircle)));
        }

        #endregion PrivateMethods
    }
}