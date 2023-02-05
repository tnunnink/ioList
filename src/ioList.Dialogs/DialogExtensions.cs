using System;
using ioList.Core.Naming;
using Prism.Services.Dialogs;

namespace ioList.Dialogs
{
    public static class DialogExtensions
    {
        public static void ShowConfirmation(this IDialogService service, string message, string caption,
            Action<IDialogResult> callBack)
        {
            var parameters = new DialogParameters { { "Message", message }, { "Caption", caption } };
            service.ShowDialog(DialogNames.ConfirmationDialog, parameters, callBack);
        }
    }
}