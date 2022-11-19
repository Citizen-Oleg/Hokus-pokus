using System;
using BuildingSystem.CashSystem;
using UnityEngine;
using Zenject;

namespace VisitorSystem.Spawner
{
    public class VisitorSpawner : ITickable
    {
        public event Action<Visitor> OnSpawnVisitor;
        
        private readonly float _appearanceTime;
        private readonly Transform _startPoint;

        private readonly VisitorPool _visitorPool;
        private readonly ServiceOrganigram _serviceOrganigram;
        private readonly ServiceType _serviceType;

        private float _currentTime;
        
        public VisitorSpawner(Settings settings, VisitorPool visitorPool, ServiceOrganigram serviceOrganigram)
        {
            _appearanceTime = settings.AppearanceTime;
            _visitorPool = visitorPool;
            _serviceType = settings.StartService;
            _startPoint = settings.StartPoint;
            _serviceOrganigram = serviceOrganigram;
        }
        
        public void Tick()
        {
            if (Time.time > _currentTime && _serviceOrganigram.IsTheServiceAvailable(_serviceType))
            {
                _currentTime = Time.time + _appearanceTime;
                Spawn();
            }
        }

        private void Spawn()
        {
            var visitor = _visitorPool.GetRandomVisitor();
            visitor.gameObject.SetActive(false);
            visitor.transform.position = _startPoint.position;
            visitor.gameObject.SetActive(true);
            OnSpawnVisitor?.Invoke(visitor);
        }

        [Serializable]
        public class Settings
        {
            public float AppearanceTime;
            public Transform StartPoint;
            public ServiceType StartService;
        }
    }
}