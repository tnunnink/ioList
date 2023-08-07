using System.Data;
using System.Globalization;
using System.IO;
using CsvHelper;
using ioList.Model;

namespace ioList.Generation.Steps;

public class WriteCsvStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        if (!context.Config.Overwrite && File.Exists(context.Destination)) return;
        using var writer = new StreamWriter(context.Destination);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        
        // Write columns
        foreach (DataColumn column in context.Table.Columns)
        {
            if (column.ColumnMapping != MappingType.Hidden)
                csv.WriteField(column.ColumnName);
        }
        csv.NextRecord();
 
        // Write row values
        foreach (DataRow row in context.Table.Rows)
        {
            for (var i = 0; i < context.Table.Columns.Count; i++)
            {
                if (context.Table.Columns[i].ColumnMapping != MappingType.Hidden)
                    csv.WriteField(row[i]);
            }
            csv.NextRecord();
        }
    }
}