using System.Data;
using System.Linq;
using ioList.Model;

namespace ioList.Generation.Steps;

public class CreateTableStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var columns = context.Config.Columns.Select(c => c.ToDataColumn()).ToArray();
        var table = new DataTable();
        table.Columns.AddRange(columns);

        foreach (var tag in context.Tags)
        {
            var row = table.NewRow();

            foreach (var column in context.Config.Columns.Where(c => c.Type == ColumnType.Property))
            {
                var value = tag.GetType().GetProperties().First(p => p.Name == column.Property).GetValue(tag) ??
                            string.Empty;
                row[column.ColumnName] = value;
            }

            table.Rows.Add(row);
        }

        context.Table = table;
    }
}