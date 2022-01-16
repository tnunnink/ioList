using Prism.Mvvm;
using Prism.Regions;

namespace ioList.Module.IoSelection.ViewModels
{
    public class IoGridViewModel : BindableBase, INavigationAware
    {
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}