using System.Windows.Media;
using CB.Model.Common;


namespace CB.Media.Brushes
{
    public abstract class CustomBrushBase: ObservableObject
    {
        #region Abstract
        protected abstract Brush CreateBrush();
        #endregion


        #region  Properties & Indexers
        public virtual Brush Brush { get; protected set; }
        #endregion


        #region Implementation
        protected virtual void SetBrush()
        {
            Brush = CreateBrush();
            NotifyPropertyChanged(nameof(Brush));
        }
        #endregion
    }
}