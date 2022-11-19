using System;
using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;
using BuildingSystem;
using UnityEngine;
using Zenject;

namespace TimerViewSystem
{
    public class TimerViewManager : ITickable
    {
        private readonly DefaultMonoBehaviourPool<TimerView> _pool;
        private readonly List<TimerView> _activeTimer = new List<TimerView>();

        private readonly Camera _camera;

        public TimerViewManager(Settings settings)
        {
            _pool = new DefaultMonoBehaviourPool<TimerView>(settings.TimerView, settings.Canvas, settings.PoolSize);
            _camera = Camera.main;

            foreach (var settingsBuildingMiner in settings.BuildingMiners)
            {
                settingsBuildingMiner.OnActivate += CreateView;
            }
        }

        private void CreateView(BuildingMiner buildingMiner)
        {
            var view = _pool.Take();
            view.Initialize(_camera, buildingMiner);
            _activeTimer.Add(view);
        }
        
        public void Tick()
        {
            for (var i = 0; i < _activeTimer.Count; i++)
            {
                _activeTimer[i].UpdateUser();
            }
        }
        
        [Serializable]
        public class Settings
        {
            public int PoolSize;
            public TimerView TimerView;
            public Transform Canvas;
            public List<BuildingMiner> BuildingMiners = new List<BuildingMiner>();
        }
    }
}