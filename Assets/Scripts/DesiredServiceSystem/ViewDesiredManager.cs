using System;
using System.Collections.Generic;
using Base.SimpleEventBus_and_MonoPool;
using BuildingSystem.CashSystem;
using ItemSystem;
using UnityEngine;
using VisitorSystem;
using Zenject;

namespace DesiredServiceSystem
{
    public class ViewDesiredManager : IDisposable
    {
        private readonly Dictionary<Visitor, ViewDesiredService> _visitors = new Dictionary<Visitor, ViewDesiredService>();
        
        private readonly DefaultMonoBehaviourPool<ViewDesiredService> _pool;
        private readonly VisitorManager _visitorManager;
        private readonly IconDesiredProvider _iconDesiredProvider;
        private readonly Camera _camera;

        public ViewDesiredManager(Settings settings, VisitorManager visitorManager, IconDesiredProvider iconDesiredProvider)
        {
            _camera = Camera.main;
            _visitorManager = visitorManager;
            _iconDesiredProvider = iconDesiredProvider;
            _pool = new DefaultMonoBehaviourPool<ViewDesiredService>(settings.ViewDesiredService, settings.Canvas, settings.PoolSize);
            _visitorManager.OnSetServiceVisitor += HandlerView;
        }

        private void HandlerView(Visitor visitor, ServiceZone serviceZone)
        {
            if (serviceZone.ServiceType == ServiceType.Exit || serviceZone.ServiceType == ServiceType.Performance)
            {
                if (_visitors.ContainsKey(visitor))
                {
                    var view = _visitors[visitor];
                    _visitors.Remove(visitor);
                    _pool.Release(view);
                }

                return;
            }

            if (!_visitors.ContainsKey(visitor))
            {
                var view = _pool.Take();
                _visitors.Add(visitor, view);
                view.Initialize(_camera, visitor);
            }

            var sprite = _iconDesiredProvider.GetSpriteBySpriteTypeDesired(GetSpriteTypeDesiredByServiceZone(serviceZone));
            _visitors[visitor].ChangeSprite(sprite);
        }

        private SpriteTypeDesired GetSpriteTypeDesiredByServiceZone(ServiceZone serviceZone)
        {
            if (serviceZone.ServiceType == ServiceType.Ticket)
            {
                return SpriteTypeDesired.Ticket;
            }

            if (serviceZone is ProvisionZone provisionZone)
            {
                switch (provisionZone.SoldItem)
                {
                    case ItemType.Burger:
                        return SpriteTypeDesired.Burger;
                    case ItemType.Soda:
                        return SpriteTypeDesired.Cola;
                }
            }

            return SpriteTypeDesired.Ticket;
        }
        
        public void Dispose()
        {
            _visitorManager.OnSetServiceVisitor -= HandlerView;
        }

        [Serializable]
        public class Settings
        {
            public int PoolSize;
            public Transform Canvas;
            public ViewDesiredService ViewDesiredService;
        }
    }
}