using System;
using Base.MoveAnimation._3D;
using Portal.PlayerComponent;
using StaffSystem;
using UnityEngine;
using Zenject;

namespace BuildingSystem
{
    [RequireComponent(typeof(Collider))]
    public class GarbageCan : MonoBehaviour
    {
        public Transform StayPosition => _stayPosition;
        
        [SerializeField]
        private Transform _stayPosition;
        [SerializeField]
        private float _ejectionTime;
        [SerializeField]
        private Transform _centerPoint;
        
        private float _startTime;
        
        [Inject]
        private AnimationManager _animationManager;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsTimeCollect() && other.TryGetComponent(out Player player) && player.Inventory.HasItems)
            {
                var item = player.Inventory.GetLastItem();
                item.Transform.parent = transform;
                _animationManager.ShowFlyingResource(item.Transform, _centerPoint, Vector3.zero, () =>
                {
                    item.Release();
                });
            }
        }

        private bool IsTimeCollect()
        {
            if (Time.time > _startTime + _ejectionTime)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }
}