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
        
        public TypeVisitor TypeVisitor => _typeVisitor;
        public Transform UiAttachPoint => _uiAttachPoint;
        
        [SerializeField]
        private TypeVisitor _typeVisitor;
        [SerializeField]
        private Transform _uiAttachPoint;

        private VisitCounter _visitCounter;
        private AIMovementController _visitorMovementController;
        private VisitorPool _visitorPool;
        private VisitorAnimationController _visitorAnimationController;
        
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