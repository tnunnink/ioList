using ioList.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ioList
{
    public partial class Shell
    {
        public Shell()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<ShellViewModel>();
        }
    }
}