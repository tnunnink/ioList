using System.Linq;
using ioList.Model;
using L5Sharp.Extensions;

namespace ioList.Generation.Steps;

public class DetermineBuffersStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var tagDictionary = context.Content.Tags();

        foreach (var tag in context.Tags)
        {
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

                    if (!tagDictionary.Contains(buffer)) continue;

                    var member = tagDictionary.FindAll(buffer).First();
                    tag.BufferTag = member.TagName;
                    tag.BufferDescription = member.Description;
                    break;
                }
            }
        }
    }
}