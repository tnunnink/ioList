using System;
using System.Collections.Generic;

namespace ioList.Model
{
    public class IoList
    {
        private IoList()
        {
        }
        
        public IoList(string name, string sourceFile, string description = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description ?? string.Empty;
            Owner = Environment.UserName;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            Tags = new List<IoTag>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public IoSource Source { get; set; }
        public IEnumerable<IoTag> Tags { get; set; }
    }
}