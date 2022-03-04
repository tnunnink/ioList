using System;
using EntityObserver;
using ioList.Model;

namespace ioList.Observers
{
    public class ListFileObserver : Observer<ListFile>
    {
        public ListFileObserver(ListFile model) : base(model)
        {
        }

        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }
        
        public DateTime LastWriteTime => GetValue<DateTime>();
    }
}