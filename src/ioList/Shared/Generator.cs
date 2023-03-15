using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using ioList.Model;
using L5Sharp;
using L5Sharp.Components;
using L5Sharp.Enums;
using L5Sharp.Extensions;

namespace ioList.Shared;

public class Generator
{
    private static readonly List<string> BufferPatterns = new()
    {
        Instruction.MOV.Signature,
        string.Concat(Instruction.XIC.Signature, @"(?:\s+)?", Instruction.OTE.Signature),
        string.Concat(Instruction.XIO.Signature, @"(?:\s+)?", Instruction.OTE.Signature)
    };

    public static void Generate(string source, string destination)
    {
        var content = LogixContent.Load(source);

        var points = GetPointsFromModules(content);

        AttachBuffers(content, points);

        var list = points.Where(p => p.Buffer is not null);

        WriteCsv(list, destination);
    }

    private static void AttachBuffers(LogixContent content, List<Point> points)
    {
        var referenceLookup = content.LogicFlatten()
            .SelectMany(t => t.SplitBy(BufferPatterns))
            .ToTagLookup();

        var tagLookup = content.Query<Tag>()
            .SelectMany(t => t.MembersAndSelf())
            .ToLookup(k => k.TagName, t => t);

        foreach (var point in points)
        {
            referenceLookup.TryGetValue(point.Tag, out var references);

            if (references is null || references.Count == 0) continue;

            //Just get the first reference for now...
            var reference = references.FirstOrDefault();
            var other = reference?.Tags().FirstOrDefault(t => t != point.Tag);
            
            if (other is null) continue;
            if (!tagLookup.Contains(other)) continue;

            var member = tagLookup[other].First();
            point.Buffer = new BufferTag(member);
        }
    }

    private static List<Point> GetPointsFromModules(ILogixContent content)
    {
        var points = new List<Point>();

        var modules = content.Modules();

        foreach (var module in modules)
        {
            //As of not only supporting the single input connection. Will look at potential for multiple later.
            var input = module.Connections.FirstOrDefault()?.Input;

            if (input is not null)
                points.AddRange(input.Members().Select(t => new Point(module, t, "Input")));

            //As of not only supporting the single output connection. Will look at potential for multiple later.
            var output = module.Connections.FirstOrDefault()?.Output;

            if (output is not null)
                points.AddRange(output.Members().Select(t => new Point(module, t, "Output")));
        }

        return points;
    }

    private static void WriteCsv(IEnumerable<Point> points, string destination)
    {
        Console.WriteLine($"Writing to destination file {destination}");
        using var writer = new StreamWriter(destination);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<PointMap>();
        csv.WriteRecords(points);
    }
}