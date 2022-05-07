using CoreTools.WPF.Mvvm;
using ioList.Domain;
using ioList.Services;
using Prism.Commands;

namespace ioList.ViewModels
{
    public class NewListViewModel : DialogViewModelBase
    {
        private readonly IListBuilder _listBuilder;
        private string _listName;
        private string _listDirectory;
        private string _comment;
        private DelegateCommand _createListCommand;

        public NewListViewModel()
        {
        }

        public NewListViewModel(IListBuilder listBuilder)
        {
            _listBuilder = listBuilder;
            Title = "New List";
        }


        public string ListName
        {
            get => _listName;
            set => SetProperty(ref _listName, value);
        }

        public string ListDirectory
        {
            get => _listDirectory;
            set => SetProperty(ref _listDirectory, value);
        }
        
        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand, CanExecuteCreateListCommand)
                .ObservesProperty(() => ListName)
                .ObservesProperty(() => ListDirectory);

        private void ExecuteCreateListCommand()
        {
            var list = new List(ListName, ListDirectory, Comment);
            _listBuilder.Build(list);
        }

        private bool CanExecuteCreateListCommand() => IsValidName(ListName) && IsValidDirectory(ListDirectory);

        private static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name);
        }

        private static bool IsValidDirectory(string directory)
        {
            return !string.IsNullOrEmpty(directory);
        }
    }
}