using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using ResourceSystem;
using StaffSystem;
using UnityEngine;

namespace HireHelperSystem
{
    public class HireHelperController
    {
        private readonly ResourceManagerGame _resourceManagerGame;
        private readonly HireHelperDataBaseInformation _hireHelperDataBaseInformation;
        private readonly StaffProvider _staffProvider;
        private readonly List<PriceStaff> _priceStaves;
        
        public HireHelperController(Settings settings, HireHelperDataBaseInformation hireHelperDataBaseInformation, 
            StaffProvider staffProvider, ResourceManagerGame resourceManagerGame)
        {
            _hireHelperDataBaseInformation = hireHelperDataBaseInformation;
            _priceStaves = settings.PriceStaves;
            _staffProvider = staffProvider;
            _resourceManagerGame = resourceManagerGame;
        }

        public bool HasBuyStaff(StaffType staffType)
        {
            return _resourceManagerGame.HasEnough(GetStaffPrice(staffType));
        }

        public Resource GetStaffPrice(StaffType staffType)
        {
            return _priceStaves.FirstOrDefault(priceStaff => priceStaff.StaffType == staffType).Price;
        }

        public void BuyStaff(StaffType staffType, ServiceZone serviceZone)
        {
            var staff = _staffProvider.GetStaffByStaffType(staffType);
            staff.Initialize(serviceZone);
            _hireHelperDataBaseInformation.RemoveNeedStaff(staffType, serviceZone);
            var price = GetStaffPrice(staffType);
            _resourceManagerGame.Pay(price);
        }

        [Serializable]
        public class Settings
        {
            public List<PriceStaff> PriceStaves = new List<PriceStaff>();
        }

        [Serializable]
        public struct PriceStaff
        {
            public StaffType StaffType;
            public Resource Price;
        }
    }
}