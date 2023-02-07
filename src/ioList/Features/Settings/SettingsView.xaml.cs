using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Features.Settings
{
    public partial class SettingsView
    {
        public SettingsView(IRegionManager regionManager)
        {
            InitializeComponent();
            var scopedRegion = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(this, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(this, scopedRegion);
        }
    }
}