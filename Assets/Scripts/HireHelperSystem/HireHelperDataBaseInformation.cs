using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using Events;
using StaffSystem;
using Tools.SimpleEventBus;
using UnityEngine;

namespace HireHelperSystem
{
    public class HireHelperDataBaseInformation : IDisposable
    {
        public List<ServiceInformation> ActiveService => _activeService;
        
        private readonly List<ServiceInformation> _activeService = new List<ServiceInformation>();

        private readonly List<ServiceInformation> _settingsServiceInformation;

        private readonly IDisposable _subscription;

        public HireHelperDataBaseInformation(Settings settings)
        {
            _subscription = EventStreams.UserInterface.Subscribe<EventNewServiceZone>(AddServiceInformation);

            _settingsServiceInformation = settings.SettingsServiceInformations;

            foreach (var serviceZone in settings.StartServiceZone)
            {
                AddServiceInformation(serviceZone);
            }
        }

        public void RemoveNeedStaff(StaffType staffType, ServiceZone serviceZone)
        {
            for (var i = 0; i < _activeService.Count; i++)
            {
                var service = _activeService[i].ServiceZone;
                if (serviceZone.Equals(service))
                {
                    _activeService[i].NeedStaff.Remove(staffType);

                    if (_activeService[i].NeedStaff.Count == 0)
                    {
                        _activeService.RemoveAt(i);
                    }
                    
                    break;
                }
            }
        }

        private void AddServiceInformation(EventNewServiceZone eventNewServiceZone)
        {
            AddServiceInformation(eventNewServiceZone.ServiceZone);
        }

        private void AddServiceInformation(ServiceZone serviceZone)
        {
            var information = GetServiceInformationByService(serviceZone);
            information.ServiceZone = serviceZone;
            _activeService.Add(information);
        }

        private ServiceInformation GetServiceInformationByService(ServiceZone serviceZone)
        {
            var oldInformation =
                _settingsServiceInformation.FirstOrDefault(service => service.ServiceType == serviceZone.ServiceType);
            
            var newInformation = new ServiceInformation
            {
                ServiceType = oldInformation.ServiceType,
                NeedStaff = new List<StaffType>(oldInformation.NeedStaff)
            };

            return newInformation;
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
        
        [Serializable]
        public class Settings
        {
            public List<ServiceZone> StartServiceZone = new List<ServiceZone>();
            public List<ServiceInformation> SettingsServiceInformations = new List<ServiceInformation>();
        }
        
        [Serializable]
        public class ServiceInformation
        {
            public ServiceType ServiceType;
            [HideInInspector]
            public ServiceZone ServiceZone;
            public List<StaffType> NeedStaff = new List<StaffType>();
        }
    }
}