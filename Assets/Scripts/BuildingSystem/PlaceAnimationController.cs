using System;
using System.Collections;
using System.Numerics;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

namespace BuildingSystem
{
    public class PlaceAnimationController
    {
        private readonly TextMeshProUGUI _textCash;
        private readonly Image _image;
        private readonly int _startAmount;

        private readonly Vector3 _endScale;
        private readonly float _timeScale;
        
        private bool _isPlayAnimation;

        public PlaceAnimationController(Settings settings, int startAmount)
        {
            _textCash = settings.TextCash;
            _image = settings.Image;
            _startAmount = startAmount;

            _endScale = settings.EndScale;
            _timeScale = settings.TimeScale;

            _textCash.text = _startAmount.ToString();
        }

        public void ZoomAnimation(GameObject gameObject)
        {
            ZoomObject(gameObject);
        }

        private async UniTask ZoomObject(GameObject gameObject)
        {
            var currentTime = 0f;
            var startScale = Vector3.zero;

            while (_timeScale > currentTime)
            {
                currentTime += Time.deltaTime;

                gameObject.transform.localScale = Vector3.Lerp(startScale, _endScale, currentTime / _timeScale);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public void StartAnimation(float animationTime, Action callBack)
        {
            _isPlayAnimation = true;
            PlayAnimation(animationTime, callBack);
        }

        public void StopAnimation()
        {
            _isPlayAnimation = false;
            _image.fillAmount = 0f;
            _textCash.text = _startAmount.ToString();
        }

        private async UniTask PlayAnimation(float animationTime, Action callBack)
        {
            _image.fillAmount = 0;
            var currentTime = 0f;
            var currentAmount = _startAmount;

            while (_isPlayAnimation && animationTime > currentTime)
            {
                currentTime += Time.deltaTime;
                _image.fillAmount += Time.deltaTime / animationTime;

                _textCash.text = Mathf.Lerp(currentAmount, 0, currentTime / animationTime).ToString("#"); 

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (_isPlayAnimation)
            {
                callBack?.Invoke();
            }
        }

        [Serializable]
        public class Settings
        {
            public TextMeshProUGUI TextCash;
            public Image Image;

            public float TimeScale = 0.5f;
            public Vector3 EndScale = Vector3.one;
        }
    }
}