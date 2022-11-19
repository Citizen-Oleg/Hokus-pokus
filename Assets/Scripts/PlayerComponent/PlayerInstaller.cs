using Portal.PlayerComponent;
using UnityEngine;
using Zenject;

namespace PlayerComponent
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Player.Settings _playerSettings;
        [SerializeField]
        private InventorySettings _inventorySettings;
        [SerializeField]
        private Inventarizator _inventarizator;
        [SerializeField]
        private Player _player;
        [SerializeField]
        private PlayerAnimationController.Settings _settingsPlayerAnimationController;
        [SerializeField]
        private CollectItemHandler _collectItemHandler;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_playerSettings);
            Container.BindInstance(_inventorySettings);
            Container.BindInstance(_settingsPlayerAnimationController);
            Container.BindInstance(_collectItemHandler);
            
            Container.BindInstance(_inventarizator).AsSingle();
            Container.BindInstance(_player).AsSingle();
            
            Container.Bind<PlayerAnimationController>().AsSingle().NonLazy();
            Container.Bind<Inventory>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<MovementController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerModuleController>().AsSingle().NonLazy();
        }
    }
}