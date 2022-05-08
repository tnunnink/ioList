using System.Collections.ObjectModel;
using System.Linq;
using ioList.Common.Naming;
using ioList.Domain;
using ioList.Observers;
using ioList.Services;
using NLog;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IListFileService _listFileService;
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private DelegateCommand _createListCommand;
        private DelegateCommand _renameListCommand;
        private DelegateCommand _removeListCommand;
        private DelegateCommand _deleteListCommand;
        private ObservableCollection<ListFileObserver> _lists;
        private ListFileObserver _selectedList;
        private bool _addingList;


        public ListViewModel()
        {
            Lists = new ObservableCollection<ListFileObserver>
            {
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File"))
            };
        }

        public ListViewModel(IListFileService listFileService,
            IDialogService dialogService, IRegionManager regionManager)
        {
            _listFileService = listFileService;
            _dialogService = dialogService;
            _regionManager = regionManager;

            Lists = new ObservableCollection<ListFileObserver>();

            Load();
        }

        public ObservableCollection<ListFileObserver> Lists
        {
            get => _lists;
            private set => SetProperty(ref _lists, value);
        }

        public ListFileObserver SelectedList
        {
            get => _selectedList;
            set => SetProperty(ref _selectedList, value);
        }

        public bool AddingList
        {
            get => _addingList;
            set => SetProperty(ref _addingList, value);
        }

        public DelegateCommand NewListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand);

        public DelegateCommand RenameListCommand =>
            _renameListCommand ??= new DelegateCommand(ExecuteRenameListCommand, CanExecuteListCommand)
                .ObservesProperty(() => SelectedList);

        public DelegateCommand RemoveListCommand =>
            _removeListCommand ??= new DelegateCommand(ExecuteRemoveListCommand, CanExecuteRemoveListCommand)
                .ObservesProperty(() => SelectedList);
        public DelegateCommand DeleteListCommand =>
            _deleteListCommand ??= new DelegateCommand(ExecuteDeleteListCommand, CanExecuteListCommand)
                .ObservesProperty(() => SelectedList);

        private void ExecuteCreateListCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewListDialog, r =>
            {
                if (r.Result != ButtonResult.OK) return;
                
                var list = r.Parameters.GetValue<ListObserver>("List");
                
                var listFile = new ListFile(list.Entity.FullPath);
                _listFileService.Add(listFile);
                
                Lists.Add(new ListFileObserver(listFile));
            });
        }

        private void ExecuteRenameListCommand()
        {
            var parameters = new DialogParameters { { "List", SelectedList } };
            
            _dialogService.ShowDialog(DialogNames.RenameListDialog, parameters, r =>
            {
                if (r.Result == ButtonResult.OK)
                    Load();
            });
        }

        private void ExecuteRemoveListCommand()
        {
            var file = SelectedList.Entity;

            //todo confirmation...

            _listFileService.Remove(file);

            Load();
        }

        private void ExecuteDeleteListCommand()
        {
            //_dialogService.ShowConfigirmationDialog();
        }

        private bool CanExecuteRemoveListCommand() => SelectedList is not null;

        private bool CanExecuteListCommand() => SelectedList is not null && SelectedList.Exists;

        private void Load()
        {
            Lists.Clear();
            var lists = _listFileService.GetAll().Select(m => new ListFileObserver(m));
            Lists = new ObservableCollection<ListFileObserver>(lists);
        }

        private void OpenList(ListFile listFile)
        {
            var parameters = new NavigationParameters { { "ListFile", listFile } };

            if (!listFile.Exists)
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListInvalidView", parameters);
                return;
            }

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ContentView", parameters);
        }
    }
}