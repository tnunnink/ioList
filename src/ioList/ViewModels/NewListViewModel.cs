using System;
using System.Threading;
using System.Threading.Tasks;
using CoreTools.WPF.Mvvm;
using EntityObserver;
using ioList.Domain;
using ioList.Observers;
using ioList.Services;
using NLog;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class NewListViewModel : DialogViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IListBuilder _listBuilder;
        private DelegateCommand _createListCommand;
        private readonly ListObserver _list;
        private bool _creating;

        public NewListViewModel(IListBuilder listBuilder)
        {
            _listBuilder = listBuilder;
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
            
            _listBuilder.BuildAsync(List.Entity, CancellationToken.None)
                .Await(ListCreationComplete, ListCreationError, true);
        }

        private bool CanExecuteCreateListCommand() =>
            !string.IsNullOrEmpty(List.Name) && !string.IsNullOrEmpty(List.Directory);

        private void ListCreationComplete()
        {
            Logger.Info($"IO list {List.Name} was created by {List.Entity.CreatedBy}");
            var parameters = new DialogParameters { { "List", List } };
            RaiseRequestClose(new DialogResult(ButtonResult.OK, parameters));
        }

        private void ListCreationError(Exception obj)
        {
            Logger.Error(obj, $"IO list creation failed with message {obj.Message}");
            RaiseRequestClose(new DialogResult(ButtonResult.None));
        }
    }
}