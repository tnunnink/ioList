using System.Windows;
using System.Windows.Data;
using LambdaConverters;

namespace ioList.Resources
{
    public static class Converters
    {
        public static readonly IValueConverter VisibleIfTrue =
            ValueConverter.Create<bool, Visibility>(e => e.Value ? Visibility.Visible : Visibility.Collapsed);
        
        public static readonly IValueConverter VisibleIfFalse =
            ValueConverter.Create<bool, Visibility>(e => e.Value ? Visibility.Collapsed : Visibility.Visible);
    }
}