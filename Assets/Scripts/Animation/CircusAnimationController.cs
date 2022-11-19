using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CircusAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animatorCircus;
    [SerializeField]
    private ParticleSystem _particleSystem;

    public void Activate()
    {
        _animatorCircus.Play("Shot");
    }
    

    [UsedImplicitly]
    public void ActivateFX()
    {
        _particleSystem.Play();
    }

    public void Reset()
    {
        _animatorCircus.Play("ResetCircus");
    }
}
