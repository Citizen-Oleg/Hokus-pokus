using System;
using System.Collections.Generic;
using System.Linq;
using Base.MoveAnimation._3D;
using ItemSystem;
using ResourceSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class Inventory
    {
        public int Weight { get; private set; }
        public bool IsInventoryFull => Weight >= _capacity;
        public bool IsCountInventoryFull => _items.Count >= _capacity;
        public bool HasItems => _items.Count != 0;

        private readonly int _capacity;
        private readonly List<IInventoryItem> _items = new List<IInventoryItem>();
        private readonly Inventarizator _inventarizator;
        private readonly AnimationManager _animationManager;

        public Inventory(InventorySettings inventorySettings, Inventarizator inventarizator, AnimationManager animationManager)
        {
            _capacity = inventorySettings.Capacity;
            _inventarizator = inventarizator;
            _animationManager = animationManager;
        }

        public bool HasItem(ItemType itemType)
        {
            return _items.Any(inventoryItem => inventoryItem.ItemType == itemType);
        }

        public IInventoryItem GetInventoryItemByItemType(ItemType itemType)
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if (_items[i].ItemType == itemType)
                {
                    var item = _items[i];
                    Weight -= item.Weight;
                    _items.RemoveAt(i);
                    InventoryOffset();
                    return item;
                }
            }
            
            return null;
        }

        public IInventoryItem GetLastItem()
        {
            var item = _items[_items.Count - 1];
            Weight -= item.Weight;
            _items.RemoveAt(_items.Count - 1);
            return item;
        }
        
        public void ReplenishInventory(IInventoryItem item)
        {
            Weight += item.Weight;
            var endTransform = _items.Count == 0 ? _inventarizator.StartPositionResource : _items[_items.Count - 1].TopItem;
            _items.Add(item);

            _animationManager.ShowFlyingResource(item.Transform, endTransform, Vector3.zero, () =>
                {
                    if (_items.Contains(item))
                    {
                        _inventarizator.InventarizationItem(item.Transform, endTransform);
                    }
                });
        }

        private void InventoryOffset()
        {
            for (var i = 0; i < _items.Count; i++)
            {
                var endPosition = i == 0 ? _inventarizator.StartPositionResource : _items[i - 1].TopItem;
                _inventarizator.InventarizationItem(_items[i].Transform, endPosition);
            }
        }
    }

    [Serializable]
    public class InventorySettings
    {
        public int Capacity;
    }
}