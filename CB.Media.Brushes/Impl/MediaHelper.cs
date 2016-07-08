using System;
using System.Windows.Media;


namespace CB.Media.Brushes.Impl
{
    public static class MediaHelper
    {
        #region Methods
        public static SolidColorBrush AdjustBrightness(this SolidColorBrush brush, double brightness)
            => SetSolidColorBrush(brush, brightness, AdjustBrightness);

        public static TGradientBrush AdjustBrightness<TGradientBrush>(this TGradientBrush brush, double brightness)
            where TGradientBrush: GradientBrush
            => SetGradientBrush(brush, brightness, AdjustBrightness);

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

        public static double CalculateBrightness(Color color)
            => (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;

        public static double GetAbsoluteBrighness(this Color color)
            => 0.299 * color.R + 0.587 * color.G + 0.114 * color.B;

        public static double GetBrightness(this Color color)
            => GetAbsoluteBrighness(color) / byte.MaxValue;

        public static Brush GetContrastBlackWhiteBrush(Color color)
        {
            return GetContrastBrush(color, System.Windows.Media.Brushes.White, System.Windows.Media.Brushes.Black);
        }

        public static Color GetContrastBlackWhiteColor(Color color)
        {
            return GetContrastColor(color, Colors.White, Colors.Black);
        }

        public static Brush GetContrastBrush(Color color, Brush brightBrush, Brush darkBrush)
        {
            return CalculateBrightness(color) > 0.5 ? darkBrush : brightBrush;
        }

        public static Color GetContrastColor(Color color, Color brightColor, Color darkColor)
        {
            return CalculateBrightness(color) > 0.5 ? darkColor : brightColor;
        }

        public static object GetContrastObject(Color color, object brightObject, object darkObject)
        {
            return CalculateBrightness(color) > 0.5 ? darkObject : brightObject;
        }

        /*public static double? GetHue(this Color color)
        {
            double offsetX, offsetY;
            var rootColor = GetRootColor(color, out offsetX, out offsetY);
            if (rootColor == null) return null;
            var value = rootColor.Value;

            if (value.R == 255 && value.B == 0) return CalculateHue(0, value.G, true);
            if (value.B == 0 && value.G == 255) return CalculateHue(60, value.R, false);
            if (value.G == 255 && value.R == 0) return CalculateHue(120, value.B, true);
            if (value.R == 0 && value.B == 255) return CalculateHue(180, value.G, false);
            if (value.B == 255 && value.G == 0) return CalculateHue(240, value.R, true);
            if (value.G == 0 && value.R == 255) return CalculateHue(300, value.B, false);
            return null;
        }*/

        public static double? GetHue(this Color color)
        {
            if (color.R == color.G && color.G == color.B) return null;
            if (color.R >= color.G && color.G >= color.B) return CalculateHue(0, color.B, color.R, color.G, true);
            if (color.G >= color.R && color.R >= color.B) return CalculateHue(60, color.B, color.G, color.R, false);
            if (color.G >= color.B && color.B >= color.R) return CalculateHue(120, color.R, color.G, color.B, true);
            if (color.B >= color.G && color.G >= color.R) return CalculateHue(180, color.R, color.B, color.G, false);
            if (color.B >= color.R && color.R >= color.G) return CalculateHue(240, color.G, color.B, color.R, true);
            if (color.R >= color.B && color.B >= color.G) return CalculateHue(300, color.G, color.R, color.B, false);
            return null;
        }

        public static double? GetRelativeHue(this Color color)
            => GetHue(color) / 360;

        public static Color? GetRootColor(this Color color, out double offsetX, out double offsetY)
        {
            if (color.R == color.G && color.G == color.B)
            {
                offsetX = 0;
                offsetY = 1.0 - color.R / 255.0;
                return null;
            }
            var reducedBlackColor = ReduceBlack(color, out offsetY);
            return ReduceWhite(reducedBlackColor, out offsetX);
        }

        public static double GetSaturation(this Color color)
            => 1 - GetMinScRgb(color) / GetMaxScRgb(color);

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

        public static Color? SetHue(this Color color, double hue)
        {
            var min = GetMinScRgb(color);
            var max = GetMaxScRgb(color);
            if (Math.Abs(max - min) < double.Epsilon) return null;

            if (hue < 60) return Color.FromScRgb(color.ScA, max, CalculateHueComponent(min, max, hue, true), min);
            if (hue < 120)
                return Color.FromScRgb(color.ScA, CalculateHueComponent(min, max, hue - 60, false), max, min);
            if (hue < 180)
                return Color.FromScRgb(color.ScA, min, max, CalculateHueComponent(min, max, hue - 120, true));
            if (hue < 240)
                return Color.FromScRgb(color.ScA, min, CalculateHueComponent(min, max, hue - 180, false), max);
            if (hue < 300)
                return Color.FromScRgb(color.ScA, CalculateHueComponent(min, max, hue - 240, true), min, max);
            if (hue < 360)
                return Color.FromScRgb(color.ScA, max, min, CalculateHueComponent(min, max, hue - 300, false));
            return null;
        }

        public static Color? SetRelativeHue(this Color color, double hue)
            => SetHue(color, hue * 360);

        public static Color SetSaturation(this Color color, double saturation)
        {
            if (color.R == color.G && color.G == color.B) return Color.FromArgb(color.A, 0, 0, 0);

            var max = GetMaxScRgb(color);
            var currentSaturation = GetSaturation(color);
            return Color.FromScRgb(color.ScA,
                CalculateSaturationComponent(max, color.ScR, saturation, currentSaturation),
                CalculateSaturationComponent(max, color.ScG, saturation, currentSaturation),
                CalculateSaturationComponent(max, color.ScB, saturation, currentSaturation));
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

        /*private static double CalculateHue(double baseNumber, byte variable, bool increasing)
            => baseNumber + 60 * (increasing ? variable / 255.0 : 1 - variable / 255.0);*/

        private static double CalculateHue(double baseValue, double min, double max, double variable, bool increasing)
        {
            var diff = (variable - min) / (max - min);
            return baseValue + 60 * (increasing ? diff : 1 - diff);
        }

        private static float CalculateHueComponent(float min, float max, double offset, bool increasing)
        {
            var diff = offset / 60 * (max - min);
            return (float)(increasing ? min + diff : max - diff);
        }

        private static float CalculateSaturationComponent(float max, float currentComponent, double newSaturation,
            double currentSaturation)
            => (float)(max - newSaturation / currentSaturation * (max - currentComponent));

        private static float GetMaxScRgb(Color color)
            => Math.Max(Math.Max(color.ScR, color.ScG), color.ScB);

        private static float GetMinScRgb(Color color)
            => Math.Min(Math.Min(color.ScR, color.ScG), color.ScB);

        private static Color ReduceBlack(Color color, out double offsetY)
        {
            var ratio = 255.0 / Math.Max(Math.Max(color.R, color.G), color.B);
            offsetY = 1 - 1 / ratio;
            return new Color
            {
                A = byte.MaxValue,
                R = ReduceBlackComponent(color.R, ratio),
                G = ReduceBlackComponent(color.G, ratio),
                B = ReduceBlackComponent(color.B, ratio)
            };
        }

        private static byte ReduceBlackComponent(byte component, double offsetY)
            => (byte)Math.Round(offsetY * component);

        private static Color ReduceWhite(Color color, out double offsetX)
        {
            var ratio = Math.Min(Math.Min(color.R, color.G), color.B) / 255.0;
            offsetX = 1.0 - ratio;
            return new Color
            {
                A = byte.MaxValue,
                R = ReduceWhiteComponent(color.R, ratio),
                G = ReduceWhiteComponent(color.G, ratio),
                B = ReduceWhiteComponent(color.B, ratio)
            };
        }

        private static byte ReduceWhiteComponent(byte component, double offsetX)
            => (byte)Math.Round((component - 255 * offsetX) / (1 - offsetX));

        private static byte ToByte(double brightness)
            => (byte)(brightness < 0 ? 0 : brightness > byte.MaxValue ? byte.MaxValue : (byte)(brightness + 0.5));
        #endregion
    }
}