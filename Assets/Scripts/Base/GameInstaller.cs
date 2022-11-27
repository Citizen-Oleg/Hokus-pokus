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
using ResourceSystem.FactoryResources;
using ServiceViewSystem;
using VisitorSystem.Spawner;
using Zenject;

namespace Base
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IconDesiredProvider>().AsSingle().NonLazy();
            Container.Bind<IconServiceProvider>().AsSingle().NonLazy();
            Container.Bind<ColorPayButtonProvider>().AsSingle().NonLazy();
            Container.Bind<ProfessionIconProvider>().AsSingle().NonLazy();
            Container.Bind<ServiceIconProvider>().AsSingle().NonLazy();
            Container.Bind<StaffIconProvider>().AsSingle().NonLazy();
            Container.Bind<ServiceNameProvider>().AsSingle().NonLazy();
            Container.Bind<StaffProvider>().AsSingle().NonLazy();
            
            Container.Bind<ItemPool>().AsSingle().NonLazy();
            Container.Bind<ScreenManager>().AsSingle().NonLazy();
            Container.Bind<ResourceManagerGame>().AsSingle().NonLazy();
            Container.Bind<VisitorPool>().AsSingle().WithArguments(transform).NonLazy();
            Container.Bind<JunkItemPool>().AsSingle().NonLazy();

            Container.BindFactory<ItemType, Item, ItemFactory>();
            Container.BindFactory<ItemType, JunkItem, JunkItemFactory>();
            
            Container.BindInterfacesAndSelfTo<AnimationManagerUI>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AnimationManager>().AsSingle().NonLazy();
        }
    }
}
