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
        [SerializeField]
        private float _ejectionTime;    
        [SerializeField]
        private float _radiusCollection;
        
        private readonly Collider[] _colliders = new Collider[50];
        private float _startTime;
        
        [Inject]
        private AnimationManager _animationManager;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsTimeCollect())
            {
                Collection();
            }
        }

        private void Collection()
        {
            var count = Physics.OverlapSphereNonAlloc(transform.position, _radiusCollection, _colliders);

            if (count == 0)
            {
                return;
            }
            
            for (var i = 0; i < count; i++)
            {
                if (_colliders[i] == null)
                {
                    break;
                }
                if (_colliders[i].TryGetComponent(out IStaffInventory player) && player.Inventory.HasItems)
                {
                    var item = player.Inventory.GetLastItem();
                    item.Transform.parent = transform;
                    _animationManager.ShowFlyingResource(item.Transform, transform, Vector3.zero, () =>
                    {
                        item.Release();
                    });
                }
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

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, _radiusCollection);
        }
    }
}