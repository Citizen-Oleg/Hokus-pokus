using System;
using System.Collections.Generic;
using ItemSystem;
using PlayerComponent;
using ResourceSystem;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    public class Stock : MonoBehaviour
    {
        public bool IsWarehouseFull => _placementPosition.Count == _items.Count;
        public bool IsWarehouseEmpty => _items.Count == 0;
        
        [SerializeField]
        private List<Transform> _placementPosition;
        [SerializeField]
        private Inventarizator _inventarizator;
        
        private Stack<Item> _items = new Stack<Item>();
        
        public void TopUp(Item item)
        {
            _items.Push(item);
            _inventarizator.InventarizationItem(item.transform, _placementPosition[_items.Count - 1]);
        }

        public Item TakeItem()
        {
            var item = _items.Pop();
            return item;
        }
    }
}