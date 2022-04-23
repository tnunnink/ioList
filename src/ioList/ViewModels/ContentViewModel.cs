using CoreTools.WPF.Mvvm;
using ioList.Domain;
using ioList.Observers;
using Prism.Regions;

namespace ioList.ViewModels
{
    public class ContentViewModel : NavigationViewModelBase
    {
        
        public ContentViewModel()
        {
        }
        

        public override bool KeepAlive => false;

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}