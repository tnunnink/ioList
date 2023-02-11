namespace ioList.Entities
{
    public sealed class Device
    {
        public Device()
        {
        }

        public int Id { get; set; }
        public int ControllerId { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public string CatalogNumber { get; set; }
        public DeviceInfo Info { get; set; }
        public string Revision { get; set; }
        public int Slot { get; set; }
        public string Description { get; set; }
    }
}