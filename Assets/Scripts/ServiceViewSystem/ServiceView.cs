using Base.Tools;
using BuildingSystem.CashSystem;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace ServiceViewSystem
{
    public class ServiceView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Image _fillImage;
        [SerializeField]
        private ViewAnimationController.Settings _settingsViewAnimationController;
        
        private Camera _camera;
        private RectTransform _currentTransform;
        private RectTransform _container;
        private Transform _attachPoint;
        private ServiceZone _serviceZone;

        private ViewAnimationController _viewAnimationController;

        private void Awake()
        {
            _viewAnimationController = new ViewAnimationController(_settingsViewAnimationController);
        }
        
        public void Initialize(Camera camera, Sprite icon, ServiceZone serviceZone)
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(false);
            _icon.sprite = icon;
            _camera = camera;

            _serviceZone = serviceZone;
            _container = transform.parent as RectTransform;
            _currentTransform = transform as RectTransform;
            _attachPoint = serviceZone.UiAttachPosition;

            serviceZone.OnStartTimer += _viewAnimationController.Show;
            serviceZone.OnStopTimer += Hide;
        }

        private void Hide()
        {
            _viewAnimationController.Hide(() =>
            {
                gameObject.SetActive(false);
                _fillImage.fillAmount = 0;
            });
        }

        private void Update()
        {
            if (_attachPoint != null)
            {
                _currentTransform.anchoredPosition = UIUtility.WorldToCanvasAnchoredPosition(_camera, _container, _attachPoint.position);
            }

            if (_serviceZone != null)
            {
                _fillImage.fillAmount = _serviceZone.Progress;
            }
        }
    }
}