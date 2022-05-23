namespace ioList.Domain
{
    public sealed class Module
    {
        public Module()
        {
        }

        public int Id { get; set; }
        public int SourceId { get; set; }
        public Source Source { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string CatalogNumber { get; set; }
        public ModuleInfo Info { get; set; }
        public string Revision { get; set; }
        public int Slot { get; set; }
        public string Description { get; set; }
    }
}