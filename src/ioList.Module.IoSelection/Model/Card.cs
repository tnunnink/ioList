using System.Collections.Generic;

namespace ioList.Model
{
    public class Card
    {
        public Card(string name, string catalogNumber, string productType, int major, int minor, int slot,
            string parent, IEnumerable<Card> modules)
        {
            Name = name;
            CatalogNumber = catalogNumber;
            ProductType = productType;
            Major = major;
            Minor = minor;
            Slot = slot;
            Parent = parent;
            Modules = modules;
        }
        
        public string Name { get; }
        public string CatalogNumber { get; }
        public string ProductType { get; }
        public int Major { get; }
        public int Minor { get; }
        public int Slot { get; }
        public string Parent { get; }
        public IEnumerable<Card> Modules { get; }
    }
}