using CommunityToolkit.Mvvm.ComponentModel;
using ioList.Generation;

namespace ioList.ViewModels;

public partial class ConfigurationViewModel : ObservableValidator
{
    private readonly GeneratorConfig _config;

    public ConfigurationViewModel(GeneratorConfig config)
    {
        _config = config;
    }

    [ObservableProperty]
    private bool _findBuffers;
}