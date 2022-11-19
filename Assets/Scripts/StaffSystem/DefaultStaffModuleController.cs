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
		
        public DefaultStaffModuleController(AIMovementController aiMovementController, PlayerAnimationController playerAnimationController)
        {
            _playerAnimationController = playerAnimationController;
            _aiMovementController = aiMovementController;
        }

        public virtual void Tick()
        {
            var isRun = !_aiMovementController.IsLookTarget();
            _playerAnimationController.SetRun(isRun, false);
        }
    }
}