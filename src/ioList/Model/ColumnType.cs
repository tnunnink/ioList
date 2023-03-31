using System.ComponentModel;

namespace ioList.Model;

public enum ColumnType
{
    [Description(nameof(Property))]
    Property,
    [Description(nameof(Field))]
    Field
    
    //Expression, todo would be cool but DataColumn doesn't really have useful string functions that users may want
                    //todo this feature for. You can always just add expression columns from Excel.
}