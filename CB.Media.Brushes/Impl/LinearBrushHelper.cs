using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;


namespace CB.Media.Brushes.Impl
{
    /*public class LinearBrushHelperF
    {
        #region Methods
        public static Color GetBrightnessScaleColor(Color rootColor, double offsetX, double offsetY)
            => new Color
            {
                ScA = 1.0f,
                ScR = CalculateBrightnessScaleComponent(rootColor.ScR, offsetX, offsetY),
                ScG = CalculateBrightnessScaleComponent(rootColor.ScG, offsetX, offsetY),
                ScB = CalculateBrightnessScaleComponent(rootColor.ScB, offsetX, offsetY)
            };

        public static double GetLinearOffset(Color color, ICollection<GradientStop> stops)
        {
            if (stops == null || stops.Count <= 0) return NaN;

            var orderedStops = stops.OrderBy(s => s.Offset).ToArray();

            for (var i = 0; i < stops.Count - 1; i++)
            {
                var offset = GetLinearOffset(color, orderedStops[i], orderedStops[i + 1]);
                if (!IsNaN(offset))
                {
                    return offset;
                }
            }

            return NaN;
        }

        public static Color? GetLinearOffsetColor(double offset, ICollection<GradientStop> stops)
        {
            if (stops == null || stops.Count == 0) return null;

            var orderedStop = stops.OrderBy(gs => gs.Offset);
            GradientStop prevStop = null, nextStop = null;

            foreach (var item in orderedStop)
            {
                if (item.Offset >= offset)
                {
                    nextStop = item;
                    break;
                }
                prevStop = item;
            }

            return nextStop == null
                       ? orderedStop.Last().Color
                       : prevStop == null
                             ? orderedStop.First().Color
                             : GetLinearOffsetColor(offset, prevStop, nextStop);
        }

        public static Color? GetRootColor(Color color, out double offsetX, out double offsetY)
        {
            if (color.R == color.G && color.G == color.B)
            {
                offsetX = offsetY = NaN;
                return null;
            }

            var reducedBlackColor = ReduceBlack(color, out offsetY);
            return ReduceWhite(reducedBlackColor, out offsetX);
        }
        #endregion


        #region Implementation
        private static float CalculateBrightnessScaleComponent(float comp, double offsetX, double offsetY)
            => (float)((comp + (1.0 - comp) * offsetX) * (1 - offsetY));

        private static float CalculateLinearOffsetComponent(float startComponent, float endComponent, double ratio)
            => (float)(ratio * (endComponent - startComponent) + startComponent);

        private static double GetLinearOffset(Color color, GradientStop prevStop, GradientStop nextStop)
        {
            Color color1 = prevStop.Color, color2 = nextStop.Color;
            if (color == color1) return prevStop.Offset;
            if (color == color2) return nextStop.Offset;
            if (!IsBetweenTwoColor(color, color1, color2)) return NaN;
            var offsets = new[]
            {
                (color.ScA - color1.ScA) / (color2.ScA - color1.ScA),
                (color.ScR - color1.ScR) / (color2.ScR - color1.ScR),
                (color.ScG - color1.ScG) / (color2.ScG - color1.ScG),
                (color.ScB - color1.ScB) / (color2.ScB - color1.ScB)
            };
            var valuedOffsets = offsets.Where(o => !IsNaN(o)).ToArray();
            var offset = valuedOffsets.Average();
            return valuedOffsets.All(o => Abs(offset - o) < 0.01) ? offset : NaN;
        }

        private static Color GetLinearOffsetColor(double offset, GradientStop startStop, GradientStop endStop)
        {
            var ratio = (offset - startStop.Offset) / (endStop.Offset - startStop.Offset);
            var startStopColor = startStop.Color;
            var endStopColor = endStop.Color;

            return new Color
            {
                ScA = CalculateLinearOffsetComponent(startStopColor.ScA, endStopColor.ScA, ratio),
                ScR = CalculateLinearOffsetComponent(startStopColor.ScR, endStopColor.ScR, ratio),
                ScG = CalculateLinearOffsetComponent(startStopColor.ScG, endStopColor.ScG, ratio),
                ScB = CalculateLinearOffsetComponent(startStopColor.ScB, endStopColor.ScB, ratio)
            };
        }

        private static bool IsBetweenTwoColor(Color color, Color prevColor, Color nextColor)
            => (color.A - prevColor.A) * (color.A - nextColor.A) <= 0 &&
               (color.R - prevColor.R) * (color.R - nextColor.R) <= 0 &&
               (color.G - prevColor.G) * (color.G - nextColor.G) <= 0 &&
               (color.B - prevColor.B) * (color.B - nextColor.B) <= 0;

        private static Color ReduceBlack(Color color, out double offsetY)
        {
            var maxComponent = Max(Max(color.ScR, color.ScG), color.ScB);
            offsetY = 1.0 / maxComponent;

            return new Color
            {
                ScA = 1.0f,
                ScR = ReduceBlackComponent(color.ScR, offsetY),
                ScG = ReduceBlackComponent(color.ScG, offsetY),
                ScB = ReduceBlackComponent(color.ScB, offsetY)
            };
        }

        private static float ReduceBlackComponent(float component, double ratio) => (float)(ratio * component);

        private static Color ReduceWhite(Color color, out double offsetX)
        {
            var minComponent = Min(Min(color.ScR, color.ScG), color.ScB);
            offsetX = minComponent;
            return new Color
            {
                ScA = 1.0f,
                ScR = ReduceWhiteComponent(color.ScR, offsetX),
                ScG = ReduceWhiteComponent(color.ScG, offsetX),
                ScB = ReduceWhiteComponent(color.ScB, offsetX)
            };
        }

        private static float ReduceWhiteComponent(float component, double offsetX)
            => (float)((component - offsetX) / (1 - offsetX));
        #endregion
    }*/

    public class LinearBrushHelper
    {
        #region Methods
        public static Color GetBrightnessScaleColor(Color rootColor, double offsetX, double offsetY)
            => new Color
            {
                A = byte.MaxValue,
                R = CalculateBrightnessScaleComponent(rootColor.R, offsetX, offsetY),
                G = CalculateBrightnessScaleComponent(rootColor.G, offsetX, offsetY),
                B = CalculateBrightnessScaleComponent(rootColor.B, offsetX, offsetY)
            };

        public static double GetLinearOffset(Color color, ICollection<GradientStop> stops)
        {
            if (stops == null || stops.Count <= 0) return Double.NaN;

            var orderedStops = stops.OrderBy(s => s.Offset).ToArray();

            for (var i = 0; i < stops.Count - 1; i++)
            {
                var offset = GetLinearOffset(color, orderedStops[i], orderedStops[i + 1]);
                if (!Double.IsNaN(offset))
                {
                    return offset;
                }
            }

            return Double.NaN;
        }

        public static Color? GetLinearOffsetColor(double offset, GradientStopCollection stops)
        {
            if (stops == null || stops.Count == 0 || Double.IsNaN(offset))
            {
                return null;
            }

            var orderedStop = stops.OrderBy(gs => gs.Offset);
            GradientStop prevStop = null, nextStop = null;

            foreach (var item in orderedStop)
            {
                if (item.Offset >= offset)
                {
                    nextStop = item;
                    break;
                }
                prevStop = item;
            }

            return nextStop == null
                       ? orderedStop.Last().Color
                       : (prevStop == null
                              ? orderedStop.First().Color
                              : GetLinearOffsetColor(offset, prevStop, nextStop));
        }

        public static Color? GetRootColor(Color color, out double offsetX, out double offsetY)
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
        #endregion


        #region Implementation
        private static byte CalculateBrightnessScaleComponent(byte comp, double offsetX, double offsetY)
            => (byte)((comp + (255 - comp) * offsetX) * (1 - offsetY));

        private static byte CalculateLinearOffsetComponent(byte startComponent, byte endComponent, double ratio)
            => (byte)(ratio * (endComponent - startComponent) + startComponent);

        private static double GetLinearOffset(Color color, GradientStop prevStop, GradientStop nextStop)
        {
            Color color1 = prevStop.Color, color2 = nextStop.Color;
            if (color == color1) return prevStop.Offset;
            if (color == color2) return nextStop.Offset;
            if (!IsBetweenTwoColor(color, color1, color2)) return Double.NaN;
            var offsets = new[]
            {
                (color.ScA - color1.ScA) / (color2.ScA - color1.ScA),
                (color.ScR - color1.ScR) / (color2.ScR - color1.ScR),
                (color.ScG - color1.ScG) / (color2.ScG - color1.ScG),
                (color.ScB - color1.ScB) / (color2.ScB - color1.ScB)
            };
            var valuedOffsets = offsets.Where(o => !Double.IsNaN(o)).ToArray();
            var offset = valuedOffsets.Average();
            return valuedOffsets.All(o => Math.Abs(offset - o) < 0.01)
                       ? offset * (nextStop.Offset - prevStop.Offset) + prevStop.Offset
                       : Double.NaN;
        }

        private static Color GetLinearOffsetColor(double offset, GradientStop startStop, GradientStop endStop)
        {
            var ratio = (offset - startStop.Offset) / (endStop.Offset - startStop.Offset);
            var startStopColor = startStop.Color;
            var endStopColor = endStop.Color;

            return new Color
            {
                A = CalculateLinearOffsetComponent(startStopColor.A, endStopColor.A, ratio),
                R = CalculateLinearOffsetComponent(startStopColor.R, endStopColor.R, ratio),
                G = CalculateLinearOffsetComponent(startStopColor.G, endStopColor.G, ratio),
                B = CalculateLinearOffsetComponent(startStopColor.B, endStopColor.B, ratio)
            };
        }

        private static bool IsBetweenTwoColor(Color color, Color prevColor, Color nextColor)
            => (color.A - prevColor.A) * (color.A - nextColor.A) <= 0 &&
               (color.R - prevColor.R) * (color.R - nextColor.R) <= 0 &&
               (color.G - prevColor.G) * (color.G - nextColor.G) <= 0 &&
               (color.B - prevColor.B) * (color.B - nextColor.B) <= 0;

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

        private static byte ReduceBlackComponent(byte component, double offsetY) => (byte)Math.Round(offsetY * component);

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
        #endregion
    }
}