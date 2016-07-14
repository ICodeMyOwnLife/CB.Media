using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace Test.CB.Media.Brushes.Helpers
{
    [ValueConversion(typeof(Color), typeof(string))]
    public class ColorToStringConverter: IValueConverter
    {
        #region  Properties & Indexers
        public ColorSystemType ColorSystemType { get; set; }
        #endregion


        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color)value;

            switch (ColorSystemType)
            {
                case ColorSystemType.Rgb:
                    return $"R: {color.R} G: {color.G} B: {color.B}";
                case ColorSystemType.Argb:
                    return $"A: {color.A} R: {color.R} G: {color.G} B: {color.B}";
                case ColorSystemType.ScRgb:
                    return
                        $"ScR: {color.ScR.ToString("N")} ScG: {color.ScG.ToString("N")} ScB: {color.ScB.ToString("N")}";
                case ColorSystemType.ScArgb:
                    return
                        $"ScA: {color.ScA.ToString("N")} ScR: {color.ScR.ToString("N")} ScG: {color.ScG.ToString("N")} ScB: {color.ScB.ToString("N")}";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
        #endregion
    }
}