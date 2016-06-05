using System;
using System.Windows.Media;


namespace CB.Media.Brushes
{
    public static class BrushHelper
    {
        #region Methods
        public static Color AdjustBrightness(this Color color, double brightness)
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

        public static SolidColorBrush AdjustBrightness(this SolidColorBrush brush, double brightness)
            => SetSolidColorBrush(brush, brightness, AdjustBrightness);

        public static TGradientBrush AdjustBrightness<TGradientBrush>(this TGradientBrush brush, double brightness)
            where TGradientBrush: GradientBrush
            => SetGradientBrush(brush, brightness, AdjustBrightness);

        public static double GetAbsoluteBrighness(this Color color)
            => 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;

        public static double GetBrightness(this Color color)
            => GetAbsoluteBrighness(color) / byte.MaxValue;

        public static double GetHue(this Color color)
        {
            if (color.R == color.G && color.G == color.B)
            {
                return double.NaN;
            }
            return double.NaN; // UNDONE: GetHue
        }

        public static bool IsBlack(this Color color)
            => color.R == 0 && color.G == 0 && color.B == 0;

        public static bool IsWhite(this Color color)
            => color.R == byte.MaxValue && color.G == byte.MaxValue && color.B == byte.MaxValue;

        public static Color SetAbsoluteBrightness(this Color color, double brightness)
        {
            var comp = ToByte(brightness);
            return IsBlack(color)
                       ? Color.FromArgb(color.A, comp, comp, comp)
                       : AdjustRatio(color, brightness / color.GetAbsoluteBrighness());
        }

        public static Color SetBrightness(this Color color, double brightness)
            => IsBlack(color)
                   ? Color.FromScRgb(color.ScA, (float)brightness, (float)brightness, (float)brightness) :
                   AdjustRatio(color, brightness / color.GetBrightness());

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

        private static Color AdjustRatio(Color color, double ratio)
            => Color.FromScRgb(color.ScA, AdjustRatio(color.ScR, ratio), AdjustRatio(color.ScG, ratio),
                AdjustRatio(color.ScB, ratio));

        private static float AdjustRatio(float scB, double ratio)
            => (float)(scB * ratio);

        private static float AdjustWhite(float sc, double brightness)
            => (float)(sc + (1 - sc) * brightness);

        private static byte ToByte(double brightness)
            => (byte)(brightness < 0 ? 0 : brightness > byte.MaxValue ? byte.MaxValue : (byte)(brightness + 0.5));
        #endregion
    }
}


// TODO: GetHue, SetHue, AdjustHue, GetSaturation, SetSaturation, AdjustSaturation, Edit SetBrightness