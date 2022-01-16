using System;
using System.IO;
using Prism.Mvvm;

namespace ioList.Module.LoadFile.Model
{
    public class RecentFile : BindableBase
    {
        private string _fullPath;
        private string _name;
        private bool _exists;
        private string _directory;
        private DateTime _lastWriteTime;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable Need to keep alive for event.
        private readonly FileSystemWatcher _watcher;


        /// <summary>
        /// Design time constructor
        /// </summary>
        public RecentFile()
        {
            var info = new FileInfo(@"C:\Users\tnunnink\Local\Projects\logix-ioList\Samples\Test.L5X");
            
            UpdateProperties(info);
        }

        public RecentFile(string fullName)
        {
            var info = new FileInfo(fullName);

            UpdateProperties(info);

            if (info.DirectoryName is null) return;
            _watcher = new FileSystemWatcher(info.DirectoryName, $"{info.Name}");
            _watcher.Deleted += OnFileDeleted;
            _watcher.Created += OnFileCreated;
            _watcher.EnableRaisingEvents = true;
        }

        public string FullPath
        {
            get => _fullPath;
            private set => SetProperty(ref _fullPath, value);
        }

        public string Name
        {
            get => _name;
            private set => SetProperty(ref _name, value);
        }

        public bool Exists
        {
            get => _exists;
            private set => SetProperty(ref _exists, value);
        }

        public string Directory
        {
            get => _directory;
            private set => SetProperty(ref _directory, value);
        }

        public DateTime LastWriteTime
        {
            get => _lastWriteTime;
            private set => SetProperty(ref _lastWriteTime, value);
        }

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