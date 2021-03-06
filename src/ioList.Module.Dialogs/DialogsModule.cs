using ioList.Common.Naming;
using ioList.Module.Dialogs.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList.Module.Dialogs
{
    public class DialogsModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<ConfirmationDialogView>(DialogNames.ConfirmationDialog);
            containerRegistry.RegisterDialog<NewListView>(DialogNames.NewListDialog);
            containerRegistry.RegisterDialog<RenameListView>(DialogNames.RenameListDialog);
            containerRegistry.RegisterDialog<DeleteListView>(DialogNames.DeleteListDialog);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }
    }
}