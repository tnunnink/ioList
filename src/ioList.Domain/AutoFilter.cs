using System;

namespace ioList.Domain
{
    public class AutoFilter
    {
        public Type FilterType { get; set; }
        public string PropertyName { get; set; }
        public object PropertyValue { get; set; }
    }
}