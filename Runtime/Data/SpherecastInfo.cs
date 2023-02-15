using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    internal struct SpherecastInfo
    {
        public readonly Vector3 from;
        public readonly Vector3 direction;
        public readonly float radius;
        public readonly float distance;
        public readonly bool isSingle;
        public readonly PhysicsCastResult result;

        public SpherecastInfo(
            Vector3 from,
            Vector3 direction,
            float radius,
            float distance,
            bool isSingle,
            PhysicsCastResult result)
        {
            this.from = from;
            this.direction = direction;
            this.radius = radius;
            this.distance = distance;
            this.isSingle = isSingle;
            this.result = result;
        }
    }
}

