using CoreTools.WPF.Mvvm;
using EntityObserver;
using ioList.Observers;
using ioList.Services;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class RenameListViewModel : DialogViewModelBase
    {
        private readonly IListFileService _fileService;
        private readonly IListService _listService;
        private ListFileObserver _list;
        private string _name;
        private DelegateCommand _renameCommand;

        public RenameListViewModel(IListFileService fileService, IListService listService)
        {
            _fileService = fileService;
            _listService = listService;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _list = parameters.GetValue<ListFileObserver>("List");
            Title = $"Rename List {_list.Name}";
            Name = _list.Name;
        }

        public DelegateCommand RenameCommand =>
            _renameCommand ??= new DelegateCommand(ExecuteRenameCommand, CanExecuteRenameCommand)
                .ObservesProperty(() => Name);

        private void ExecuteRenameCommand()
        {
            _list.Validate(ValidationOption.All);
            if (_list.HasErrors) return;

            _list.Name = Name;
            //rename list in db
            //update list file xml
            
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        private bool CanExecuteRenameCommand() => !string.IsNullOrEmpty(Name);
    }
}