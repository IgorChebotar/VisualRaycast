using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal struct SphereOverlapInfo
    {
        public readonly Vector3 center;
        public readonly float radius;
        public readonly PhysicsOverlapResult result;

        public SphereOverlapInfo(
            Vector3 center,
            float radius,
            PhysicsOverlapResult result)
        {
            this.center = center;
            this.radius = radius;
            this.result = result;
        }
    }
}

