using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class ServiceIconProvider
    {
        private readonly List<ServiceIcon> _serviceIcons;
        private readonly List<ProductServiceIcon> _productServiceIcons;
        
        public ServiceIconProvider(Settings settings)
        {
            _serviceIcons = settings.ServiceIcons;
            _productServiceIcons = settings.ProductServiceIcons;
        }

        public Sprite GetSpriteByServiceZone(ServiceZone serviceZone)
        {
            if (serviceZone is ProvisionZone provisionZone)
            {
                return _productServiceIcons.FirstOrDefault(product => product.ItemType == provisionZone.SoldItem)
                    .Sprite;
            }

            return _serviceIcons.FirstOrDefault(service => service.ServiceType == serviceZone.ServiceType).Sprite;
        }

        [Serializable]
        public class Settings
        {
            public List<ServiceIcon> ServiceIcons = new List<ServiceIcon>();
            public List<ProductServiceIcon> ProductServiceIcons = new List<ProductServiceIcon>();
        }
    }
}