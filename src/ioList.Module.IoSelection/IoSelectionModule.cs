using ioList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ioList.Module.IoSelection
{
    public class IoSelectionModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(IoView));
        }
    }
}