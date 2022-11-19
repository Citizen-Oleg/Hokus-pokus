using Events;
using ItemSystem;
using ResourceSystem;
using ResourceSystem.FactoryResources;
using Tools.SimpleEventBus;
using UnityEngine;
using Zenject;

namespace BuildingSystem.CashSystem
{
    public class CheckoutArea : ServiceZone
    {
        [SerializeField]
        protected Resource _price;
        [SerializeField]
        protected Transform _spawnPositionMoney;
        [SerializeField]
        protected MoneyWarehouse _moneyWarehouse;
        
        [Inject]
        protected ItemFactory _itemFactory;
        
        protected override void ActivateService()
        {
            var visitor = _cashQueue.GetFirstVisitor();
            _cashQueue.RefreshQueue();
            EventStreams.UserInterface.Publish(new EventServedVisitor(_serviceType, visitor, this));

            var cash = _itemFactory.Create(ItemType.Cash) as ResourceItem;
            cash.Resource = _price;
            cash.transform.position = _spawnPositionMoney.position;
            _moneyWarehouse.AddResourceItem(cash);
        }
    }
}