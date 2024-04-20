using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DamageMeter.UI.HUD.Converters;

public class MainWindowTemplateSelector : MarkupExtension, IValueConverter
{
    public DataTemplate? Empty { get; set; }
    public DataTemplate? Main { get; set; }

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true 
            ? Empty 
            : Main;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this;
    }
}