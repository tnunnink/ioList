using System;
using System.Threading.Tasks;
using Prism.Ioc;
using Prism.Services.Dialogs;

namespace ioList.Extensions
{
    public static class DialogServiceExtensions
    {
        /// <summary>
        /// Displays a dialog asynchronously.
        /// </summary>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="name">The unique name of the dialog to display. Must match an entry in the <see cref="IContainerRegistry"/>.</param>
        /// <param name="parameters">Parameters that the dialog can use for custom functionality.</param>
        /// <returns><see cref="IDialogResult"/> indicating whether the request was successful or if there was an encountered <see cref="Exception"/>.</returns>
        public static Task<IDialogResult> ShowDialogAsync(this IDialogService dialogService, string name, IDialogParameters parameters = null)
        {
            var tcs = new TaskCompletionSource<IDialogResult>();
            
            void DialogCallback(IDialogResult dialogResult)
            {
                tcs.SetResult(dialogResult);
            }
            
            dialogService.ShowDialog(name, parameters, DialogCallback);
            
            return tcs.Task;
        }
    }
}