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
        
        [SerializeField]
        private List<MinerPoint> _minerPoints = new List<MinerPoint>();
        
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