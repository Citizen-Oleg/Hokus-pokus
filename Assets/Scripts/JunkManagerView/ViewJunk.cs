using System;
using Base.Tools;
using BuildingSystem.CashSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace JunkManagerView
{
    public class ViewJunk : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private ViewAnimationController.Settings _settingsViewJunkAnimationController;
        
        private Row _row;
        private ManagerJunkView _managerJunkView;
        
        private Camera _camera;
        private RectTransform _currentTransform;
        private RectTransform _container;
        private Transform _attachPoint;

        private ViewAnimationController _viewJunkAnimationController;

        private void Awake()
        {
            _viewJunkAnimationController = new ViewAnimationController(_settingsViewJunkAnimationController);
        }

        public void Initialize(Camera camera, Row row, ManagerJunkView managerJunkView)
        {
            _row = row;
            _managerJunkView = managerJunkView;
            
            _camera = camera;
            
            _container = transform.parent as RectTransform;
            _currentTransform = transform as RectTransform;
            _attachPoint = _row.UIAttachPosition;

            _image.fillAmount = 0;
            _row.OnCleared += Hide;
            Show();
        }

        private void Show()
        {
            _viewJunkAnimationController.Show();
        }

        private void Hide()
        {
            _viewJunkAnimationController.Hide(() =>
            {
                _row.OnCleared -= Hide;
                _image.fillAmount = 0;
                _attachPoint = null;
                _row = null;
                _managerJunkView.Release(this);
            });
        }

        private void Update()
        {
            if (_attachPoint != null)
            {
                _currentTransform.anchoredPosition = UIUtility.WorldToCanvasAnchoredPosition(_camera, _container, _attachPoint.position);

                if (!_row.IsCleared)
                {
                    _image.fillAmount = _row.CleaningProgress;
                }
            }
        }
    }
}