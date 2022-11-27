using BuildingSystem.CashSystem;

namespace StaffSystem
{
    public class TicketStaff : Staff
    {
        public override void Initialize(ServiceZone serviceZone)
        {
            _stayPosition = serviceZone.StayPosition;
            _aiMovementController.TeleportToPoint(_stayPosition);
        }
    }
}