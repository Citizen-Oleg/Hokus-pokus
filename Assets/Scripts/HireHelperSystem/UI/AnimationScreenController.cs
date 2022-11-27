using DG.Tweening;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class AnimationScreenController : MonoBehaviour
    {
        [SerializeField]
        private float _travelTime;
        [SerializeField]
        private RectTransform _startPosition;
        [SerializeField]
        private RectTransform _endPosition;
        [SerializeField]
        private RectTransform _movePanel;
        [SerializeField]
        private RectTransform _screen;
        
        private Sequence _sequence;
        
        public void Show()
        {
            _sequence = DOTween.Sequence();
            _movePanel.anchoredPosition = _startPosition.anchoredPosition;
            _screen.gameObject.SetActive(true);
            _sequence.Append(_movePanel.DOAnchorPos(_endPosition.anchoredPosition, _travelTime));
        }
    }
}