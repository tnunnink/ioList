using System;

namespace ioList.Entities
{
    public class AutoFilter
    {
        public Type FilterType { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}