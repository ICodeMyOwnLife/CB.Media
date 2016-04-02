using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace CB.Media.Brushes.Impl
{
    public abstract class BrushCreatorBase
    {
        #region Fields
        protected const int BYTE_PER_PIXELS = 4;
        private const int DIMENSION = 200;
        protected const double DPI = 96.0;
        protected int Height;
        protected int Stride;
        protected int Width;
        #endregion


        #region  Constructors & Destructor
        protected BrushCreatorBase(int width, int height)
        {
            Width = width;
            Height = height;
            Stride = Width * BYTE_PER_PIXELS;
        }

        protected BrushCreatorBase(): this(DIMENSION, DIMENSION) { }
        #endregion


        #region Abstract
        protected internal abstract Color GetColorAt(int index);
        #endregion


        #region Methods
        public virtual Brush Create() => new DrawingBrush(CreateDrawing());

        public virtual Drawing CreateDrawing() => new ImageDrawing(CreateImageSource(), CreateRect());

        public virtual ImageSource CreateImageSource() => CreateImageSource(CreatePixels());
        #endregion


        #region Implementation
        /*private Brush CreateBrush(ImageSource imageSource)
            => new DrawingBrush(new ImageDrawing(imageSource, CreateRect()));*/

        private ImageSource CreateImageSource(Array pixels)
        {
            var src = new WriteableBitmap(Width, Height, DPI, DPI, PixelFormats.Bgra32, null);
            var sourceRect = new Int32Rect(0, 0, Width, Height);
            src.WritePixels(sourceRect, pixels, Stride, 0);
            return src;
        }

        private unsafe Array CreatePixels()
        {
            var length = Width * Height * BYTE_PER_PIXELS;
            var pixels = new byte[length];
            var stopwatch = Stopwatch.StartNew();
            fixed (byte* p = pixels)
            {
                for (var i = 0; i < length; i += BYTE_PER_PIXELS)
                {
                    SetPixel(p + i, GetColorAt(i));
                }
            }
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
            return pixels;
        }

        private Rect CreateRect() => new Rect(0, 0, Width, Height);

        protected internal void GetCoordinate(int index, out int x, out int y)
        {
            x = index / 4 % Width;
            y = index / 4 / Width;
        }

        private static unsafe void SetPixel(byte* pixel, Color color)
        {
            pixel[0] = color.B;
            pixel[1] = color.G;
            pixel[2] = color.R;
            pixel[3] = color.A;
        }
        #endregion
    }
}