using System;
using EntityObserver;
using ioList.Domain;

namespace ioList.Observers
{
    public class ListFileObserver : Observer<ListFile>
    {
        public ListFileObserver() : base(new ListFile(@"C:\Users\tnunnink\Desktop\My Lists\List Name.xml"))
        {
        }
        
        public ListFileObserver(ListFile model) : base(model)
        {
        }

        public string Name => GetValue<string>();
        
        public string Path => GetValue<string>();
        
        public bool Exists => GetValue<bool>();

        public DateTime LastWriteTime => GetValue<DateTime>();
    }
}