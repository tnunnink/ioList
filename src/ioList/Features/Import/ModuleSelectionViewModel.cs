using CoreTools.WPF.Mvvm;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace ioList.Features.Import
{
    public class ModuleSelectionViewModel : NavigationViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _nextCommand;
        private DelegateCommand _cancelCommand;

        public ModuleSelectionViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        
        public DelegateCommand NextCommand =>
            _nextCommand ??= new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand);

        public DelegateCommand CancelCommand =>
            _cancelCommand ??= new DelegateCommand(ExecuteCancelCommand);
        
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        private void ExecuteNextCommand()
        {
            RegionManager?.RequestNavigate("PageRegion", "TagSelectionView");
        }
        
        private bool CanExecuteNextCommand()
        {
            return true;
        }
        
        private void ExecuteCancelCommand()
        {
            _eventAggregator.GetEvent<ImportCancelEvent>().Publish();
        }
    }
}