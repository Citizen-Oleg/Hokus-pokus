using System;
using PlayerComponent;
using StaffSystem;
using UnityEngine;
using Zenject;

namespace Portal.PlayerComponent
{
    public class Player : MonoBehaviour, IStaffInventory
    {
        public Inventory Inventory { get; private set; }

        public Transform BodyPosition => _bodyPosition;

        [SerializeField]
        private Transform _bodyPosition;

        [Inject]
        public void Constructor(Inventory inventory)
        {
            Inventory = inventory;
        }
        
        [Serializable]
        public class Settings
        {
            public float Speed => _speed;
            public Rigidbody Rigidbody => _rigidbody;

            [SerializeField]
            private float _speed;
            [SerializeField]
            private Rigidbody _rigidbody;
            public float Gravitaion;
        }
    }
}