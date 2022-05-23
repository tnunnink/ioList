using Prism.Commands;

namespace ioList.Commands
{
    public interface IApplicationCommands
    {
        DelegateCommand NewListCommand { get; }
        DelegateCommand OpenListCommand { get; }
        DelegateCommand ToggleListMenu { get; }
    }
}