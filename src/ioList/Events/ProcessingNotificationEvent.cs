using System.Threading;
using Prism.Events;

namespace ioList.Events
{
    public class ProcessingNotificationEvent : PubSubEvent<ProcessingNotificationEventArgs>
    {
        
    }

    public class ProcessingNotificationEventArgs
    {
        public int PercentComplete { get; set; }
        public string Notification { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}