using System.Windows;
using CoreTools.WPF.Prism;
using CoreTools.WPF.Prism.RegionBehaviors;
using ioList.Commands;
using ioList.Common.Logging;
using ioList.Common.Naming;
using ioList.Data;
using ioList.Module.Dialogs;
using ioList.Module.Import;
using ioList.Services;
using ioList.ViewModels;
using ioList.Views;
using NLog;
using NLog.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IListFileService, ListFileService>();
            containerRegistry.Register<IListBuilder, ListBuilder>();
            containerRegistry.Register<IListProvider, ListProvider>();
            containerRegistry.Register<IListRepository, ListRepository>();

            containerRegistry.RegisterSingleton<IApplicationCommands, ApplicationCommands>();

            containerRegistry.RegisterForNavigation<ListView, ListViewModel>();
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

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<DialogsModule>();
            moduleCatalog.AddModule<ImportModule>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void Initialize()
        {
            ConfigureLogging();
            base.Initialize();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<ListMenuView>(RegionNames.ListRegion);
            regionManager.RegisterViewWithRegion<ListBarView>(RegionNames.ListBarRegion);
            regionManager.RegisterViewWithRegion<FooterView>(RegionNames.FooterRegion);
            regionManager.RegisterViewWithRegion<DefaultView>(RegionNames.ContentRegion);
        }

        private static void ConfigureLogging()
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("MemoryEvent", typeof(MemoryEventTarget));

            var config = new LoggingConfiguration();
            
            var notificationTarget = new MemoryEventTarget(LoggerNames.NotificationLogger);
            config.AddTarget(notificationTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, notificationTarget));
            
            LogManager.ThrowExceptions = true;
            LogManager.Configuration = config;
        }
    }
}