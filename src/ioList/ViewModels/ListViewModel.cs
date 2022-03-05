using System;
using System.Collections.ObjectModel;
using System.Linq;
using ioList.Common;
using ioList.Events;
using ioList.Model;
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
        private readonly IListFileService _listFileService;
        private readonly IDialogService _dialogService;
        private readonly IRegionManager _regionManager;
        private DelegateCommand _createListCommand;
        private DelegateCommand _openListCommand;
        private ObservableCollection<ListFileObserver> _lists;
        private ListFileObserver _selectedList;

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
            IDialogService dialogService, IRegionManager regionManager)
        {
            _listFileService = listFileService;
            _dialogService = dialogService;
            _regionManager = regionManager;

            eventAggregator.GetEvent<ListCreatedEvent>().Subscribe(OnListCreated);

            Lists = new ObservableCollection<ListFileObserver>();
            
            Load();
        }

        public string CurrentUser => Environment.UserName;

        public ObservableCollection<ListFileObserver> Lists
        {
            get => _lists;
            private set => SetProperty(ref _lists, value);
        }

        public ListFileObserver SelectedList
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

        private void ExecuteCreateListCommand()
        {
            _dialogService.ShowDialog(DialogNames.NewListDialog);
        }

        private void Load()
        {
            Lists.Clear();
            var lists = _listFileService.GetAll().Select(m => new ListFileObserver(m));
            Lists = new ObservableCollection<ListFileObserver>(lists);
        }
        
        private void OnListCreated(ListFile file)
        {
            _listFileService.Add(file);
            Load();
            OpenList(file);
        }

        private void OpenList(ListFile listFile)
        {
            var parameters = new NavigationParameters { { "ListFile", listFile } };
            _regionManager.RequestNavigate(RegionNames.ContentRegion, "ContentView", parameters);
        }
    }
}