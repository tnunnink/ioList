using System;
using EntityObserver;
using ioList.Domain;

namespace ioList.Module.LoadFile.Observers
{
    public class RecentFileObserver : Observer<RecentFile>
    {
        //Design time constructor
        public RecentFileObserver() : base(new RecentFile(@"C:\Path\To\File.xml"))
        {
        }
        
        public RecentFileObserver(RecentFile model) : base(model)
        {
        }

        public string FullPath
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool Exists
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public string Directory
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime LastWriteTime
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }
    }
}