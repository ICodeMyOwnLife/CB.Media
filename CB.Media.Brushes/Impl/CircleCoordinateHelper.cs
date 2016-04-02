using System;
using static System.Double;


namespace CB.Media.Brushes.Impl
{
    public class CircleCoordinateHelper
    {
        #region Fields
        private readonly double _hRadius;
        private readonly double _vRadius;
        #endregion


        #region  Constructors & Destructor
        public CircleCoordinateHelper(double width, double height)
        {
            _hRadius = width / 2;
            _vRadius = height / 2;
        }
        #endregion


        #region Methods
        public double GetAngularOffset(double x, double y)
        {
            double deltaX, deltaY;
            CalculateDeltas(x, y, out deltaX, out deltaY);

            if (Math.Abs(deltaY) < Epsilon) return deltaX > 0 ? 0.25 : deltaX < 0 ? 0.75 : NaN;

            var offset = Math.Atan(deltaX / Math.Abs(deltaY)) / (2 * Math.PI);
            if (deltaY < 0 && deltaX < 0) return 1.0 + offset;
            if (deltaY > 0) return 0.5 - offset;
            return offset;
        }

        public double GetRadialOffset(double x, double y)
        {
            double deltaX, deltaY;
            CalculateDeltas(x, y, out deltaX, out deltaY);
            return CalculateHypotenuse(deltaX, deltaY) / CalculateHypotenuse(_hRadius, _vRadius);
        }
        #endregion


        #region Implementation
        private void CalculateDeltas(double x, double y, out double deltaX, out double deltaY)
        {
            deltaX = x - _hRadius;
            deltaY = y - _vRadius;
        }

        private static double CalculateHypotenuse(double a, double b)
        {
            return Math.Sqrt(a * a + b * b);
        }
        #endregion
    }
}