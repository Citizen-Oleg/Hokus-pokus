using System;
using System.Collections.Generic;
using Base;
using Base.SimpleEventBus_and_MonoPool;
using Tools.SimpleEventBus;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace VisitorSystem.Spawner
{
    public class VisitorPool
    {
        private readonly Dictionary<TypeVisitor, ZenjectMonoBehaviourPool<Visitor>> _monoBehaviourPools = 
            new Dictionary<TypeVisitor, ZenjectMonoBehaviourPool<Visitor>>();
        
        public VisitorPool(Settings settings, DiContainer diContainer, Transform transform)
        {
            foreach (var settingsVisitor in settings.Visitors)
            {
                var pool = new ZenjectMonoBehaviourPool<Visitor>(settingsVisitor, transform, diContainer, settings.PoolSize);
                _monoBehaviourPools.Add(settingsVisitor.TypeVisitor, pool);
            }
        }

        public Visitor GetRandomVisitor()
        {
            var visitor = _monoBehaviourPools[(TypeVisitor) GetRandomIndex()].Take();
            visitor.Initialize(this);
            return visitor;
        }

        private int GetRandomIndex()
        {
            return Random.Range(0, _monoBehaviourPools.Count);
        }

        public void Release(Visitor visitor)
        {
            _monoBehaviourPools[visitor.TypeVisitor].Release(visitor);
        }
        
        [Serializable]
        public class Settings
        {
            public int PoolSize = 5;
            public List<Visitor> Visitors = new List<Visitor>();
        }
    }
}