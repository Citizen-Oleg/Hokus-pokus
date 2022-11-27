using System;
using DG.Tweening;
using UnityEngine;

namespace ItemSystem
{
    [RequireComponent(typeof(Collider))]
    public class JunkItem : Item
    {
        [SerializeField]
        private GameObject _puddle;
        [SerializeField]
        private GameObject _model;
        [SerializeField]
        private float _scaleTime = 0.5f;
        
        private Quaternion _startRotation;
        private Sequence _sequence;

        private void Awake()
        {
            _startRotation = _model.transform.rotation;
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnEnable()
        {
            _puddle.gameObject.SetActive(true);
            _model.transform.rotation = _startRotation;
        }

        public override void Release()
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOScale(Vector3.zero, _scaleTime));
            _sequence.AppendCallback(() =>
            {
                base.Release();
                transform.localScale = Vector3.one;
            });
        }
    }
}