using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public struct RaycastDeclaration
    {
        public struct SetDirectionFromWorld
        {
            internal readonly Vector3 from;

            public SetDirectionFromWorld(Vector3 from)
            {
                this.from = from;
            }

            public SetHitMode ToDirection(Vector3 direction, float castDistance = float.PositiveInfinity)
            {
                return new SetHitMode(from, direction, castDistance);
            }

            public SetHitMode ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                return new SetHitMode(from, point - from, castDistance);
            }

            public SetHitMode ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public SetHitMode ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
            {
                return ToGameObjectInWorld(point.transform, castDistance);
            }
        }

        public struct SetDirectionFromCamera
        {
            internal readonly Camera from;

            public SetDirectionFromCamera(Camera from)
            {
                if (from.NotExist())
                {
                    throw new ArgumentNullException(nameof(from));
                }
                this.from = from;
            }

            public SetHitMode ToForward(float castDistance = float.PositiveInfinity)
            {
                return new SetHitMode(
                    from.transform.position,
                    from.transform.forward,
                    castDistance.ClampPositive());
            }

            public SetHitMode ToMousePositionInWorld(float castDistance = float.PositiveInfinity)
            {
                Ray ray = from.ScreenPointToRay(Input.mousePosition);

                return new SetHitMode(
                    ray.origin,
                    ray.direction,
                    castDistance.ClampPositive());
            }

            public SetHitMode ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                Vector3 cameraPosition = from.transform.position;
                Vector3 direction = point - cameraPosition;

                return new SetHitMode(
                    cameraPosition,
                    direction,
                    castDistance.ClampPositive());
            }

            public SetHitMode ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public SetHitMode ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
            {
                return ToGameObjectInWorld(point.transform, castDistance);
            }
        }

        public struct SetHitMode
        {
            public struct SetLayer
            {
                public struct SetIgnoredColliders
                {
                    internal readonly Vector3 from;
                    internal readonly Vector3 direction;
                    internal readonly float distance;
                    internal readonly bool singleHit;
                    internal readonly LayerMask mask;

                    public SetIgnoredColliders(
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



                internal readonly Vector3 from;
                internal readonly Vector3 direction;
                internal readonly float distance;
                internal readonly bool singleHit;

                public SetLayer(
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
                    SetIgnoredColliders layerMaskData = new SetIgnoredColliders(
                        from,
                        direction,
                        distance,
                        singleHit,
                        Physics.DefaultRaycastLayers);

                    return layerMaskData.DontIgnoreAnything();
                }

                public SetIgnoredColliders UseDefaultLayerMask()
                {
                    return UseCustomLayerMask(UnityEngine.Physics.DefaultRaycastLayers);
                }

                public SetIgnoredColliders UseCustomLayerMask(LayerMask mask)
                {
                    return new SetIgnoredColliders(
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





            public SetHitMode(
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
                SetLayer complexityData = new SetLayer(
                    from,
                    direction,
                    distance,
                    true);

                return complexityData.ContinueWithDefaultParams();
            }

            public SetLayer Single()
            {
                return new SetLayer(
                    from,
                    direction,
                    distance,
                    true);
            }

            public SetLayer MultiHits()
            {
                return new SetLayer(
                    from,
                    direction,
                    distance,
                    false);
            }
        }

        public SetDirectionFromCamera FromMainCamera()
        {
            return new SetDirectionFromCamera(Camera.main);
        }

        public SetDirectionFromCamera FromCamera(Camera point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return new SetDirectionFromCamera(point);
        }

        public SetDirectionFromWorld FromPointInWorld(Vector3 point)
        {
            return new SetDirectionFromWorld(point);
        }

        public SetDirectionFromWorld FromGameObjectInWorld(Transform point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromPointInWorld(point.position);
        }

        public SetDirectionFromWorld FromGameObjectInWorld(GameObject point)
        {
            return FromGameObjectInWorld(point.transform);
        }
    }
}

