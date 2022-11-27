using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using PlayerComponent;
using UnityEngine;
using Zenject;

namespace StaffSystem
{
    public class GarbageCollectorStaff : Staff, IStaffInventory
    {  
        public Inventory Inventory { get; private set; }
        
        private PerformanceService _performanceService;

        [Inject]
        public void Constructor(Inventory inventory)
        {
            Inventory = inventory;
        }

        public override void Initialize(ServiceZone serviceZone)
        {
            if (serviceZone is PerformanceService performanceService)
            {
                _stayPosition = performanceService.GarbageCan.StayPosition;
                _aiMovementController.TeleportToPoint(_stayPosition);
                _performanceService = performanceService;
            }
        }
        
        private void Update()
        {
            if (_performanceService == null)
            {
                return;
            }
            
            if (_performanceService.CurrentRow.TrueForAll(row => row.IsCleared))
            {
                _aiMovementController.MoveToPoint(_stayPosition);
            }
            else
            {
                foreach (var row in _performanceService.CurrentRow.Where(row => !row.IsCleared))
                {
                    _aiMovementController.MoveToPoint(row.StayPoint);
                    return;
                }
            }
        }
    }
}