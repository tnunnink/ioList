using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;

namespace ioList.Startup
{
    public class StartupModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            ViewModelLocationProvider.Register<StartupView, StartupViewModel>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }
    }
}