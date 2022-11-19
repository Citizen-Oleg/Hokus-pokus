using Zenject;

namespace ItemSystem
{
    public class JunkItemFactory: PlaceholderFactory<ItemType, JunkItem>
    {
        private readonly JunkItemPool _itemPool;

        public JunkItemFactory(JunkItemPool itemPool)
        {
            _itemPool = itemPool;
        }
        
        public override JunkItem Create(ItemType itemType)
        {
            var resourceItem = _itemPool.GetItemByItemType(GetJunkItemType(itemType)) as JunkItem;
            return resourceItem;
        }

        private ItemType GetJunkItemType(ItemType itemType)
        {
            return itemType switch
            {
                ItemType.Burger => ItemType.Junk_Burger,
                ItemType.Soda => ItemType.Junk_Soda,
                _ => ItemType.Junk_Burger
            };
        }
    }
}