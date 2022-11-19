using BuildingSystem;
using BuildingSystem.CashSystem;
using Joystick_and_Swipe;
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

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsServiceOrganigram);
            Container.BindInstance(_settingsVisitorSpawner);
            Container.BindInstance(_joystickController);

            Container.Bind<ServiceOrganigram>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<VisitorSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VisitorManager>().AsSingle().NonLazy();
        }
    }
}