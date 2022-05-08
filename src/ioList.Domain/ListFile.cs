using System;
using System.IO;

namespace ioList.Domain
{
    public class ListFile
    {
        private FileInfo _info;

        public ListFile(string fileName)
        {
            _info = new FileInfo(fileName);
            InitializeWatcher(_info);
        }

        public string Name => Path.GetFileNameWithoutExtension(_info.Name);
        public string DirectoryName => _info.DirectoryName;
        public string FullName => _info.FullName;
        public string Extension => _info.Extension;
        public bool Exists => _info.Exists;
        public DateTime CreationTime => _info.CreationTime;
        public DateTime LastWriteTime => _info.LastWriteTime;
        public DateTime LastAccessTime => _info.LastAccessTime;
        public event EventHandler<ListFile> FileChanged;
        public event EventHandler<ListFile> FileRenamed;

        public void Rename(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name can not be null or empty.");
            
            var newName = Path.Combine(DirectoryName, name);
            
            File.Move(FullName, newName);
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullName) return;
            
            _info = new FileInfo(fullPath);
        }

        private void OnFileDeleted(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullName) return;
            
            _info = new FileInfo(fullPath);
        }

        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullName) return;
            
            _info = new FileInfo(fullPath);
            
            RaiseFileChanged(this);
        }

        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            var fullPath = e.FullPath;
            
            if (fullPath != FullName) return;
            
            _info = new FileInfo(fullPath);
            
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