using System;
using System.Collections.Generic;
using L5Sharp;

namespace ioList.Model
{
    public class IoTag
    {
        public IoTag(IModule module, ITag<IDataType> tag)
        {
            ModuleName = module.Name;
            CatalogNumber = module.CatalogNumber;
            TagName = tag.TagName;
            Rack = tag.TagName.Base;
            Description = tag.Description;
            
        }
        
        public string ModuleName { set; get; }
        public string CatalogNumber { get; set; }
        public string ModuleType { get; set; }
        public string ConnectionType { get; set; }
        public string TagName { get; set; }
        public string Rack { get; set; }
        public string Slot { get; set; }
        public string Member { get; set; }
        public string Description { get; set; }
        public string Scaling { get; set; }
        public string Units { get; set; }
        public bool Validated { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public IEnumerable<string> Categories { get; set; }
        public IEnumerable<string> References { get; set; }
    }
}