﻿using System;
using System.Collections.Generic;
 using Base.SimpleEventBus_and_MonoPool;
 using ItemSystem;
 using ResourceSystem;
 using Tools.SimpleEventBus;
 using UnityEngine;

namespace Pools
{
    public class ItemPool : DefaultItemPool
    {
        public ItemPool(SettingsItemPool settings)
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
    }
    
    [Serializable]
    public class SettingsItemPool
    {
        public int PullSize;
        public List<Item> Items = new List<Item>();
    }
}