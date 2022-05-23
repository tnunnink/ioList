using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CoreTools.WPF.Mvvm;
using GongSolutions.Wpf.DragDrop;
using ioList.Module.Import.Events;
using Prism.Commands;
using Prism.Events;

namespace ioList.Module.Import.ViewModels
{
    public class FileSelectionViewModel : NavigationViewModelBase, IDropTarget
    {
        private readonly IEventAggregator _eventAggregator;
        private string _fileName;
        private bool _isValidFile;
        private DelegateCommand _nextCommand;
        private DelegateCommand _cancelCommand;

        private readonly List<string> _validExtensions = new()
        {
            ".L5X",
            ".xml"
        };

        public FileSelectionViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }
        
        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }
        
        public DelegateCommand NextCommand =>
            _nextCommand ??= new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand)
                .ObservesProperty(() => FileName);

        public DelegateCommand CancelCommand =>
            _cancelCommand ??= new DelegateCommand(ExecuteCancelCommand);
        

        public void DragOver(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;

            dropInfo.Effects = info is not null && info.Exists && _validExtensions.Contains(info.Extension)
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            IsValidFile = dropInfo.Effects == DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;

            if (info is null || !info.Exists || !_validExtensions.Contains(info.Extension))
                return;

            FileName = info.FullName;
        }

        private void ExecuteNextCommand()
        {
            RegionManager?.RequestNavigate("PageRegion", "ModuleSelectionView");
        }
        
        private bool CanExecuteNextCommand()
        {
            return !string.IsNullOrEmpty(FileName);
        }
        
        private void ExecuteCancelCommand()
        {
            _eventAggregator.GetEvent<ImportCancelEvent>().Publish();
        }
    }
}