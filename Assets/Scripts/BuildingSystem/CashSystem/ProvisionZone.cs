using System.Collections.Generic;
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
        public List<GroceryCheckoutStaff.MinerPoint> MinerPoints => _minerPoints;
        
        [SerializeField]
        private ItemType _soldItem;
        [SerializeField]
        private List<GroceryCheckoutStaff.MinerPoint> _minerPoints = new List<GroceryCheckoutStaff.MinerPoint>();

        protected override void ActivateService()
        {
            if (_sellersPlace.Staff is IStaffInventory staffInventory)
            {
                var item = staffInventory.Inventory.GetInventoryItemByItemType(_soldItem);
                item.Transform.parent = transform;

                var visitor = _cashQueue.GetFirstVisitor();
                EventStreams.UserInterface.Publish(new EventServedVisitorNextPoint(_serviceType, visitor, this));
                
                visitor.Inventory.ReplenishInventory(item, () =>
                {
                    if (visitor.VisitorMovementController.IsPointReached())
                    {
                        visitor.MoveToNextPoint();
                    }

                    _cashQueue.RefreshQueue();
                    var cash = _itemFactory.Create(ItemType.Cash) as ResourceItem;
                    cash.Resource = _price;
                    cash.transform.position = _spawnPositionMoney.position;
                    _moneyWarehouse.AddResourceItem(cash);
                });
            }
        }

        protected override void Update()
        {
            UpdateTimerOnEnableOrDisable();
            if (ConditionsMet())
            {
                ActivateService();
            }
        }

        private bool ConditionsMet()
        {
            return _sellersPlace.Staff is IStaffInventory staffInventory &&
                   staffInventory.Inventory.HasItem(_soldItem) && IsActivateService();
        }
    }
}