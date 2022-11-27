using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using ItemSystem;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class ServiceNameProvider
    {
        private List<ServiceName> _serviceIcons;
        private List<ProductServiceName> _productServiceIcons;
        
        public ServiceNameProvider(Settings settings)
        {
            _serviceIcons = settings.ServiceIcons;
            _productServiceIcons = settings.ProductServiceIcons;
        }

        public string GetNameByService(ServiceZone serviceZone)
        {
            if (serviceZone is ProvisionZone provisionZone)
            {
                return _productServiceIcons.FirstOrDefault(product => product.ItemType == provisionZone.SoldItem)
                    .Name;
            }

            return _serviceIcons.FirstOrDefault(service => service.ServiceType == serviceZone.ServiceType).Name;
        }
        
        [Serializable]
        public class Settings
        {
            public List<ServiceName> ServiceIcons = new List<ServiceName>();
            public List<ProductServiceName> ProductServiceIcons = new List<ProductServiceName>();
        }

        [Serializable]
        public struct ProductServiceName
        {
            public string Name;
            public ItemType ItemType;
        }

        [Serializable]
        public struct ServiceName
        {
            public string Name;
            public ServiceType ServiceType;
        }
    }
}