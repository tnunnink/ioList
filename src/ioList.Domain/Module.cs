using System.Collections.Generic;
using System.Linq;

namespace ioList.Domain
{
    public class Module
    {
        public Module()
        {
            Name = "TestModule";
            CatalogNumber = "1756-IF8";
            ProductType = "8 Channel Non Isolated Voltage/Current Analog Input";
            Major = 1;
            Minor = 001;
            Slot = 2;
            Parent = "Rack1";
        }

        public Module(string name, string catalogNumber, string productType, int major, int minor, int slot,
            string parent, IEnumerable<Module> modules = null)
        {
            Name = name;
            CatalogNumber = catalogNumber;
            ProductType = productType;
            Major = major;
            Minor = minor;
            Slot = slot;
            Parent = parent;
            Modules = modules ?? Enumerable.Empty<Module>();
        }

        public string Name { get; }
        public string CatalogNumber { get; }
        public string ProductType { get; }
        public int Major { get; }
        public int Minor { get; }
        public string Revision => $"{Major}.{Minor}";
        public int Slot { get; }
        public string Parent { get; }
        public IEnumerable<Module> Modules { get; }
    }
}