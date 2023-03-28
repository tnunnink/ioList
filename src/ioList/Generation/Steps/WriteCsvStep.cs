using System.Globalization;
using System.IO;
using CsvHelper;
using ioList.Entities;

namespace ioList.Generation.Steps;

public class WriteCsvStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        if (!context.Config.Overwrite && File.Exists(context.Destination)) return;
        using var writer = new StreamWriter(context.Destination);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<TagMap>();
        csv.WriteRecords(context.Tags);
    }
}