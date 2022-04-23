using L5Sharp;

namespace ioList.Domain
{
    public class ListSource
    {
        private ListSource()
        {
        }
        
        public ListSource(string content)
        {
            Content = content;
        }
        
        public int ListId { get; }
        public string Content { get; }

        public ILogixContext Context() => LogixContext.Parse(Content);
    }
}