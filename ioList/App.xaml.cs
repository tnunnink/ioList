using System.Windows;
using ioList.Views;
using Prism.Ioc;

namespace ioList
{
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }
    }
}