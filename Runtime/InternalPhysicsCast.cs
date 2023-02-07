using SimpleMan.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    internal static class InternalPhysicsCast
    {
        public static event Action<RaycastInfo> OnRaycast;
        public static event Action<SpherecastInfo> OnSpherecast;
        public static event Action<BoxcastInfo> OnBoxcast;
        public static event Action<SphereOverlapInfo> OnSphereOverlap;
        public static event Action<BoxOverlapInfo> OnBoxOverlap;




        public static PhysicsCastResult Raycast(
            Vector3 from,
            Vector3 direction,
            float distance,
            bool singleCast,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            PhysicsCastResult result = singleCast ?
                RaycastSingle(from, direction, distance, collidersToIgnore, layerMask) :
                RaycastComplex(from, direction, distance, collidersToIgnore, layerMask);

            OnRaycast?.Invoke(new RaycastInfo(from, direction, distance, singleCast, result));
            return result;
        }

        public static PhysicsCastResult Spherecast(
            Vector3 from,
            Vector3 direction,
            float distance,
            float radius,
            bool singleCast,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            PhysicsCastResult result = singleCast ?
                SphereCastSingle(from, direction, radius, distance, collidersToIgnore, layerMask) :
                SphereCastComplex(from, radius, direction, distance, collidersToIgnore, layerMask);

            OnSpherecast?.Invoke(new SpherecastInfo(from, direction, radius, distance, singleCast, result));
            return result;
        }

        public static PhysicsCastResult Boxcast(
            Vector3 from,
            Vector3 direction,
            Vector3 size,
            Quaternion orientation,
            float distance,
            bool singleCast,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            PhysicsCastResult result = singleCast ?
                BoxCastSingle(from, size, direction, distance, orientation, collidersToIgnore, layerMask) :
                BoxCastComplex(from, size, direction, distance, orientation, collidersToIgnore, layerMask);

            OnBoxcast?.Invoke(new BoxcastInfo(from, direction, size, orientation, distance, singleCast, result));
            return result;
        }

        public static PhysicsOverlapResult SphereOverlap(
            Vector3 from,
            float radius,
            LayerMask mask,
            Collider[] collidersToIgnore)
        {
            PhysicsOverlapResult result;
            Collider[] detectedColliders = UnityEngine.Physics.OverlapSphere(from, radius, mask);

            if (collidersToIgnore.NotExist() || collidersToIgnore.Length == 0)
            {
                result = new PhysicsOverlapResult(detectedColliders);
            }
            else
            {
                result = new PhysicsOverlapResult(
                collidersToIgnore.Where(x => !collidersToIgnore.Contains(x)).ToArray());
            }

            OnSphereOverlap?.Invoke(new SphereOverlapInfo(from, radius, result));
            return result;
        }

        public static PhysicsOverlapResult BoxOverlap(
            Vector3 from,
            Vector3 size,
            Quaternion rotation,
            LayerMask mask,
            Collider[] collidersToIgnore)
        {
            PhysicsOverlapResult result;
            Collider[] detectedColliders = UnityEngine.Physics.OverlapBox(from, size * 0.5f, rotation, mask);
            if (collidersToIgnore.NotExist() || collidersToIgnore.Length == 0)
            {
                result = new PhysicsOverlapResult(detectedColliders);
            }
            else
            {
                result = new PhysicsOverlapResult(
                collidersToIgnore.Where(x => !collidersToIgnore.Contains(x)).ToArray());
            }

            OnBoxOverlap?.Invoke(new BoxOverlapInfo(from, size, rotation, result));
            return result;
        }

        private static PhysicsCastResult RaycastComplex(
            Vector3 from,
            Vector3 direction,
            float distance,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            RaycastHit[] hitsArray = Physics.RaycastAll(
                from,
                direction,
                distance,
                layerMask);

            if(hitsArray.Length == 0) 
                return new PhysicsCastResult();

            if(collidersToIgnore.Exist() && collidersToIgnore.Length > 0)
                hitsArray = hitsArray.Where(x => !collidersToIgnore.Contains(x.collider)).ToArray();

            hitsArray = PhysicsUtilities.OrderHitsByDistance(from, hitsArray);
            return new PhysicsCastResult(hitsArray);
        }

        private static PhysicsCastResult RaycastSingle(
            Vector3 from,
            Vector3 direction,
            float distance,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            if(UnityEngine.Physics.Raycast(
                from,
                direction,
                out RaycastHit hit,
                distance,
                layerMask))
            {
                if(collidersToIgnore.Exist() && collidersToIgnore.Contains(hit.collider))
                    return new PhysicsCastResult();

                return new PhysicsCastResult(new RaycastHit[] { hit });
            }

            return new PhysicsCastResult();
        }

        private static PhysicsCastResult SphereCastSingle(
            Vector3 from,
            Vector3 direction,
            float radius,
            float distance,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            RaycastHit hit;
            if (Physics.SphereCast(from, radius, direction, out hit, distance, layerMask))
            {
                if (collidersToIgnore != null && collidersToIgnore.Contains(hit.collider))
                    return new PhysicsCastResult();

                return new PhysicsCastResult(new RaycastHit[] { hit });
            }
            return new PhysicsCastResult();
        }

        private static PhysicsCastResult SphereCastComplex(
            Vector3 from,
            float radius,
            Vector3 direction,
            float distance,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            RaycastHit[] hitsArray = Physics.SphereCastAll(
                from,
                radius,
                direction,
                distance,
                layerMask);

            if (hitsArray.Length == 0)
                return new PhysicsCastResult();

            if (collidersToIgnore.Exist() && collidersToIgnore.Length > 0)
                hitsArray = hitsArray.Where(x => !collidersToIgnore.Contains(x.collider)).ToArray();

            hitsArray = PhysicsUtilities.OrderHitsByDistance(from, hitsArray);
            return new PhysicsCastResult(hitsArray);
        }

        private static PhysicsCastResult BoxCastSingle(
            Vector3 from,
            Vector3 size,
            Vector3 direction,
            float distance,
            Quaternion orientation,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            RaycastHit hit;
            if (Physics.BoxCast(from, size * 0.5f, direction, out hit, orientation, distance, layerMask))
            {
                if (collidersToIgnore != null && collidersToIgnore.Contains(hit.collider))
                    return new PhysicsCastResult();

                 return new PhysicsCastResult(new RaycastHit[] { hit });
            }
            return new PhysicsCastResult();
        }

        private static PhysicsCastResult BoxCastComplex(
            Vector3 from,
            Vector3 size,
            Vector3 direction,
            float distance,
            Quaternion orientation,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            RaycastHit[] hitsArray = Physics.BoxCastAll(
                from,
                size * 0.5f,
                direction,
                orientation,
                distance,
                layerMask);

            if (hitsArray.Length == 0)
                return new PhysicsCastResult();

            if (collidersToIgnore.Exist() && collidersToIgnore.Length > 0)
                hitsArray = hitsArray.Where(x => !collidersToIgnore.Contains(x.collider)).ToArray();

            hitsArray = PhysicsUtilities.OrderHitsByDistance(from, hitsArray);
            return new PhysicsCastResult(hitsArray);
        }
    }
}

