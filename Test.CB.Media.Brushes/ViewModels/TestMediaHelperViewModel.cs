using System;
using System.Windows.Input;
using System.Windows.Media;
using CB.Model.Prism;
using Prism.Commands;


namespace Test.CB.Media.Brushes.ViewModels
{
    public class TestMediaHelperViewModel: PrismViewModelBase
    {
        #region Fields
        private static readonly byte[] _colorBytes = new byte[3];
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);
        #endregion


        #region  Constructors & Destructor
        public TestMediaHelperViewModel()
        {
            ResetCommand = new DelegateCommand(Reset);
            Reset();
        }
        #endregion


        #region  Commands
        public ICommand ResetCommand { get; }
        #endregion


        #region  Properties & Indexers
        public global::CB.Media.Brushes.ColorInfo ColorInfo { get; } = new global::CB.Media.Brushes.ColorInfo();
        #endregion


        #region Methods
        public void Reset()
            => ColorInfo.Color = CreateRandomColor();
        #endregion


        #region Implementation
        private static Color CreateRandomColor()
        {
            _random.NextBytes(_colorBytes);
            return Color.FromRgb(_colorBytes[0], _colorBytes[1], _colorBytes[2]);
        }
        #endregion
    }
}