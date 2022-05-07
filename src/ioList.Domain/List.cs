using System;
using System.IO;

namespace ioList.Domain
{
    public class List
    {
        private List()
        {
        }

        public List(string name, string directory, string comment = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Comment = comment ?? string.Empty;
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            CreatedBy = Environment.UserName;
            CreatedOn = DateTime.Now;
        }

        public int Id { get; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public string Directory { get; private set; }
        public string FullPath => Path.Combine(Directory, $"{Name}.db");
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
    }
}