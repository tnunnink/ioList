using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ioList.Resources.Converters
{
    public class BooleanVisibilityConverter : MarkupExtension, IValueConverter
    {
        public bool IsInverse { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var convertValue = false;

            if (value is bool b)
            {
                convertValue = b;
            }

            return (convertValue ^ IsInverse) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return (visibility == Visibility.Visible) ^ IsInverse;
            }

            return false;
        }
    }
}