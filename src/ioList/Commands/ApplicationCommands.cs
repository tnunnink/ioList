using ioList.Common.Naming;
using ioList.Domain;
using ioList.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;

namespace ioList.Commands
{
    public class ApplicationCommands : IApplicationCommands
    {
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _newListCommand;
        private DelegateCommand _toggleListMenu;

        public ApplicationCommands(IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
        }

        public DelegateCommand NewListCommand =>
            _newListCommand ??= new DelegateCommand(ExecuteNewListCommand);

        public DelegateCommand OpenListCommand { get; }

        public DelegateCommand ToggleListMenu =>
            _toggleListMenu ??= new DelegateCommand(ExecuteShowListMenu);

        private void ExecuteNewListCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewListDialog, r =>
            {
                if (r.Result != ButtonResult.OK) return;
                var listFile = r.Parameters.GetValue<ListFile>("ListFile");
                _eventAggregator.GetEvent<ListCreatedEvent>().Publish(listFile);
            });
        }

        private void ExecuteShowListMenu() => _eventAggregator.GetEvent<ToggleListMenu>().Publish();
    }
}