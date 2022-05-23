using CoreTools.WPF.Mvvm;
using ioList.Commands;

namespace ioList.ViewModels
{
    public class ListBarViewModel : NavigationViewModelBase
    {
        private readonly IApplicationCommands _applicationCommands;
        
        public ListBarViewModel( IApplicationCommands applicationCommands)
        {
            ApplicationCommands = applicationCommands;
        }
        
        public IApplicationCommands ApplicationCommands
        {
            get => _applicationCommands;
            private init => SetProperty(ref _applicationCommands, value);
        }

    }
}