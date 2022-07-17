using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public static class ComponentExtension
    {
        private const float DEFAULT_MAX_DISTANCE = float.MaxValue;
        private const int DEFAULT_LAYER_MASK = ~0;
        private const bool DEFAULT_CAST_ALL = false;

        #region RAYCAST
        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, float maxDistance, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float maxDistance, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, mask, castAll, ignoreSelf);
        }

        public static CastResult Raycast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeRaycast(component, new Ray(originPoint, direction), maxDistance, mask, castAll, ignoreSelf);
        }

        private static CastResult MakeRaycast(Component sender, Ray ray, float maxDistance, LayerMask mask, bool castAll, bool ignoreSelf)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance, mask);
            CastResult result = CalculateResult(hits, sender, !castAll, ignoreSelf);

            VisualCastDrawer.Instance?.DrawGizmo(new RaycastInfo(ray, maxDistance, castAll, result));
            return result;
        }
        #endregion

        #region BOXCAST
        public static CastResult Boxcast(this Component component, Vector3 originPoint, Vector3 direction, Vector3 boxSize, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, boxSize, component.transform.rotation, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf); 
        }

        public static CastResult Boxcast(this Component component, Vector3 originPoint, Vector3 direction, Vector3 boxSize, float maxDistance, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), maxDistance, boxSize, component.transform.rotation, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, Vector3 boxSize, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, boxSize, component.transform.rotation, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, Vector3 boxSize, float maxDistance, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), maxDistance, boxSize, component.transform.rotation, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, Vector3 originPoint, Vector3 direction, Vector3 boxSize, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, boxSize, component.transform.rotation, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, Vector3 originPoint, Vector3 direction, Vector3 boxSize, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), maxDistance, boxSize, component.transform.rotation, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, Vector3 boxSize, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, boxSize, component.transform.rotation, mask, castAll, ignoreSelf);
        }

        public static CastResult Boxcast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, Vector3 boxSize, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeBoxcast(component, new Ray(originPoint, direction), maxDistance, boxSize, component.transform.rotation, mask, castAll, ignoreSelf);
        }

        private static CastResult MakeBoxcast(Component sender, Ray ray, float maxDistance, Vector3 size, Quaternion rotation, LayerMask mask, bool castAll, bool ignoreSelf)
        {
            RaycastHit[] hits = Physics.BoxCastAll(ray.origin, size * 0.5f, ray.direction, rotation, maxDistance, mask);
            CastResult result = CalculateResult(hits, sender, !castAll, ignoreSelf);

            VisualCastDrawer.Instance?.DrawGizmo(new BoxcastInfo(ray, size, rotation, maxDistance, castAll, result));
            return result;
        }
        #endregion

        #region SPHERECAST
        public static CastResult SphereCast(this Component component, Vector3 originPoint, Vector3 direction, float radius, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, radius, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, Vector3 originPoint, Vector3 direction, float radius, float maxDistance, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), maxDistance, radius, DEFAULT_LAYER_MASK, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult Spherecast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float radius, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, radius, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float radius, float maxDistance, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), maxDistance, radius, DEFAULT_LAYER_MASK, castAll, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, Vector3 originPoint, Vector3 direction, float radius, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, radius, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, Vector3 originPoint, Vector3 direction, float radius, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), maxDistance, radius, mask, DEFAULT_CAST_ALL, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float radius, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), DEFAULT_MAX_DISTANCE, radius, mask, castAll, ignoreSelf);
        }

        public static CastResult SphereCast(this Component component, bool castAll, Vector3 originPoint, Vector3 direction, float radius, float maxDistance, LayerMask mask, bool ignoreSelf = true)
        {
            return MakeSpherecast(component, new Ray(originPoint, direction), maxDistance, radius, mask, castAll, ignoreSelf);
        }

        private static CastResult MakeSpherecast(Component sender, Ray ray, float maxDistance, float radius, LayerMask mask, bool castAll, bool ignoreSelf)
        {
            RaycastHit[] hits = Physics.SphereCastAll(ray, radius, maxDistance, mask);
            CastResult result = CalculateResult(hits, sender, !castAll, ignoreSelf);

            VisualCastDrawer.Instance?.DrawGizmo(new SpherecastInfo(ray, radius, maxDistance, castAll, result));
            return result;
        }
        #endregion

        #region BOX OVERLAP
        public static Collider[] BoxOverlap(this Component sender, Vector3 center, Vector3 boxSize, Quaternion rotation, LayerMask layerMask, bool ignoreSelf = true)
        {
            return MakeBoxOverlap(sender, center, boxSize, rotation, layerMask, ignoreSelf);
        }

        public static Collider[] BoxOverlap(this Component sender, Vector3 center, Vector3 boxSize, LayerMask layerMask, bool ignoreSelf = true)
        {
            return MakeBoxOverlap(sender, center, boxSize, sender.transform.rotation, layerMask, ignoreSelf);
        }

        public static Collider[] BoxOverlap(this Component sender, Vector3 center, Vector3 boxSize, Quaternion rotation, bool ignoreSelf = true)
        {
            return MakeBoxOverlap(sender, center, boxSize, rotation, DEFAULT_LAYER_MASK, ignoreSelf);
        }

        public static Collider[] BoxOverlap(this Component sender, Vector3 center, Vector3 boxSize, bool ignoreSelf = true)
        {
            return MakeBoxOverlap(sender, center, boxSize, sender.transform.rotation, DEFAULT_LAYER_MASK, ignoreSelf);
        }

        private static Collider[] MakeBoxOverlap(Component sender, Vector3 center, Vector3 boxSize, Quaternion rotation, LayerMask layerMask, bool ignoreSelf)
        {
            Collider[] colliders = Physics.OverlapBox(center, boxSize * 0.5f, rotation, layerMask);
            CalculateOverlapResult(ref colliders, sender, ignoreSelf);

            VisualCastDrawer.Instance?.DrawGizmo(new BoxOverlapInfo(center, boxSize, rotation, colliders));
            return colliders;
        }
        #endregion

        #region SPHERE OVERLAP
        public static Collider[] SphereOverlap(this Component sender, Vector3 center, float radius, LayerMask layerMask, bool ignoreSelf = true)
        {
            return MakeSphereOverlap(sender, center, radius, layerMask, ignoreSelf);
        }

        public static Collider[] SphereOverlap(this Component sender, Vector3 center, float radius, bool ignoreSelf = true)
        {
            return MakeSphereOverlap(sender, center, radius, DEFAULT_LAYER_MASK, ignoreSelf);
        }

        private static Collider[] MakeSphereOverlap(Component sender, Vector3 center, float radius, LayerMask layerMask, bool ignoreSelf)
        {
            Collider[] colliders = Physics.OverlapSphere(center, radius, layerMask);
            CalculateOverlapResult(ref colliders, sender, ignoreSelf);

            VisualCastDrawer.Instance?.DrawGizmo(new SphereOverlapInfo(center, radius, colliders));
            return colliders;
        }
        #endregion

        private static CastResult CalculateResult(RaycastHit[] hits, Component sender, bool fistHitOnly, bool ignoreSelf)
        {
            LinkedList<RaycastHit> hitsList = new LinkedList<RaycastHit>(hits);
            hits = null;

            if (ignoreSelf)
            {
                Transform itemTransform;
                Transform senderTransform;
                List<RaycastHit> toRemove = new List<RaycastHit>();

                foreach (var item in hitsList)
                {
                    itemTransform = item.transform;
                    senderTransform = sender.transform;
                    if (itemTransform == senderTransform || itemTransform.IsChildOf(sender.transform))
                        toRemove.Add(item);
                }

                foreach (var item in toRemove)
                {
                    hitsList.Remove(item);
                }
            }

            if (fistHitOnly && hitsList.Count > 1)
                return new CastResult(hitsList.First.Value);

            else
                return new CastResult(hitsList.ToArray());
        }

        private static void CalculateOverlapResult(ref Collider[] hits, Component sender, bool ignoreSelf)
        {
            if (ignoreSelf)
            {
                LinkedList<Collider> hitsList = new LinkedList<Collider>(hits);
                List<Collider> selfHits = new List<Collider>();
                hits = null;


                Transform itemTransform;
                Transform senderTransform;


                foreach (var item in hitsList)
                {
                    itemTransform = item.transform;
                    senderTransform = sender.transform;
                    if (itemTransform == senderTransform || itemTransform.IsChildOf(sender.transform))
                        selfHits.Add(item);
                }

                foreach (var item in selfHits)
                {
                    hitsList.Remove(item);
                }

                hits = hitsList.ToArray();
            }
        }
    }
}
