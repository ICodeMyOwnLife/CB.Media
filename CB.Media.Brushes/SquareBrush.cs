using System.Windows;
using System.Windows.Media;


namespace CB.Media.Brushes
{
    public class SquareBrush: CustomBrushBase
    {
        #region Fields
        private Color _color1 = Colors.Gray;
        private Color _color2 = Colors.White;
        private int _height = 10;
        private int _width = 10;
        #endregion


        #region  Constructors & Destructor
        public SquareBrush()
        {
            // ReSharper disable once VirtualMemberCallInContructor
            SetBrush();
        }
        #endregion


        #region  Properties & Indexers
        public Color Color1
        {
            get { return _color1; }
            set { if (SetProperty(ref _color1, value)) SetBrush(); }
        }

        public Color Color2
        {
            get { return _color2; }
            set { if (SetProperty(ref _color2, value)) SetBrush(); }
        }

        public int Height
        {
            get { return _height; }
            set { if (SetProperty(ref _height, value)) SetBrush(); }
        }

        public int Width
        {
            get { return _width; }
            set { if (SetProperty(ref _width, value)) SetBrush(); }
        }
        #endregion


        #region Methods
        public static ImageBrush Create(Color color1, Color color2, int squareWidth, int squareHeight)
            => CreateBrush(squareWidth, squareHeight, CreateImageSource(color1, color2, squareWidth, squareHeight));
        #endregion


        #region Override
        protected override Brush CreateBrush()
            => Create(Color1, Color2, Width, Height);
        #endregion


        #region Implementation
        private static ImageBrush CreateBrush(int squareWidth, int squareHeight, ImageSource imageSource)
            => new ImageBrush(imageSource)
            {
                TileMode = TileMode.Tile,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewport = CreateViewPort(squareWidth, squareHeight)
            };

        private static ImageSource CreateImageSource(Color color1, Color color2, int squareWidth,
            int squareHeight) => new SquareBrushCreator(squareWidth, squareHeight, color1, color2).CreateImageSource();

        private static Rect CreateViewPort(int squareWidth, int squareHeight)
            => new Rect(0, 0, 2 * squareWidth, 2 * squareHeight);
        #endregion
    }

    /*public class SquareBrush : ObservableObject
    {
        #region Fields
        private readonly ImageBrush _brush;
        private Color _color1 = Colors.Gray;
        private Color _color2 = Colors.White;
        private int _height = 10;
        private int _width = 10;
        #endregion


        #region  Constructors & Destructors
        public SquareBrush()
        {
            _brush = Create(Color1, Color2, Width, Height);
        }
        #endregion


        #region  Properties & Indexers
        public Brush Brush => _brush;

        public Color Color1
        {
            get { return _color1; }
            set { if (SetProperty(ref _color1, value)) ApplyBrush(); }
        }

        public Color Color2
        {
            get { return _color2; }
            set { if (SetProperty(ref _color2, value)) ApplyBrush(); }
        }

        public int Height
        {
            get { return _height; }
            set { if (SetProperty(ref _height, value)) ApplyBrush(); }
        }

        public int Width
        {
            get { return _width; }
            set { if (SetProperty(ref _width, value)) ApplyBrush(); }
        }
        #endregion


        #region Methods
        public static ImageBrush Create(Color color1, Color color2, int squareWidth, int squareHeight)
            => CreateBrush(squareWidth, squareHeight, CreateWriteableBitmap(color1, color2, squareWidth, squareHeight));
        #endregion


        #region Implementation
        private void ApplyBrush()
        {
            var writeableBitmap = CreateWriteableBitmap(Color1, Color2, Width, Height);
            if (_brush == null) return;
            _brush.ImageSource = writeableBitmap;
            _brush.Viewport = CreateViewPort();
        }

        private static ImageBrush CreateBrush(int squareWidth, int squareHeight, ImageSource imageSource)
            => new ImageBrush(imageSource)
            {
                TileMode = TileMode.Tile,
                ViewportUnits = BrushMappingMode.Absolute,
                Viewport = CreateViewPort(squareWidth, squareHeight)
            };

        private Rect CreateViewPort() => CreateViewPort(Width, Height);

        private static Rect CreateViewPort(int squareWidth, int squareHeight)
            => new Rect(0, 0, 2 * squareWidth, 2 * squareHeight);

        private static WriteableBitmap CreateWriteableBitmap(Color color1, Color color2, int squareWidth,
            int squareHeight)
        {
            int pixelWidth = squareWidth * 2, pixelHeight = squareHeight * 2;
            const int DPI = 96;

            var writeableBitmap = new WriteableBitmap(pixelWidth, pixelHeight, DPI, DPI, PixelFormats.Bgra32, null);
            var bytesPerPixel = writeableBitmap.Format.BitsPerPixel / 8;
            var pixels = new byte[pixelWidth * pixelHeight * bytesPerPixel];

            for (var i = 0; i < pixels.Length; i += bytesPerPixel)
            {
                int x = i / bytesPerPixel % pixelWidth, y = i / bytesPerPixel / pixelWidth;
                var pixelColor = (x < squareWidth && y < squareHeight) || (x >= squareWidth && y >= squareHeight)
                                     ? color2 : color1;
                SetPixel(pixels, i, pixelColor);
            }

            var stride = writeableBitmap.PixelWidth * writeableBitmap.Format.BitsPerPixel / 8;
            writeableBitmap.WritePixels(new Int32Rect(0, 0, pixelWidth, pixelHeight), pixels, stride, 0);
            return writeableBitmap;
        }

        private static void SetPixel(IList<byte> pixels, int offset, Color pixelColor)
        {
            pixels[offset] = pixelColor.B;
            pixels[offset + 1] = pixelColor.G;
            pixels[offset + 2] = pixelColor.R;
            pixels[offset + 3] = pixelColor.A;
        }
        #endregion
    }*/
}