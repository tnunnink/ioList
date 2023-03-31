using System;
using System.Collections.Generic;
using System.Data;
using L5Sharp;

namespace ioList.Model;

public class GeneratorContext
{
    public GeneratorContext(string sourceFile, string destination, GeneratorConfig config)
    {
        Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        Content = LogixContent.Load(sourceFile);
        Config = config;
        Tags = new List<DeviceTag>();
    }

    public string Destination { get; }
    public LogixContent Content { get; }
    public GeneratorConfig Config { get; }
    public List<DeviceTag> Tags { get; set; }
    public DataTable Table { get; set; }
}