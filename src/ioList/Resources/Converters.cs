using System;
using System.Windows;
using System.Windows.Data;
using ioList.Model;
using LambdaConverters;

namespace ioList.Resources
{
    public static class Converters
    {
        public static readonly IValueConverter VisibleIfTrue =
            ValueConverter.Create<bool, Visibility>(e => e.Value ? Visibility.Visible : Visibility.Collapsed);

        public static readonly IValueConverter VisibleIfPropertyColumn =
            ValueConverter.Create<ColumnType, Visibility>(e =>
                e.Value == ColumnType.Property ? Visibility.Visible : Visibility.Collapsed);

        public static readonly IValueConverter VisibleIfNotPropertyColumn =
            ValueConverter.Create<ColumnType, Visibility>(e =>
                e.Value == ColumnType.Property ? Visibility.Collapsed : Visibility.Visible);

        public static readonly IValueConverter EnumName =
            ValueConverter.Create<Enum, string>(e => Enum.GetName(e.Value.GetType(), e.Value));
    }
}