using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace VisitorSystem
{
    public class AIMovementController : ITickable
    {
        public PointType PointType => _pointType;

        private readonly float _rotationSpeed;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _angularSpeed;

        private PointType _pointType = PointType.Queue;
        private Transform _point;

        public AIMovementController(Settings settings)
        {
            _navMeshAgent = settings.NavMeshAgent;
            _rotationSpeed = settings.RotationSpeed;

            _angularSpeed = _navMeshAgent.angularSpeed;
            _navMeshAgent.speed = settings.Speed;
        }

        public void MoveToPoint(Transform point, PointType pointType = PointType.Queue)
        {
            _pointType = pointType;
            _point = point;
            _navMeshAgent.destination = point.position;
        }
        
        public void Tick()
        {
            if (_point == null)
            {
                return;
            }

            if (IsLookTarget())
            {
                _navMeshAgent.angularSpeed = 0;
                var direction = _point.forward;
                direction.y = 0;
                var rotation = Quaternion.LookRotation(direction);
                _navMeshAgent.transform.rotation = Quaternion.Lerp(_navMeshAgent.transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            }
            else
            {
                _navMeshAgent.angularSpeed = _angularSpeed;
            }
        }

        public bool IsLookTarget()
        {
            if (_point == null)
            {
                return false;
            }
            
            var pointOne = new Vector2(_point.position.x, _point.position.z);
            var pointTwo = new Vector2(_navMeshAgent.transform.position.x, _navMeshAgent.transform.position.z);
            return Vector2.Distance(pointOne, pointTwo) < 0.35f;
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
            public float RotationSpeed;
            public NavMeshAgent NavMeshAgent;
        }
    }
}