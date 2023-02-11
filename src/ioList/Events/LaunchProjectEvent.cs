using ioList.Entities;
using Prism.Events;

namespace ioList.Events
{
    public class LaunchProjectEvent : PubSubEvent<LaunchProjectEventArgs>
    {
    }

    public class LaunchProjectEventArgs
    {
        public ProjectFile ProjectFile { get; set; }
        public string SourceFile { get; set; }
    }
}