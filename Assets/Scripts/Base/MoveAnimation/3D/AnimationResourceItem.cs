using System;
using UnityEngine;

namespace Base.MoveAnimation._3D
{
    public struct AnimationResourceItem
    {
        public Transform Item;
        public Vector3 StartPosition;
        public Quaternion StartRotation;
        public Transform EndTransformPosition;
        public float Progress;
        public Action CallBack;
    }
}