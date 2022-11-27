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
        public RewardController RewardController { get; private set; }
        public bool IsRun => _playerModuleController.IsRun;

        public Transform BodyPosition => _bodyPosition;

        [SerializeField]
        private Transform _bodyPosition;

        private PlayerModuleController _playerModuleController;

        [Inject]
        public void Constructor(Inventory inventory, RewardController rewardController, PlayerModuleController playerModuleController)
        {
            Inventory = inventory;
            RewardController = rewardController;
            _playerModuleController = playerModuleController;
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
        }
    }
}