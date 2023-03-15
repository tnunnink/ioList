using System.Linq;
using L5Sharp;
using L5Sharp.Components;
using L5Sharp.Core;

namespace ioList.Model
{
    public class Point
    {
        public Point(Module module, ILogixTag member, string type)
        {
            ModuleName = module.Name;
            ParentName = module.ParentModule;
            CatalogNumber = module.CatalogNumber;
            Slot = module.Slot.ToString();
            Type = type;
            Tag = member.TagName;
            Path = member.TagName.Path;
            Comment = member.Description;
            Unit = member is TagMember m ? m.Unit : string.Empty;

            var parent = member.TagName.Members.ElementAt(member.TagName.Depth - 1);
            High = module.Config?.Member($"{parent}.HighEngineering")?.Value?.ToString();
            Low = module.Config?.Member($"{parent}.LowEngineering")?.Value?.ToString();
        }

        public BufferTag Buffer { get; set; } = default;
        public string ModuleName { get; set; }
        public string ParentName { get; set; }
        public string CatalogNumber { get; set; }
        public string Slot { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public string Path { get; set; }
        public string Comment { get; set; }
        public string Unit { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Initials { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}