using CommonLibrary.ConfigLayer;
using CommonLibrary.Function;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Entities.Arm.GripperBase
{
    public class Gripper
    {
        public Point baseGripper;
        public Point baseGripperA;
        public Point baseGripperB;
        public Point baseGripperC;
        public Point baseGripperD;
        public int angle;
        public int R;
        public int r = 50;

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
            }
        }

        public bool Status { get => status; set => status = value; }

        public Gripper(int angle, bool statusGripper, Point initialPoint)
        {
            this.angle = angle;
            this.Status = statusGripper;
            this.baseGripper = initialPoint;
        }
    }
}