using Joystick_and_Swipe;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
	public class PlayerModuleController : ITickable
	{
		public bool IsRun { get; private set; }

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
			IsRun = inputDirection != Vector3.zero || _joystickController.IsDrag;

			var hasItem = _inventory.HasItems;
			if (!IsRun)
			{
				_playerAnimationController.SetIdleState(hasItem);
				return;
			}
			
			_playerAnimationController.SetRun(true, hasItem);
		}
	}
}