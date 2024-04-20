using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

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
        public static readonly DependencyProperty ContentMarginProperty = DependencyProperty.RegisterAttached("ContentMargin",
            typeof(Thickness),
            typeof(ComboBoxExtensions),
            new PropertyMetadata(new Thickness(1)));
        public static Thickness GetContentMargin(DependencyObject obj) => (Thickness)obj.GetValue(ContentMarginProperty);
        public static void SetContentMargin(DependencyObject obj, Thickness value) => obj.SetValue(ContentMarginProperty, value);
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

    public class ComboBoxTemplateSelector : DataTemplateSelector
    {

        public DataTemplate? SelectedItemTemplate { get; set; }
        public DataTemplateSelector? SelectedItemTemplateSelector { get; set; }
        public DataTemplate? DropdownItemsTemplate { get; set; }
        public DataTemplateSelector? DropdownItemsTemplateSelector { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {

            var itemToCheck = container;

            // Search up the visual tree, stopping at either a ComboBox or
            // a ComboBoxItem (or null). This will determine which template to use
            while (itemToCheck is not null
                   and not ComboBox
                   and not ComboBoxItem)
                itemToCheck = VisualTreeHelper.GetParent(itemToCheck);

            // If you stopped at a ComboBoxItem, you're in the dropdown
            var inDropDown = itemToCheck is ComboBoxItem;

            return inDropDown
                ? DropdownItemsTemplate ?? DropdownItemsTemplateSelector?.SelectTemplate(item, container)
                : SelectedItemTemplate ?? SelectedItemTemplateSelector?.SelectTemplate(item, container);
        }

    }
    public class ComboBoxTemplateSelectorExtension : MarkupExtension
    {

        public DataTemplate? SelectedItemTemplate { get; set; }
        public DataTemplateSelector? SelectedItemTemplateSelector { get; set; }
        public DataTemplate? DropdownItemsTemplate { get; set; }
        public DataTemplateSelector? DropdownItemsTemplateSelector { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
            => new ComboBoxTemplateSelector()
            {
                SelectedItemTemplate = SelectedItemTemplate,
                SelectedItemTemplateSelector = SelectedItemTemplateSelector,
                DropdownItemsTemplate = DropdownItemsTemplate,
                DropdownItemsTemplateSelector = DropdownItemsTemplateSelector
            };
    }
}