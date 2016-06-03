using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CB.Media.Brushes;
using CB.Model.Prism;
using Microsoft.Practices.Prism.Commands;


namespace Test.CB.Media.Brushes.ViewModels
{
    public class TestBrushHelperViewModel: PrismViewModelBase
    {
        #region Fields
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        private double _brightness;
        private Color _color, _sourceColor;
        private LinearGradientBrush _linearBrush;
        private RadialGradientBrush _radialBrush;
        private SolidColorBrush _solidBrush;
        private LinearGradientBrush _sourceLinearBrush;
        private RadialGradientBrush _sourceRadialBrush;
        private SolidColorBrush _sourceSolidBrush;
        #endregion


        #region  Constructors & Destructor
        public TestBrushHelperViewModel()
        {
            SetMedia();
            ResetCommand = new DelegateCommand(Reset);
        }
        #endregion


        #region  Commands
        public ICommand ResetCommand { get; }
        #endregion


        #region  Properties & Indexers
        public double Brightness
        {
            get { return _brightness; }
            set
            {
                if (!SetProperty(ref _brightness, value)) return;

                Color = _sourceColor.SetBrightness(value);
                SolidBrush = _sourceSolidBrush.SetBrightness(value);
                LinearBrush = _sourceLinearBrush.SetBrightness(value);
                RadialBrush = _sourceRadialBrush.SetBrightness(value);
            }
        }

        public Color Color
        {
            get { return _color; }
            private set { SetProperty(ref _color, value); }
        }

        public LinearGradientBrush LinearBrush
        {
            get { return _linearBrush; }
            private set { SetProperty(ref _linearBrush, value); }
        }

        public RadialGradientBrush RadialBrush
        {
            get { return _radialBrush; }
            private set { SetProperty(ref _radialBrush, value); }
        }

        public SolidColorBrush SolidBrush
        {
            get { return _solidBrush; }
            private set { SetProperty(ref _solidBrush, value); }
        }
        #endregion


        #region Methods
        public void Reset()
        {
            Brightness = 0;
            SetMedia();
        }
        #endregion


        #region Implementation
        private static byte GetRandomByte()
            => (byte)_random.Next(0, byte.MaxValue);

        private static Color GetRandomColor()
            => Color.FromRgb(GetRandomByte(), GetRandomByte(), GetRandomByte());

        private void SetMedia()
        {
            Color = _sourceColor = GetRandomColor();
            SolidBrush = _sourceSolidBrush = new SolidColorBrush(GetRandomColor());
            LinearBrush =
                _sourceLinearBrush =
                new LinearGradientBrush(GetRandomColor(), GetRandomColor(), new Point(0, 0.5), new Point(1, 0.5));
            RadialBrush = _sourceRadialBrush = new RadialGradientBrush(GetRandomColor(), GetRandomColor());
        }
        #endregion
    }
}