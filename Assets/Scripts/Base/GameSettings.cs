using Base.MoveAnimation.UI;
using Managers.ScreensManager;
using UnityEngine;
using Zenject;

namespace Base
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Settings/GameSettings", order = 0)]
    public class GameSettings : ScriptableObjectInstaller
    {
        [SerializeField]
        private AnimationManagerUI.Settings _settingsAnimationManagerUI;
        [SerializeField]
        private ScreenManager.Settings _settingsScreenManager;

        public override void InstallBindings()
        {
            Container.BindInstance(_settingsAnimationManagerUI);
            Container.BindInstance(_settingsScreenManager);
        }
    }
}