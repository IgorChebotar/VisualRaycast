using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal static class PhysicsUtilities
    {
        public static RaycastHit[] OrderHitsByDistance(Vector3 origin, RaycastHit[] hits)
        {
            ValidateHits(origin, hits);
            return hits.OrderBy(x => Vector3.Distance(origin, x.point)).ToArray();
        }

        public static void ValidateHits(Vector3 origin, RaycastHit[] hits)
        {
            for (int i = 0; i < hits.Length; i++)
                ValidateHit(origin, ref hits[i]);
        }

        public static void ValidateHit(Vector3 origin, ref RaycastHit hit)
        {
            if(hit.point != Vector3.zero) return;

            Vector3 closestPoint = hit.collider.ClosestPoint(origin);
            hit.point = closestPoint;
            hit.distance = Vector3.Distance(origin, closestPoint);
        }
    }
}

