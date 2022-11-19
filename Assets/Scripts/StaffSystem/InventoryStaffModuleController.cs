using PlayerComponent;
using VisitorSystem;

namespace StaffSystem
{
    public class InventoryStaffModuleController : DefaultStaffModuleController
    {
        private readonly Inventory _inventory;
        
        public InventoryStaffModuleController(AIMovementController aiMovementController, PlayerAnimationController playerAnimationController,
            Inventory inventory)
            : base(aiMovementController, playerAnimationController)
        {
            _inventory = inventory;
        }

        public override void Tick()
        {
            var isRun = !_aiMovementController.IsLookTarget();
            var hasItem = _inventory.HasItems;
            if (!isRun)
            {
                _playerAnimationController.SetIdleState(hasItem);
                return;
            }
			
            _playerAnimationController.SetRun(true, hasItem);
        }
    }
}