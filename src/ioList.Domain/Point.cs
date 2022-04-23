using System;
using System.Collections.Generic;
using System.Xml.Linq;
using L5Sharp;
using L5Sharp.Core;
using L5Sharp.Creators;

namespace ioList.Domain
{
    public class Point
    {
        public int Id { get; set; }
        public bool Include { get; set; }
        public TagName TagName { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public string Units { get; set; }
        public Scaling Scaling { get; set; }
        public Card Card { get; set; }
        public Buffer Buffer { get; set; }
        public bool Validated { get; set; }
        public string ValidatedBy { get; set; }
        public DateTime ValidatedOn { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}