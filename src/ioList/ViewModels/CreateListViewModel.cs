using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using GongSolutions.Wpf.DragDrop;
using ioList.Abstractions;
using ioList.Events;
using ioList.Model;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace ioList.ViewModels
{
    public class CreateListViewModel : BindableBase, IDialogAware, IDropTarget
    {
        private readonly ILoadFileService _fileService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _browseFileCommand;
        private string _listName;
        private string _listDescription;
        private bool _fileLoaded;
        private bool _isValidFile;


        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        public CreateListViewModel(ILoadFileService fileService,
            IEventAggregator eventAggregator)
        {
            _fileService = fileService;
            _eventAggregator = eventAggregator;
        }

        public DelegateCommand BrowseFileCommand =>
            _browseFileCommand ??= new DelegateCommand(ExecuteBrowseFileCommand);

        public string ListName
        {
            get => _listName;
            set => SetProperty(ref _listName, value);
        }
        
        public string ListDescription
        {
            get => _listDescription;
            set => SetProperty(ref _listDescription, value);
        }

        public bool FileLoaded
        {
            get => _fileLoaded;
            set => SetProperty(ref _fileLoaded, value);
        }

        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }

        public string Title => "Create List";

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

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }

        private void LoadFile(string fileName)
        {
            //validate?...

            var list = new IoList(ListName, fileName);
            
            _eventAggregator.GetEvent<ListCreatedEvent>().Publish(ListName);
        }

        public event Action<IDialogResult> RequestClose;
    }
}