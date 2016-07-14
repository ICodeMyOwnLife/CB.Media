using System.Windows.Media;
using CB.Media.Brushes.Impl;
using CB.Model.Common;


namespace CB.Media.Brushes
{
    public class ColorInfo: BindableObject
    {
        #region Fields
        private byte _a, _r, _g, _b;
        private Color _color, _root;
        private double _hue, _saturation, _lightness;
        private float _scA, _scR, _scG, _scB;
        #endregion


        #region  Constructors & Destructor
        public ColorInfo()
        {
            Color = Color.FromRgb(byte.MaxValue, 0, 0);
        }
        #endregion


        #region  Properties & Indexers
        public byte A
        {
            get { return _a; }
            set
            {
                if (!SetProperty(ref _a, value)) return;

                UpdateColorFromArgb();
                UpdateScA();
            }
        }

        public byte B
        {
            get { return _b; }
            set
            {
                if (!SetProperty(ref _b, value)) return;

                UpdateFromArgb();
                UpdateScB();
            }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                if (!SetProperty(ref _color, value)) return;

                UpdateRoot();
                UpdateComponents();
                UpdateFromComponent();
                UpdateOpacity();
            }
        }

        public byte G
        {
            get { return _g; }
            set
            {
                if (!SetProperty(ref _g, value)) return;

                UpdateFromArgb();
                UpdateScG();
            }
        }

        public double Hue
        {
            get { return _hue; }
            set
            {
                if (!SetProperty(ref _hue, value)) return;

                UpdateFromHsl();
                UpdateRoot();
            }
        }

        public double Lightness
        {
            get { return _lightness; }
            set { if (SetProperty(ref _lightness, value)) UpdateFromHsl(); }
        }

        public byte R
        {
            get { return _r; }
            set
            {
                if (!SetProperty(ref _r, value)) return;

                UpdateFromArgb();
                UpdateScR();
            }
        }

        public Color Root => _root;

        public double Saturation
        {
            get { return _saturation; }
            set { if (SetProperty(ref _saturation, value)) UpdateFromHsl(); }
        }

        public float ScA
        {
            get { return _scA; }
            set
            {
                if (!SetProperty(ref _scA, value)) return;

                UpdateColorFromScRgb();
                UpdateA();
            }
        }

        public float ScB
        {
            get { return _scB; }
            set
            {
                if (!SetProperty(ref _scB, value)) return;

                UpdateFromScRgb();
                UpdateB();
            }
        }

        public float ScG
        {
            get { return _scG; }
            set
            {
                if (!SetProperty(ref _scG, value)) return;

                UpdateFromScRgb();
                UpdateG();
            }
        }

        public float ScR
        {
            get { return _scR; }
            set
            {
                if (!SetProperty(ref _scR, value)) return;

                UpdateFromScRgb();
                UpdateR();
            }
        }
        #endregion


        #region Implementation
        private void SetColor(Color color)
            => SetField(ref _color, color, nameof(Color));

        private void UpdateA()
            => SetField(ref _a, _color.A, nameof(A));

        private void UpdateB()
            => SetField(ref _b, _color.B, nameof(B));

        private void UpdateColorFromArgb()
            => SetColor(Color.FromArgb(A, R, G, B));

        private void UpdateColorFromScRgb()
            => SetColor(Color.FromScRgb(ScA, ScR, ScG, ScB));

        private void UpdateComponents()
        {
            UpdateR();
            UpdateG();
            UpdateB();
            UpdateScR();
            UpdateScG();
            UpdateScB();
        }

        private void UpdateFromArgb()
        {
            UpdateColorFromArgb();
            UpdateFromComponent();
        }

        private void UpdateFromComponent()
        {
            UpdateRoot();
            UpdateHue();
            UpdateSaturation();
            UpdateLightness();
        }

        private void UpdateFromHsl()
        {
            SetColor(MediaHelper.CreateColor(Hue, Saturation, Lightness));
            UpdateComponents();
        }

        private void UpdateFromScRgb()
        {
            UpdateColorFromScRgb();
            UpdateFromComponent();
        }

        private void UpdateG()
            => SetField(ref _g, _color.G, nameof(G));

        private void UpdateHue()
        {
            var hue = _color.GetHue();
            if (hue.HasValue) SetField(ref _hue, hue.Value, nameof(Hue));
        }

        private void UpdateLightness()
            => SetField(ref _lightness, _color.GetLightness(), nameof(Lightness));

        private void UpdateOpacity()
        {
            UpdateA();
            UpdateScA();
        }

        private void UpdateR()
            => SetField(ref _r, _color.R, nameof(R));

        private void UpdateRoot()
            => SetField(ref _root, MediaHelper.CreateColor(Hue), nameof(Root));

        private void UpdateSaturation()
            => SetField(ref _saturation, _color.GetSaturation(), nameof(Saturation));

        private void UpdateScA()
            => SetField(ref _scA, _color.ScA, nameof(ScA));

        private void UpdateScB()
            => SetField(ref _scB, _color.ScB, nameof(ScB));

        private void UpdateScG()
            => SetField(ref _scG, _color.ScG, nameof(ScG));

        private void UpdateScR()
            => SetField(ref _scR, _color.ScR, nameof(ScR));
        #endregion
    }
}