using System.Collections.Generic;
using System.Linq;
using L5Sharp;
using L5Sharp.Components;
using L5Sharp.Core;

namespace ioList.Entities
{
    public class DeviceTag
    {
        public DeviceTag(Module module, ILogixTag member, string type)
        {
            Module = module.Name;
            Parent = module.ParentModule;
            Catalog = module.CatalogNumber;
            Slot = module.Slot.ToString();
            Type = type;
            Tag = member.TagName;
            Address = member.TagName.Path;
            Comment = member.Description;
            Unit = member is TagMember m ? m.Unit : string.Empty;

            //todo would like to make this configurable as I imagine not all analogs have the same member names.
            var parent = member.TagName.Members.ElementAt(member.TagName.Depth - 1);
            High = module.Config?.Member($"{parent}.HighEngineering")?.Value?.ToString();
            Low = module.Config?.Member($"{parent}.LowEngineering")?.Value?.ToString();
        }

        public string Module { get; set; }
        public string Catalog { get; set; }
        public string Tag { get; set; }
        public string Parent { get; set; }
        public string Slot { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public string Unit { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public List<NeutralText> References { get; set; } = new();
        public string BufferTag { get; set; } = string.Empty;
        public string BufferDescription { get; set; } = string.Empty;
        public string Initials { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}