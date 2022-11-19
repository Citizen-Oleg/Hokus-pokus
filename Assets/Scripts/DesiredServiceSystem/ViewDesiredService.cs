using Base.Tools;
using UnityEngine;
using UnityEngine.UI;
using VisitorSystem;

namespace DesiredServiceSystem
{
    public class ViewDesiredService : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        
        private RectTransform _currentTransform;
        private RectTransform _container;
        private Transform _attachPoint;
        private Camera _camera;
        
        public void Initialize(Camera camera, Visitor visitor)
        {
            _camera = camera;
            _container = transform.parent as RectTransform;
            _currentTransform = transform as RectTransform;
            _attachPoint = visitor.UiAttachPoint;
        }

        public void ChangeSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
        
        public void LateUpdate()
        {
            if (_attachPoint != null)
            {
                _currentTransform.anchoredPosition = UIUtility.WorldToCanvasAnchoredPosition(_camera, _container, _attachPoint.position);
            }
        }
    }
}