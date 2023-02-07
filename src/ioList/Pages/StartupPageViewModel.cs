using CoreTools.WPF.Mvvm;
using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Pages
{
    public class StartupPageViewModel : ViewModelBase, IRegionManagerAware
    {
        public StartupPageViewModel()
        {
            Title = "Project Launcher";
        }

        public IRegionManager RegionManager { get; set; }
    }
}