using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public struct SphereOverlapDeclaration
    {
        public struct SetRadius
        {
            public struct SetLayerMask
            {
                public struct SetIgnoredColliders
                {
                    internal Vector3 originPoint;
                    internal LayerMask mask;
                    internal float radius;

                    public SetIgnoredColliders(Vector3 originPoint, float radius, LayerMask mask)
                    {
                        this.originPoint = originPoint;
                        this.radius = radius;
                        this.mask = mask;
                    }

                    public PhysicsOverlapResult DontIgnoreAnything()
                    {
                        return InternalPhysicsCast.SphereOverlap(
                            originPoint,
                            radius,
                            mask,
                            null);
                    }

                    public PhysicsOverlapResult IgnoreColliders(params Collider[] collidersToIgnore)
                    {
                        return InternalPhysicsCast.SphereOverlap(
                            originPoint,
                            radius,
                            mask,
                            collidersToIgnore);
                    }

                    public PhysicsOverlapResult IgnoreObjects(params GameObject[] objectsToIgnore)
                    {
                        return InternalPhysicsCast.SphereOverlap(
                            originPoint,
                            radius,
                            mask,
                            GetCollidersFromObjects(objectsToIgnore));
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




                internal Vector3 originPoint;
                internal float radius;

                public SetLayerMask(Vector3 originPoint, float radius)
                {
                    this.originPoint = originPoint;
                    this.radius = radius;
                }

                public PhysicsOverlapResult ContinueWithDefaultParams()
                {
                    return UseDefaultLayerMask().DontIgnoreAnything();
                }

                public SetIgnoredColliders UseDefaultLayerMask()
                {
                    return new SetIgnoredColliders(originPoint, radius, Physics.DefaultRaycastLayers);
                }

                public SetIgnoredColliders UseCustomLayerMask(LayerMask mask)
                {
                    return new SetIgnoredColliders(originPoint, radius, mask);
                }
            }




            internal Vector3 originPoint;

            public SetRadius(Vector3 originPoint)
            {
                this.originPoint = originPoint;
            }

            public SetLayerMask WithRadius(float radius)
            {
                return new SetLayerMask(originPoint, radius);
            }
        }




        public SetRadius FromMainCamera()
        {
            return FromCamera(Camera.main);
        }

        public SetRadius FromCamera(Camera point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return new SetRadius(point.transform.position);
        }

        public SetRadius FromPointInWorld(Vector3 point)
        {
            return new SetRadius(point);
        }

        public SetRadius FromGameObjectInWorld(Transform point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromPointInWorld(point.position);
        }

        public SetRadius FromGameObjectInWorld(GameObject point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromGameObjectInWorld(point.transform);
        }
    }
}

