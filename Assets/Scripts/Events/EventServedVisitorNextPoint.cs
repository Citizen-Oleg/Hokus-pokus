using BuildingSystem.CashSystem;
using SimpleEventBus.Events;
using VisitorSystem;

namespace Events
{
    public class EventServedVisitorNextPoint : EventBase
    {
        public ServiceType ServiceType { get; }
        public Visitor Visitor { get; }
        public ServiceZone ServiceZone { get; }

        public EventServedVisitorNextPoint(ServiceType serviceType, Visitor visitor, ServiceZone serviceZone)
        {
            ServiceType = serviceType;
            Visitor = visitor;
            ServiceZone = serviceZone;
        }
    }
}