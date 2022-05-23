using System.Collections.ObjectModel;
using System.Linq;
using ioList.Commands;
using ioList.Common.Naming;
using ioList.Domain;
using ioList.Events;
using ioList.Module.Dialogs;
using ioList.Observers;
using ioList.Services;
using NLog;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListMenuViewModel : BindableBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IListFileService _listFileService;
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private readonly IApplicationCommands _applicationCommands;
        private DelegateCommand<ListFileObserver> _openListCommand;
        private DelegateCommand _renameListCommand;
        private DelegateCommand _removeListCommand;
        private DelegateCommand _deleteListCommand;
        private ObservableCollection<ListFileObserver> _lists;
        private ListFileObserver _selectedList;

        public ListMenuViewModel()
        {
            Lists = new ObservableCollection<ListFileObserver>
            {
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File"))
            };
        }

        public ListMenuViewModel(IListFileService listFileService, IApplicationCommands applicationCommands,
            IDialogService dialogService, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _listFileService = listFileService;
            _dialogService = dialogService;
            _regionManager = regionManager;

            ApplicationCommands = applicationCommands;
            Lists = new ObservableCollection<ListFileObserver>();

            eventAggregator.GetEvent<ListCreatedEvent>().Subscribe(OnListCreated);

            Load();
        }
        
        public IApplicationCommands ApplicationCommands
        {
            get => _applicationCommands;
            private init => SetProperty(ref _applicationCommands, value);
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

        public DelegateCommand<ListFileObserver> OpenListCommand =>
            _openListCommand ??= new DelegateCommand<ListFileObserver>(ExecuteOpenListCommand);

        public DelegateCommand RenameListCommand =>
            _renameListCommand ??= new DelegateCommand(ExecuteRenameListCommand, CanExecuteListCommand)
                .ObservesProperty(() => SelectedList);

        public DelegateCommand RemoveListCommand =>
            _removeListCommand ??= new DelegateCommand(ExecuteRemoveListCommand, CanExecuteRemoveListCommand)
                .ObservesProperty(() => SelectedList);

        public DelegateCommand DeleteListCommand =>
            _deleteListCommand ??= new DelegateCommand(ExecuteDeleteListCommand, CanExecuteListCommand)
                .ObservesProperty(() => SelectedList);

        private void ExecuteOpenListCommand(ListFileObserver listFile)
        {
            var parameters = new NavigationParameters { { "ListFile", listFile } };

            if (!listFile.Exists)
            {
                _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListInvalidView", parameters);
                return;
            }

            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ListView", parameters);
        }

        private void ExecuteRenameListCommand()
        {
            var parameters = new DialogParameters { { "List", SelectedList } };

            _dialogService.ShowDialog(DialogNames.RenameListDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK) return;
                Logger.Info("IO list renames to ");
                Load();
            });
        }

        private void ExecuteRemoveListCommand()
        {
            //todo should probably replace with CoreTool lib version....
            _dialogService.ShowConfirmation("Remove List",
                $"Removing this the list '{SelectedList.Name}' will remove the list from the application, but not from " +
                $"the file system. Continue removing IO list?",
                r =>
            {
                if (r.Result != ButtonResult.OK) return;
                _listFileService.Remove(SelectedList.Entity);
                Lists.Remove(SelectedList);
            });
        }

        private void ExecuteDeleteListCommand()
        {
            var parameters = new DialogParameters { { "ListName", SelectedList.Name } };
            
            _dialogService.ShowDialog(DialogNames.DeleteListDialog, parameters, r =>
            {
                if (r.Result != ButtonResult.OK) return;

                _listFileService.Remove(SelectedList.Entity);
                SelectedList.Entity.Delete();
                Lists.Remove(SelectedList);
            });
        }

        private bool CanExecuteRemoveListCommand() => SelectedList is not null;

        private bool CanExecuteListCommand() => SelectedList is not null && SelectedList.Exists;

        private void OnListCreated(ListFile listFile)
        {
            Lists.Add(new ListFileObserver(listFile));
        }

        private void Load()
        {
            Lists.Clear();
            var lists = _listFileService.GetAll().Select(m => new ListFileObserver(m));
            Lists = new ObservableCollection<ListFileObserver>(lists);
        }
    }
}