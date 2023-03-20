using ioList.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ioList.Views;

public partial class OptionsView
{
    public OptionsView()
    {
        InitializeComponent();
        DataContext = App.Current.Services.GetService<OptionsViewModel>();
    }
}