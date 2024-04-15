using System.Windows;

namespace DamageMeter.UI.Controls
{
    public class CheckBoxExtensions
    {

        public static readonly DependencyProperty SizeProperty = DependencyProperty.RegisterAttached("Size",
            typeof(double),
            typeof(CheckBoxExtensions),
            new PropertyMetadata(18D));
        public static double GetSize(DependencyObject obj) => (double)obj.GetValue(SizeProperty);
        public static void SetSize(DependencyObject obj, double value) => obj.SetValue(SizeProperty, value);


    }

    public class ComboBoxExtensions
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius",
            typeof(CornerRadius),
            typeof(ComboBoxExtensions),
            new PropertyMetadata(new CornerRadius(0)));
        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
    }
    public class ToggleButtonExtensions
    {
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached("CornerRadius",
            typeof(CornerRadius),
            typeof(ToggleButtonExtensions),
            new PropertyMetadata(new CornerRadius(0)));
        public static CornerRadius GetCornerRadius(DependencyObject obj) => (CornerRadius)obj.GetValue(CornerRadiusProperty);
        public static void SetCornerRadius(DependencyObject obj, CornerRadius value) => obj.SetValue(CornerRadiusProperty, value);
    }
}