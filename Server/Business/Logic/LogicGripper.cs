using CommonLibrary.ConfigLayer;
using CommonLibrary.Entities.Arm.GripperBase;
using CommonLibrary.Entities.Graphics;
using CommonLibrary.Function;
using System;

namespace Server.Business.Logic
{
    internal static class LogicGripper
    {
        public static void RecalculatePointIntialEndPoint(int Angle, Gripper gripper)
        {
            gripper.R = (int)Math.Sqrt((Math.Pow((Config.Large / 2), 2) + Math.Pow(gripper.r, 2))); // Calculate the bigger radius
            int theta = (int)Functions.RadianToDegree(Math.Atan((double)(Config.Large / 2) / gripper.r));

            ////Center P5
            int x4 = Functions.LineCoord(Angle + 360 - theta, gripper.R, gripper.BaseGripper.X)[0];
            int y4 = Functions.LineCoord(Angle + 360 - theta, gripper.R, gripper.BaseGripper.Y)[1];

            gripper.BaseGripperA = new Point(Functions.LineCoord(Angle + 50, (gripper.r / 10) * 8, gripper.BaseGripper.X)[0], Functions.LineCoord(Angle + 50, (gripper.r / 10) * 8, gripper.BaseGripper.Y)[1]);
            gripper.BaseGripperB = new Point(Functions.LineCoord(Angle - 50, (gripper.r / 10) * 8, gripper.BaseGripper.X)[0], Functions.LineCoord(Angle - 50, (gripper.r / 10) * 8, gripper.BaseGripper.Y)[1]);

            if (gripper.Status)
            {
                gripper.BaseGripperC = new Point(Functions.LineCoord(Angle - 50, (gripper.r / 10) * 1, x4)[0], Functions.LineCoord(Angle - 50, (gripper.r / 10) * 1, y4)[1]);
                gripper.BaseGripperD = new Point(Functions.LineCoord(Angle + 50, (gripper.r / 10) * 1, x4)[0], Functions.LineCoord(Angle + 50, (gripper.r / 10) * 1, y4)[1]);

                //this.btnGripper.Text = "Abrir Pinza";
            }
            else
            {
                gripper.baseGripperC = new Point(Functions.LineCoord(Angle + 100, (gripper.r / 10) * 3, x4)[0], Functions.LineCoord(Angle + 100, (gripper.r / 10) * 3, y4)[1]);
                gripper.baseGripperD = new Point(Functions.LineCoord(Angle - 100, (gripper.r / 10) * 3, x4)[0], Functions.LineCoord(Angle - 100, (gripper.r / 10) * 3, y4)[1]);
            }
        }
    }
}