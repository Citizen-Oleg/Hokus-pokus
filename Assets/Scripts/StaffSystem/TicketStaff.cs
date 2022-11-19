using UnityEngine;

namespace StaffSystem
{
    public class TicketStaff : Staff
    {
        public override void Activate()
        {
            base.Activate();
            _aiMovementController.MoveToPoint(_stayPosition);
        }
    }
}