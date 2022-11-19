using System;
using System.Collections.Generic;
using ResourceSystem;
using UnityEngine;

namespace PlayerComponent
{
    public class Inventarizator : MonoBehaviour
    {
        public Transform StartPositionResource => _startPositionResource;

        [SerializeField]
        private Transform _startPositionResource;
        [Range(1, 12)]
        [SerializeField]
        private int _numberOfLines = 1;
        [Range(1, 12)]
        [SerializeField]
        private int _amountResourcesLines = 1;
        [SerializeField]
        private float _offsetX;
        [SerializeField]
        private float _offsetY;
        [SerializeField]
        private float _offsetZ;

        public void InventarizationItem(Transform item, Transform endTransform, bool useStartParent = true)
        {
            item.transform.position = endTransform.position;
            item.transform.parent = useStartParent ? _startPositionResource.transform : endTransform;
            item.transform.localRotation = Quaternion.identity;
        }

        public void InventarizationSpecificIndex(Transform resourceItem, int index)
        {
            InventarizationSpecificOffset(resourceItem, GetOffSetByIndex(index));
        }
        
        public void InventarizationSpecificOffset(Transform resourceItem, Vector3 offset)
        {
            resourceItem.transform.parent = _startPositionResource.transform;
            resourceItem.transform.localPosition = offset;
            resourceItem.transform.localRotation = Quaternion.identity;
        }

        public Vector3 GetOffSetByIndex(int index)
        {
            var offsetY = index / (_numberOfLines * _amountResourcesLines) * _offsetY;
            var offsetX = index % _numberOfLines * _offsetX;
            var offsetZ = index / _numberOfLines % _amountResourcesLines * _offsetZ;

            return new Vector3(offsetX, offsetY, offsetZ);
        }
    }
}