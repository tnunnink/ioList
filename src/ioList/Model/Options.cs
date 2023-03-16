using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using L5Sharp.Enums;

namespace ioList.Model;

public class Options
{
    private const string FileName = "Options.json";
    
    public Options()
    {
        SearchForBuffers = true;

        BufferPatterns = new List<string>
        {
            Instruction.MOV.Signature,
            string.Concat(Instruction.XIC.Signature, @"(?:\s+)?", Instruction.OTE.Signature),
            string.Concat(Instruction.XIO.Signature, @"(?:\s+)?", Instruction.OTE.Signature)
        };

        Filters = new List<Predicate<Point>>
        {
            p => p.Buffer is not null,
            p => p.Comment != string.Empty,
            p => !p.Address.EndsWith(".Fault")
        };
    }
    
    public bool SearchForBuffers { get; set; }
    
    public List<string> BufferPatterns { get; set; }
    
    public List<Predicate<Point>> Filters { get; set; }
    
    
    public static Options Load()
    {
        var file = new FileInfo(FileName);

        if (file.Directory is not null && !file.Directory.Exists)
            file.Directory.Create();

        if (!file.Exists)
            File.WriteAllText(FileName, JsonSerializer.Serialize(new Options()));

        return JsonSerializer.Deserialize<Options>(File.ReadAllText(FileName));
    }

    public void Save() => File.WriteAllText(FileName, JsonSerializer.Serialize(this));
}