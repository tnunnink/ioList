using System.Windows;
using CoreTools.WPF.Prism;
using CoreTools.WPF.Prism.RegionBehaviors;
using ioList.Common;
using ioList.Data;
using ioList.Module.Settings;
using ioList.Services;
using ioList.ViewModels;
using ioList.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IListInfoService, ListInfoService>();

            containerRegistry.RegisterForNavigation<ContentView, ContentViewModel>();
            containerRegistry.RegisterForNavigation<ListInvalidView, ListInvalidViewModel>();
        }
        
        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
            containerRegistry.RegisterSingleton<IRegionNavigationContentLoader, ScopedRegionNavigationContentLoader>();
        }
        
        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            regionBehaviors.AddIfMissing(RegionManagerAwareBehavior.BehaviorKey, typeof(RegionManagerAwareBehavior));
            regionBehaviors.AddIfMissing(DependentViewRegionBehavior.BehaviorKey, typeof(DependentViewRegionBehavior));
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        }


        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<ListView>(RegionNames.ListRegion);
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<SettingsModule>();
        }
    }
}