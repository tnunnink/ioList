using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ioList.Resources
{
    public class BooleanDoubleConverter : MarkupExtension, IValueConverter
    {
        public double TrueValue { get; set; }
        public double FalseValue { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var active = false;
            
            if (value is bool b)
                active = b;

            return active ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var active = false;
            
            if (value is double d)
                active = Math.Abs(TrueValue - d) < double.Epsilon;
            
            return active;
        }
    }
}