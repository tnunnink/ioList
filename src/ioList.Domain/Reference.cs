namespace ioList.Domain
{
    public class Reference
    {
        public int Id { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public string Text { get; set; }
    }
}