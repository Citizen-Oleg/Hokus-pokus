using System;
using System.Collections.Generic;
using BuildingSystem.CashSystem;
using PlayerComponent;
using UnityEngine;
using Zenject;

namespace StaffSystem
{
    public class GarbageCollectorStaff : Staff, IStaffInventory
    {  
        public Inventory Inventory { get; private set; }
        
        [SerializeField]
        private PerformanceService _performanceService;

        private readonly NearestItemProvider _nearestItemProvider = new NearestItemProvider();

        [Inject]
        public void Constructor(Inventory inventory)
        {
            Inventory = inventory;
        }
        
        private void Update()
        {
            if (!_isActivate)
            {
                return;
            }

            if (Inventory.HasItems || _performanceService.Trash.Count == 0)
            {
                _aiMovementController.MoveToPoint(_stayPosition);
            }
            else
            {
                var item = _nearestItemProvider.GetNearestJunkItem(_performanceService.Trash, transform.position);
                _aiMovementController.MoveToPoint(item.transform);
            }
        }
    }
}