using Base.Tools;
using BuildingSystem;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TimerViewSystem
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private float _timeAnimation;

        private BuildingMiner _buildingMiner;
        private Camera _camera;
        private RectTransform _currentTransform;
        private RectTransform _container;
        private Transform _attachPoint;

        private Sequence _sequence;
        private bool _isActive;
        
        public void Initialize(Camera camera, BuildingMiner buildingMiner)
        {
            _buildingMiner = buildingMiner;
            _buildingMiner.OnRefreshTimer += StartTimer;
            _buildingMiner.OnStartWork += ShowTimer;
            _buildingMiner.OnStopWork += HideTimer;
            
            _camera = camera;
            
            _container = transform.parent as RectTransform;
            _currentTransform = transform as RectTransform;
            _attachPoint = buildingMiner.UiAttachPosition;
        }

        private void ShowTimer()
        {
            _sequence?.Kill();
            _sequence.Append(transform.DOScale(Vector3.one, _timeAnimation));
        }

        private void HideTimer()
        {
            _sequence?.Kill();
            _sequence.Append(transform.DOScale(Vector3.zero, _timeAnimation));
        }
        
        private void StartTimer(float cooldown)
        {
            StartAsyncTimer(cooldown).Forget();
        }

        private async UniTaskVoid StartAsyncTimer(float cooldown)
        {
            _image.fillAmount = 0;
            var startTime = 0f;

            while (cooldown >= startTime && _image != null)
            {
                startTime += Time.deltaTime;
                _image.fillAmount += Time.deltaTime / cooldown;

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public void UpdateUser()
        {
            if (_attachPoint != null)
            {
                _currentTransform.anchoredPosition = UIUtility.WorldToCanvasAnchoredPosition(_camera, _container, _attachPoint.position);
            }
        }
    }
}