using CoreTools.WPF.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.Module.Dialogs.ViewModels
{
    public class DeleteListViewModel : DialogViewModelBase
    {
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var name = parameters.GetValue<string>("ListName");
            Title = $"Delete List {name}";
        }
    }
}