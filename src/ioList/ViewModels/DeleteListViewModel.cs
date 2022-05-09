using CoreTools.WPF.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    //todo replace with generic confirmation popup...will add module for list dialogs.
    public class DeleteListViewModel : DialogViewModelBase
    {
        private string _message;

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var name = parameters.GetValue<string>("ListName");
            Title = $"Delete List {name}";
            Message = $"Permanently delete IO list '{name}' from the system? This will delete the entire list file.";
        }

        public string Message
        {
            get => _message;
            private set => SetProperty(ref _message, value);
        }
    }
}