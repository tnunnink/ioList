using System;
using EntityObserver;
using ioList.Model;

namespace ioList.Observers
{
    public class ListFileObserver : Observer<ListFile>
    {
        public ListFileObserver() : base(new ListFile("List Name 01", @"C:\Users\tnunnink\Desktop\My Lists\List Name.xml"))
        {
            
        }
        public ListFileObserver(ListFile model) : base(model)
        {
        }

        public string ListName => GetValue<string>();
        
        public string ListPath => GetValue<string>();
        
        public bool Exists => GetValue<bool>();

        public DateTime LastWriteTime => GetValue<DateTime>();
    }
}