using Base.MoveAnimation._3D;
using Base.MoveAnimation.UI;
using BuildingSystem;
using ItemSystem;
using Managers.ScreensManager;
using Pools;
using ResourceSystem;
using UnityEngine;
using VisitorSystem.Spawner;
using Zenject;

namespace Base
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 0)]
    public class GameSettings : ScriptableObjectInstaller
    {
        [SerializeField]
        private AnimationManagerUI.Settings _settingsAnimationManagerUI;
        [SerializeField]
        private ScreenManager.Settings _settingsScreenManager;
        [SerializeField]
        private AnimationSettings _animationSettings;
        [SerializeField]
        private SettingsItemPool _settingsResourcePool;
        [SerializeField]
        private JunkItemPool.Settings _settingsJunkItemPool;
        [SerializeField]
        private ResourceManager.Settings _settingsResourceManager;
        [SerializeField]
        private VisitorPool.Settings _settingsVisitorPool;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsAnimationManagerUI);
            Container.BindInstance(_settingsScreenManager);
            Container.BindInstance(_animationSettings);
            Container.BindInstance(_settingsResourcePool);
            Container.BindInstance(_settingsJunkItemPool);
            Container.BindInstance(_settingsResourceManager);
            Container.BindInstance(_settingsVisitorPool);
        }
    }
}