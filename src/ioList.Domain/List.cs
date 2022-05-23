using System;
using System.Collections.Generic;
using System.IO;

namespace ioList.Domain
{
    public class List
    {
        public List()
        {
            Name = string.Empty;
            Directory = string.Empty;
            Comment = string.Empty;
            CreatedBy = Environment.UserName;
            CreatedOn = DateTime.Now;
            Settings = ImportSettings.Default();
        }

        public string Name { get; set; }
        public string Directory { get; set; }
        public string Comment { get; set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public ImportSettings Settings { get; }
        public string FullPath => Path.Combine(Directory, $"{Name}.db");
    }
}