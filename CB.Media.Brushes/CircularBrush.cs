using System.Windows.Media;


namespace CB.Media.Brushes
{
    public class CircularBrush: CustomBrushBase
    {
        #region Fields
        private GradientStopCollection _gradientStops;
        #endregion


        #region  Properties & Indexers
        public GradientStopCollection GradientStops
        {
            get { return _gradientStops; }
            set { if (SetProperty(ref _gradientStops, value)) SetBrush(); }
        }
        #endregion


        #region Override
        protected override Brush CreateBrush()
        {
            return new CircularBrushCreator(GradientStops).Create();
        }
        #endregion
    }
}