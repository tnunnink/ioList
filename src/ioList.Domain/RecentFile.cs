using System;
using System.IO;

namespace ioList.Domain
{
    public class RecentFile
    {
        public RecentFile(string fullName)
        {
            var info = new FileInfo(fullName);

            UpdateProperties(info);

            if (info.DirectoryName is null) return;
            
            var watcher = new FileSystemWatcher(info.DirectoryName, $"{info.Name}");
            watcher.Deleted += OnFileDeleted;
            watcher.Created += OnFileCreated;
            watcher.EnableRaisingEvents = true;
        }

        public string FullPath { get; private set; }
        public string Name { get; private set; }
        public bool Exists { get; private set; }
        public string Directory { get; private set; }
        public DateTime LastWriteTime { get; private set; }
        

        private void UpdateProperties(FileInfo info)
        {
            FullPath = info.FullName;
            Name = info.Name;
            Exists = info.Exists;
            Directory = info.DirectoryName;
            LastWriteTime = info.LastWriteTime;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullPath) return;
            
            var info = new FileInfo(fullPath);
            UpdateProperties(info);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullPath) return;
            
            var info = new FileInfo(fullPath);
            UpdateProperties(info);
        }
    }
}