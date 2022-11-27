using UnityEngine;
using Zenject;

namespace VisitorSystem
{
    public class VisitorModuleController : ITickable
    {
        private readonly VisitorInventory _visitorInventory;
        private readonly AIMovementController _visitorMovementController;
        private readonly VisitorAnimationController _visitorAnimationController;
        
        public VisitorModuleController(VisitorInventory visitorInventory, AIMovementController visitorMovementController, 
            VisitorAnimationController visitorAnimationController)
        {
            _visitorMovementController = visitorMovementController;
            _visitorInventory = visitorInventory;
            _visitorAnimationController = visitorAnimationController;
        }

        public void Tick()
        {
            var isRun = !_visitorMovementController.IsPointReached();
            _visitorAnimationController.SetIsRun(isRun);
            _visitorAnimationController.SetSitDownAnimation(_visitorMovementController.PointType == PointType.Circus);
            
           
            _visitorAnimationController.SetAnimationWithOneItem(_visitorInventory.HasItemInOneHand);
            _visitorAnimationController.SetAnimationWithTwoItem(_visitorInventory.IsFull);
        }
    }
}