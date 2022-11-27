using System;
using System.Collections.Generic;
using BuildingSystem.CashSystem;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ServiceViewSystem
{
    public class ServiceViewManager
    {
        private readonly ServiceView _serviceView;
        private readonly RectTransform _canvas;
        private readonly Camera _camera;
        
        private readonly IconServiceProvider _iconServiceProvider;

        public ServiceViewManager(Settings settings, IconServiceProvider iconServiceProvider)
        {
            _iconServiceProvider = iconServiceProvider;
            _canvas = settings.Canvas;
            _serviceView = settings.ServiceView;
            
            _camera = Camera.main;

            foreach (var settingsServiceZone in settings.ServiceZones)
            {
                settingsServiceZone.OnActivate += CreateView;
            }
        }

        private void CreateView(ServiceZone serviceZone)
        {
            var icon = _iconServiceProvider.GetSpriteByService(serviceZone);
            var view = Object.Instantiate(_serviceView, _canvas);
            view.Initialize(_camera, icon, serviceZone);
        }
        
        [Serializable]
        public class Settings
        {
            public ServiceView ServiceView;
            public RectTransform Canvas;
            public List<ServiceZone> ServiceZones = new List<ServiceZone>();
        }
    }
}