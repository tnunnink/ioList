using System.Linq;
using ioList.Model;
using L5Sharp.Extensions;

namespace ioList.Generation.Steps;

public class FindReferencesStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var referenceLookup = context.Content.LogicFlatten().ToTagLookup();

        foreach (var tag in context.Tags)
        {
           referenceLookup.TryGetValue(tag.TagName, out var references);

            if (references is null || references.Count == 0) continue;

            tag.References = references.ToList();
        }
    }
}