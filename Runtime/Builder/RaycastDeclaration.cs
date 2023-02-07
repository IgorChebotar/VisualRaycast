using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMan.VisualRaycast
{
    public struct RaycastDeclaration
    {
        public struct FromData
        {
            internal readonly Vector3 from;

            public FromData(Vector3 from)
            {
                this.from = from;
            }

            public ToData ToDirection(Vector3 direction, float castDistance = float.PositiveInfinity)
            {
                return new ToData(from, direction, castDistance);
            }

            public ToData ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                return new ToData(from, point - from, castDistance);
            }

            public ToData ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public ToData ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
            {
                return ToGameObjectInWorld(point.transform, castDistance);
            }
        }

        public struct FromCameraData
        {
            internal readonly Camera from;

            public FromCameraData(Camera from)
            {
                if (from.NotExist())
                {
                    throw new ArgumentNullException(nameof(from));
                }
                this.from = from;
            }

            public ToData ToForward(float castDistance = float.PositiveInfinity)
            {
                return new ToData(
                    from.transform.position,
                    from.transform.forward,
                    castDistance.ClampPositive());
            }

            public ToData ToMousePositionInWorld(float castDistance = float.PositiveInfinity)
            {
                Ray ray = from.ScreenPointToRay(Input.mousePosition);

                return new ToData(
                    ray.origin,
                    ray.direction,
                    castDistance.ClampPositive());
            }

            public ToData ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                Vector3 cameraPosition = from.transform.position;
                Vector3 direction = point - cameraPosition;

                return new ToData(
                    cameraPosition,
                    direction,
                    castDistance.ClampPositive());
            }

            public ToData ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public ToData ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
            {
                return ToGameObjectInWorld(point.transform, castDistance);
            }
        }

        public struct ToData
        {
            public struct ComplexityData
            {
                internal readonly Vector3 from;
                internal readonly Vector3 direction;
                internal readonly float distance;
                internal readonly bool singleHit;

                public struct LayerMaskData
                {
                    internal readonly Vector3 from;
                    internal readonly Vector3 direction;
                    internal readonly float distance;
                    internal readonly bool singleHit;
                    internal readonly LayerMask mask;

                    public LayerMaskData(
                        Vector3 from,
                        Vector3 direction,
                        float distance,
                        bool singleHit,
                        LayerMask mask)
                    {
                        this.from = from;
                        this.direction = direction;
                        this.distance = distance;
                        this.singleHit = singleHit;
                        this.mask = mask;
                    }

                    public PhysicsCastResult DontIgnoreAnything()
                    {
                        return InternalPhysicsCast.Raycast(
                            from,
                            direction,
                            distance,
                            singleHit,
                            null,
                            mask);
                    }

                    public PhysicsCastResult IgnoreColliders(params Collider[] collidersToIgnore)
                    {
                        return InternalPhysicsCast.Raycast(
                            from,
                            direction,
                            distance,
                            singleHit,
                            collidersToIgnore,
                            mask);
                    }

                    public PhysicsCastResult IgnoreObjects(params GameObject[] objectsToIgnore)
                    {
                        return InternalPhysicsCast.Raycast(
                            from,
                            direction,
                            distance,
                            singleHit,
                            GetCollidersFromObjects(objectsToIgnore),
                            mask);
                    }

                    private Collider[] GetCollidersFromObjects(GameObject[] objectsToIgnore)
                    {
                        if (objectsToIgnore.NotExist() || objectsToIgnore.Length == 0)
                            return null;

                        List<Collider> collidersToIgnore = new List<Collider>(128);
                        foreach (var gameObject in objectsToIgnore)
                        {
                            if (gameObject.NotExist())
                                continue;

                            collidersToIgnore.AddRange(gameObject.GetComponentsInChildren<Collider>());
                        }

                        return collidersToIgnore.ToArray();
                    }
                }

                public ComplexityData(
                        Vector3 from,
                        Vector3 direction,
                        float distance,
                        bool singleHit)
                {
                    this.from = from;
                    this.direction = direction;
                    this.distance = distance;
                    this.singleHit = singleHit;
                }

                public PhysicsCastResult ContinueWithDefaultParams()
                {
                    LayerMaskData layerMaskData = new LayerMaskData(
                        from,
                        direction,
                        distance,
                        singleHit,
                        UnityEngine.Physics.DefaultRaycastLayers);

                    return layerMaskData.DontIgnoreAnything();
                }

                public LayerMaskData UseDefaultLayerMask()
                {
                    return UseCustomLayerMask(UnityEngine.Physics.DefaultRaycastLayers);
                }

                public LayerMaskData UseCustomLayerMask(LayerMask mask)
                {
                    return new LayerMaskData(
                        from,
                        direction,
                        distance,
                        singleHit,
                        mask);
                }
            }

            internal readonly Vector3 from;
            internal readonly Vector3 direction;
            internal readonly float distance;

            

            

            public ToData(
                Vector3 from,
                Vector3 direction,
                float distance)
            {
                this.from = from;
                this.direction = direction;
                this.distance = distance;
            }

            public PhysicsCastResult ContinueWithDefaultParams()
            {
                ComplexityData complexityData = new ComplexityData(
                    from,
                    direction,
                    distance,
                    true);

                return complexityData.ContinueWithDefaultParams();
            }

            public ComplexityData Single()
            {
                return new ComplexityData(
                    from,
                    direction, 
                    distance,
                    true);
            }

            public ComplexityData Multiple()
            {
                return new ComplexityData(
                    from,
                    direction,
                    distance,
                    false);
            }
        }

        public FromCameraData FromMainCamera()
        {
            return new FromCameraData(Camera.main);
        }

        public FromCameraData FromCamera(Camera point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return new FromCameraData(point);
        }

        public FromData FromPointInWorld(Vector3 point)
        {
            return new FromData(point);
        }

        public FromData FromGameObjectInWorld(Transform point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromPointInWorld(point.position);
        }

        public FromData FromGameObjectInWorld(GameObject point)
        {
            return FromGameObjectInWorld(point.transform);
        }
    }
}

