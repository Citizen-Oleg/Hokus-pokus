using System;
using BuildingSystem.CashSystem;
using Events;
using Tools.SimpleEventBus;
using UniRx;
using UnityEngine;
using VisitorSystem.Spawner;
using Random = UnityEngine.Random;

namespace VisitorSystem
{
    public class VisitorManager : IDisposable
    {
        public event Action<Visitor, ServiceZone> OnSetServiceVisitor;
        
        private readonly CompositeDisposable _subscription;
        private readonly VisitorSpawner _visitorSpawner;
        private readonly ServiceOrganigram _serviceOrganigram;
        
        public VisitorManager(VisitorSpawner visitorSpawner, ServiceOrganigram serviceOrganigram)
        {
            _subscription = new CompositeDisposable
            {
                EventStreams.UserInterface.Subscribe<EventServedVisitor>(SetVisitorPointMovement),
                EventStreams.UserInterface.Subscribe<EventServedVisitorNextPoint>(SetVisitorNextPointMovement)
                
            };
            _serviceOrganigram = serviceOrganigram;
            _visitorSpawner = visitorSpawner;
            _visitorSpawner.OnSpawnVisitor += SetSpawnVisitorPointMovement;
        }

        private void SetVisitorPointMovement(EventServedVisitor eventServedVisitor)
        {
            SetVisitorPointMovement(eventServedVisitor.Visitor, eventServedVisitor.ServiceZone,eventServedVisitor.ServiceType);
        } 
        
        private void SetVisitorNextPointMovement(EventServedVisitorNextPoint eventServedVisitor)
        {
            SetVisitorNextPointMovement(eventServedVisitor.Visitor, eventServedVisitor.ServiceZone,eventServedVisitor.ServiceType);
        }
        
        private void SetSpawnVisitorPointMovement(Visitor visitor)
        {
            var service = _serviceOrganigram.GetAvailableRandomServiceZone(ServiceType.Ticket);
            service.CashQueue.AddVisitor(visitor);
            visitor.VisitCounter.AddServiceVisit(ServiceType.Ticket);
            
            var point = service.CashQueue.GetPoint();
            visitor.SetDestination(point, PointType.Queue);
            
            OnSetServiceVisitor?.Invoke(visitor, service);
        }
        
        private void SetVisitorPointMovement(Visitor visitor, ServiceZone serviceZone, ServiceType serviceType = ServiceType.Ticket)
        {
            var service = GetServiceZone(visitor, serviceZone,serviceType);
            service.CashQueue.AddVisitor(visitor);
            visitor.VisitCounter.AddServiceVisit(serviceType);
            
            var point = service.CashQueue.GetPoint();
            visitor.SetDestination(point, PointType.Queue);
            
            OnSetServiceVisitor?.Invoke(visitor, service);
        }
        
        private void SetVisitorNextPointMovement(Visitor visitor, ServiceZone serviceZone, ServiceType serviceType = ServiceType.Ticket)
        {
            var service = GetServiceZone(visitor, serviceZone,serviceType);
            service.CashQueue.AddVisitor(visitor);
            visitor.VisitCounter.AddServiceVisit(serviceType);
            
            var point = service.CashQueue.GetPoint();
            visitor.SetNextPoint(point, PointType.Queue);
            
            OnSetServiceVisitor?.Invoke(visitor, service);
        }

        private ServiceZone GetServiceZone(Visitor visitor, ServiceZone serviceZone, ServiceType serviceType)
        {
            if (visitor.Inventory.IsFull || 
                 !_serviceOrganigram.DoesVisitorHaveRightToEnterService(visitor, serviceType) || 
                 !_serviceOrganigram.IsTheServiceAvailable(serviceType, serviceZone))
            {
                return _serviceOrganigram.GetRandomFollowingServiceZone(serviceType);
            }
            
            var randomNumber = Random.Range(0, 2);
            return randomNumber switch
            {
                0 => _serviceOrganigram.GetAvailableRandomServiceZone(serviceType, serviceZone),
                1 => _serviceOrganigram.GetRandomFollowingServiceZone(serviceType),
                _ => _serviceOrganigram.GetRandomFollowingServiceZone(serviceType)
            };
        }

        public void Dispose()
        {
            _subscription?.Dispose();
            _visitorSpawner.OnSpawnVisitor -= SetSpawnVisitorPointMovement;
        }
    }
}