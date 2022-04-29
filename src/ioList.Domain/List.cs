using System;
using System.IO;

namespace ioList.Domain
{
    public class List
    {
        private List()
        {
        }

        public List(string name, string directory, string source, string comment = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Comment = comment ?? string.Empty;
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            Source = new ListSource(this, source);
            CreatedBy = Environment.UserName;
            CreatedOn = DateTime.Now;
            Options = ListOptions.Default(this);
        }

        public int Id { get; }
        public string Name { get; private set; }
        public string Comment { get; private set; }
        public string Directory { get; private set; }
        public string FileName => Path.Combine(Directory, $"{Name}.db");
        public ListSource Source { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public ListOptions Options { get; private set; }
    }
}