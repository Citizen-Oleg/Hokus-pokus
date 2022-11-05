using Base.MoveAnimation.UI;
using Managers.ScreensManager;
using Zenject;

namespace Base
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ScreenManager>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<AnimationManagerUI>().AsSingle().NonLazy();
        }
    }
}
