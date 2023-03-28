using System.Linq;
using ioList.Entities;
using LinqKit;

namespace ioList.Generation.Steps;

public class ApplyFiltersStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var predicate = PredicateBuilder.New<DeviceTag>(true);

        predicate = context.Config.Filters.Aggregate(predicate, (current, filter) => current.And(filter.ToPredicate()));

        context.Tags = context.Tags.Where(predicate.Compile()).ToList();
    }
}