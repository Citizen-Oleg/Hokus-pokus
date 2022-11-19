using Joystick_and_Swipe;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
	public class PlayerModuleController : ITickable
	{
		private readonly JoystickController _joystickController;
		private readonly Inventory _inventory;
		private readonly PlayerAnimationController _playerAnimationController;
		
		public PlayerModuleController(JoystickController joystickController, Inventory inventory, PlayerAnimationController playerAnimationController)
		{
			_joystickController = joystickController;
			_inventory = inventory;
			_playerAnimationController = playerAnimationController;
		}

		public void Tick()
		{
			var inputDirection = _joystickController.InputDirection;
			var isRun = inputDirection != Vector3.zero || _joystickController.IsDrag;

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