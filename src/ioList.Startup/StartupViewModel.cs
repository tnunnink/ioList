using CoreTools.WPF.Mvvm;
using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Startup
{
    public class StartupViewModel : ViewModelBase, IRegionManagerAware
    {
        public StartupViewModel()
        {
            Title = "Project Launcher";
        }

        public IRegionManager RegionManager { get; set; }
    }
}