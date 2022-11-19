using System;
using BuildingSystem.CashSystem;
using UnityEngine;

namespace StaffSystem
{
    public class QueueStaff : Staff
    {
        [SerializeField]
        private Transform _queuePoint;
        [SerializeField]
        private CashQueue _cashQueue;

        private void Update()
        {
            if (!_isActivate)
            {
                return;
            }
            
            _aiMovementController.MoveToPoint(_cashQueue.IsEmptyQueue ? _stayPosition : _queuePoint);
        }
    }
}