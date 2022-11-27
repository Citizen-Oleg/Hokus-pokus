using BuildingSystem;
using BuildingSystem.CashSystem;
using HireHelperSystem;
using Joystick_and_Swipe;
using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using VisitorSystem.Spawner;
using Zenject;

namespace Level
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private JoystickController _joystickController;
        [SerializeField]
        private ServiceOrganigram.Settings _settingsServiceOrganigram;
        [SerializeField]
        private VisitorSpawner.Settings _settingsVisitorSpawner;
        [SerializeField]
        private RectTransform _resourceView;
        [SerializeField]
        private RewardController.Settings _settingsRewardController;
        [SerializeField]
        private HireHelperController.Settings _settingsHireHelperController;
        [SerializeField]
        private HireHelperDataBaseInformation.Settings _settingsHireHelperDataBaseInformation;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsServiceOrganigram);
            Container.BindInstance(_settingsVisitorSpawner);
            Container.BindInstance(_joystickController);
            Container.BindInstance(_settingsRewardController);
            Container.BindInstance(_settingsHireHelperDataBaseInformation);
            Container.BindInstance(_settingsHireHelperController);

            Container.BindInterfacesAndSelfTo<ServiceOrganigram>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HireHelperDataBaseInformation>().AsSingle().NonLazy();
            Container.Bind<RewardController>().AsSingle().WithArguments(_resourceView).NonLazy();
            
            Container.Bind<HireHelperController>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<VisitorSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VisitorManager>().AsSingle().NonLazy();
        }
    }
}