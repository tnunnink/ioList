using System;
using System.IO;

namespace ioList.Domain
{
    public class List
    {
        public List()
        {
            Id = 0;
            Name = string.Empty;
            Directory = string.Empty;
            Comment = string.Empty;
            CreatedBy = Environment.UserName;
            CreatedOn = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public string FullPath => Path.Combine(Directory, $"{Name}.db");
    }
}