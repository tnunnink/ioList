using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using ioList.Domain;
using ioList.Events;
using ioList.Module.LoadFile.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace ioList.Module.LoadFile.ViewModels
{
    public class SelectFileViewModel : BindableBase, IDropTarget
    {
        private readonly ILoadFileService _fileService;
        private readonly IRecentFileDataService _recentFileDataService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _browseFileCommand;
        private bool _isValidFile;
        
        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        public SelectFileViewModel(ILoadFileService fileService, 
            IRecentFileDataService recentFileDataService,
            IEventAggregator eventAggregator)
        {
            _fileService = fileService;
            _recentFileDataService = recentFileDataService;
            _eventAggregator = eventAggregator;
        }

        public DelegateCommand BrowseFileCommand =>
            _browseFileCommand ??= new DelegateCommand(ExecuteBrowseFileCommand);

        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;
            
            dropInfo.Effects = info is not null && info.Exists && ValidExtensions.Contains(info.Extension)
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            IsValidFile = dropInfo.Effects == DragDropEffects.Copy;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var files = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>().ToList();

            var info = files.Count == 1 ? new FileInfo(files[0]) : null;

            if (info is null || !info.Exists || !ValidExtensions.Contains(info.Extension))
                return;

            IsValidFile = false;

            LoadFile(info.FullName);
        }

        private void ExecuteBrowseFileCommand()
        {
            var fileName = _fileService.OpenFile();

            if (string.IsNullOrEmpty(fileName))
                return;

            LoadFile(fileName);
        }

        private void LoadFile(string fileName)
        {
            _recentFileDataService.Add(new RecentFile(fileName));
            _eventAggregator.GetEvent<LoadFileEvent>().Publish(fileName);
        }
    }
}