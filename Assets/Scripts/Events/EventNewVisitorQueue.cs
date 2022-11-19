using SimpleEventBus.Events;
using VisitorSystem;

namespace Events
{
    public class EventNewVisitorQueue : EventBase
    {
        public Visitor Visitor { get; }

        public EventNewVisitorQueue(Visitor visitor)
        {
            Visitor = visitor;
        }
    }
}