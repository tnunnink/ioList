using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Startup
{
    public partial class StartupView
    {
        public StartupView(IRegionManager regionManager)
        {
            InitializeComponent();
            var scopedRegion = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(this, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(this, scopedRegion);
        }
    }
}