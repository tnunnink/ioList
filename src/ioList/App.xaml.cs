using System.Windows;
using CoreWPF.Prism;
using CoreWPF.Prism.RegionBehaviors;
using ioList.Composites;
using ioList.Features.Startup;
using ioList.Shared;
using ioList.Shared.Logging;
using ioList.Shared.Services;
using NLog;
using NLog.Config;
using Prism.Ioc;
using Prism.Regions;

namespace ioList
{
    public partial class App
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IScopedShellCreator, ShellCreator>();

            containerRegistry.RegisterForNavigation<FooterView, FooterViewModel>();
            containerRegistry.RegisterForNavigation<StartupView, StartupViewModel>();
            containerRegistry.RegisterForNavigation<ProjectView, ProjectViewModel>();
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
            var creator = Container.Resolve<IScopedShellCreator>();
            return creator.Create();
        }

        protected override void InitializeShell(Window shell)
        {
            var regionManager = RegionManager.GetRegionManager(shell);
            regionManager.RequestNavigate(Regions.ContentRegion, nameof(StartupView));
            regionManager.RequestNavigate(Regions.FooterRegion, nameof(FooterView));
            base.InitializeShell(shell);
        } 

        protected override void Initialize()
        {
            ConfigureLogging();

            base.Initialize();
        }

        private static void ConfigureLogging()
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("MemoryEvent", typeof(MemoryEventTarget));

            var config = new LoggingConfiguration();
            
            var notificationTarget = new MemoryEventTarget(Loggers.NotificationLogger);
            config.AddTarget(notificationTarget);
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, notificationTarget));
            
            LogManager.ThrowExceptions = true;
            LogManager.Configuration = config;
        }
    }
}