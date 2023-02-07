using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public struct SphereOverlapDeclaration
    {
        public struct FromData
        {
            public struct RadiusData
            {
                public struct LayerMaskData
                {
                    internal readonly Vector3 from;
                    internal readonly float radius;
                    internal readonly LayerMask mask;

                    public LayerMaskData(
                        Vector3 from,
                        float radius,
                        LayerMask mask)
                    {
                        this.from = from;
                        this.radius = radius;
                        this.mask = mask;
                    }

                    public PhysicsOverlapResult DontIgnoreAnything()
                    {
                        return InternalPhysicsCast.SphereOverlap(from, radius, mask, null);
                    }

                    public PhysicsOverlapResult IgnoreColliders(params Collider[] collidersToIgnore)
                    {
                        return InternalPhysicsCast.SphereOverlap(from, radius, mask, collidersToIgnore);
                    }

                    public PhysicsOverlapResult IgnoreObjects(params GameObject[] objectsToIgnore)
                    {
                        return IgnoreColliders(GetCollidersFromObjects(objectsToIgnore));
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
                internal readonly float radius;

                public RadiusData(
                    Vector3 from,
                    float radius)
                {
                    this.from = from;
                    this.radius = radius;
                }

                public PhysicsOverlapResult ContinueWithDefaultParams()
                {
                    LayerMaskData layerMaskData = new LayerMaskData(
                        from,
                        radius,
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
                        radius,
                        mask);
                }
            }



            internal readonly Vector3 from;

            public FromData(Vector3 from)
            {
                this.from = from;
            }

            public RadiusData WithRadius(float radius)
            {
                if(radius <= 0)
                {
                    throw new ArgumentException(
                        "Radius of overlap sphere must be positive");
                }

                return new RadiusData(from, radius);
            }
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

