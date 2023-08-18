using System.Linq;
using ioList.Model;

namespace ioList.Generation.Steps;

public class GetDeviceTagsStep : IGeneratorStep
{
    public void Execute(GeneratorContext context)
    {
        var modules = context.Content.Modules.ToList();

        foreach (var module in modules)
        {
            var config = module.Communications?.ConfigTag;

            //As of now only supporting the single input connection. Will look at potential for multiple later.
            var inputs = module.Communications?.Connections.FirstOrDefault()?.InputTag?.Members().ToList();

            if (inputs is not null)
            {
                var deviceTags = inputs.Select(t => new DeviceTag(module, config, t, "Input")).ToList();
                context.Tags.AddRange(deviceTags);
            }

            //As of now only supporting the single output connection. Will look at potential for multiple later.
            var output = module.Communications?.Connections.FirstOrDefault()?.OutputTag;

            if (output is not null)
                context.Tags.AddRange(output.Members().Select(t => new DeviceTag(module, config, t, "Output")));
        }
    }
}