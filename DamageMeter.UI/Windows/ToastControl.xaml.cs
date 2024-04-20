using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Nostrum.Factories;

namespace DamageMeter.UI.Windows
{
    public partial class ToastControl
    {
        private const int _animDuration = 250;

        private readonly DoubleAnimation _fadeIn;
        private readonly DoubleAnimation _fadeOut;
        private readonly DoubleAnimation _slideIn;
        private readonly DoubleAnimation _slideOut;

        private readonly Geometry _infoGeometry;
        private readonly Geometry _successGeometry;
        private readonly Geometry _warningGeometry;
        private readonly Geometry _errorGeometry;

        public string ToastText
        {
            get => (string) GetValue(ToastTextProperty);
            set => SetValue(ToastTextProperty, value);
        }
        public static readonly DependencyProperty ToastTextProperty = DependencyProperty.Register(nameof(ToastText), typeof(string), typeof(ToastControl), new PropertyMetadata(""));

        public ToastInfo.Severity ToastSeverity
        {
            get => (ToastInfo.Severity) GetValue(ToastSeverityProperty);
            set => SetValue(ToastSeverityProperty, value);
        }
        public static readonly DependencyProperty ToastSeverityProperty = DependencyProperty.Register(nameof(ToastSeverity), typeof(ToastInfo.Severity), typeof(ToastControl), new PropertyMetadata(ToastInfo.Severity.Success, OnSeverityChanged));

        public double SlideOffset
        {
            get => (double) GetValue(SlideOffsetProperty);
            set => SetValue(SlideOffsetProperty, value);
        }
        public static readonly DependencyProperty SlideOffsetProperty = DependencyProperty.Register(nameof(SlideOffset), typeof(double), typeof(ToastControl), new PropertyMetadata(0D, OnSlideOffsetChanged));

        public bool IsShowed
        {
            get => (bool) GetValue(IsShowedProperty);
            set => SetValue(IsShowedProperty, value);
        }
        public static readonly DependencyProperty IsShowedProperty = DependencyProperty.Register(nameof(IsShowed), typeof(bool), typeof(ToastControl), new PropertyMetadata(false, OnShowedChanged));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius) GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(ToastControl));


        public ToastControl()
        {
            _fadeIn = AnimationFactory.CreateDoubleAnimation(_animDuration, 1, 0, true);
            _fadeOut = AnimationFactory.CreateDoubleAnimation(_animDuration, 0, 1, true);
            _slideIn = AnimationFactory.CreateDoubleAnimation(_animDuration, SlideOffset, 0, true);
            _slideOut = AnimationFactory.CreateDoubleAnimation(_animDuration, 0, SlideOffset, true);

            _infoGeometry = Geometry.Parse("M480-280q17 0 28.5-11.5T520-320v-160q0-17-11.5-28.5T480-520q-17 0-28.5 11.5T440-480v160q0 17 11.5 28.5T480-280Zm0-320q17 0 28.5-11.5T520-640q0-17-11.5-28.5T480-680q-17 0-28.5 11.5T440-640q0 17 11.5 28.5T480-600Zm0 520q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Zm0-80q134 0 227-93t93-227q0-134-93-227t-227-93q-134 0-227 93t-93 227q0 134 93 227t227 93Zm0-320Z");
            _successGeometry= Geometry.Parse("m424-408-86-86q-11-11-28-11t-28 11q-11 11-11 28t11 28l114 114q12 12 28 12t28-12l226-226q11-11 11-28t-11-28q-11-11-28-11t-28 11L424-408Zm56 328q-83 0-156-31.5T197-197q-54-54-85.5-127T80-480q0-83 31.5-156T197-763q54-54 127-85.5T480-880q83 0 156 31.5T763-763q54 54 85.5 127T880-480q0 83-31.5 156T763-197q-54 54-127 85.5T480-80Zm0-80q134 0 227-93t93-227q0-134-93-227t-227-93q-134 0-227 93t-93 227q0 134 93 227t227 93Zm0-320Z");
            _warningGeometry = Geometry.Parse("M109-120q-11 0-20-5.5T75-140q-5-9-5.5-19.5T75-180l370-640q6-10 15.5-15t19.5-5q10 0 19.5 5t15.5 15l370 640q6 10 5.5 20.5T885-140q-5 9-14 14.5t-20 5.5H109Zm69-80h604L480-720 178-200Zm302-40q17 0 28.5-11.5T520-280q0-17-11.5-28.5T480-320q-17 0-28.5 11.5T440-280q0 17 11.5 28.5T480-240Zm0-120q17 0 28.5-11.5T520-400v-120q0-17-11.5-28.5T480-560q-17 0-28.5 11.5T440-520v120q0 17 11.5 28.5T480-360Zm0-100Z");
            _errorGeometry = Geometry.Parse("M363-120q-16 0-30.5-6T307-143L143-307q-11-11-17-25.5t-6-30.5v-234q0-16 6-30.5t17-25.5l164-164q11-11 25.5-17t30.5-6h234q16 0 30.5 6t25.5 17l164 164q11 11 17 25.5t6 30.5v234q0 16-6 30.5T817-307L653-143q-11 11-25.5 17t-30.5 6H363Zm1-80h232l164-164v-232L596-760H364L200-596v232l164 164Zm116-224 86 86q11 11 28 11t28-11q11-11 11-28t-11-28l-86-86 86-86q11-11 11-28t-11-28q-11-11-28-11t-28 11l-86 86-86-86q-11-11-28-11t-28 11q-11 11-11 28t11 28l86 86-86 86q-11 11-11 28t11 28q11 11 28 11t28-11l86-86Zm0-56Z");

            InitializeComponent();
        }

        private static void OnShowedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ToastControl tc)) { return; }
            tc.Toggle();
        }

        private static void OnSeverityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not ToastControl tc) { return; }
            tc.SetSeverity();
        }

        private static void OnSlideOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ToastControl tc)) { return; }
            tc.UpdateSlideOffset();
        }

        private void Toggle()
        {
            MainBorder.BeginAnimation(OpacityProperty, IsShowed ? _fadeIn : _fadeOut);
            MainBorder.RenderTransform.BeginAnimation(TranslateTransform.YProperty, IsShowed ? _slideIn : _slideOut);
        }

        private void SetSeverity()
        {
            Icon.Fill = ToastSeverity switch
            {
                ToastInfo.Severity.Info => Brushes.SlateGray,
                ToastInfo.Severity.Success => Brushes.MediumSeaGreen,
                ToastInfo.Severity.Warning => Brushes.DarkOrange,
                ToastInfo.Severity.Error => new SolidColorBrush(Color.FromRgb(0xff, 0x44, 0x55)),
                _ => throw new ArgumentOutOfRangeException()
            };

            Icon.Data = ToastSeverity switch
            {
                ToastInfo.Severity.Info => _infoGeometry,
                ToastInfo.Severity.Success => _successGeometry,
                ToastInfo.Severity.Warning => _warningGeometry,
                ToastInfo.Severity.Error => _errorGeometry,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private void UpdateSlideOffset()
        {
            _slideIn.From = SlideOffset;
            _slideOut.To = SlideOffset;
        }
    }
}