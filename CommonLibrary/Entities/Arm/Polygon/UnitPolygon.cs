using CommonLibrary.ConfigLayer;
using CommonLibrary.Entities.Graphics;
using CommonLibrary.Function;
using System;


namespace CommonLibrary.Entities.Arm.Polygon
{
    public class UnitPolygon
    {
        public int R;
        public int r = 50;

        #region ctor

        public UnitPolygon(int _Angle, Point _Init)
        {
            this.angle = _Angle;
            this.initialPoint = _Init;
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
            }
        }

        public Point InitialPoint { get => initialPoint; set => initialPoint = value; }
        public Point FinalPoint { get => finalPoint; set => finalPoint = value; }
        public Point[] PolygonPoints { get => polygonPoints; set => polygonPoints = value; }

        #endregion Gets & Sets
    }
}