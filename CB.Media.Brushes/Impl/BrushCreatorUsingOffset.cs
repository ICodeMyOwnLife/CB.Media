using System.Windows.Media;


namespace CB.Media.Brushes.Impl
{
    public abstract class BrushCreatorUsingOffset: BrushCreatorBase
    {
        #region  Constructors & Destructor
        protected BrushCreatorUsingOffset() { }

        protected BrushCreatorUsingOffset(int width, int height): base(width, height) { }
        #endregion


        #region Abstract
        protected abstract Color GetColorAt(double offsetX, double offsetY);
        #endregion


        #region Override
        protected internal override Color GetColorAt(int index)
        {
            int x, y;
            GetCoordinate(index, out x, out y);
            double offsetX = (double)x / Width, offsetY = (double)y / Height;
            return GetColorAt(offsetX, offsetY);
        }
        #endregion
    }
}