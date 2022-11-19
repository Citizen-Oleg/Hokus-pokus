using Events;
using ItemSystem;
using StaffSystem;
using Tools.SimpleEventBus;
using UnityEngine;

namespace BuildingSystem.CashSystem
{
    public class ProvisionZone : CheckoutArea
    {
        public ItemType SoldItem => _soldItem;
        
        [SerializeField]
        private ItemType _soldItem;

        protected override void ActivateService()
        {
            if (_sellersPlace.Staff is IStaffInventory staffInventory)
            {
                var item = staffInventory.Inventory.GetInventoryItemByItemType(_soldItem);
                item.Transform.parent = transform;

                var visitor = _cashQueue.GetFirstVisitor();
                visitor.Inventory.ReplenishInventory(item, () =>
                {
                    _cashQueue.RefreshQueue();
                    EventStreams.UserInterface.Publish(new EventServedVisitor(_serviceType, visitor, this));
                    var cash = _itemFactory.Create(ItemType.Cash) as ResourceItem;
                    cash.Resource = _price;
                    cash.transform.position = _spawnPositionMoney.position;
                    _moneyWarehouse.AddResourceItem(cash);
                });
            }
        }

        protected override void Update()
        {
            if (_sellersPlace.OnSite && 
                _sellersPlace.Staff is IStaffInventory staffInventory && 
                staffInventory.Inventory.HasItem(_soldItem) && 
                _cashQueue.HasFirstVisitorAtTheCheckout() && 
                _serviceOrganigram.IsTheFollowingServiceAvailable(_serviceType))
            {
                if (_isActivateTimer && IsItemProcessingTime())
                { 
                    ActivateService();
                    _isActivateTimer = false;
                }

                if (!_isActivateTimer)
                {
                    RefreshTimer();
                    _isActivateTimer = true;
                }
            }
            else
            {
                _isActivateTimer = false;
            }
        }
    }
}