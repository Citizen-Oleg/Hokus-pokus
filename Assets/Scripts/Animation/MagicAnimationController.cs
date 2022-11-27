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
    private GameObject _hideFirstObject;
    [SerializeField]
    private GameObject _hideTwoObject;
    [SerializeField]
    private CircusAnimationController _circusAnimationController;

    private int _indexHide;
    
    public void StartAnimation()
    {
        _animator.SetTrigger("Activate");
    }

    [UsedImplicitly]
    public void ActivateFXExperimental()
    {
        _particleSystem.Play();

        _indexHide++;
        if (_indexHide == 1)
        {
            _hideFirstObject.SetActive(false);
            _hideTwoObject.SetActive(true);
        }
        else
        {
            _indexHide = 0;
            _hideTwoObject.SetActive(false);
            _animator.ResetTrigger("Activate");
        }
    }

    public void Reset()
    {
        _hideFirstObject.SetActive(true);
        _circusAnimationController.Reset();
    }

    [UsedImplicitly]
    public void ActivateGun()
    {
        _circusAnimationController.Activate();
    }
}
