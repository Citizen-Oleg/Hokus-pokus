
using Joystick_and_Swipe;
using Portal.PlayerComponent;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class MovementController : IFixedTickable
    {
        private readonly JoystickController _joystickController;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        private readonly float _gravitaion;
        
        public MovementController(JoystickController joystickController, Player.Settings playerSettings)
        {
            _joystickController = joystickController;
            _rigidbody = playerSettings.Rigidbody;
            _speed = playerSettings.Speed;
        }

        public void FixedTick()
        {
            _rigidbody.velocity = _speed * _joystickController.InputDirection;
            var direction = _rigidbody.velocity;
            if (!direction.Equals(Vector3.zero))
            {
                _rigidbody.rotation = Quaternion.LookRotation(direction);
            }

            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.velocity += Vector3.down * _gravitaion;
            }
        }
    }
}