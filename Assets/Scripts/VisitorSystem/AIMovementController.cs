using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace VisitorSystem
{
    public class AIMovementController : ITickable
    {
        public PointType PointType => _pointType;
        public bool IsRun => !IsPointReached();

        private readonly float _rotationSpeed;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly float _angularSpeed;
        private readonly float _stopDistance;

        private PointType _pointType = PointType.Queue;
        private Transform _point;

        public AIMovementController(Settings settings)
        {
            _stopDistance = settings.StopDistance;
            _navMeshAgent = settings.NavMeshAgent;
            _rotationSpeed = settings.RotationSpeed;

            _angularSpeed = _navMeshAgent.angularSpeed;
            _navMeshAgent.speed = Random.Range(settings.SpeedMin, settings.SpeedMax);
        }

        public void MoveToPoint(Transform point, PointType pointType = PointType.Queue)
        {
            _pointType = pointType;
            _point = point;
            _navMeshAgent.destination = point.position;
        }

        public void TeleportToPoint(Transform point)
        {
            _navMeshAgent.gameObject.SetActive(false);
            _navMeshAgent.transform.position = point.position;
            _navMeshAgent.transform.rotation = Quaternion.LookRotation(point.forward);
            _navMeshAgent.gameObject.SetActive(true);

            _point = point;
        }
        
        public void Tick()
        {
            if (_point == null)
            {
                return;
            }

            if (IsPointReached())
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

        public bool HasPoint()
        {
            return _point != null;
        }

        public bool IsPointReached()
        {
            return HasPoint() && IsPointReached(_point);
        }
        
        public bool IsPointReached(Transform point)
        {
            if (_point == null)
            {
                return false;
            }
            
            var pointOne = new Vector2(point.position.x, point.position.z);
            var pointTwo = new Vector2(_navMeshAgent.transform.position.x, _navMeshAgent.transform.position.z);
            var distance = Vector2.Distance(pointOne, pointTwo);
            return distance <= _stopDistance;
        }

        [Serializable]
        public class Settings
        {
            public float SpeedMax;
            public float SpeedMin;
            public float RotationSpeed;
            public NavMeshAgent NavMeshAgent;
            public float StopDistance = 0.35f;
        }
    }
}