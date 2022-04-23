using System;
using System.Collections.Generic;

namespace ioList.Domain
{
    public class ListInfo
    {
        private ListInfo()
        {
        }

        public ListInfo(string name, string comment, string directory, string source)
        {
            Name = name;
            Comment = comment;
            Directory = directory;
            Source = new ListSource(source);
            CreatedBy = Environment.UserName;
            CreatedOn = DateTime.Now;
            Options = new List<ListOption>();
        }
        
        public int Id { get; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Directory { get; set; }
        public ListSource Source { get; }
        public string CreatedBy { get; }
        public DateTime CreatedOn { get; }
        public IEnumerable<ListOption> Options { get; }
    }
}