using System;
using System.Collections.Generic;
using BuildingSystem;
using BuildingSystem.CashSystem;
using PlayerComponent;
using UnityEngine;
using Zenject;

namespace StaffSystem
{
    public class GroceryCheckoutStaff : Staff, IStaffInventory
    {
        public Inventory Inventory { get; private set; }

        private List<MinerPoint> _minerPoints;
        
        [Inject]
        public void Constructor(Inventory inventory)
        {
            Inventory = inventory;
        }

        public override void Initialize(ServiceZone serviceZone)
        {
            _stayPosition = serviceZone.StayPosition;
            _aiMovementController.TeleportToPoint(_stayPosition);
            if (serviceZone is ProvisionZone provisionZone)
            {
                _minerPoints = provisionZone.MinerPoints;
            }
        }

        private void Update()
        {
            if (_minerPoints == null)
            {
                return;
            }
            
            _aiMovementController.MoveToPoint(!Inventory.HasItems ? GetAvailableMiner().Point : _stayPosition);
        }

        private MinerPoint GetAvailableMiner()
        {
            foreach (var point in _minerPoints)
            {
                if (point.BuildingMiner.HasSubject)
                {
                    return point;
                }
            }

            return _minerPoints[0];
        }

        [Serializable]
        public class MinerPoint
        {
            public Transform Point;
            public BuildingMiner BuildingMiner;
        }
    }
}