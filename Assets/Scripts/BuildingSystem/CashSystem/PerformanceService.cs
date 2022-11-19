using System;
using System.Collections.Generic;
using Events;
using ItemSystem;
using Tools.SimpleEventBus;
using UniRx;
using UnityEngine;
using VisitorSystem;

namespace BuildingSystem.CashSystem
{
    public class PerformanceService : ServiceZone
    {
        public List<JunkItem> Trash => _trash;
        
        [SerializeField]
        private float _presentationTime;
        [SerializeField]
        private float _startAnimation;
        [SerializeField]
        private MagicAnimationController _magicAnimationController;
        [SerializeField]
        private List<Transform> _seatPositions = new List<Transform>();

        private float _startPresentationTime;
        
        private readonly List<Visitor> _watching = new List<Visitor>();
        private readonly List<JunkItem> _trash = new List<JunkItem>();
         
        protected override void ActivateService()
        {
            var visitors = _cashQueue.GetAllVisitor();

            for (var i = 0; i < visitors.Count; i++)
            {
                _watching.Add(visitors[i]);
                visitors[i].SetDestination(_seatPositions[i], PointType.Circus);
            }

            Observable.Timer(TimeSpan.FromSeconds(_startAnimation))
                .Subscribe(_ => _magicAnimationController.StartAnimation()).AddTo(this);
        }

        protected override void Update()
        {
            if (IsTimePresentation() && _watching.Count != 0)
            {
                FinishShow();
            }

            if (_trash.Count == 0 && IsActivateService() && IsTimePresentation())
            {
                RefreshPresentationTimer();
                ActivateService();
            }
        }

        private void FinishShow()
        {
            foreach (var visitor in _watching)
            {
                var listTrash = visitor.Inventory.DropItem();
                foreach (var junkItem in listTrash)
                {
                    junkItem.OnPickUp += RemoveJunkItem;
                    _trash.Add(junkItem);
                }

                EventStreams.UserInterface.Publish(new EventServedVisitor(_serviceType, visitor, this));
            }

            _watching.Clear();
            _magicAnimationController.Reset();
        }

        private void RemoveJunkItem(JunkItem junkItem)
        {
            junkItem.OnPickUp -= RemoveJunkItem;
            _trash.Remove(junkItem);
        }

        private bool IsTimePresentation()
        {
            return Time.time > _startPresentationTime;
        }

        private void RefreshPresentationTimer()
        {
            _startPresentationTime = Time.time + _presentationTime;
        }

        private void OnDestroy()
        {
            foreach (var item in _trash)
            {
                item.OnPickUp -= RemoveJunkItem;
            }
        }
    }
}