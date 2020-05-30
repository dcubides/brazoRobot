﻿using Api.Business.Logic;
using Api.Interfaces.Business;
using CommonLibrary.Entities.Angle;
using CommonLibrary.Entities.Arm;
using CommonLibrary.Entities.Arm.GripperBase;
using CommonLibrary.Entities.Arm.Polygon;
using CommonLibrary.Entities.Graphics;
using System.Collections.Generic;

namespace Api.Business
{
    public class OperationArm : IOperationArm
    {
     
        private int R;
        private int r = 0;
        private int angle = 0;
        private int angle2 = 0;
        private int angle3 = 0;
        private int angle4 = 0;
        private int angle5 = 0;
        private bool statusGripper = false;
        
        private Point originPointArm = new Point(265,378);

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
                LogicPolygon.RecalculatePointIntialEndPoint(Angle, joint1);
                joint2 = new UnitPolygon(Angle2, joint1.FinalPoint);
                LogicPolygon.RecalculatePointIntialEndPoint(Angle2, joint2);
                joint3 = new UnitPolygon(Angle3, joint2.FinalPoint);
                LogicPolygon.RecalculatePointIntialEndPoint(Angle3, joint3);
                joint4 = new UnitPolygon(Angle4, joint3.FinalPoint);
                LogicPolygon.RecalculatePointIntialEndPoint(Angle4, joint4);
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

            LogicPolygon.RecalculatePointIntialEndPoint(Angle, joint1);
            LogicPolygon.RecalculatePointIntialEndPoint(Angle2, joint2);
            LogicPolygon.RecalculatePointIntialEndPoint(Angle3, joint3);
            LogicPolygon.RecalculatePointIntialEndPoint(Angle4, joint4);
            LogicGripper.RecalculatePointIntialEndPoint(Angle5, gripper);

            return actualArm;
        }

        public Arm AlterArm(Controls controls)
        {
            this.Angle = controls.Angle != 0 ? controls.Angle : this.Angle;
            this.Angle2 = controls.Angle2 != 0 ? controls.Angle2 : this.Angle2;
            this.Angle3 = controls.Angle3 != 0 ? controls.Angle3 : this.Angle3;
            this.Angle4 = controls.Angle4 != 0 ? controls.Angle4 : this.Angle4;
            this.Angle5 = controls.Angle5 != 0 ? controls.Angle5 : this.Angle5;
            this.StatusGripper = controls.StatusGripper;
            return this.ManipulateArm();
        }
    }
}