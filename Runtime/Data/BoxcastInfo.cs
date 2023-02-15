using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    internal struct BoxcastInfo
    {
        public readonly Vector3 from;
        public readonly Vector3 direction;
        public readonly Vector3 size;
        public readonly Quaternion orientation;
        public readonly float distance;
        public readonly bool isSingle;
        public readonly PhysicsCastResult result;

        public BoxcastInfo(
            Vector3 from,
            Vector3 direction,
            Vector3 size,
            Quaternion orientation,
            float distance,
            bool isSingle,
            PhysicsCastResult result) : this()
        {
            this.from = from;
            this.direction = direction;
            this.size = size;
            this.orientation = orientation;
            this.distance = distance;
            this.isSingle = isSingle;
            this.result = result;
        }
    }
}

