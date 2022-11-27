using System;
using System.Collections.Generic;
using ItemSystem;
using StaffSystem;
using UnityEngine;

namespace BuildingSystem.CashSystem
{
    [RequireComponent(typeof(Collider))]
    public class Row : MonoBehaviour
    {
        public event Action OnCleared;

        public float CleaningProgress => _currentTime / _cleaningTime;
        public bool IsCleared { get; private set; } = true;
        public List<JunkItem> JunkItems => _junkItems;

        public Transform UIAttachPosition;
        public List<PerformanceService.SeatPosition> SeatPositions => _seatPositions;
        public Transform StayPoint => _stayPoint;

        [SerializeField]
        private Transform _stayPoint;
        [SerializeField]
        private List<PerformanceService.SeatPosition> _seatPositions = new List<PerformanceService.SeatPosition>();
        [SerializeField]
        private ParticleSystem _particleSystem;
        [SerializeField]
        private float _cleaningTime;

        private List<JunkItem> _junkItems = new List<JunkItem>();
        private float _currentTime;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public void AddJunkItem(JunkItem junkItem)
        {
            _junkItems.Add(junkItem);
            IsCleared = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!IsCleared && other.TryGetComponent(out IStaff staff) && !staff.IsRun)
            {
                _currentTime += Time.deltaTime;
                
                if (_currentTime >= _cleaningTime)
                {
                    IsCleared = true;
                    OnCleared?.Invoke();
                    foreach (var junkItem in _junkItems)
                    {
                        junkItem.Release();
                    }

                    _junkItems.Clear();
                    _particleSystem.Play();
                    _currentTime = 0;
                }
            }
        }
    }
}