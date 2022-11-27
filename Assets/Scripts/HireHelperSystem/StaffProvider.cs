using System;
using System.Collections.Generic;
using System.Linq;
using StaffSystem;
using Zenject;

namespace HireHelperSystem
{
    public class StaffProvider
    {
        private readonly DiContainer _diContainer;
        private readonly List<Staff> _staffPrefabs;
        
        public StaffProvider(DiContainer diContainer, Settings settings)
        {
            _diContainer = diContainer;
            _staffPrefabs = settings.StaffPrefabs;
        }
        
        public Staff GetStaffByStaffType(StaffType staffType)
        {
            var prefab = _staffPrefabs.FirstOrDefault(staff => staff.StaffType == staffType);
            return _diContainer.InstantiatePrefabForComponent<Staff>(prefab);
        }

        [Serializable]
        public class Settings
        {
            public List<Staff> StaffPrefabs = new List<Staff>();
        }
    }
}