using System;
using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace StaffSystem
{
    public abstract class Staff : MonoBehaviour, IStaff
    {
        [SerializeField]
        protected Transform _stayPosition;

        [Inject]
        protected AIMovementController _aiMovementController;
        
        protected bool _isActivate;

        private void Awake()
        {
            _aiMovementController.MoveToPoint(transform);
        }

        public virtual void Activate()
        {
            _isActivate = true;
        }
    }
}