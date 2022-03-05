using System.Collections.ObjectModel;
using System.Linq;
using ioList.Common;
using ioList.Model;
using ioList.Observers;
using ioList.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private readonly IListFileService _listFileService;
        private readonly IDialogService _dialogService;
        private DelegateCommand _createListCommand;
        private ObservableCollection<ListFileObserver> _lists;

        public ListViewModel()
        {
            Lists = new ObservableCollection<ListFileObserver>
            {
                new(new ListFile("List Name 01", "Path To File")),
                new(new ListFile("List Name 01", "Path To File")),
                new(new ListFile("List Name 01", "Path To File"))
            };
        }

        public ListViewModel(IListFileService listFileService, IEventAggregator eventAggregator,
            IDialogService dialogService)
        {
            _listFileService = listFileService;
            _dialogService = dialogService;
            Load();
        }

        public ObservableCollection<ListFileObserver> Lists
        {
            get => _lists;
            private set => SetProperty(ref _lists, value);
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand);

        private void ExecuteCreateListCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewListDialog);
        }

        private void Load()
        {
            var lists = _listFileService.GetAll().Select(m => new ListFileObserver(m));
            Lists = new ObservableCollection<ListFileObserver>(lists);
        }
    }
}