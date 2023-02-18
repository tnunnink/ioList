using ioList.Core;
using ioList.Entities;
using Prism.Events;

namespace ioList.Events
{
    public class OpenProjectEvent : PubSubEvent<Project>
    {
    }
}