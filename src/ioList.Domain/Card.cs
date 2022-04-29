using System.Collections.Generic;
using System.Linq;
using L5Sharp;

namespace ioList.Domain
{
    public sealed class Card
    {
        private Card()
        {
        }

        public Card(IModule module)
        {
            Id = 0;
            Include = true;
            Name = module.Name;
            CatalogNumber = module.CatalogNumber;
            Revision = module.Revision.ToString();
            CardTypeId = module.ProductType.Id;
            CardType = null;
            IoType = module.CatalogNumber.ToString().Contains("I") ? IOType.Input
                : module.CatalogNumber.ToString().Contains("O") ? IOType.Output
                : IOType.Unknown;
            Parent = module.ParentModule;
            Slot = module.Slot;
            Description = module.Description;
            Points = module.Tags.SelectMany(t => t.Members()).Select(m => new Point(m, this)).ToList();
        }

        public int Id { get; private set; }
        public bool Include { get; private set; }
        public string Name { get; private set; }
        public string CatalogNumber { get; private set; }
        public string Revision { get; private set; }
        public int CardTypeId { get; private set; }
        public CardType CardType { get; private set; }
        public IOType IoType { get; private set; }
        public string Parent { get; private set; }
        public int Slot { get; private set; }
        public string Description { get; private set; }
        public IEnumerable<Point> Points { get; }
    }
}