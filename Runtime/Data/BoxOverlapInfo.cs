using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal struct BoxOverlapInfo
    {
        public readonly Vector3 center;
        public readonly Vector3 size;
        public readonly Quaternion rotation;
        public readonly PhysicsOverlapResult result;

        public BoxOverlapInfo(
            Vector3 center,
            Vector3 size,
            Quaternion rotation,
            PhysicsOverlapResult result)
        {
            this.center = center;
            this.size = size;
            this.result = result;
            this.rotation = rotation;
        }
    }
}

