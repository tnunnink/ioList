using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CoreTools.WPF.Mvvm;
using ioList.Common.Naming;
using ioList.Domain;
using ioList.Observers;
using ioList.Services;
using NLog;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ListViewModel : NavigationViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IListProvider _listProvider;
        private readonly IDialogService _dialogService;
        private ListFile _listFile;
        private ListObserver _list;
        private ObservableCollection<Tag> _tags;
        private bool _hasPoints;

        public ListViewModel()
        {
            List = new ListObserver(new List
            {
                Name = "My Test List",
                Comment = "This is a test list for development purposes",
                Directory = @"C:\users\files\TestList.db"
            });
        }

        public ListViewModel(IListProvider listProvider, IDialogService dialogService)
        {
            _listProvider = listProvider;
            _dialogService = dialogService;
        }

        public override bool KeepAlive => false;

        public ListObserver List
        {
            get => _list;
            private set => SetProperty(ref _list, value);
        }

        public ObservableCollection<Tag> Tags
        {
            get => _tags;
            set => SetProperty(ref _tags, value);
        }

        public bool HasPoints
        {
            get => _hasPoints;
            set => SetProperty(ref _hasPoints, value);
        }

        private DelegateCommand _importCommand;

        public DelegateCommand ImportCommand =>
            _importCommand ??= new DelegateCommand(ExecuteImportCommand);

        private void ExecuteImportCommand()
        {
            var parameters = new DialogParameters { { "List", List } };
            
            _dialogService.ShowDialog(DialogNames.ImportDialog, parameters, r =>
            {
                if (r.Result == ButtonResult.OK)
                {
                    //LoadTags().Await(OnLoadPointsError);
                }
            });
        }

        private static void OnLoadPointsError(Exception exception)
        {
            Logger.Error(exception);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _listFile = navigationContext.Parameters.GetValue<ListFileObserver>("ListFile").Entity;

            LoadAsync().Await();
        }

        protected override async Task LoadAsync()
        {
            using var service = _listProvider.Connect(_listFile.FullName);

            var list = await service.GetList();
            List = new ListObserver(list);
        }

        private async Task LoadTags()
        {
            using var service = _listProvider.Connect(_listFile.FullName);

            var tags = await service.GetTags();

            Tags = new ObservableCollection<Tag>(tags);
        }
    }
}