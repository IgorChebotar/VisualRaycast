using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    internal struct RaycastInfo
    {
        public readonly Vector3 from;
        public readonly Vector3 direction;
        public readonly float distance;
        public readonly bool isSingle;
        public readonly PhysicsCastResult result;

        public RaycastInfo(
            Vector3 from,
            Vector3 direction,
            float distance,
            bool isSingle,
            PhysicsCastResult result)
        {
            this.from = from;
            this.direction = direction;
            this.distance = distance;
            this.isSingle = isSingle;
            this.result = result;
        }
    }
}

