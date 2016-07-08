using System;
using System.Windows.Input;
using System.Windows.Media;
using CB.Media.Brushes.Impl;
using CB.Model.Prism;
using Prism.Commands;


namespace Test.CB.Media.Brushes.ViewModels
{
    public class TestMediaHelperViewModel: PrismViewModelBase
    {
        #region Fields
        private static readonly byte[] _colorBytes = new byte[3];
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private byte _b;
        private double _brightness;
        private Color _color;
        private byte _g;
        private double _hue;
        private byte _r;
        private double _saturation;
        private Color _sourceColor;
        #endregion


        #region  Constructors & Destructor
        public TestMediaHelperViewModel()
        {
            ResetCommand = new DelegateCommand(Reset);
            Reset();
        }
        #endregion


        #region  Commands
        public ICommand ResetCommand { get; }
        #endregion


        #region  Properties & Indexers
        public byte B
        {
            get { return _b; }
            set { SetColor(Color.FromRgb(_color.R, _color.G, value)); }
        }

        public double Brightness
        {
            get { return _brightness; }
            set { SetColor(_sourceColor.SetBrightness(value)); }
        }

        public Color Color
        {
            get { return _color; }
            private set { SetColor(value); }
        }

        public byte G
        {
            get { return _g; }
            set { SetColor(Color.FromRgb(_color.R, value, _color.B)); }
        }

        public double Hue
        {
            get { return _hue; }
            set { SetColor(_sourceColor.SetHue(value)); }
        }

        public byte R
        {
            get { return _r; }
            set { SetColor(Color.FromRgb(value, _color.G, _color.B)); }
        }

        public double Saturation
        {
            get { return _saturation; }
            set { SetColor(_sourceColor.SetSaturation(value)); }
        }
        #endregion


        #region Methods
        public void Reset()
            => Color = _sourceColor = CreateRandomColor();
        #endregion


        #region Implementation
        private static Color CreateRandomColor()
        {
            _random.NextBytes(_colorBytes);
            return Color.FromRgb(_colorBytes[0], _colorBytes[1], _colorBytes[2]);
        }

        private void SetColor(Color? color)
        {
            if (!color.HasValue) return;
            _color = color.Value;
            _r = _color.R;
            _g = _color.G;
            _b = _color.B;
            _brightness = _color.GetBrightness();
            _saturation = _color.GetSaturation();
            var hue = _color.GetHue();
            if (hue != null) _hue = hue.Value;

            NotifyAllPropertyChanged();
        }
        #endregion
    }
}