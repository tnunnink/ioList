using System.Data;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ioList.Model;

public partial class TagColumn : ObservableObject
{
    public TagColumn()
    {
    }
    
    public TagColumn(string name, int ordinal)
    {
        Type = ColumnType.Property;
        ColumnName = name;
        Property = name;
        Ordinal = ordinal;
    }

    public TagColumn(string displayName, string property, int ordinal)
    {
        Type = ColumnType.Property;
        ColumnName = displayName;
        Property = property;
        Ordinal = ordinal;
    }

    public TagColumn(ColumnType type, string columnName, int ordinal)
    {
        ColumnName = columnName;
        Type = type;
        Ordinal = ordinal;
    }

    [ObservableProperty] private bool _enabled = true;

    [ObservableProperty] private ColumnType _type;

    [ObservableProperty] private string _columnName;

    [ObservableProperty] private string _property;

    [ObservableProperty] private int _ordinal;

    public DataColumn ToDataColumn()
    {
        var column = new DataColumn(ColumnName);
        column.DataType = typeof(string);
        column.DefaultValue = string.Empty;

        /*if (Type == ColumnType.Expression)
            column.Expression = Expression;*/

        if (!Enabled)
            column.ColumnMapping = MappingType.Hidden;

        return column;
    }
}