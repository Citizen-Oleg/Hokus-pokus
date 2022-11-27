using DesiredServiceSystem;
using JunkManagerView;
using ServiceViewSystem;
using TimerViewSystem;
using UnityEngine;
using Zenject;

namespace Level
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private TimerViewManager.Settings _settingsTimerViewManager;
        [SerializeField]
        private ManagerJunkView.Settings _settingsManagerJunkView;
        [SerializeField]
        private ServiceViewManager.Settings _settingsServiceViewManager;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_settingsTimerViewManager);
            Container.BindInstance(_settingsManagerJunkView);
            Container.BindInstance(_settingsServiceViewManager);
            
            Container.Bind<ManagerJunkView>().AsSingle().NonLazy();
            Container.Bind<ServiceViewManager>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<TimerViewManager>().AsSingle().NonLazy();
        }
    }
}