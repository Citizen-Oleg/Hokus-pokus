﻿using System;
using System.Collections.Generic;
using Base.MoveAnimation._3D;
using Base.MoveAnimation.UI;
using ItemSystem;
using PlayerComponent;
using Portal.PlayerComponent;
using ResourceSystem;
using UnityEngine;
using Zenject;

namespace BuildingSystem.CashSystem
{
    [RequireComponent(typeof(Collider))]
    public class MoneyWarehouse : MonoBehaviour
    {
        [SerializeField]
        private float _transmissionDelay;
        [SerializeField]
        private Inventarizator _inventarizator;
        
        [Inject]
        private AnimationManager _animationManager;
        [Inject]
        private RewardController _rewardController;

        private readonly Stack<ResourceItem> _resourceItems = new Stack<ResourceItem>();
        
        private float _startTime;
        private Player _player;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public void AddResourceItem(ResourceItem resourceItem)
        {
            _resourceItems.Push(resourceItem);
            var offset = _inventarizator.GetOffSetByIndex(_resourceItems.Count - 1);
            _animationManager.ShowFlyingResource(resourceItem.transform, _inventarizator.StartPositionResource, offset, () =>
            {
                _inventarizator.InventarizationSpecificOffset(resourceItem.transform, offset);
            });
        }
        
        private void Update()
        {
            if (CanDonateResource())
            {
                var targetTransform = _player.BodyPosition;
                var resourceItem = _resourceItems.Pop();
                _animationManager.ShowFlyingResource(resourceItem.transform, targetTransform, Vector3.zero, () =>
                {
                    _rewardController.AddResource(resourceItem.Resource, targetTransform.position);
                    resourceItem.Release();
                } );
            }
        }

        private bool CanDonateResource()
        {
            return _resourceItems.Count != 0 && _player != null && IsResourceProcessingTime();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = player;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _player = null;
            }
        }
        
        private bool IsResourceProcessingTime()
        {
            if (Time.time > _startTime + _transmissionDelay)
            {
                _startTime = Time.time;
                return true;
            }

            return false;
        }
    }
}