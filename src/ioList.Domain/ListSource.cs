using L5Sharp;

namespace ioList.Domain
{
    public class ListSource
    {
        private ListSource()
        {
        }
        
        public ListSource(List list, string content)
        {
            ListId = list.Id;
            List = list;
            Content = content;
        }
        
        public int ListId { get; private set; }
        public List List { get; private set; }
        public string Content { get; private set; }

        public ILogixContext Context() => LogixContext.Parse(Content);
    }
}