using System.Windows;
using CoreWPF.Prism;
using Prism.Ioc;
using Prism.Regions;

namespace ioList.Shared.Extensions
{
    public static class RegionManagerExtensions
    {
        /*public static Window CreateScopedShell<TShell>(this RegionManager regionManager) where TShell : Window, new()
        {
            var shell = new TShell();

            var manager = regionManager.CreateRegionManager();

            var scopedRegion = manager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            return shell;
        }

        public static Window ResolveScopedShell<TShell>(this RegionManager manager, IContainerProvider container)
            where TShell : Window
        {
            var shell = container.Resolve<TShell>();

            var regionManager = manager.CreateRegionManager();

            var scopedRegion = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            return shell;
        }*/
    }
}