using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using GongSolutions.Wpf.DragDrop;
using Prism.Mvvm;

namespace ioList.ViewModels
{
    public class LoadFileViewModel : BindableBase, IDropTarget
    {
        private static readonly List<string> ValidExtensions = new()
        {
            ".xml",
            ".L5X"
        };

        public void DragOver(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();

            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && ValidExtensions.Contains(extension);
            })
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        public void Drop(IDropInfo dropInfo)
        {
            var dragFileList = ((DataObject)dropInfo.Data).GetFileDropList().Cast<string>();

            dropInfo.Effects = dragFileList.Any(item =>
            {
                var extension = Path.GetExtension(item);
                return extension != null && ValidExtensions.Contains(extension);
            })
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }
    }
}