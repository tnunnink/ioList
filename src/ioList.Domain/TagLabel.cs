namespace ioList.Domain
{
    public class TagLabel
    {
        private TagLabel()
        {
        }
        
        public TagLabel(Tag tag, Label label)
        {
            TagId = tag.Id;
            Tag = tag;
            LabelId = label.Id;
            Label = label;
        }
        public int TagId { get; private set; }
        public Tag Tag { get; private set; }
        public int LabelId { get; private set; }
        public Label Label { get; private set; }
    }
}