using Pools;
using ResourceSystem;
using UnityEngine;

namespace ItemSystem
{
    public class Item : MonoBehaviour, IInventoryItem
    {
        public Transform TopItem => _topTransform;
        public Transform Transform => transform;
        public ItemType ItemType => _itemType;
        public int Weight => _weight;

        [SerializeField]
        private ItemType _itemType;
        [SerializeField]
        private Transform _topTransform;
        [SerializeField]
        private int _weight;

        private DefaultItemPool _itemPool;
        
        public void Initialize(DefaultItemPool itemPool)
        {
            _itemPool = itemPool;
        }

        public virtual void Release()
        {
            _itemPool.Release(this);
        }
    }
}