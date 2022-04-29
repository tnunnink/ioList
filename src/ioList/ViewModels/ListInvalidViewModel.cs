using CoreTools.WPF.Mvvm;
using ioList.Domain;
using ioList.Observers;
using Prism.Regions;

namespace ioList.ViewModels
{
    public class ListInvalidViewModel : NavigationViewModelBase
    {
        private ListFileObserver _listFile;

        public ListFileObserver ListFile
        {
            get => _listFile;
            private set => SetProperty(ref _listFile, value);
        }

        public override bool KeepAlive => false;

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var file = navigationContext.Parameters.GetValue<ListFile>("ListFile");
            ListFile = new ListFileObserver(file);
        }
    }
}