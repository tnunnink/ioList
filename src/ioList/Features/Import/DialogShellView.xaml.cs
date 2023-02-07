using CoreTools.WPF.Prism;
using Prism.Regions;

namespace ioList.Features.Import
{
    public partial class DialogShellView
    {
        public DialogShellView(IRegionManager regionManager)
        {
            InitializeComponent();
            var scopedRegion = regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(this, scopedRegion);
            RegionManagerAware.SetRegionManagerAware(this, scopedRegion);
        }
    }
}