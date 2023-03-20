using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using L5Sharp.Enums;

namespace ioList.Model;

public class GeneratorOptions
{
    private const string FileName = "Options.json";
    
    public GeneratorOptions()
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
    
    [JsonIgnore]
    public List<Predicate<Point>> Filters { get; set; }
    
    
    public static GeneratorOptions Load()
    {
        var file = new FileInfo(FileName);

        if (!file.Exists)
            File.WriteAllText(FileName, JsonSerializer.Serialize(new GeneratorOptions()));

        return JsonSerializer.Deserialize<GeneratorOptions>(File.ReadAllText(FileName));
    }

    public void Save() => File.WriteAllText(FileName, JsonSerializer.Serialize(this));
}