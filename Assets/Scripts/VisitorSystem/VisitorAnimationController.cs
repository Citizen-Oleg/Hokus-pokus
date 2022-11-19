using System;
using UnityEngine;

namespace VisitorSystem
{
    public class VisitorAnimationController
    {
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsOneItem = Animator.StringToHash("IsOneItem");
        private static readonly int IsTwoItem = Animator.StringToHash("IsTwoItem");
        private static readonly int IsSitDown = Animator.StringToHash("IsSitDown");

        private readonly Animator _animator;
        
        public VisitorAnimationController(Animator animator)
        {
            _animator = animator;
        }

        public void SetIsRun(bool isRun)
        {
            _animator.SetBool(IsRun, isRun);
        }

        public void SetAnimationWithOneItem(bool isOneItem)
        {
            _animator.SetBool(IsOneItem ,isOneItem);
        }
        
        public void SetAnimationWithTwoItem(bool isTwoItem)
        {
            _animator.SetBool(IsTwoItem ,isTwoItem);
        }

        public void SetSitDownAnimation(bool isSitDown)
        {
            _animator.SetBool(IsSitDown, isSitDown);
        }

        public void Reset()
        {
            _animator.SetBool(IsRun, false);
            _animator.SetBool(IsOneItem, false);
            _animator.SetBool(IsTwoItem, false);
            _animator.SetBool(IsSitDown, false);
        }
    }
}