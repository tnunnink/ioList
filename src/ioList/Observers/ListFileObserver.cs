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

        public string Name => GetValue<string>();

        public DateTime LastWriteTime => GetValue<DateTime>();
    }
}