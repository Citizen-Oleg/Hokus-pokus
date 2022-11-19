using System.Collections.Generic;
using ItemSystem;
using UnityEngine;

namespace StaffSystem
{
    public class NearestItemProvider
    {
        public JunkItem GetNearestJunkItem(List<JunkItem> junkItems, Vector3 position) 
        {
            var items = junkItems;
            if (items == null || items.Count == 0)
            {
                return null;
            }
            
            var minDistance = float.MaxValue;
            var minIndex = 0;
            
            for (var index = 0; index < items.Count; index++)
            {
                var item = items[index];
                
                var distanceToTarget = Vector3.Distance(position, item.transform.position);

                if (minDistance > distanceToTarget)
                {
                    minDistance = distanceToTarget;
                    minIndex = index;
                }
            }
            
            return items[minIndex];
        }
    }
}