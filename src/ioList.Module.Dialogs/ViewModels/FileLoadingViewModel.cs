using System;
using ioList.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.Module.Dialogs.ViewModels
{
    public class FileLoadingViewModel : BindableBase, IDialogAware
    {
        private readonly IEventAggregator _eventAggregator;
        private string _fileName;
        private double _percentDone;
        
        /// <summary>
        /// For design time data
        /// </summary>
        public FileLoadingViewModel()
        {
            FileName = @"C:\users\Someone\documents\exports\L5X\MyController.L5X";
            PercentDone = 34.6;
        }

        public FileLoadingViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string Title => "Loading File";

        public string FileName
        {
            get => _fileName;
            private set => SetProperty(ref _fileName, value);
        }

        public double PercentDone
        {
            get => _percentDone;
            set
            {
                SetProperty(ref _percentDone, value);
                if (Math.Abs(PercentDone - 100) < double.Epsilon)
                    RaiseRequestClose(new DialogResult(ButtonResult.OK));
            }
        }

        private DelegateCommand _cancelCommand;

        public DelegateCommand CancelCommand =>
            _cancelCommand ??= new DelegateCommand(ExecuteCancelCommand);

        private void ExecuteCancelCommand()
        {
            RaiseRequestClose(new DialogResult(ButtonResult.Cancel));
        }

        public bool CanCloseDialog() => true;


        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            FileName = parameters.GetValue<string>("FileName");
            
            _eventAggregator.GetEvent<LoadProgressEvent>().Subscribe(OnProgressChanged);
        }

        private void OnProgressChanged(double percentDone)
        {
            PercentDone = percentDone;
        }

        public event Action<IDialogResult> RequestClose;

        private void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
    }
}