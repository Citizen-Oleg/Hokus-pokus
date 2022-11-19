using PlayerComponent;
using UnityEngine;
using Zenject;

namespace VisitorSystem
{
    public class VisitorInstaller : MonoInstaller
    {
        [SerializeField]
        private VisitorInventory.Settings _inventorySettings;
        [SerializeField]
        private Inventarizator _inventarizator;
        [SerializeField]
        private Visitor _visitor;
        [SerializeField]
        private AIMovementController.Settings _settingsVisitorMovementController;
        [SerializeField]
        private Animator _animator;

        public override void InstallBindings()
        {
            Container.BindInstance(_inventarizator).AsSingle();
            Container.BindInstance(_visitor).AsSingle();
            Container.BindInstance(_inventorySettings).AsSingle();
            Container.BindInstance(_settingsVisitorMovementController).AsSingle();
            
            Container.Bind<VisitorInventory>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AIMovementController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<VisitorModuleController>().AsSingle().NonLazy();
            Container.Bind<VisitCounter>().AsSingle().NonLazy();
            Container.Bind<VisitorAnimationController>().AsSingle().WithArguments(_animator).NonLazy();
        }
    }
}