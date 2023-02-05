using System;
using System.Threading.Tasks;
using System.Windows;
using CoreTools.WPF.Prism;
using CoreTools.WPF.Prism.RegionBehaviors;
using ioList.Core.Logging;
using ioList.Core.Naming;
using ioList.Data;
using ioList.Dialogs;
using ioList.Import;
using ioList.Services;
using ioList.Startup;
using NLog;
using NLog.Config;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Squirrel;

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
            moduleCatalog.AddModule<StartupModule>();
            moduleCatalog.AddModule<DialogsModule>();
            moduleCatalog.AddModule<ImportModule>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<StartupView>();
        }

        protected override void Initialize()
        {
            ConfigureLogging();

            CheckForUpdates();
            
            base.Initialize();
        }

        private async Task CheckForUpdates()
        {
            using var manager = UpdateManager.GitHubUpdateManager("https://github.com/tnunnink/ioList");

            await manager.Result.UpdateApp();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
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