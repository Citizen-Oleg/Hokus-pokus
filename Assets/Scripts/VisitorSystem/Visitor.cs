using System;
using PlayerComponent;
using UnityEngine;
using VisitorSystem.Spawner;
using Zenject;

namespace VisitorSystem
{
    public class Visitor : MonoBehaviour
    {
        public VisitorInventory Inventory { get; private set; }
        public VisitCounter VisitCounter => _visitCounter;
        public AIMovementController VisitorMovementController => _visitorMovementController;
        
        public TypeVisitor TypeVisitor => _typeVisitor;

        [SerializeField]
        private TypeVisitor _typeVisitor;
        
        private VisitCounter _visitCounter;
        private AIMovementController _visitorMovementController;
        private VisitorPool _visitorPool;
        private VisitorAnimationController _visitorAnimationController;

        private Transform _nextPoint;
        private PointType _nextPointType;
        
        [Inject]
        public void Constructor(VisitorInventory inventory, AIMovementController visitorMovementController, VisitCounter visitCounter, 
            VisitorAnimationController animationController)
        {
            _visitorAnimationController = animationController;
            Inventory = inventory;
            _visitorMovementController = visitorMovementController;
            _visitCounter = visitCounter;
        }

        public void Initialize(VisitorPool visitorPool)
        {
            _visitorPool = visitorPool;
        }

        public void SetNextPoint(Transform point, PointType pointType)
        {
            _nextPoint = point;
            _nextPointType = pointType;
        }

        public void MoveToNextPoint()
        {
            _visitorMovementController.MoveToPoint(_nextPoint, _nextPointType);
        }

        public void SetDestination(Transform point, PointType pointType)
        {
            _visitorMovementController.MoveToPoint(point, pointType);
        }

        public void Release()
        {
            _visitCounter.ResetVisits();
            _visitorPool.Release(this);
            _visitorAnimationController.Reset();
        }
    }
}