using CoreTools.WPF.Mvvm;
using ioList.Common.Logging;
using ioList.Common.Naming;
using NLog;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class FooterViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly MemoryEventTarget _logTarget;
        private string _logMessage;
        private DelegateCommand _openEvenLog;

        public FooterViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
            _logTarget = LogManager.Configuration.FindTargetByName<MemoryEventTarget>(LoggerNames.NotificationLogger);
            _logTarget.EventReceived += LogEventReceived;
        }

        public string LogMessage
        {
            get => _logMessage;
            private set => SetProperty(ref _logMessage, value);
        }
        
        public DelegateCommand OpenEvenLog =>
            _openEvenLog ??= new DelegateCommand(ExecuteOpenEvenLog);

        private void ExecuteOpenEvenLog()
        {
            //_dialogService.ShowDialog();
        }

        public override void Destroy()
        {
            _logTarget.EventReceived -= LogEventReceived;
        }

        private void LogEventReceived(LogEventInfo logInfo)
        {
            LogMessage = $"{logInfo.FormattedMessage} at {logInfo.TimeStamp.ToLongTimeString()}";
        }
    }
}