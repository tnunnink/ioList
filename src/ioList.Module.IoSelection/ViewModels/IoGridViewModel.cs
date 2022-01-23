using System.Collections.ObjectModel;
using ioList.Domain;
using Prism.Mvvm;
using Prism.Regions;

namespace ioList.Module.IoSelection.ViewModels
{
    public class IoGridViewModel : BindableBase, INavigationAware
    {

        public ObservableCollection<Tag> Io { get; set; }
        
        
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => false;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}