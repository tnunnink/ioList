using System.Collections.ObjectModel;
using ioList.Abstractions;
using ioList.Common;
using ioList.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private readonly IListRepository _listRepository;
        private readonly IDialogService _dialogService;
        private DelegateCommand _createListCommand;


        public ListViewModel()
        {
            Lists = new ObservableCollection<IoList>
            {
                new("List Name 01", "Path To File", "This is fake"),
                new("List Name 01", "Path To File", "This is fake"),
                new("List Name 01", "Path To File", "This is fake")
            };
        }

        public ListViewModel(IListRepository listRepository, IEventAggregator eventAggregator,
            IDialogService dialogService)
        {
            _listRepository = listRepository;
            _dialogService = dialogService;

            Load();
        }

        public ObservableCollection<IoList> Lists { get; private set; }
        
        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand);

        private void ExecuteCreateListCommand()
        {
            _dialogService.ShowDialog(DialogNames.CreateListDialog);
        }

        private void Load()
        {
            //Lists = _listRepository.Get
        }
    }
}