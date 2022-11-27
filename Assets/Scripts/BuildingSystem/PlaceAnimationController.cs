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
        
        private readonly float _timeScale;
        
        private bool _isPlayAnimation;

        public PlaceAnimationController(Settings settings, int startAmount)
        {
            _textCash = settings.TextCash;
            _image = settings.Image;
            _startAmount = startAmount;
            
            _timeScale = settings.TimeScale;

            _textCash.text = _startAmount.ToString();
        }

        public void Open(GameObject openObject, GameObject closeObject = null)
        {
            Resize(openObject, closeObject);
        }
        
        private async UniTask Resize(GameObject openObject, GameObject closeObject = null)
        {
            var currentTime = 0f;

            while (_timeScale > currentTime)
            {
                currentTime += Time.deltaTime;

                openObject.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, currentTime / _timeScale);
                
                if (closeObject != null)
                {
                    closeObject.transform.localScale =
                        Vector3.Lerp(Vector3.one, Vector3.zero, currentTime / _timeScale);
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            if (closeObject != null)
            {
                closeObject.gameObject.SetActive(false);
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
        }
    }
}