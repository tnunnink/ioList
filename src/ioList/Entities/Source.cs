using System;

namespace ioList.Entities
{
    public class Source
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public DateTime ImportedOn { get; set; }
        public string ImportedBy { get; set; }
    }
}