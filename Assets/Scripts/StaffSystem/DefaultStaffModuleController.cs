using Joystick_and_Swipe;
using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace StaffSystem
{
    public class DefaultStaffModuleController : ITickable
    {
        protected readonly PlayerAnimationController _playerAnimationController;
        protected readonly AIMovementController _aiMovementController;
        protected readonly Staff _staff;
		
        public DefaultStaffModuleController(AIMovementController aiMovementController, PlayerAnimationController playerAnimationController, Staff staff)
        {
            _playerAnimationController = playerAnimationController;
            _staff = staff;
            _aiMovementController = aiMovementController;
        }

        public virtual void Tick()
        {
            var isRun = !_aiMovementController.IsPointReached();
            _playerAnimationController.SetRun(isRun, false);
        }
    }
}