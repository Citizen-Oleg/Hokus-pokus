using System;
using DG.Tweening;
using UnityEngine;

namespace Level
{
    public class ViewAnimationController
    {
        private readonly RectTransform _view;
        private readonly float _scaleTime;
        
        private Sequence _sequence;
        
        public ViewAnimationController(Settings settings)
        {
            _view = settings.View;
            _scaleTime = settings.ScaleTime;
        }

        public void Show()
        {
            _view.localScale = Vector3.zero;
            _view.gameObject.SetActive(true);
            _sequence = DOTween.Sequence();
            _sequence.Append(_view.DOScale(Vector3.one, _scaleTime));
        }

        public void Hide(Action callBack)
        {
            _view.localScale = Vector3.one;
            _sequence = DOTween.Sequence();
            _sequence.Append(_view.DOScale(Vector3.zero, _scaleTime));
            _sequence.AppendCallback(() => callBack?.Invoke());
        }

        [Serializable]
        public class Settings
        {
            public RectTransform View;
            public float ScaleTime;
        }
    }
}