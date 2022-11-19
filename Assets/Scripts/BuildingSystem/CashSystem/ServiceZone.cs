using ItemSystem;
using UnityEngine;
using Zenject;

namespace BuildingSystem.CashSystem
{
    public abstract class ServiceZone : MonoBehaviour
    {
        public virtual bool IsAvailable => _cashQueue.HasArePlacesInLine;
        public CashQueue CashQueue => _cashQueue;
        public ServiceType ServiceType => _serviceType;

        [SerializeField]
        protected ServiceType _serviceType;
        [SerializeField]
        private float _feedDelay;
        [SerializeField]
        protected CashQueue _cashQueue;
        [SerializeField]
        protected SellersPlace _sellersPlace;

        [Inject]
        protected ServiceOrganigram _serviceOrganigram;
        
        protected bool _isActivateTimer;
        protected float _startTime;
         
        protected virtual void Update()
        {
            if (IsActivateService())
            {
                _isActivateTimer = false;
                ActivateService();
            }
        }

        protected bool IsActivateService()
        {
            if (_sellersPlace.OnSite && _cashQueue.HasFirstVisitorAtTheCheckout() && 
                _serviceOrganigram.IsTheFollowingServiceAvailable(_serviceType))
            {
                if (_isActivateTimer && IsItemProcessingTime())
                {
                    return true;
                }

                if (!_isActivateTimer)
                {
                    RefreshTimer();
                    _isActivateTimer = true;
                }
            }
            else
            {
                _isActivateTimer = false;
            }

            return false;
        }

        protected abstract void ActivateService();
        
        protected void RefreshTimer()
        {
            _startTime = Time.time;
        }

        protected bool IsItemProcessingTime()
        {
            return Time.time > _startTime + _feedDelay;
        }
    }
}