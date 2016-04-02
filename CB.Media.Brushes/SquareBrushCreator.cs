using System.Windows.Media;
using CB.Media.Brushes.Impl;


namespace CB.Media.Brushes
{
    public class SquareBrushCreator: BrushCreatorUsingCoordinate
    {
        #region  Constructors & Destructors
        public SquareBrushCreator() { }

        public SquareBrushCreator(Color color1, Color color2)
        {
            Color1 = color1;
            Color2 = color2;
        }

        public SquareBrushCreator(int width, int height): base(2 * width, 2 * height) { }

        public SquareBrushCreator(int width, int height, Color color1, Color color2): this(width, height)
        {
            Color1 = color1;
            Color2 = color2;
        }
        #endregion


        #region  Properties & Indexers
        public Color Color1 { get; set; } = Colors.Gray;

        public Color Color2 { get; set; } = Colors.White;
        #endregion


        #region Override
        protected override Color GetColorAt(int x, int y)
        {
            int squareWidth = Width / 2, squareHeight = Height / 2;
            return (x < squareWidth && y < squareHeight) || (x >= squareWidth && y >= squareHeight)
                       ? Color2 : Color1;
        }
        #endregion
    }
}