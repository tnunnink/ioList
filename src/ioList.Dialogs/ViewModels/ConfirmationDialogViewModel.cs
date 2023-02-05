using CoreTools.WPF.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.Dialogs.ViewModels
{
    public class ConfirmationDialogViewModel : DialogViewModelBase
    {
        private string _message;
        private string _caption;

        public string Message
        {
            get => _message;
            private set => SetProperty(ref _message, value);
        }

        public string Caption
        {
            get => _caption;
            private set => SetProperty(ref _caption, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Title = "Confirm Action";
            Message = parameters.GetValue<string>("Message");
            Caption = parameters.GetValue<string>("Caption");
        }
    }
}