using CommonLibrary.ConfigLayer;
using CommonLibrary.Function;
using System;
using System.Drawing;

namespace CommonLibrary.Entities.Arm.Polygon
{
    public class UnitPolygon
    {
        private int R;
        private int r = 50;

        #region ctor

        public UnitPolygon(int _Angle, Point _Init)
        {
            this.angle = _Angle;
            this.initialPoint = _Init;
            this.RecalculatePointIntialEndPoint();
        }

        #endregion ctor

        #region fields

        private int angle;
        private Point initialPoint;
        private Point finalPoint;
        private Point[] polygonPoints;

        #endregion fields

        #region Gets & Sets

        public int Angle
        {
            get => angle;
            set
            {
                angle = value;
                RecalculatePointIntialEndPoint();
            }
        }

        private void RecalculatePointIntialEndPoint()
        {
            int theta = (int)Functions.RadianToDegree(Math.Atan((double)(Config.Large / 2) / r));
            int x0;
            int y0;
            x0 = Functions.LineCoord(Angle + 360 - theta, R, InitialPoint.X)[0];
            y0 = Functions.LineCoord(Angle + 360 - theta, R, InitialPoint.Y)[1];
            finalPoint = new Point(Functions.LineCoord(Angle, r, x0)[0], Functions.LineCoord(Angle, r, y0)[1]);

            Point basePointPolygonArm = InitialPoint;
            Point leftPointPolygonArm = new Point(Functions.LineCoord(Angle - 90, (r / 10) * 5, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 90, (r / 10) * 5, basePointPolygonArm.Y)[1]);
            Point leftMiddlePointPolygonArm = new Point(Functions.LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point leftMiddleUpperPointPolygonArm = new Point(Functions.LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle - 45, (r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point upperPointPolygonArm = finalPoint;
            Point rigthMiddleUpperPointPolygonArm = new Point(Functions.LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point rigthMiddlePointPolygonArm = new Point(Functions.LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 45, (r / 10) * 7, basePointPolygonArm.Y)[1]);
            Point rigthPointPolygonArm = new Point(Functions.LineCoord(Angle + 90, (r / 10) * 5, basePointPolygonArm.X)[0], Functions.LineCoord(Angle + 90, (r / 10) * 5, basePointPolygonArm.Y)[1]);

            this.polygonPoints = new Point[] { basePointPolygonArm,
                                               leftPointPolygonArm,
                                               leftMiddlePointPolygonArm,
                                               leftMiddleUpperPointPolygonArm,
                                               upperPointPolygonArm,
                                               rigthMiddleUpperPointPolygonArm,
                                               rigthMiddlePointPolygonArm,
                                               rigthPointPolygonArm
            };
        }

        public Point InitialPoint { get => initialPoint; set => initialPoint = value; }
        public Point FinalPoint { get => finalPoint; set => finalPoint = value; }
        public Point[] PolygonPoints { get => polygonPoints; set => polygonPoints = value; }

        #endregion Gets & Sets
    }
}