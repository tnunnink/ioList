using System;
using CoreTools.WPF.Mvvm;
using NLog;
using Prism.Commands;
using Prism.Events;

namespace ioList.Import.ViewModels
{
    public class ImportViewModel : NavigationViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private DelegateCommand _importCommand;

        public ImportViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public DelegateCommand ImportCommand =>
            _importCommand ??= new DelegateCommand(ExecuteImportCommand, CanExecuteImportCommand);

        private void ExecuteImportCommand()
        {
            //_listImporter.Import(List.Entity, FileName).Await(OnImportCompleted, OnImportError, true);
        }

        private bool CanExecuteImportCommand() => false;

        private void OnImportCompleted()
        {
            
        }

        private void OnImportError(Exception exception)
        {
            Logger.Error(exception, $"L5X Import Failed {exception.Message}");
            
        }
    }
}