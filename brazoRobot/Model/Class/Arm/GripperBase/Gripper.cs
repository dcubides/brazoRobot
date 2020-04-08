using brazoRobot.ConfigLayer;
using brazoRobot.Function;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brazoRobot.ModelLayer.Class.Arm.GripperBase
{
    public class Gripper
    {
        private Point baseGripper;
        private Point baseGripperA;
        private Point baseGripperB;
        private Point baseGripperC;
        private Point baseGripperD;
        private int angle;
        private int R;
        private int r = 50;

        private bool status = false;

        public Point BaseGripper { get => baseGripper; set => baseGripper = value; }
        public Point BaseGripperA { get => baseGripperA; set => baseGripperA = value; }
        public Point BaseGripperB { get => baseGripperB; set => baseGripperB = value; }
        public Point BaseGripperC { get => baseGripperC; set => baseGripperC = value; }
        public Point BaseGripperD { get => baseGripperD; set => baseGripperD = value; }

        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                RecalculatePointIntialEndPoint();
            }
        }

        public bool Status { get => status; set => status = value; }

        private void RecalculatePointIntialEndPoint()
        {
            R = (int)Math.Sqrt((Math.Pow((Config.Large / 2), 2) + Math.Pow(r, 2))); // Calculate the bigger radius
            int theta = (int)Functions.RadianToDegree(Math.Atan((double)(Config.Large / 2) / r));

            ////Center P5
            int x4 = Functions.LineCoord(Angle + 360 - theta, R, this.BaseGripper.X)[0];
            int y4 = Functions.LineCoord(Angle + 360 - theta, R, this.BaseGripper.Y)[1];

            this.BaseGripperA = new Point(Functions.LineCoord(Angle + 50, (r / 10) * 8, this.BaseGripper.X)[0], Functions.LineCoord(Angle + 50, (r / 10) * 8, this.BaseGripper.Y)[1]);
            this.BaseGripperB = new Point(Functions.LineCoord(Angle - 50, (r / 10) * 8, this.BaseGripper.X)[0], Functions.LineCoord(Angle - 50, (r / 10) * 8, this.BaseGripper.Y)[1]);

            if (Status)
            {
                this.BaseGripperC = new Point(Functions.LineCoord(Angle - 50, (r / 10) * 1, x4)[0], Functions.LineCoord(Angle - 50, (r / 10) * 1, y4)[1]);
                this.BaseGripperD = new Point(Functions.LineCoord(Angle + 50, (r / 10) * 1, x4)[0], Functions.LineCoord(Angle + 50, (r / 10) * 1, y4)[1]);

                //this.btnGripper.Text = "Abrir Pinza";
            }
            else
            {
                this.baseGripperC = new Point(Functions.LineCoord(Angle + 100, (r / 10) * 3, x4)[0], Functions.LineCoord(Angle + 100, (r / 10) * 3, y4)[1]);
                this.baseGripperD = new Point(Functions.LineCoord(Angle - 100, (r / 10) * 3, x4)[0], Functions.LineCoord(Angle - 100, (r / 10) * 3, y4)[1]);
            }
        }

        public Gripper(int angle, bool statusGripper, Point initialPoint)
        {
            this.angle = angle;
            this.Status = statusGripper;
            this.baseGripper = initialPoint;
            this.RecalculatePointIntialEndPoint();
        }
    }
}