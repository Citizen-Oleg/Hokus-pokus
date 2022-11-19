using System.Collections.Generic;
using BuildingSystem.CashSystem;

namespace VisitorSystem
{
    public class VisitCounter
    {
        private readonly Dictionary<ServiceType, int> _serviceVisit = new Dictionary<ServiceType, int>();

        public void AddServiceVisit(ServiceType serviceType)
        {
            CheckContainsKey(serviceType);
            _serviceVisit[serviceType]++;
        }

        public int GetNumberVisitsToTheService(ServiceType serviceType)
        {
            CheckContainsKey(serviceType);
            return _serviceVisit[serviceType];
        }

        public void ResetVisits()
        {
            for (var i = 0; i < _serviceVisit.Count; i++)
            {
                _serviceVisit[(ServiceType) i] = 0;
            }
        }

        private void CheckContainsKey(ServiceType serviceType)
        {
            if (!_serviceVisit.ContainsKey(serviceType))
            {
                _serviceVisit.Add(serviceType, 0);
            }
        }
    }
}