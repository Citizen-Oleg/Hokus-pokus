using System;
using ItemSystem;
using ResourceSystem.FactoryResources;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    public class BuildingMiner : Building
    {
        public event Action<BuildingMiner> OnActivate;
        public event Action<float> OnRefreshTimer;
        public event Action OnStopWork;
        public event Action OnStartWork;
        
        public bool HasSubject => _pointOfIssue.ThereItemsToGiveOut;
        public Transform UiAttachPosition => _uiAttachPosition;
        public ItemType CraftItemType => _craftItemType;

        [SerializeField]
        private float _timeSpawn;
        [SerializeField]
        private ItemType _craftItemType;
        [SerializeField]
        private PointOfIssue _pointOfIssue;
        [SerializeField]
        private Transform _uiAttachPosition;

        [Inject]
        private ItemFactory _itemFactory;
        
        private float _startTime;
        private bool _isActive;

        private void OnEnable()
        {
            OnActivate?.Invoke(this);
            _startTime = Time.time;
        }

        private void Update()
        {
            var isAvailable = _pointOfIssue.IsTopUpAvailable();
            if (!isAvailable && _isActive)
            {
                _isActive = false;
                OnStopWork?.Invoke();
                return;
            }
            
            if (isAvailable && !_isActive)
            {
                OnStartWork?.Invoke();
                OnRefreshTimer?.Invoke(_timeSpawn);
                _isActive = true;
                _startTime = Time.time;
                return;
            }

            if (IsTimeSpawn() && isAvailable)
            {
                var item = _itemFactory.Create(_craftItemType);
                item.transform.position = transform.position;
                _pointOfIssue.SendResourceToWarehouse(item);

                if (_pointOfIssue.IsTopUpAvailable())
                {
                    OnRefreshTimer?.Invoke(_timeSpawn);
                }
            }
        }
        
        private bool IsTimeSpawn()
        {
            if (Time.time > _startTime + _timeSpawn)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }
}