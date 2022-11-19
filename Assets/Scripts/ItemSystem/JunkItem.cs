using System;
using UnityEngine;

namespace ItemSystem
{
    [RequireComponent(typeof(Collider))]
    public class JunkItem : Item
    {
        public event Action<JunkItem> OnPickUp;

        public bool IsCollect => _isCollect;

        private bool _isCollect;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public void PickUp()
        {
            OnPickUp?.Invoke(this);
            _isCollect = true;
        }

        public override void Release()
        {
            _isCollect = false;
            base.Release();
        }
    }
}