using DesiredServiceSystem;
using TimerViewSystem;
using UnityEngine;
using Zenject;

namespace Level
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private ViewDesiredManager.Settings _settingsViewManager;
        [SerializeField]
        private IconDesiredProvider.Settings _settingsDesiredProvider;
        [SerializeField]
        private TimerViewManager.Settings _settingsTimerViewManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_settingsViewManager);
            Container.BindInstance(_settingsDesiredProvider);
            Container.BindInstance(_settingsTimerViewManager);

            Container.Bind<IconDesiredProvider>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<ViewDesiredManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TimerViewManager>().AsSingle().NonLazy();
        }
    }
}