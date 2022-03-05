using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CoreTools.WPF.Mvvm;
using GongSolutions.Wpf.DragDrop;
using ioList.Events;
using ioList.Model;
using ioList.Observers;
using ioList.Services;
using L5Sharp;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;

namespace ioList.ViewModels
{
    public class NewListViewModel : DialogViewModelBase, IDropTarget
    {
        private readonly IListFileService _listFileService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _createListCommand;
        private DelegateCommand _browseFileCommand;
        private bool _loadResult;
        private bool _isValidFile;
        private L5XContext _context;

        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        public NewListViewModel(IListFileService listFileService, IEventAggregator eventAggregator)
        {
            Title = "ioList";
            
            _listFileService = listFileService;
            _eventAggregator = eventAggregator;

            List = new ListInfoObserver(new ListInfo());
        }

        public ListInfoObserver List { get; }

        public bool LoadResult
        {
            get => _loadResult;
            set => SetProperty(ref _loadResult, value);
        }

        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??= new DelegateCommand(ExecuteCreateListCommand, CanExecuteCreateListCommand);

        public DelegateCommand BrowseFileCommand =>
            _browseFileCommand ??= new DelegateCommand(ExecuteBrowseFileCommand);
        

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

        private void ExecuteCreateListCommand()
        {
            var listFileName = Path.Combine(List.Directory, $"{List.Name}.xml");

            var list = new IOList(List.Name, listFileName, List.Comment, _context);

            list.Save();
            
            _listFileService.Add(list.File);
            
            _eventAggregator.GetEvent<ListCreatedEvent>().Publish(List.Name);
        }

        private bool CanExecuteCreateListCommand()
        {
            return IsValidFile && !string.IsNullOrEmpty(List.Name);
        }

        private void ExecuteBrowseFileCommand()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".L5X",
                Filter = "XML Files(.L5X, .xml)|*.L5X;*.xml",
                Multiselect = false
            };

            var fileName = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;

            if (string.IsNullOrEmpty(fileName))
                return;

            LoadFile(fileName);
        }

        private void LoadFile(string fileName)
        {
            try
            {
                _context = new L5XContext(fileName);
                LoadResult = true;
            }
            catch (Exception e)
            {
                //todo log error
                LoadResult = false;
            }
        }
    }
}