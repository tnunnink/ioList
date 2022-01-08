using ioList.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace ioList.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion<LoadFileView>("ContentRegion");
        }
    }
}