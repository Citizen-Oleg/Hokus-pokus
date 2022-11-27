using System;
using System.Collections.Generic;
using System.Linq;
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
        public event Action<PerformanceService> OnStartJunkCollect;

        public List<Row> CurrentRow => _row;
        public GarbageCan GarbageCan => _garbageCan;
        
        [SerializeField]
        private float _presentationTime;
        [SerializeField]
        private float _startAnimation;
        [SerializeField]
        private GarbageCan _garbageCan;
        [SerializeField]
        private MagicAnimationController _magicAnimationController;
        [SerializeField]
        private List<Row> _row = new List<Row>();

        private float _startPresentationTime;
        
        private readonly List<Visitor> _watching = new List<Visitor>();

        protected override void ActivateService()
        {
            var visitors = _cashQueue.GetAllVisitor();
            var countVisitor = visitors.Count;
            var indexVisitor = 0;

            foreach (var row in _row)
            {
                if (countVisitor == 0)
                {
                    break;
                }

                foreach (var seatPosition in row.SeatPositions.TakeWhile(seatPosition => countVisitor != 0))
                {
                    _watching.Add(visitors[indexVisitor]);
                    visitors[indexVisitor].SetDestination(seatPosition.Point, PointType.Circus);
                    countVisitor--;
                    indexVisitor++;
                }
            }

            Observable.Timer(TimeSpan.FromSeconds(_startAnimation))
                .Subscribe(_ => _magicAnimationController.StartAnimation()).AddTo(this);
        }

        protected override void Update()
        {
            UpdateTimerOnEnableOrDisable();
            
            if (IsTimePresentation() && _watching.Count != 0)
            {
                FinishShow();
            }

            if (_row.TrueForAll(row => row.IsCleared) && IsTimePresentation() && IsActivateService())
            {
                RefreshPresentationTimer();
                ActivateService();
            }
        }

        private void FinishShow()
        {
            var countWatching = _watching.Count;
            var indexWatching = 0;

            foreach (var row in _row)
            {
                if (countWatching == 0)
                {
                    break;
                }

                foreach (var seatPosition in row.SeatPositions.TakeWhile(seatPosition => countWatching != 0))
                {
                    var listTrash = _watching[indexWatching].Inventory.DropItem(seatPosition);
                    foreach (var junkItem in listTrash)
                    {
                        row.AddJunkItem(junkItem);
                    }
                    
                    EventStreams.UserInterface.Publish(new EventServedVisitor(_serviceType, _watching[indexWatching], this));
                    
                    countWatching--;
                    indexWatching++;
                }
            }

            OnStartJunkCollect?.Invoke(this);
            _watching.Clear();
            _magicAnimationController.Reset();
        }

        private bool IsTimePresentation()
        {
            return Time.time > _startPresentationTime;
        }

        private void RefreshPresentationTimer()
        {
            _startPresentationTime = Time.time + _presentationTime;
        }

        [Serializable]
        public class SeatPosition
        {
            public Transform Point;
            public Transform PointJunkItemOne;
            public Transform PointJunkItemTwo;
        }
    }
}