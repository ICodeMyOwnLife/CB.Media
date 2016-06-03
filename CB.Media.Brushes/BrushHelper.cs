using System;
using System.Windows.Media;


namespace CB.Media.Brushes
{
    public static class BrushHelper
    {
        #region Methods
        public static double GetHue(this Color color)
        {
            if (color.R == color.G && color.G == color.B)
            {
                return double.NaN;
            }
            return Double.NaN; // UNDONE: GetHue
        }

        public static Color SetBrightness(this Color color, double brightness)
            => brightness >= 1
                   ? Colors.White
                   : brightness <= -1
                         ? Colors.Black
                         : (brightness > 0
                                ? Color.FromScRgb(color.ScA, AdjustWhite(color.ScR, brightness),
                                    AdjustWhite(color.ScG, brightness), AdjustWhite(color.ScB, brightness))
                                : brightness < 0
                                      ? Color.FromScRgb(color.ScA, AdjustBlack(color.ScR, brightness),
                                          AdjustBlack(color.ScG, brightness), AdjustBlack(color.ScB, brightness))
                                      : color);

        public static SolidColorBrush SetBrightness(this SolidColorBrush brush, double brightness)
            => SetSolidColorBrush(brush, brightness, SetBrightness);

        public static TGradientBrush SetBrightness<TGradientBrush>(this TGradientBrush brush, double brightness)
            where TGradientBrush: GradientBrush
            => SetGradientBrush(brush, brightness, SetBrightness);

        public static TGradientBrush SetGradientBrush<TGradientBrush, TValue>(TGradientBrush brush, TValue value,
            Func<Color, TValue, Color> setColorFunc) where TGradientBrush: GradientBrush
        {
            var result = brush.Clone();
            for (var i = 0; i < brush.GradientStops.Count; i++)
            {
                result.GradientStops[i].Color = setColorFunc(brush.GradientStops[i].Color, value);
            }
            return (TGradientBrush)result;
        }

        public static Color SetHue(this Color color, double hue)
        {
            return new Color(); // UNDONE: SetHue
        }

        public static SolidColorBrush SetSolidColorBrush<TValue>(SolidColorBrush brush, TValue value,
            Func<Color, TValue, Color> setColorFunc)
        {
            var result = brush.Clone();
            result.Color = setColorFunc(brush.Color, value);
            return result;
        }
        #endregion


        #region Implementation
        private static float AdjustBlack(float sc, double brightness)
            => (float)(sc * (1 + brightness));

        private static float AdjustWhite(float sc, double brightness)
            => (float)(sc + (1 - sc) * brightness);
        #endregion
    }
}