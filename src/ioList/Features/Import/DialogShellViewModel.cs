using CoreTools.WPF.Mvvm;
using NLog;
using Prism.Events;
using Prism.Services.Dialogs;

namespace ioList.Features.Import
{
    public class DialogShellViewModel : DialogViewModelBase
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        
        public DialogShellViewModel(IEventAggregator eventAggregator)
        {
            Title = "Import";
            eventAggregator.GetEvent<ImportCompleteEvent>().Subscribe(OnImportCompleteEvent);
            eventAggregator.GetEvent<ImportCancelEvent>().Subscribe(OnImportCancelEvent);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            Logger.Trace("Calling navigation requrest to the resrouce selection view");
            RegionManager?.RequestNavigate("PageRegion", "FileSelectionView");
        }

        private void OnImportCompleteEvent()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        private void OnImportCancelEvent()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }
    }
}