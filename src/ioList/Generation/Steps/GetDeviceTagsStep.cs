using System.Linq;
using ioList.Entities;

namespace ioList.Generation.Steps;

public class GetDeviceTagsStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var modules = context.Content.Modules();

        foreach (var module in modules)
        {
            //As of not only supporting the single input connection. Will look at potential for multiple later.
            var input = module.Connections.FirstOrDefault()?.Input;

            if (input is not null)
                context.Tags.AddRange(input.Members().Select(t => new DeviceTag(module, t, "Input")));

            //As of not only supporting the single output connection. Will look at potential for multiple later.
            var output = module.Connections.FirstOrDefault()?.Output;

            if (output is not null)
                context.Tags.AddRange(output.Members().Select(t => new DeviceTag(module, t, "Output")));
        }
    }
}