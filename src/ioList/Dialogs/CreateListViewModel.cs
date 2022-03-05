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
using Prism.Services.Dialogs;

namespace ioList.Dialogs
{
    public class CreateListViewModel : DialogViewModelBase, IDropTarget
    {
        private readonly IListFileService _listFileService;
        private readonly IEventAggregator _eventAggregator;
        private DelegateCommand _createListCommand;
        private DelegateCommand _browseFileCommand;
        private FileLoadResult _loadResult;
        private bool _isValidFile;
        private L5XContext _source;

        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        public CreateListViewModel(IListFileService listFileService, IEventAggregator eventAggregator)
        {
            Title = "ioList";

            _listFileService = listFileService;
            _eventAggregator = eventAggregator;

            List = new ListInfoObserver(new ListInfo());
        }

        public ListInfoObserver List { get; }

        public FileLoadResult LoadResult
        {
            get => _loadResult;
            set => SetProperty(ref _loadResult, value);
        }

        private string _loadResultMessage;

        public string LoadResultMessage
        {
            get => _loadResultMessage;
            set => SetProperty(ref _loadResultMessage, value);
        }

        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }

        public DelegateCommand CreateListCommand =>
            _createListCommand ??=
                new DelegateCommand(ExecuteCreateListCommand, CanExecuteCreateListCommand)
                    .ObservesProperty(() => LoadResult)
                    .ObservesProperty(() => List.Name)
                    .ObservesProperty(() => List.Directory);

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
            List.SourceFile = info.FullName;

            LoadSource(List.SourceFile);
        }

        private void ExecuteCreateListCommand()
        {
            var listFileName = Path.Combine(List.Directory, $"{List.Name}.xml");

            var list = new IOList(List.Name, listFileName, List.Comment, _source);
            
            _eventAggregator.GetEvent<ListCreatedEvent>().Publish(list.File);
            
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        private bool CanExecuteCreateListCommand()
        {
            return _source is not null &&
                   LoadResult == FileLoadResult.Success &&
                   !string.IsNullOrEmpty(List.Name) &&
                   !string.IsNullOrEmpty(List.Directory);
        }

        private void ExecuteBrowseFileCommand()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".L5X",
                Filter = "XML Files(.L5X, .xml)|*.L5X;*.xml",
                Multiselect = false
            };

            List.SourceFile = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;

            if (string.IsNullOrEmpty(List.SourceFile))
                return;

            LoadSource(List.SourceFile);
        }

        private void LoadSource(string fileName)
        {
            try
            {
                _source = new L5XContext(fileName);
                LoadResult = FileLoadResult.Success;
                LoadResultMessage = "Successfully loaded L5X file.";
            }
            catch (Exception e)
            {
                //todo log error
                LoadResult = FileLoadResult.Failure;
                LoadResultMessage = "Failed to load file. See log for details.";
            }
        }
    }
}