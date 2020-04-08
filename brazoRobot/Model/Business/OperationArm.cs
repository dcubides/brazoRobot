using brazoRobot.ConfigLayer;
using brazoRobot.Function;
using brazoRobot.ModelLayer.Class.Arm;
using brazoRobot.ModelLayer.Class.Arm.GripperBase;
using brazoRobot.ModelLayer.Class.Arm.Polygon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brazoRobot.ModelLayer.Business
{
    public class OperationArm
    {
        public OperationArm(Point _originPointArm)
        {
            this.OriginPointArm = _originPointArm;
        }

        private int R;
        private int r = 0;
        private int angle = 0;
        private int angle2 = 0;
        private int angle3 = 0;
        private int angle4 = 0;
        private int angle5 = 0;
        private bool statusGripper = false;

        private Point originPointArm;

        public Point OriginPointArm { get => originPointArm; set => originPointArm = value; }

        private Arm actualArm = null;

        public Arm ActualArm
        {
            get
            {
                if (actualArm == null)
                {
                    actualArm = ManipulateArm();
                }
                return actualArm;
            }
        }

        public int Angle { get => angle; set => angle = value; }
        public int Angle2 { get => angle2; set => angle2 = value; }
        public int Angle3 { get => angle3; set => angle3 = value; }
        public int Angle4 { get => angle4; set => angle4 = value; }
        public int Angle5 { get => angle5; set => angle5 = value; }
        public bool StatusGripper { get => statusGripper; set => statusGripper = value; }

        /// <summary>
        /// This method build the starter specification
        /// </summary>
        /// <param name="_OriginPointArm">Origin point in map</param>
        /// <returns>object arm to modify</returns>
        public Arm ManipulateArm()
        {
            UnitPolygon joint1;
            UnitPolygon joint2;
            UnitPolygon joint3;
            UnitPolygon joint4;
            Gripper gripper;
            if (actualArm == null)
            {
                joint1 = new UnitPolygon(Angle, this.OriginPointArm);
                joint2 = new UnitPolygon(Angle2, joint1.FinalPoint);
                joint3 = new UnitPolygon(Angle3, joint2.FinalPoint);
                joint4 = new UnitPolygon(Angle4, joint3.FinalPoint);
                gripper = new Gripper(Angle5, StatusGripper, joint4.FinalPoint);

                this.actualArm = new Arm(gripper, new List<UnitPolygon>() { joint1, joint2, joint3, joint4 });
            }
            else
            {
                joint1 = actualArm.Joints[0];
                joint1.Angle = Angle;
                joint2 = actualArm.Joints[1];
                joint2.InitialPoint = joint1.FinalPoint;
                joint2.Angle = Angle2;
                joint3 = actualArm.Joints[2];
                joint3.InitialPoint = joint2.FinalPoint;
                joint3.Angle = Angle3;
                joint4 = actualArm.Joints[3];
                joint4.InitialPoint = joint3.FinalPoint;
                joint4.Angle = Angle4;
                gripper = actualArm.Gripper;
                gripper.BaseGripper = joint4.FinalPoint;
                gripper.Status = StatusGripper;
                gripper.Angle = Angle5;
            }

            return actualArm;
        }
    }
}