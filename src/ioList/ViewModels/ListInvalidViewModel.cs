using CoreTools.WPF.Mvvm;
using ioList.Domain;
using ioList.Observers;
using Prism.Regions;

namespace ioList.ViewModels
{
    public class ListInvalidViewModel : NavigationViewModelBase
    {
        private ListInfoObserver _listInfo;

        public ListInfoObserver ListInfo
        {
            get => _listInfo;
            private set => SetProperty(ref _listInfo, value);
        }

        public override bool KeepAlive => false;

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var file = navigationContext.Parameters.GetValue<ListFile>("ListFile");
            ListInfo = new ListInfoObserver(file);
        }
    }
}