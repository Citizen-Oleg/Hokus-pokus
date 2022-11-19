using System;
using ItemSystem;
using Portal.PlayerComponent;
using StaffSystem;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    [RequireComponent(typeof(Collider))]
    public class CollectItemHandler : MonoBehaviour
    {
        [Inject]
        private Inventory _inventory;
        
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_inventory.IsInventoryFull && other.TryGetComponent(out JunkItem junkItem) && !junkItem.IsCollect)
            {
                junkItem.PickUp();
                _inventory.ReplenishInventory(junkItem);
            }
        }
    }
}