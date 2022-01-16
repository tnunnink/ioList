namespace ioList.Module.IoSelection.Model
{
    public class IoPoint
    {
        public PnId PnId { get; set; }
        public string ModuleName { get; set; }
        public string CardType { get; set; }
        public string BaseName => $"{ParentName}:{Slot}:{Suffix}";
        public string FullName => $"{ParentName}:{Slot}:{Suffix}.{Member}";
        public string ParentName { get; set; }
        public string Slot { get; set; }
        public string Suffix { get; set; }
        public string Member { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public string Scaling { get; set; }
        public string Initials { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
    }
}