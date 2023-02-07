using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal struct RaycastInfo
    {
        public enum ECastType
        {
            Raycast,
            Spherecast,
            Boxcast
        }

        public readonly Vector3 from;
        public readonly Vector3 direction;
        public readonly float distance;
        public readonly bool isSingle;
        public readonly PhysicsCastResult result;
        public readonly ECastType castType;

        public RaycastInfo(
            Vector3 from,
            Vector3 direction,
            float distance,
            bool isSingle,
            PhysicsCastResult result,
            ECastType castType)
        {
            this.from = from;
            this.direction = direction;
            this.distance = distance;
            this.isSingle = isSingle;
            this.result = result;
            this.castType = castType;
        }
    }
}

