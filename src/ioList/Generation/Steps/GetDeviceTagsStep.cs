using System.Linq;
using ioList.Model;

namespace ioList.Generation.Steps;

public class GetDeviceTagsStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var modules = context.Content.Modules;

        foreach (var module in modules)
        {
            //As of now only supporting the single input connection. Will look at potential for multiple later.
            var input = module.Communications?.Connections.FirstOrDefault()?.InputTag;

            if (input is not null)
                context.Tags.AddRange(input.Members().Select(t => new DeviceTag(module, t, "Input")));

            //As of now only supporting the single output connection. Will look at potential for multiple later.
            var output = module.Communications?.Connections.FirstOrDefault()?.OutputTag;

            if (output is not null)
                context.Tags.AddRange(output.Members().Select(t => new DeviceTag(module, t, "Output")));
        }
    }
}