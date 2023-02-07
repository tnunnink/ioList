using System.Windows;
using CoreTools.WPF.Prism;
using CoreTools.WPF.Prism.RegionBehaviors;
using ioList.Core.Logging;
using ioList.Core.Naming;
using ioList.Pages;
using NLog;
using NLog.Config;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<StartupPage, StartupPageViewModel>();
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
            return Container.Resolve<Shell>();
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