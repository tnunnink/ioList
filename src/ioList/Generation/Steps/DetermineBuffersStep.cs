using System.Linq;
using L5Sharp.Components;
using L5Sharp.Extensions;

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
            if (tag.References.Count == 0) continue;

            foreach (var reference in tag.References)
            {
                foreach (var pattern in context.Config.Patterns)
                {
                    if (!reference.OperandsByKey(pattern.ReferenceKey).Any()) continue;

                    var bufferReference = reference.OperandsByKey(pattern.BufferKey).FirstOrDefault().Value;

                    if (bufferReference is null) continue;
                    
                    var buffer = bufferReference.ElementAt(pattern.BufferOrdinal);

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