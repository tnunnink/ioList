using System.Windows;
using CoreWPF.Prism;
using ioList.Shared.Services;
using Prism.Ioc;
using Prism.Regions;

namespace ioList
{
    public class ShellCreator : IScopedShellCreator
    {
        private readonly IContainerProvider _container;
        private readonly IRegionManager _regionManager;

        public ShellCreator(IContainerProvider container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public Window Create()
        {
            var shell = _container.Resolve<Shell>();
            
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            return shell;
        }

        public Window Create(string region, string uri)
        {
            var shell = _container.Resolve<Shell>();
            
            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            scopedRegion.RequestNavigate(region, uri);

            return shell;
        }
    }
}