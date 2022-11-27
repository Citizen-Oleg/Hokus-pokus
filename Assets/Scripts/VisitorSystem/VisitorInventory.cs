using System;
using System.Collections.Generic;
using Base.MoveAnimation._3D;
using BuildingSystem.CashSystem;
using ItemSystem;
using PlayerComponent;
using ResourceSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VisitorSystem
{
    public class VisitorInventory
    {
        public bool IsFull => _inventoryItems.Count == 2;
        public bool HasItemInOneHand => _inventoryItems.Count == 1;
        
        private readonly List<IInventoryItem> _inventoryItems = new List<IInventoryItem>();
        
        private readonly Transform _leftHandBurger;
        private readonly Transform _leftHandCola;
        
        private readonly Transform _rightHandBurger;
        private readonly Transform _rightHandCola;

        private readonly Inventarizator _inventarizator;
        private readonly AnimationManager _animationManager;
        private readonly JunkItemFactory _junkItemFactory;
        
        public VisitorInventory(Settings setting, Inventarizator inventarizator, AnimationManager animationManager, 
            JunkItemFactory junkItemFactory)
        {
            _leftHandBurger = setting.LeftHandBurger;
            _rightHandBurger = setting.RightHandBurger;

            _leftHandCola = setting.LeftHandCola;
            _rightHandCola = setting.RightHandCola;

            _junkItemFactory = junkItemFactory;
            _inventarizator = inventarizator;
            _animationManager = animationManager;
        }
        
        public void ReplenishInventory(IInventoryItem item, Action callBack)
        {
            var isBurger = item.ItemType == ItemType.Burger;
            Transform endTransform;

            if (isBurger)
            {
                endTransform = _inventoryItems.Count == 0 ? _rightHandBurger : _leftHandBurger;
            }
            else
            {
                endTransform = _inventoryItems.Count == 0 ? _rightHandCola : _leftHandCola;
            }

            _inventoryItems.Add(item);

            _animationManager.ShowFlyingResource(item.Transform, endTransform, Vector3.zero, () =>
            {
                callBack?.Invoke();
                if (_inventoryItems.Contains(item))
                {
                    _inventarizator.InventarizationItem(item.Transform, endTransform, false);
                }
            });
        }

        public List<JunkItem> DropItem(PerformanceService.SeatPosition seatPosition)
        {
            var list = new List<JunkItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                _inventoryItems[i].Release();
                var item = _junkItemFactory.Create(_inventoryItems[i].ItemType);
                item.transform.position = i == 1 ?
                    seatPosition.PointJunkItemOne.position :
                    seatPosition.PointJunkItemTwo.position;

                var rotation = item.transform.rotation;
                rotation.y = Random.rotation.y;
                item.transform.rotation = rotation;
                
                list.Add(item);
            }

            _inventoryItems.Clear();

            return list;
        }


        [Serializable]
        public class Settings
        {
            public Transform LeftHandCola;
            public Transform LeftHandBurger;
            public Transform RightHandCola;
            public Transform RightHandBurger;
        }
    }
}