using System;
using System.Threading;
using System.Threading.Tasks;
using CoreTools.WPF.Mvvm;
using EntityObserver;
using ioList.Observers;
using ioList.Services;
using ioList.Entities;
using NLog;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace ioList.Dialogs.ViewModels
{
    public class NewListViewModel : DialogViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IListBuilder _listBuilder;
        private readonly IListFileService _fileService;
        private DelegateCommand _createListCommand;
        private readonly ListObserver _list;
        private bool _creating;

        public NewListViewModel(IListBuilder listBuilder, IListFileService fileService)
        {
            _listBuilder = listBuilder;
            _fileService = fileService;
            List = new ListObserver(new List());
            Title = "New List";
        }

        public ListObserver List
        {
            get => _list;
            private init => SetProperty(ref _list, value);
        }

        public bool Creating
        {
            get => _creating;
            set => SetProperty(ref _creating, value);
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand, CanExecuteCreateListCommand)
                .ObservesProperty(() => List.IsChanged);

        private void ExecuteCreateListCommand()
        {
            List.Validate(ValidationOption.All);

            if (List.HasErrors) return;

            Creating = true;

            _listBuilder.Build(List.Entity, CancellationToken.None)
                .Await(ListCreationComplete, ListCreationError, true);
        }

        private bool CanExecuteCreateListCommand() =>
            !string.IsNullOrEmpty(List.Name) && !string.IsNullOrEmpty(List.Directory);

        private void ListCreationComplete()
        {
            Logger.Info($"IO list {List.Name} was created by {List.Entity.CreatedBy}");
            
            Logger.Debug($"Adding list {List.Name} to user list file.");
            var file = new ListFile(List.Entity.FullPath);
            _fileService.Add(file);
            
            Logger.Debug($"Returning from creation for list {List.Name}.");
            var parameters = new DialogParameters { { "ListFile", file } };
            RaiseRequestClose(new DialogResult(ButtonResult.OK, parameters));
        }

        private void ListCreationError(Exception obj)
        {
            Logger.Error(obj, $"IO list creation failed with message {obj.Message}");
            RaiseRequestClose(new DialogResult(ButtonResult.None));
        }
    }
}