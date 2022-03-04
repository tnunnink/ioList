using L5Sharp;

namespace ioList.Model
{
    public class IoSource
    {
        public IoSource(string sourcePath, string contentFileName)
        {
            
        }

        public string Type { get; set; }
        public string FileName { get; }
        public string Content { get; set; }
        public L5XContext Context { get; }
    }
}