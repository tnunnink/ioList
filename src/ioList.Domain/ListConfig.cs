namespace ioList.Domain
{
    public class ListConfig
    {
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Directory { get; set; }
        public string SourceFile { get; set; }
        public bool FilterOnReferenced { get; set; }
        public bool FilterOnCommented { get; set; }
        public bool SearchScaling { get; set; }
        public bool SearchBuffer { get; set; }
    }
}