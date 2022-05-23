using ioList.Commands;
using ioList.Events;
using Prism.Events;
using Prism.Mvvm;

namespace ioList.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IApplicationCommands _applicationCommands;
        private bool _showListMenu;
        private bool _showListBar;

        public ShellViewModel(IApplicationCommands applicationCommands, IEventAggregator eventAggregator)
        {
            ApplicationCommands = applicationCommands;

            ShowListMenu = true;
            ShowListBar = false;
            eventAggregator.GetEvent<ToggleListMenu>().Subscribe(OnShowListMenu);
        }

        public IApplicationCommands ApplicationCommands
        {
            get => _applicationCommands;
            private init => SetProperty(ref _applicationCommands, value);
        }
        
        public bool ShowListMenu
        {
            get => _showListMenu;
            set => SetProperty(ref _showListMenu, value);
        }

        public bool ShowListBar
        {
            get => _showListBar;
            set => SetProperty(ref _showListBar, value);
        }

        private void OnShowListMenu()
        {
            ShowListMenu = !ShowListMenu;
            ShowListBar = !ShowListBar;
        }
    }
}