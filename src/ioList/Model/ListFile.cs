using System;
using System.IO;
using System.Xml.Linq;

namespace ioList.Model
{
    [Serializable]
    public class ListFile
    {
        public ListFile(string name, string path)
        {
            ListName = name;
            ListPath = path;
            FileInfo = new FileInfo(path);
            InitializeWatcher(FileInfo);
        }

        /// <summary>
        /// Gets the string name of the list.
        /// </summary>
        public string ListName { get; set; }

        /// <summary>
        /// Gets the full path to the <see cref="IOList"/> file.
        /// </summary>
        public string ListPath { get; set; }

        /// <summary>
        /// Gets the <see cref="FileInfo"/> data for the IO list.
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        /// <summary>
        /// Gets a value indicating that changes have been made to the underlying IO list file.
        /// </summary>
        public bool HasUpdates { get; private set; }

        public int TotalTags { get; private set; }

        public int ValidatedTag { get; private set; }

        public static ListFile Materialize(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            var name = element.Attribute(nameof(ListName))?.Value;
            var path = element.Value;

            return new ListFile(name, path);
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != ListPath) return;
            
            FileInfo = new FileInfo(fullPath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != ListPath) return;
            
            FileInfo = new FileInfo(fullPath);
        }
        
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != ListPath) return;
            
            FileInfo = new FileInfo(fullPath);
            HasUpdates = true;
        }

        private void InitializeWatcher(FileInfo info)
        {
            if (info?.DirectoryName is null) return;

            var watcher = new FileSystemWatcher(info.DirectoryName, $"{info.Name}");
            watcher.Deleted += OnFileDeleted;
            watcher.Created += OnFileCreated;
            watcher.Changed += OnFileChanged;
            watcher.EnableRaisingEvents = true;
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(ListFile), ListPath);
            element.Add(new  XAttribute(nameof(ListName), ListName));
            return element;
        }
    }
}