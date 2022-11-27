using System;
using Base;
using Base.MoveAnimation.UI;
using Base.Tools;
using Portal.PlayerComponent;
using ResourceSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class RewardController
    {
        private readonly ObjectPool _objectPool;
        private readonly AnimationManagerUI _animationManagerUi;
        private readonly ResourceManagerGame _resourceManagerGame;

        private readonly Camera _camera;
        private readonly Settings _settings;
        private readonly RectTransform _viewResource;

        public RewardController(Settings settings, AnimationManagerUI animationManagerUi, ResourceManagerGame resourceManagerGame, RectTransform viewResource)
        {
            _animationManagerUi = animationManagerUi;
            _resourceManagerGame = resourceManagerGame;
            _viewResource = viewResource;

            _camera = Camera.main;
            _settings = settings;
            _objectPool = new ObjectPool(settings.PrefabMoneyImage, settings.ParentCanvas, settings.PoolCapacity);
        }
        
        public void AddResource(Resource resource, Vector3 position)
        {
            var startPosition =
                UIUtility.WorldToCanvasPosition(_camera, position);
            var money = _objectPool.Take();
            var rectTransform = money.transform as RectTransform;
            
            _animationManagerUi.ShowAnimationUI(rectTransform, startPosition, _viewResource,
                () =>
                {
                    _objectPool.Release(money);
                    _resourceManagerGame.AddResource(ResourceType.Cash ,resource.Amount);
                });
        }

        [Serializable]
        public class Settings
        {
            public RectTransform ParentCanvas;
            public GameObject PrefabMoneyImage;
            public int PoolCapacity;
        }
    }
}