using System;
using System.Collections.Generic;
using System.Linq;
using Events;
using ItemSystem;
using Tools.SimpleEventBus;
using UnityEngine;
using VisitorSystem;
using Random = UnityEngine.Random;

namespace BuildingSystem.CashSystem
{
    public class ServiceOrganigram : IDisposable
    {
        private readonly Dictionary<ServiceType, List<ServiceZone>> _dictionary =
            new Dictionary<ServiceType, List<ServiceZone>>();
        
        private readonly Dictionary<ItemType, List<ProvisionZone>> _provisions = 
            new Dictionary<ItemType, List<ProvisionZone>>();

        private readonly List<OrganigramSettings> _organigramSettingses;

        private readonly List<ServiceZone> _availableServise = new List<ServiceZone>();
        private readonly IDisposable _subscription;

        public ServiceOrganigram(Settings settings)
        {
            _organigramSettingses = settings.ServiceSequence;
            _subscription = EventStreams.UserInterface.Subscribe<EventNewServiceZone>(AddService);
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

        private void AddService(EventNewServiceZone eventNewServiceZone)
        {
            var serviceZone = eventNewServiceZone.ServiceZone;
            if (serviceZone.ServiceType == ServiceType.Product && serviceZone is ProvisionZone provisionZone)
            {
                if (_provisions.ContainsKey(provisionZone.SoldItem))
                {
                    _provisions[provisionZone.SoldItem].Add(provisionZone);
                }
                else
                {
                    _provisions.Add(provisionZone.SoldItem, new List<ProvisionZone> { provisionZone });
                }
            }
            else
            {
                if (_dictionary.ContainsKey(serviceZone.ServiceType))
                {
                    _dictionary[serviceZone.ServiceType].Add(serviceZone);
                }
                else
                {
                    _dictionary.Add(serviceZone.ServiceType, new List<ServiceZone> { serviceZone });
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

            return _dictionary[followingType].Count == 0 ?
                IsTheFollowingServiceAvailable(serviceType) : 
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

            if (serviceFollowing != ServiceType.Product && _dictionary[serviceFollowing].Count == 0)
            {
                return GetRandomFollowingServiceZone(serviceType);
            }
            
            var service = GetRandomServiceZone(serviceFollowing);

            if (!service.IsAvailable)
            {
                return GetRandomFollowingServiceZone(serviceType);
            }
            
            return service;
        }
        
        public ServiceZone GetAvailableRandomServiceZone(ServiceType serviceType, ServiceZone ignoreZone = null)
        {
            var service = GetRandomServiceZone(serviceType, ignoreZone);
            return service;
        }

        private bool IsProvisionServiceAvailable(ServiceZone ignoreService = null)
        {
            ItemType ignoreItem = ItemType.Burger;
            if (ignoreService is ProvisionZone provisionZoneIgnore)
            {
                ignoreItem = provisionZoneIgnore.SoldItem;
            }
            
            foreach (var keyValuePair in _provisions)
            {
                if (ignoreService != null && keyValuePair.Key == ignoreItem)
                {
                    continue;
                }

                if (keyValuePair.Value.TrueForAll(zone => !zone.IsAvailable))
                {
                    return false;
                }
            }

            return true;
        }

        private ServiceType GetFollowingServiceZones(ServiceType serviceType)
        {
            return (ServiceType) ((int) serviceType + 1);
        }

        private ServiceZone GetRandomServiceZone(ServiceType serviceType, ServiceZone ignoreService = null)
        {
            FillAvailableServices(serviceType, ignoreService);
            var randomIndex = GetRandomIndex(_availableServise.Count);

            var services = _availableServise[randomIndex];
            return services;
        }

        private void FillAvailableServices(ServiceType serviceType, ServiceZone ignoreService = null)
        {
            _availableServise.Clear();

            if (serviceType == ServiceType.Product)
            {
                FillProductService(ignoreService);
                return;
            }
            
            foreach (var serviceZone in _dictionary[serviceType])
            {
                if (serviceZone.IsAvailable)
                {
                    _availableServise.Add(serviceZone);
                }
            }
        }

        private void FillProductService(ServiceZone ignoreService = null)
        {
            foreach (var keyValuePair in _provisions)
            {
                foreach (var provisionZone in keyValuePair.Value)
                {
                    if (ignoreService != null && provisionZone.Equals(ignoreService))
                    {
                        continue;
                    }
                    
                    if (provisionZone.IsAvailable)
                    {
                        _availableServise.Add(provisionZone);
                    }
                }
            }
        }

        private int GetRandomIndex(int maxNumber)
        {
            return Random.Range(0, maxNumber);
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

        public void Dispose()
        {
            _subscription?.Dispose();
        }
    }
}