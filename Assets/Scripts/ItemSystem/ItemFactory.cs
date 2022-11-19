using ItemSystem;
using Pools;
using UnityEngine;
using Zenject;

namespace ResourceSystem.FactoryResources
{
    public class ItemFactory : PlaceholderFactory<ItemType, Item>
    {
        private readonly ItemPool _itemPool;

        public ItemFactory(ItemPool itemPool)
        {
            _itemPool = itemPool;
        }
        
        public override Item Create(ItemType itemType)
        {
            var resourceItem = _itemPool.GetItemByItemType(itemType);
            return resourceItem;
        }
    } 
}