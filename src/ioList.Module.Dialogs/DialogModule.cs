using ioList.Common;
using ioList.Module.Dialogs.ViewModels;
using ioList.Module.Dialogs.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList.Module.Dialogs
{
    public class DialogModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterSingleton<IDialogCommands, DialogCommands>();
        
            containerRegistry.RegisterDialog<FileLoadingView, FileLoadingViewModel>(DialogNames.FileLoadingDialog);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}