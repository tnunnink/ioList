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
        public bool SearchBuffers { get; set; }
        public bool SearchScalings { get; set; }

        public static ListOptions Default(List list)
        {
            return new ListOptions(list)
            {
                FilterOnReferenced = true,
                FilterOnComment = true,
                SearchBuffers = true,
                SearchScalings = true
            };
        }
    }
}