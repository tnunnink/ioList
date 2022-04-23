namespace ioList.Domain
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CatalogNumber { get; set; }
        public string Revision { get; set; }
        public string ProductType { get; set; }
        public string ConnectionType { get; set; }
        public string Parent { get; set; }
        public int Slot { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public bool Include { get; set; }
    }
}