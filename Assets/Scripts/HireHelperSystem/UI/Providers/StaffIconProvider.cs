using System;
using System.Collections.Generic;
using System.Linq;
using StaffSystem;
using UnityEngine;

namespace HireHelperSystem.UI
{
    public class StaffIconProvider
    {
        private readonly List<IconStaff> _iconStaves;
        
        public StaffIconProvider(Settings settings)
        {
            _iconStaves = settings.StaffIcons;
        }

        public Sprite GetSpriteByStaffType(StaffType staffType)
        {
            return _iconStaves.FirstOrDefault(staff => staff.StaffType == staffType).Sprite;
        }
        
        [Serializable]
        public class Settings
        {
            public List<IconStaff> StaffIcons = new List<IconStaff>();
        }
        
        [Serializable]
        public struct IconStaff
        {
            public StaffType StaffType;
            public Sprite Sprite;
        }
    }
}