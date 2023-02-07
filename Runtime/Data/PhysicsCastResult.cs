using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    /// <summary>
    /// Contains the result of a physics raycast.
    /// </summary>
    public struct PhysicsCastResult
    {
        /// <summary>
        /// Information about the hit objects in the raycast.
        /// </summary>
        public RaycastHit[] hits;

        public PhysicsCastResult(RaycastHit[] hitsInfo)
        {
            this.hits = hitsInfo;
        }

        /// <summary>
        /// Property indicating if the raycast hit at least one object.
        /// </summary>
        public bool wasHit => hits.Exist() && hits.Length > 0;
        public static bool operator true(PhysicsCastResult a) => a.wasHit;
        public static bool operator false(PhysicsCastResult a) => !a.wasHit;
        public static bool operator !(PhysicsCastResult a) => !a.wasHit;
    }

    /// <summary>
    /// Contains the result of a physics overlap cast.
    /// </summary>
    public struct PhysicsOverlapResult
    {
        /// <summary>
        /// Information about the hit objects in the raycast.
        /// </summary>
        public Collider[] detectedColliders;

        public PhysicsOverlapResult(Collider[] detectedColliders)
        {
            this.detectedColliders = detectedColliders;
        }

        /// <summary>
        /// Property indicating if the raycast hit at least one object.
        /// </summary>
        public bool wasHit => detectedColliders.Exist() && detectedColliders.Length > 0;
        public static bool operator true(PhysicsOverlapResult a) => a.wasHit;
        public static bool operator false(PhysicsOverlapResult a) => !a.wasHit;
        public static bool operator !(PhysicsOverlapResult a) => !a.wasHit;
    }
}

