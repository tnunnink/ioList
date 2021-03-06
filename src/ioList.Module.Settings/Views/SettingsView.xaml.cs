using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Module.Settings.Views
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