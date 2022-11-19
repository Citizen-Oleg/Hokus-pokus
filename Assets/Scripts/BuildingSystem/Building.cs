using UnityEngine;

namespace BuildingSystem
{
    public abstract class Building : MonoBehaviour
    {
        public BuildingType BuildingType => _buildingType;
        
        [SerializeField]
        private BuildingType _buildingType;
    }
}