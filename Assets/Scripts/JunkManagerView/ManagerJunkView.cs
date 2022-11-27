using System;
using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;
using BuildingSystem.CashSystem;
using UnityEngine;
using Zenject;

namespace JunkManagerView
{
    public class ManagerJunkView
    {
        private readonly DefaultMonoBehaviourPool<ViewJunk> _viewJunkPool;
        private readonly Camera _camera;

        public ManagerJunkView(Settings settings)
        {
            _camera = Camera.main;
            _viewJunkPool = new DefaultMonoBehaviourPool<ViewJunk>(settings.PrefabView, settings.Canvas);
            
            foreach (var performanceService in settings.PerformanceServices)
            {
                performanceService.OnStartJunkCollect += ShowView;
            }
        }

        private void ShowView(PerformanceService performanceService)
        {
            foreach (var row in performanceService.CurrentRow)
            {
                if (!row.IsCleared)
                {
                    var view = _viewJunkPool.Take();
                    view.Initialize(_camera, row, this);
                }
            }
        }

        public void Release(ViewJunk viewJunk)
        {
            _viewJunkPool.Release(viewJunk);
        }

        [Serializable]
        public class Settings
        {
            public RectTransform Canvas;
            public ViewJunk PrefabView;
            public List<PerformanceService> PerformanceServices = new List<PerformanceService>();
        }
    }
}