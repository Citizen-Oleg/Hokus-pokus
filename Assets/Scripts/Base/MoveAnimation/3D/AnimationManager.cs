using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Base.MoveAnimation._3D
{
    public class AnimationManager : ITickable
    {
        private readonly float _travelTime;
        private readonly AnimationCurve _yPositionCurve;
        private readonly List<AnimationResourceItem> _animationResourceItems = new List<AnimationResourceItem>();
        
        public AnimationManager(AnimationSettings animationSettings)
        {
            _travelTime = animationSettings.TravelTime;
            _yPositionCurve = animationSettings.YPositionCurve;
        }

        public void ShowFlyingResource(Transform item, Transform endTransformPosition, Vector3 offSet, Action callBack)
        {
            _animationResourceItems.Add(new AnimationResourceItem
            {
                Item = item,
                StartPosition = item.transform.position,
                StartRotation = item.transform.rotation,
                CallBack = callBack,
                EndTransformPosition = endTransformPosition,
                Progress = 0,
                Offset = offSet
            });
        }
        
        public void Tick()
        {
            for (int i = 0; i < _animationResourceItems.Count; i++)
            {
                var positionYcurve = Vector3.zero;
                var information = _animationResourceItems[i];
                positionYcurve = _yPositionCurve.Evaluate(information.Progress) * Vector3.up;
                
                information.Progress += Time.deltaTime / _travelTime;

                var endTransform = information.EndTransformPosition;
                var endPosition = endTransform.TransformPoint(information.Offset);
                var positionItem = Vector3.Lerp(information.StartPosition, endPosition, information.Progress)
                                   + positionYcurve;
                information.Item.transform.position = positionItem;
                
                var rotationItem = Quaternion.Lerp(information.StartRotation, endTransform.rotation,
                    information.Progress);
                
                information.Item.transform.rotation = rotationItem;
              
                _animationResourceItems[i] = information;
                if (information.Progress >= 1)
                {
                    information.CallBack?.Invoke();
                    _animationResourceItems.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}