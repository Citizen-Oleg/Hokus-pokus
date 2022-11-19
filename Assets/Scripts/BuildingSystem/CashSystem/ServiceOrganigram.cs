using System;
using System.Collections.Generic;
using System.Linq;
using ItemSystem;
using VisitorSystem;
using Random = UnityEngine.Random;

namespace BuildingSystem.CashSystem
{
    public class ServiceOrganigram
    {
        private readonly Dictionary<ServiceType, List<ServiceZone>> _dictionary =
            new Dictionary<ServiceType, List<ServiceZone>>();
        
        private readonly Dictionary<ItemType, List<ProvisionZone>> _provisions = 
            new Dictionary<ItemType, List<ProvisionZone>>();

        private readonly List<OrganigramSettings> _organigramSettingses;
        
        public ServiceOrganigram(Settings settings)
        {
            _organigramSettingses = settings.ServiceSequence;
            foreach (var organigramSettingse in settings.ServiceSequence)
            {
                if (organigramSettingse.ServiceType == ServiceType.Product)
                {
                    FillProvisionDictionary(organigramSettingse.ServiceZones);
                }
                else
                {
                    _dictionary.Add(organigramSettingse.ServiceType, organigramSettingse.ServiceZones);
                }
            }
        }

        private void FillProvisionDictionary(List<ServiceZone> serviceZones)
        {
            foreach (var serviceZone in serviceZones)
            {
                if (serviceZone is ProvisionZone provisionZone)
                {
                    if (!_provisions.ContainsKey(provisionZone.SoldItem))
                    {
                        var provisions = new List<ProvisionZone> { provisionZone };
                        _provisions.Add(provisionZone.SoldItem, provisions);
                        continue;
                    }
                    
                    _provisions[provisionZone.SoldItem].Add(provisionZone);
                }
            }
        }

        public bool DoesVisitorHaveRightToEnterService(Visitor visitor, ServiceType serviceType)
        {
            var settingsService = _organigramSettingses.FirstOrDefault(settings => settings.ServiceType == serviceType);
            var numberVisit = visitor.VisitCounter.GetNumberVisitsToTheService(serviceType);
            return settingsService.NumberVisits > numberVisit;
        }

        public bool IsTheFollowingServiceAvailable(ServiceType serviceType)
        {
            var followingType = (ServiceType) ((int) serviceType + 1);

            if (followingType == ServiceType.Product)
            {
                return IsProvisionServiceAvailable();
            }
            
            return _dictionary.ContainsKey(followingType) && 
                   _dictionary[followingType].Any(service => service.IsAvailable);
        }
        
        public bool IsTheServiceAvailable(ServiceType serviceType, ServiceZone ignoreZone = null)
        {
            if (serviceType == ServiceType.Product)
            {
                return IsProvisionServiceAvailable(ignoreZone);
            }

            return ignoreZone == null ?
                _dictionary[serviceType].Any(service => service.IsAvailable) :
                _dictionary[serviceType].Any(service => service.IsAvailable && !service.Equals(ignoreZone));
        }

        public ServiceZone GetRandomFollowingServiceZone(ServiceType serviceType)
        {
            var serviceFollowing = GetFollowingServiceZones(serviceType);
            var service = GetRandomServiceZone(serviceFollowing);

            if (!service.IsAvailable)
            {
                return GetRandomFollowingServiceZone(serviceType);
            }
            
            return service;
        }
        
        public ServiceZone GetAvailableRandomServiceZone(ServiceType serviceType, ServiceZone ignoreZone = null)
        {
            var service = GetRandomServiceZone(serviceType);
            
            if (ignoreZone != null)
            {
                if (service.Equals(ignoreZone) || !service.IsAvailable)
                {
                    return GetAvailableRandomServiceZone(serviceType, ignoreZone);
                }
            }
            else
            {
                if (!service.IsAvailable)
                {
                    return GetAvailableRandomServiceZone(serviceType, ignoreZone);
                }
            }

            return service;
        }

        private bool IsProvisionServiceAvailable(ServiceZone ignoreService = null)
        {
            bool isAvailable;
            bool isEquals;
            foreach (var keyValuePair in _provisions)
            {
                isAvailable = false;
                isEquals = false;
                foreach (var provisionZone in keyValuePair.Value)
                {
                    if (provisionZone.IsAvailable)
                    {
                        if (ignoreService != null && ignoreService.Equals(provisionZone))
                        {
                            isEquals = true;
                            continue;
                        }
                        
                        isAvailable = true;
                    }
                    break;
                }

                if (!isAvailable && !isEquals)
                {
                    return isAvailable;
                }
            }

            return true;
        }

        private ServiceType GetFollowingServiceZones(ServiceType serviceType)
        {
            return (ServiceType) ((int) serviceType + 1);
        }

        private ServiceZone GetRandomServiceZone(ServiceType serviceType)
        {
            if (serviceType == ServiceType.Product)
            {
                var provisions = _provisions[GetRandomType(_provisions.Count)];
                return provisions[GetRandomIndex(provisions.Count)];
            }

            var services = _dictionary[serviceType];
            return services[GetRandomIndex(services.Count)];
        }

        private int GetRandomIndex(int maxNumber)
        {
            return Random.Range(0, maxNumber);
        }

        private ItemType GetRandomType(int maxNumber)
        {
            var index = GetRandomIndex(maxNumber);

            return index switch
            {
                0 => ItemType.Burger,
                1 => ItemType.Soda,
                _ => ItemType.Burger
            };
        }

        [Serializable]
        public class Settings
        {
            public List<OrganigramSettings> ServiceSequence = new List<OrganigramSettings>();
        }

        [Serializable]
        public class OrganigramSettings
        {
            public int NumberVisits;
            public ServiceType ServiceType;
            public List<ServiceZone> ServiceZones = new List<ServiceZone>();
        }
    }
}