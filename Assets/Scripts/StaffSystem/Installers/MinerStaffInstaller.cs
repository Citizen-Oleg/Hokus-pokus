using PlayerComponent;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace StaffSystem
{
    public class MinerStaffInstaller : MonoInstaller
    {
        [SerializeField]
        private PlayerAnimationController.Settings _settingsPlayerAnimationController;
        [SerializeField]
        private Staff _staff;
        [SerializeField]
        private AIMovementController.Settings _settingsAiMovementController;
        [SerializeField]
        private InventorySettings _inventorySettings;
        [SerializeField]
        private Inventarizator _inventarizator;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsPlayerAnimationController);
            
            Container.Bind<PlayerAnimationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryStaffModuleController>().AsSingle().NonLazy();
            
            Container.BindInstance(_staff).AsSingle();
            Container.BindInstance(_settingsAiMovementController).AsSingle();

            Container.BindInterfacesAndSelfTo<AIMovementController>().AsSingle().NonLazy();
            
            Container.BindInstance(_inventorySettings);
            Container.BindInstance(_inventarizator).AsSingle();

            Container.Bind<Inventory>().AsSingle().NonLazy();
        }
    }
}