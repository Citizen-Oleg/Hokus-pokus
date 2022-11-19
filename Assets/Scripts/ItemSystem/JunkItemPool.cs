using System;
using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;
using Pools;
using UnityEngine;

namespace ItemSystem
{
    public class JunkItemPool : DefaultItemPool
    {
        public JunkItemPool(Settings settings)
        {
            var parent = new GameObject
            {
                name = "ResourcePool"
            };

            foreach (var resourceItem in settings.Items)
            {
                var pool = new DefaultMonoBehaviourPool<Item>(resourceItem, parent.transform, settings.PullSize);
                _monoBehaviourPools.Add(resourceItem.ItemType, pool);
            }
        }

        [Serializable]
        public class Settings
        {
            public int PullSize;
            public List<Item> Items = new List<Item>();
        }
    }
}