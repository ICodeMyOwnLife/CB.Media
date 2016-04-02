using System.Windows.Media;
using CB.Media.Brushes.Impl;


namespace CB.Media.Brushes
{
    public class CircularBrushCreator: BrushCreatorUsingCoordinate
    {
        #region  Constructors & Destructor
        public CircularBrushCreator() { }

        public CircularBrushCreator(GradientStopCollection gradientStops)
        {
            GradientStops = gradientStops;
        }

        public CircularBrushCreator(int width, int height): base(width, height) { }

        public CircularBrushCreator(int width, int height, GradientStopCollection gradientStops): this(width, height)
        {
            GradientStops = gradientStops;
        }
        #endregion


        #region  Properties & Indexers
        public GradientStopCollection GradientStops { get; set; }
        #endregion


        #region Override
        protected override Color GetColorAt(int x, int y)
            =>
                LinearBrushHelper.GetLinearOffsetColor(
                    new CircleCoordinateHelper(Width, Height).GetAngularOffset(x, y), GradientStops) ??
                Color.FromArgb(255, 255, 255, 255);
        #endregion
    }

    /*public class CircularBrushCreator
    {
        #region Fields
        private const int BYTE_PER_PIXELS = 4;
        private const double DPI = 96.0;
        private const int PIXEL_DIMENSION = 200;
        private const int STRIDE = PIXEL_DIMENSION * BYTE_PER_PIXELS;
        #endregion


        #region Methods
        public static Brush Create() => CreateBrush(CreateImageSource());

        public static Brush Create(GradientStopCollection gradientStops)
            => CreateBrush(CreateImageSource(gradientStops));
        #endregion


        #region Implementation
        private static Brush CreateBrush(ImageSource imageSource)
            => new DrawingBrush(new ImageDrawing(imageSource, CreateRect()));

        private static ImageSource CreateImageSource(GradientStopCollection gradientStops)
            => CreateImageSource(CreatePixels(gradientStops));

        private static ImageSource CreateImageSource() => CreateImageSource(CreatePixels());

        private static ImageSource CreateImageSource(byte[] pixels)
        {
            var src = new WriteableBitmap(PIXEL_DIMENSION, PIXEL_DIMENSION, DPI, DPI, PixelFormats.Bgra32, null);
            var sourceRect = new Int32Rect(0, 0, PIXEL_DIMENSION, PIXEL_DIMENSION);
            src.WritePixels(sourceRect, pixels, STRIDE, 0);
            return src;
        }

        private static byte[] CreatePixels(GradientStopCollection gradientStops)
        {
            var pixels = new byte[PIXEL_DIMENSION * PIXEL_DIMENSION * BYTE_PER_PIXELS];
            for (var i = 0; i < pixels.Length; i += BYTE_PER_PIXELS)
            {
                var color = GetColorAt(i, gradientStops);
                SetPixel(pixels, i, color);
            }
            return pixels;
        }

        private static byte[] CreatePixels()
        {
            var pixels = new byte[PIXEL_DIMENSION * PIXEL_DIMENSION * BYTE_PER_PIXELS];
            for (var i = 0; i < pixels.Length; i += BYTE_PER_PIXELS)
            {
                var color = GetColorAt(i);
                SetPixel(pixels, i, color);
            }
            return pixels;
        }

        private static Rect CreateRect() => new Rect(0, 0, PIXEL_DIMENSION, PIXEL_DIMENSION);

        private static Color GetColorAt(int index, GradientStopCollection gradientStops)
            => LinearBrushHelper.GetLinearOffsetColor(GetOffset(index), gradientStops) ?? Color.FromArgb(255, 255, 255, 255);

        private static Color GetColorAt(int index)
        {
            var offset = GetOffset(index);

            if (IsNaN(offset)) return Color.FromArgb(0, 0, 0, 0);
            var component = (byte)(offset * byte.MaxValue);
            return Color.FromRgb(component, component, component);
        }

        private static double GetOffset(int index)
        {
            int x = index / 4 % PIXEL_DIMENSION, y = index / 4 / PIXEL_DIMENSION;
            int deltaX = x - PIXEL_DIMENSION / 2, deltaY = y - PIXEL_DIMENSION / 2;

            if (deltaY == 0) return deltaX > 0 ? 0.25 : deltaX < 0 ? 0.75 : NaN;

            var offset = Atan((double)(deltaX) / Abs(deltaY)) / (2 * PI);
            if (deltaY < 0 && deltaX < 0) return 1.0 + offset;
            if (deltaY > 0) return 0.5 - offset;
            return offset;
        }

        private static void SetPixel(IList<byte> pixels, int index, Color color)
        {
            pixels[index + 0] = color.B;
            pixels[index + 1] = color.G;
            pixels[index + 2] = color.R;
            pixels[index + 3] = color.A;
        }
        #endregion
    }*/
}