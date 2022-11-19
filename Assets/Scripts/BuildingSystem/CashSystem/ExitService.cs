using System;
using UnityEngine;
using VisitorSystem;

namespace BuildingSystem.CashSystem
{
    [RequireComponent(typeof(Collider))]
    public class ExitService : ServiceZone
    {
        public override bool IsAvailable => true;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Visitor visitor))
            {
                visitor.Release();
            }
        }

        protected override void ActivateService()
        {
        }

        protected override void Update()
        {
        }
    }
}