using System;
using Events;
using ItemSystem;
using Tools.SimpleEventBus;
using UnityEngine;
using Zenject;

namespace BuildingSystem.CashSystem
{
    public abstract class ServiceZone : MonoBehaviour
    {
        public event Action<ServiceZone> OnActivate;

        public event Action OnStartTimer;
        public event Action OnStopTimer;

        public Transform UiAttachPosition => _uiAttachPosition;
        public float Progress => _currentTime / _feedDelay;
        public virtual bool IsAvailable => _cashQueue.HasArePlacesInLine;
        public CashQueue CashQueue => _cashQueue;
        public ServiceType ServiceType => _serviceType;
        public Transform StayPosition => _stayPosition;

        [SerializeField]
        private Transform _stayPosition;
        [SerializeField]
        private Transform _uiAttachPosition;
        [SerializeField]
        private bool _activateWhenTurnedOn;
        [Space(20f)]
        [SerializeField]
        protected ServiceType _serviceType;
        [SerializeField]
        protected float _feedDelay;
        [SerializeField]
        protected CashQueue _cashQueue;
        [SerializeField]
        protected SellersPlace _sellersPlace;

        [Inject]
        protected ServiceOrganigram _serviceOrganigram;
        
        protected float _currentTime;
        private bool _isActivateTimer;
        
        protected void OnEnable()
        {
            OnActivate?.Invoke(this);
            if (_activateWhenTurnedOn)
            {
                EventStreams.UserInterface.Publish(new EventNewServiceZone(this));
            }
        }

        protected virtual void Update()
        {
            if (IsActivateService())
            {
                ActivateService();
            }
            
            UpdateTimerOnEnableOrDisable();
        }

        protected void UpdateTimerOnEnableOrDisable()
        {
            if (_cashQueue.IsEmptyQueue && _isActivateTimer)
            {
                _isActivateTimer = false;
                OnStopTimer?.Invoke();
            }

            if (!_cashQueue.IsEmptyQueue && _cashQueue.HasFirstVisitorAtTheCheckout() && !_isActivateTimer)
            {
                _isActivateTimer = true;
                OnStartTimer?.Invoke();
            }
        }

        protected bool IsActivateService()
        {
            if (_sellersPlace.OnSite && _cashQueue.HasFirstVisitorAtTheCheckout() && 
                _serviceOrganigram.IsTheFollowingServiceAvailable(_serviceType))
            {
                _currentTime += Time.deltaTime;

                if (_currentTime >= _feedDelay)
                {
                    _currentTime = 0f;
                    return true;
                }
                
                return false;
            }

            _currentTime = 0f;
            return false;
        }

        protected abstract void ActivateService();
    }
}