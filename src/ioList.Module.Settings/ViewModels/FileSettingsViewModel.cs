using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using CoreTools.WPF.Mvvm;
using GongSolutions.Wpf.DragDrop;
using ioList.Observers;
using Prism.Regions;

namespace ioList.Module.Settings.ViewModels
{
    public class FileSettingsViewModel : NavigationViewModelBase, IDropTarget
    {
        private bool _isValidFile;

        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        private ListObserver _list;

        public ListObserver List
        {
            get => _list;
            private set => SetProperty(ref _list, value);
        }

        public bool IsValidFile
        {
            get => _isValidFile;
            set => SetProperty(ref _isValidFile, value);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            List = navigationContext.Parameters.GetValue<ListObserver>("Option");
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

            List.SourceFile = info.FullName;
        }
    }
}