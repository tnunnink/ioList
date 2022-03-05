using System;
using System.IO;
using System.Xml.Linq;

namespace ioList.Model
{
    public class ListFile
    {
        public ListFile(string name, string path)
        {
            ListName = name;
            ListPath = path;
            ListInfo = new FileInfo(path);
            InitializeWatcher(ListInfo);
        }

        /// <summary>
        /// Gets the string name of the list.
        /// </summary>
        public string ListName { get; init; }

        /// <summary>
        /// Gets the full path to the <see cref="IOList"/> file.
        /// </summary>
        public string ListPath { get; set; }
        
        public string Directory => ListInfo.DirectoryName;

        public bool Exists => ListInfo.Exists;

        public string Extension => ListInfo.Extension;

        /// <summary>
        /// Gets the <see cref="ListInfo"/> data for the IO list.
        /// </summary>
        public FileInfo ListInfo { get; private set; }

        public int TotalTags { get; private set; }

        public int ValidatedTag { get; private set; }

        public event EventHandler<ListFile> FileChanged;
        public event EventHandler<ListFile> FileRenamed; 

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
            
            ListInfo = new FileInfo(fullPath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != ListPath) return;
            
            ListInfo = new FileInfo(fullPath);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != ListPath) return;
            
            ListInfo = new FileInfo(fullPath);
            RaiseFileChanged(this);
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            var fullPath = e.FullPath;
            ListInfo = new FileInfo(fullPath);
            RaiseFileRenamed(this);
        }

        private void InitializeWatcher(FileInfo info)
        {
            if (info?.DirectoryName is null) return;

            var watcher = new FileSystemWatcher(info.DirectoryName, $"{info.Name}");
            watcher.Deleted += OnFileDeleted;
            watcher.Created += OnFileCreated;
            watcher.Changed += OnFileChanged;
            watcher.Renamed += OnFileRenamed;
            watcher.EnableRaisingEvents = true;
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(ListFile), ListPath);
            element.Add(new  XAttribute(nameof(ListName), ListName));
            return element;
        }
        
        private void RaiseFileChanged(ListFile e)
        {
            FileChanged?.Invoke(this, e);   
        }

        private void RaiseFileRenamed(ListFile e)
        {
            FileRenamed?.Invoke(this, e);   
        }
    }
}