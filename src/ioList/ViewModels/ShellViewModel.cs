using System.ComponentModel;
using ioList.Common;
using ioList.Events;
using ioList.Module.IoSelection.Views;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        public ShellViewModel()
        {
        }
        
        public ShellViewModel(IEventAggregator eventAggregator, IRegionManager regionManager,
            IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;

            eventAggregator.GetEvent<LoadFileEvent>().Subscribe(OnLoadFileEvent);
        }

        private void OnLoadFileEvent(string fileName)
        {
            var parameters = new NavigationParameters
            {
                { "FileName", fileName }
            };
            
            _regionManager.RequestNavigate(RegionNames.ListRegion, nameof(IoTreeView), parameters);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, nameof(IoGridView), parameters);
        }
    }
}