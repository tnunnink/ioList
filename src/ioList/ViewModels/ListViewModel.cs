using System;
using System.Collections.ObjectModel;
using System.Linq;
using ioList.Common;
using ioList.Domain;
using ioList.Module.Settings.Events;
using ioList.Observers;
using ioList.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListViewModel : BindableBase
    {
        private readonly IListInfoService _listInfoService;
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private DelegateCommand _createListCommand;
        private DelegateCommand _deleteListCommand;
        private ObservableCollection<ListInfoObserver> _lists;
        private ListInfoObserver _selectedList;

        public ListViewModel()
        {
            Lists = new ObservableCollection<ListInfoObserver>
            {
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File")),
                new(new ListFile("Path To File"))
            };
        }

        public ListViewModel(IListInfoService listInfoService, IEventAggregator eventAggregator,
            IDialogService dialogService, IRegionManager regionManager)
        {
            _listInfoService = listInfoService;
            _dialogService = dialogService;
            _regionManager = regionManager;

            eventAggregator.GetEvent<ListCreatedEvent>().Subscribe(OnListCreated);

            Lists = new ObservableCollection<ListInfoObserver>();

            Load();
        }

        public string CurrentUser => Environment.UserName;

        public ObservableCollection<ListInfoObserver> Lists
        {
            get => _lists;
            private set => SetProperty(ref _lists, value);
        }

        public ListInfoObserver SelectedList
        {
            get => _selectedList;
            set
            {
                SetProperty(ref _selectedList, value);
                OpenList(_selectedList.Entity);
            }
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand);

        public DelegateCommand DeleteListCommand =>
            _deleteListCommand ??= new DelegateCommand(ExecuteDeleteListCommand, CanExecuteDeleteListCommand)
                .ObservesProperty(() => SelectedList);

        private void ExecuteDeleteListCommand()
        {
            var file = SelectedList.Entity;
            
            //todo confirmation...

            _listInfoService.Remove(file);
            
            Load();
        }

        private bool CanExecuteDeleteListCommand()
        {
            return SelectedList is not null;
        }

        private void ExecuteCreateListCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewListDialog);
        }

        private void Load()
        {
            Lists.Clear();
            var lists = _listInfoService.GetAll().Select(m => new ListInfoObserver(m));
            Lists = new ObservableCollection<ListInfoObserver>(lists);
        }

        private void OnListCreated(ListFile file)
        {
            _listInfoService.Add(file);
            Load();
            OpenList(file);
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