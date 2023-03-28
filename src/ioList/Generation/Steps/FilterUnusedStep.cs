using System.Linq;

namespace ioList.Generation.Steps;

public class FilterUnusedStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
         context.Tags = context.Tags.Where(t => t.References.Count > 0).ToList();
    }
}