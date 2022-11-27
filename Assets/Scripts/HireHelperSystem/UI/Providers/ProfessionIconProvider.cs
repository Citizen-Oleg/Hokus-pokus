using System;
using System.Collections.Generic;
using System.Linq;
using BuildingSystem.CashSystem;
using StaffSystem;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class ProfessionIconProvider
    {
        private readonly List<ProfessionIcon> _professionIcons;
        private readonly List<ProductServiceIcon> _productServiceIcons;
      
        public ProfessionIconProvider(Settings settings)
        {
            _professionIcons = settings.ProfessionIcons;
            _productServiceIcons = settings.ProductServiceIcons;
        }

        public Sprite GetSpriteByServiceZone(StaffType staffType, ServiceZone serviceZone)
        {
            if (serviceZone is ProvisionZone provisionZone)
            {
                return _productServiceIcons.FirstOrDefault(service => service.ItemType == provisionZone.SoldItem).Sprite;
            }
            
            return _professionIcons.FirstOrDefault(profession => profession.StaffType == staffType).Sprite;
        }

        [Serializable]
        public class Settings
        {
            public List<ProfessionIcon> ProfessionIcons = new List<ProfessionIcon>();
            public List<ProductServiceIcon> ProductServiceIcons = new List<ProductServiceIcon>();
        }

        [Serializable]
        public struct ProfessionIcon
        {
            public StaffType StaffType;
            public Sprite Sprite;
        }
    }
}