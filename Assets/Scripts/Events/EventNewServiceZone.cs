using BuildingSystem.CashSystem;
using SimpleEventBus.Events;

namespace Events
{
    public class EventNewServiceZone : EventBase
    {
        public ServiceZone ServiceZone { get; }

        public EventNewServiceZone(ServiceZone serviceZone)
        {
            ServiceZone = serviceZone;
        }
    }
}