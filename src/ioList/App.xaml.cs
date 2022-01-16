using System.Windows;
using ioList.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<Module.LoadFile.LoadFileModule>();
            moduleCatalog.AddModule<Module.IoSelection.IoSelectionModule>();
        }
    }
}