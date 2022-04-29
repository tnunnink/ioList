namespace ioList.Domain
{
    public class ListOptions
    {
        private ListOptions()
        {
        }
        
        private ListOptions(List list)
        {
            ListId = list.Id;
            List = list;
        }

        public int ListId { get; private set; }
        public List List { get; private set; }
        public bool FilterOnReferenced { get; set; }
        public bool FilterOnComment { get; set; }
        public bool SearchForBuffers { get; set; }
        public bool SearchForScaling { get; set; }

        public static ListOptions Default(List list)
        {
            return new ListOptions(list)
            {
                FilterOnReferenced = true,
                FilterOnComment = true,
                SearchForBuffers = true,
                SearchForScaling = true
            };
        }
    }
}