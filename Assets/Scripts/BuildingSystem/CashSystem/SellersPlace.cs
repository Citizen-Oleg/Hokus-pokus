using System;
using Portal.PlayerComponent;
using StaffSystem;
using UnityEngine;

namespace BuildingSystem.CashSystem
{
    [RequireComponent(typeof(Collider))]
    public class SellersPlace : MonoBehaviour
    {
        public bool OnSite => _staff != null;
        public IStaff Staff => _staff;

        private IStaff _staff;
        
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out IStaff staff))
            {
                _staff = staff;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IStaff staff))
            {
                _staff = null;
            }
        }
    }
}