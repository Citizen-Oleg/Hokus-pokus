using System;
using UnityEngine;

namespace Base.MoveAnimation._3D
{
    [Serializable]
    public class AnimationSettings
    {
        public float TravelTime => _travelTime;
        public AnimationCurve YPositionCurve => _yPositionCurve;

        [SerializeField]
        private float _travelTime;
        [SerializeField]
        private AnimationCurve _yPositionCurve;
    }
}