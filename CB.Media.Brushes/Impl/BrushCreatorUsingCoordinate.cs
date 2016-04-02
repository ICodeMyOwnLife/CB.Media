using System.Windows.Media;


namespace CB.Media.Brushes.Impl
{
    public abstract class BrushCreatorUsingCoordinate: BrushCreatorBase
    {
        #region  Constructors & Destructor
        protected BrushCreatorUsingCoordinate(int width, int height): base(width, height) { }

        protected BrushCreatorUsingCoordinate() { }
        #endregion


        #region Abstract
        protected abstract Color GetColorAt(int x, int y);
        #endregion


        #region Override
        protected internal override Color GetColorAt(int index)
        {
            int x, y;
            GetCoordinate(index, out x, out y);
            return GetColorAt(x, y);
        }
        #endregion
    }
}