using System;
using System.IO;
using System.Xml.Linq;

namespace ioList.Model
{
    [Serializable]
    public class ListFile
    {
        public ListFile(XElement element)
        {
            if (element is null)
                throw new ArgumentNullException(nameof(element));

            ListName = element.Attribute(nameof(ListName))?.Value;
            ListName = element.Attribute(nameof(TotalTags))?.Value;
            ListName = element.Attribute(nameof(TotalValidated))?.Value;
            ListPath = element.Value;

        }
        
        public ListFile(string name, string fullPath, int totalTags = default, int totalValidated = default)
        {
            ListName = name;
            ListPath = fullPath;
            TotalTags = totalTags;
            TotalValidated = totalValidated;
            FileInfo = new FileInfo(fullPath);
            InitializeWatcher(FileInfo);
        }
        
        public string ListName { get; set; }
        
        public string ListPath { get; set; }

        public int TotalTags { get; set; }
        
        public int TotalValidated { get; set; }

        public FileInfo FileInfo { get; private set; }

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

        private void InitializeWatcher(FileInfo info)
        {
            if (info?.DirectoryName is null) return;

            var watcher = new FileSystemWatcher(info.DirectoryName, $"{info.Name}");
            watcher.Deleted += OnFileDeleted;
            watcher.Created += OnFileCreated;
            watcher.EnableRaisingEvents = true;
        }

        public XElement Serialize()
        {
            var element = new XElement(nameof(ListFile), ListPath);
            element.Add(new  XAttribute(nameof(ListName), ListName));
            element.Add(new  XAttribute(nameof(TotalTags), TotalTags));
            element.Add(new  XAttribute(nameof(TotalValidated), TotalValidated));
            return element;
        }
    }
}