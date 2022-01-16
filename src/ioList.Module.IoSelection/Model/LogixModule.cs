using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;

namespace ioList.Module.IoSelection.Model
{
    public class LogixModule : BindableBase
    {
        public LogixModule()
        {
            Name = "TestModule";
            CatalogNumber = "1756-IF8";
            ProductType = "8 Channel Non Isolated Voltage/Current Analog Input";
            Major = 1;
            Minor = 001;
            Slot = 2;
            Parent = "Rack1";
        }
        public LogixModule(string name, string catalogNumber, string productType, int major, int minor, int slot,
            string parent, IEnumerable<LogixModule> modules = null)
        {
            Name = name;
            CatalogNumber = catalogNumber;
            ProductType = productType;
            Major = major;
            Minor = minor;
            Slot = slot;
            Parent = parent;
            Modules = modules ?? Enumerable.Empty<LogixModule>();
        }

        private readonly string _name;

        public string Name
        {
            get => _name;
            private init => SetProperty(ref _name, value);
        }
        public string CatalogNumber { get; }
        public string ProductType { get; }
        public int Major { get; }
        public int Minor { get; }
        public int Slot { get; }
        public string Parent { get; }
        public IEnumerable<LogixModule> Modules { get; }
    }
}