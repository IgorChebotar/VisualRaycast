using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    public struct BoxOverlapDeclaration
    {
        public struct FromData
        {
            public struct SizeData
            {
                public struct LayerMaskData
                {
                    public struct RotationData
                    {
                        internal readonly Vector3 from;
                        internal readonly Vector3 size;
                        internal readonly LayerMask mask;
                        internal readonly Quaternion rotation;

                        public RotationData(Vector3 from, Vector3 size, LayerMask mask, Quaternion rotation)
                        {
                            this.from = from;
                            this.size = size;
                            this.mask = mask;
                            this.rotation = rotation;
                        }

                        public PhysicsOverlapResult DontIgnoreAnything()
                        {
                            return InternalPhysicsCast.BoxOverlap(from, size, rotation, mask, null);
                        }

                        public PhysicsOverlapResult IgnoreColliders(params Collider[] collidersToIgnore)
                        {
                            return InternalPhysicsCast.BoxOverlap(from, size, rotation, mask, collidersToIgnore);
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
                    internal readonly Vector3 size;
                    internal readonly LayerMask mask;

                    public LayerMaskData(Vector3 from, Vector3 size, LayerMask mask)
                    {
                        this.from = from;
                        this.size = size;
                        this.mask = mask;
                    }

                    public PhysicsOverlapResult ContinueWithDefaultParams()
                    {
                        return new RotationData(from, size, mask, Quaternion.identity).DontIgnoreAnything();
                    }

                    public RotationData UseRotation(Quaternion rotation)
                    {
                        return new RotationData(from, size, mask, rotation);
                    }

                    public RotationData UseRotationFrom(Transform target)
                    {
                        if (target.NotExist())
                        {
                            throw new ArgumentNullException(nameof(target));
                        }

                        return UseRotation(target.rotation);
                    }

                    public RotationData UseRotationFrom(GameObject target)
                    {
                        if (target.NotExist())
                        {
                            throw new ArgumentNullException(nameof(target));
                        }

                        return UseRotationFrom(target.transform);
                    }
                }

                internal readonly Vector3 from;
                internal readonly Vector3 size;

                public SizeData(
                    Vector3 from,
                    Vector3 size)
                {
                    this.from = from;
                    this.size = size;
                }

                public PhysicsOverlapResult ContinueWithDefaultParams()
                {
                    LayerMaskData layerMaskData = new LayerMaskData(
                        from,
                        size,
                        UnityEngine.Physics.DefaultRaycastLayers);

                    return layerMaskData.ContinueWithDefaultParams();
                }

                public LayerMaskData UseDefaultLayerMask()
                {
                    return UseCustomLayerMask(UnityEngine.Physics.DefaultRaycastLayers);
                }

                public LayerMaskData UseCustomLayerMask(LayerMask mask)
                {
                    return new LayerMaskData(
                        from,
                        size,
                        mask);
                }
            }



            internal readonly Vector3 from;

            public FromData(Vector3 from)
            {
                this.from = from;
            }

            public SizeData WithSize(Vector3 size)
            {
                return new SizeData(from, size);
            }

            public SizeData WithExtents(Vector3 extents)
            {
                return WithSize(extents * 0.5f);
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

