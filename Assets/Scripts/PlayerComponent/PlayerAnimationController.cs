using System;
using UnityEngine;

namespace PlayerComponent
{
	public class PlayerAnimationController
	{
		private static readonly int _isRun = Animator.StringToHash("IsRun");
		private static readonly int _hasItem = Animator.StringToHash("HasItem");

		private readonly Animator _animator;

		public PlayerAnimationController(Settings settings)
		{
			_animator = settings.Animator;
		}

		public void SetIdleState(bool hasItem)
		{
			_animator.SetBool(_isRun, false);
			_animator.SetBool(_hasItem, hasItem);
		}

		public void SetRun(bool isRun, bool hasItem)
		{
			_animator.SetBool(_isRun, isRun);
			_animator.SetBool(_hasItem, hasItem);
		}

		[Serializable]
		public class Settings
		{
			public Animator Animator;
		}
	}
}