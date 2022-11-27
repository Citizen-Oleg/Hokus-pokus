using Base.MoveAnimation._3D;
using Base.MoveAnimation.UI;
using BuildingSystem;
using DesiredServiceSystem;
using HireHelperSystem;
using HireHelperSystem.UI;
using ItemSystem;
using Managers.ScreensManager;
using Pools;
using ResourceSystem;
using ServiceViewSystem;
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
        [Header("Providers")]
        [SerializeField]
        private IconServiceProvider.Settings _settingsServiceCreateViewTimer;
        [SerializeField]
        private IconDesiredProvider.Settings _settingsDesiredProvider;
        [SerializeField]
        private ColorPayButtonProvider.Settings _settingsColorPayButtonProvider;
        [SerializeField]
        private ProfessionIconProvider.Settings _settingsProfessionIconProvider;
        [SerializeField]
        private ServiceIconProvider.Settings _settingsServiceIconProvider;
        [SerializeField]
        private StaffIconProvider.Settings _settingsStaffIconProvider;
        [SerializeField]
        private ServiceNameProvider.Settings _settingsServiceNameProvider;
        [SerializeField]
        private StaffProvider.Settings _settingsStaffProvider;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsAnimationManagerUI);
            Container.BindInstance(_settingsStaffProvider);
            Container.BindInstance(_settingsDesiredProvider);
            Container.BindInstance(_settingsServiceCreateViewTimer);
            Container.BindInstance(_settingsScreenManager);
            Container.BindInstance(_animationSettings);
            Container.BindInstance(_settingsResourcePool);
            Container.BindInstance(_settingsJunkItemPool);
            Container.BindInstance(_settingsResourceManager);
            Container.BindInstance(_settingsVisitorPool);
            Container.BindInstance(_settingsColorPayButtonProvider);
            Container.BindInstance(_settingsProfessionIconProvider);
            Container.BindInstance(_settingsServiceIconProvider);
            Container.BindInstance(_settingsStaffIconProvider);
            Container.BindInstance(_settingsServiceNameProvider);
        }
    }
}