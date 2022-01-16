using ioList.Module.IoSelection.ViewModels;
using ioList.Module.IoSelection.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList.Module.IoSelection
{
    public class IoSelectionModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IoTreeView, IoTreeViewModel>(nameof(IoTreeView));
            containerRegistry.RegisterForNavigation<IoGridView, IoGridViewModel>(nameof(IoGridView));
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}