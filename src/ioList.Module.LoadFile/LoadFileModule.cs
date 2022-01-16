using ioList.Common;
using ioList.Module.LoadFile.Services;
using ioList.Module.LoadFile.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ioList.Module.LoadFile
{
    public class LoadFileModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRecentFileDataService, RecentFileDataService>();
            containerRegistry.Register<ILoadFileService, LoadFileService>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<RecentFilesView>(RegionNames.ListRegion);
            regionManager.RegisterViewWithRegion<SelectFileView>(RegionNames.ContentRegion);
        }
    }
}