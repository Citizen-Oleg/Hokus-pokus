using System;
using UnityEngine;

namespace BuildingSystem
{
    [Serializable]
    public struct InstallBuildingSettings
    {
        public BuildingType BuildingType;
        public Transform Position;
    }
}