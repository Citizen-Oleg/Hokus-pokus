using PlayerComponent;
using UnityEngine;
using Zenject;

namespace StaffSystem
{
    public class StaffGarbageInstaller : MinerStaffInstaller
    {
        [SerializeField]
        private CollectItemHandler _collectItemHandler;
        
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.BindInstance(_collectItemHandler).AsSingle().NonLazy();
        }
    }
}