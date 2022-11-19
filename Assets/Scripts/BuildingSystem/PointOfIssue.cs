using System;
using ItemSystem;
using Portal.PlayerComponent;
using StaffSystem;
using UnityEngine;

namespace BuildingSystem
{
    [RequireComponent(typeof(Collider))]
    public class PointOfIssue : MonoBehaviour
    {
        public bool ThereItemsToGiveOut => !_stock.IsWarehouseEmpty; 
        
        [SerializeField]
        private float _resourceProcessingTime;
        [SerializeField]
        private Stock _stock;

        private float _startTime;
        private IStaffInventory _staffInventory;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public bool IsTopUpAvailable()
        {
            return !_stock.IsWarehouseFull;
        }

        private bool CanTakeResources()
        {
            return !_stock.IsWarehouseEmpty;
        }
        
        public void SendResourceToWarehouse(Item item)
        {
            if (CanDonateResource())
            {
                _staffInventory.Inventory.ReplenishInventory(item);
            }
            else
            {
                _stock.TopUp(item);
            }
        }

        private Item TakeItem()
        {
            return _stock.TakeItem();
        }

        private void OnTriggerStay(Collider other)
        {
            if (CanDonateResource())
            {
                _staffInventory.Inventory.ReplenishInventory(TakeItem());
                return;
            }
            
            if (other.TryGetComponent(out IStaffInventory staffInventory))
            {
                _staffInventory = staffInventory;
            }
        }

        private bool CanDonateResource()
        {
            return _staffInventory != null && !_staffInventory.Inventory.IsInventoryFull && !_staffInventory.Inventory.IsCountInventoryFull
                   && CanTakeResources() && IsResourceProcessingTime();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IStaffInventory staffInventory))
            {
                _staffInventory = null;
            }
        }

        private bool IsResourceProcessingTime()
        {
            if (Time.time > _startTime + _resourceProcessingTime)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }
}