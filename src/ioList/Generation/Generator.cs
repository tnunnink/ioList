using System.Collections.Generic;
using System.Linq;
using ioList.Generation.Steps;
using ioList.Model;

namespace ioList.Generation;

public class Generator
{
    private readonly GeneratorContext _context;
    private readonly List<IGeneratorStep> _steps = new();

    public Generator(string sourceFile, string destination, GeneratorConfig config)
    {
        _context = new GeneratorContext(sourceFile, destination, config);

        _steps.Add(new GetDeviceTagsStep());
        _steps.Add(new FindReferencesStep());

        if (_context.Config.FindBuffers)
            _steps.Add(new DetermineBuffersStep());

        if (_context.Config.FilterUnused)
            _steps.Add(new FilterUnusedStep());

        if (_context.Config.Filters.Any())
            _steps.Add(new ApplyFiltersStep());
        
        _steps.Add(new CreateTableStep());
        _steps.Add(new WriteCsvStep());
    }

    public void Generate()
    {
        foreach (var step in _steps)
            step.Execute(_context);
    }
}