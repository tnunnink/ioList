using System.Linq;
using ioList.Model;
using L5Sharp.Components;

namespace ioList.Generation.Steps;

public class DetermineBuffersStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var tagLookup = context.Content.Query<Tag>()
            .SelectMany(t => t.MembersAndSelf())
            .ToLookup(k => k.TagName, t => t);

        foreach (var tag in context.Tags)
        {
            if (tag.TagName == "IO_INJ_FLEX:4:O.Ch00.Data")
            {
                
            }
            if (tag.References.Count == 0) continue;

            foreach (var reference in tag.References)
            {
                foreach (var pattern in context.Config.Patterns.Where(p => p.Enabled))
                {
                    if (!reference.SplitByKey(pattern.ReferenceKey).Any(t => t.ContainsTag(tag.TagName))) continue;

                    var operands = reference.SplitByKey(pattern.BufferKey).FirstOrDefault()?.Operands().ToList();

                    if (operands is null || pattern.BufferOrdinal < 0 || pattern.BufferOrdinal >= operands.Count) 
                        continue;

                    var buffer = operands[pattern.BufferOrdinal];

                    if (!tagLookup.Contains(buffer)) continue;

                    var member = tagLookup[buffer].First();
                    tag.BufferTag = member.TagName;
                    tag.BufferDescription = member.Description;
                    break;
                }
            }
        }
    }
}