using System.Windows.Media;
using CB.Media.Brushes.Impl;
using CB.Model.Common;


namespace Test.CB.Media.Brushes
{
    public class ColorInfo: BindableObject
    {
        #region Fields
        private double _absoluteBrightness;
        private double _brightness;
        private Color _color;
        #endregion


        #region  Properties & Indexers
        public double AbsoluteBrightness
        {
            get { return _absoluteBrightness; }
            set
            {
                if (!SetProperty(ref _absoluteBrightness, value)) return;

                _color = Color.SetAbsoluteBrightness(value);
                _brightness = _color.GetBrightness();
                NotifyPropertiesChanged(nameof(Color), nameof(Brightness));
            }
        }

        public double Brightness
        {
            get { return _brightness; }
            set
            {
                if (!SetProperty(ref _brightness, value)) return;

                _color = Color.SetBrightness(value);
                _absoluteBrightness = _color.GetAbsoluteBrighness();
                NotifyPropertiesChanged(nameof(Color), nameof(AbsoluteBrightness));
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                if (!SetProperty(ref _color, value)) return;

                _brightness = value.GetBrightness();
                _absoluteBrightness = value.GetAbsoluteBrighness();
                NotifyPropertiesChanged(nameof(Brightness), nameof(AbsoluteBrightness));
            }
        }
        #endregion
    }
}