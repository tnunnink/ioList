using System.Windows;
using CoreTools.WPF.Prism;
using CoreTools.WPF.Prism.RegionBehaviors;
using ioList.Common.Logging;
using ioList.Common.Naming;
using ioList.Data;
using ioList.Services;
using ioList.ViewModels;
using ioList.Views;
using NLog;
using NLog.Config;
using Prism.Ioc;
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
            containerRegistry.Register<IListService, ListService>();

            containerRegistry.RegisterForNavigation<ContentView, ContentViewModel>();
            containerRegistry.RegisterForNavigation<ListInvalidView, ListInvalidViewModel>();
            
            containerRegistry.RegisterDialog<NewListView>(DialogNames.NewListDialog);
            containerRegistry.RegisterDialog<RenameListView>(DialogNames.RenameListDialog);
            containerRegistry.RegisterDialog<DeleteListView>(DialogNames.DeleteListDialog);
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

        protected override void Initialize()
        {
            ConfigureLogging();
            base.Initialize();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var regionManager = Container.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion<ListView>(RegionNames.ListRegion);
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