using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Pages
{
    public partial class StartupPage
    {
        public StartupPage(IRegionManager regionManager)
        {
            InitializeComponent();
            var scopedRegion = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(this, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(this, scopedRegion);
        }
    }
}