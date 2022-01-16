using System.Collections.Generic;
using ioList.Module.IoSelection.Model;

namespace ioList.Module.IoSelection.Services.Fakes
{
    public class FakeModuleDataService : IModuleDataService
    {
        private List<LogixModule> _cards;

        public void Load(string fileName)
        {
            _cards = new List<LogixModule>
            {
                new("Module_01", "1756-XXX01", "Product Type", 1, 0, 0, "ParentName", new List<LogixModule>
                {
                    new("Child_01", "1756-XXX01", "Product Type", 1, 0, 0, "Card_01"),
                    new("Child_02", "1756-XXX02", "Product Type", 1, 1, 1, "Card_01"),
                    new("Child_03", "1756-XXX03", "Product Type", 1, 2, 2, "Card_01"),
                    new("Child_04", "1756-XXX04", "Product Type", 1, 3, 3, "Card_01"),
                }),
                new("Module_02", "1756-XXX01", "Product Type", 1, 0, 0, "ParentName", new List<LogixModule>
                {
                    new("Child_01", "1756-XXX01", "Product Type", 1, 0, 0, "Card_01"),
                    new("Child_02", "1756-XXX02", "Product Type", 1, 1, 1, "Card_01"),
                    new("Child_03", "1756-XXX03", "Product Type", 1, 2, 2, "Card_01"),
                    new("Child_04", "1756-XXX04", "Product Type", 1, 3, 3, "Card_01"),
                }),
                new("Module_03", "1756-XXX01", "Product Type", 1, 0, 0, "ParentName")
            };
        }

        public IEnumerable<LogixModule> GetAll() => _cards;

        public LogixModule Get()
        {
            return new LogixModule("Card_01", "1756-XXX01", "Product Type", 1, 0, 0, "ParentName", new List<LogixModule>());
        }
    }
}