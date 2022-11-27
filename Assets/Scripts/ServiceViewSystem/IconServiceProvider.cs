using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using DesiredServiceSystem;
using UnityEngine;

namespace ServiceViewSystem
{
    public class IconServiceProvider
    {
        private readonly List<IconService> _iconServices;
        private readonly IconDesiredProvider _iconDesiredProvider;
        
        public IconServiceProvider(Settings settings, IconDesiredProvider iconDesiredProvider)
        {
            _iconServices = settings.IconServices;
            _iconDesiredProvider = iconDesiredProvider;
        }
        
        public Sprite GetSpriteByService(ServiceZone serviceZone)
        {
            if (serviceZone is ProvisionZone provisionZone)
            {
                return _iconDesiredProvider.GetSpriteByItemType(provisionZone.SoldItem);
            }

            return _iconServices.FirstOrDefault(iconService => iconService.ServiceType == serviceZone.ServiceType).Sprite;
        }
        
        [Serializable]
        public class Settings
        {
            public List<IconService> IconServices = new List<IconService>();
        }
        
        [Serializable]
        public struct IconService
        {
            public Sprite Sprite;
            public ServiceType ServiceType;
        }
    }
}