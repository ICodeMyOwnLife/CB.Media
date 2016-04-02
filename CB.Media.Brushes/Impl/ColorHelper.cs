using System.Windows.Media;


namespace CB.Media.Brushes.Impl
{
    public class ColorHelper
    {
        #region Methods
        public static double CalculateBrightness(Color color)
        {
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255.0;
        }

        public static Brush GetContrastBlackWhiteBrush(Color color)
        {
            return GetContrastBrush(color, System.Windows.Media.Brushes.White, System.Windows.Media.Brushes.Black);
        }

        public static Brush GetContrastBrush(Color color, Brush brightBrush, Brush darkBrush)
        {
            return CalculateBrightness(color) > 0.5 ? darkBrush : brightBrush;
        }

        public static Color GetContrastBlackWhiteColor(Color color)
        {
            return GetContrastColor(color, Colors.White, Colors.Black);
        }

        public static Color GetContrastColor(Color color, Color brightColor, Color darkColor)
        {
            return CalculateBrightness(color) > 0.5 ? darkColor : brightColor;
        }

        public static object GetContrastObject(Color color, object brightObject, object darkObject)
        {
            return CalculateBrightness(color) > 0.5 ? darkObject : brightObject;
        }
        #endregion
    }
}