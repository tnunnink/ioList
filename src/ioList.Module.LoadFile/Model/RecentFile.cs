using System;
using System.IO;

namespace ioList.Module.LoadFile.Model
{
    public class RecentFile
    {
        private readonly FileInfo _info;

        /// <summary>
        /// Design time constructor
        /// </summary>
        public RecentFile()
        {
            _info = new FileInfo(@"C:\Users\tnunnink\Local\Projects\logix-ioList\Samples\Test.L5X");
        }
        public RecentFile(string fullName)
        {
            _info = new FileInfo(fullName);
        }

        public string FullName => _info.FullName;
        public string Name => _info.Name;
        public string Directory => _info.DirectoryName;
        
        public string Extension => _info.Extension;
        
        public DateTime LastWriteTime => _info.LastWriteTime;

        public bool Exists => _info.Exists;
    }
}