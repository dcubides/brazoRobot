using CommonLibrary.ConfigLayer;
using CommonLibrary.Entities.Arm.Polygon;
using CommonLibrary.Entities.Graphics;
using CommonLibrary.Function;
using System;


namespace Server.Business.Logic
{
    public static class LogicPolygon
    {
        public static void RecalculatePointIntialEndPoint(int Angle, UnitPolygon polygon)
        {
            int theta = (int)Functions.RadianToDegree(Math.Atan((double)(Config.Large / 2) / polygon.r));
            int x0;
            int y0;
            x0 = Functions.LineCoord(Angle + 360 - theta, polygon.R, polygon.InitialPoint.X)[0];
            y0 = Functions.LineCoord(Angle + 360 - theta, polygon.R, polygon.InitialPoint.Y)[1];
            polygon.FinalPoint = new Point(Functions.LineCoord(Angle, polygon.r, x0)[0], Functions.LineCoord(Angle, polygon.r, y0)[1]);

            Point basePointPolygonArm = polygon.InitialPoint;
            Point leftPointPolygonArm = new Point(Functions.LineCoord(Angle - 90, (polygon.r / 10) * 5, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 90, (polygon.r / 10) * 5, basePointPolygonArm.Y)[1]);
            Point leftMiddlePointPolygonArm = new Point(Functions.LineCoord(Angle - 45, (polygon.r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 45, (polygon.r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point leftMiddleUpperPointPolygonArm = new Point(Functions.LineCoord(Angle - 45, (polygon.r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 45, (polygon.r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point upperPointPolygonArm = polygon.FinalPoint;
            Point rigthMiddleUpperPointPolygonArm = new Point(Functions.LineCoord(Angle + 45, (polygon.r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 45, (polygon.r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point rigthMiddlePointPolygonArm = new Point(Functions.LineCoord(Angle + 45, (polygon.r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 45, (polygon.r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point rigthPointPolygonArm = new Point(Functions.LineCoord(Angle + 90, (polygon.r / 10) * 5, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 90, (polygon.r / 10) * 5, basePointPolygonArm.Y)[1]);

            polygon.PolygonPoints = new Point[] { basePointPolygonArm,
                                               leftPointPolygonArm,
                                               leftMiddlePointPolygonArm,
                                               leftMiddleUpperPointPolygonArm,
                                               upperPointPolygonArm,
                                               rigthMiddleUpperPointPolygonArm,
                                               rigthMiddlePointPolygonArm,
                                               rigthPointPolygonArm
            };
        }
    }
}