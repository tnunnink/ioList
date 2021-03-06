using System;
using L5Sharp;

namespace ioList.Domain
{
    public class Source
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Content { get; set; }
        public DateTime ImportedOn { get; set; }
        public string ImportedBy { get; set; }
        public ILogixContext Context => LogixContext.Parse(Content);
    }
}