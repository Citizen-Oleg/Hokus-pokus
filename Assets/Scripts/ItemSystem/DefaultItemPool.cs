using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;

namespace ItemSystem
{
    public class DefaultItemPool
    {
        protected readonly Dictionary<ItemType, DefaultMonoBehaviourPool<Item>> _monoBehaviourPools = 
            new Dictionary<ItemType, DefaultMonoBehaviourPool<Item>>();
        
        public Item GetItemByItemType(ItemType itemType)
        {
            var item = _monoBehaviourPools[itemType].Take();
            item.Initialize(this);
            return item;
        }
        
        public void Release(Item item)
        {
            _monoBehaviourPools[item.ItemType].Release(item);
        }
    }
}