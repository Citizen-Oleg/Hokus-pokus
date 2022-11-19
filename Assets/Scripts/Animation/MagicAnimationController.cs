using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using JetBrains.Annotations;
using UnityEngine;

public class MagicAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private GameObject _experimental;
    [SerializeField]
    private CircusAnimationController _circusAnimationController;
    
    public void StartAnimation()
    {
        _animator.SetTrigger("Activate");
    }

    [UsedImplicitly]
    public void ActivateFXExperimental()
    {
        _animator.ResetTrigger("Activate");
        _particleSystem.Play();
        _experimental.SetActive(false);
    }

    public void Reset()
    {
        _experimental.SetActive(true);
        _circusAnimationController.Reset();
    }

    [UsedImplicitly]
    public void ActivateGun()
    {
        _circusAnimationController.Activate();
    }
    
}
