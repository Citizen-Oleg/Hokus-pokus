using System;
using BuildingSystem.CashSystem;
using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace StaffSystem
{
    public abstract class Staff : MonoBehaviour, IStaff
    {
        public bool IsRun => _aiMovementController.IsRun;

        public StaffType StaffType => _staffType;
        
        protected Transform _stayPosition;

        [SerializeField]
        private StaffType _staffType;
        
        [Inject]
        protected AIMovementController _aiMovementController;

        public abstract void Initialize(ServiceZone serviceZone);
    }
}