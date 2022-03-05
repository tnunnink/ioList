using System.Windows;
using ioList.Common;
using ioList.Dialogs;
using ioList.Services;
using ioList.ViewModels;
using ioList.Views;
using Prism.Ioc;
using Prism.Regions;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IListFileService, ListFileService>();

            containerRegistry.RegisterForNavigation<ContentView, ContentViewModel>();
            containerRegistry.RegisterDialog<CreateListView, CreateListViewModel>(DialogNames.NewListDialog);
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
    }
}