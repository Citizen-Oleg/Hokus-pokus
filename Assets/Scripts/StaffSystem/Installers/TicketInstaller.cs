using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace StaffSystem
{
    public class TicketInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerAnimationController.Settings _settingsPlayerAnimationController;
        [SerializeField]
        private Staff _staff;
        [SerializeField]
        private AIMovementController.Settings _settingsAiMovementController;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerAnimationController>().AsSingle().NonLazy();
            
            Container.BindInstance(_settingsPlayerAnimationController).AsSingle().NonLazy();
            Container.BindInstance(_staff).AsSingle();
            Container.BindInstance(_settingsAiMovementController).AsSingle();
            
            Container.BindInterfacesAndSelfTo<DefaultStaffModuleController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AIMovementController>().AsSingle().NonLazy();
        }
    }
}