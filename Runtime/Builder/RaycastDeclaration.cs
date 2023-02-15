using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisibleRaycast
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

            public SetHitMode ToDirection(Vector3 direction)
            {
                return new SetHitMode(from, direction);
            }

            public SetHitMode ToPointInWorld(Vector3 point)
            {
                return new SetHitMode(from, point - from);
            }

            public SetHitMode ToGameObjectInWorld(Transform point)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position);
            }

            public SetHitMode ToGameObjectInWorld(GameObject point)
            {
                return ToGameObjectInWorld(point.transform);
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

            public SetHitMode ToForward()
            {
                return new SetHitMode(
                    from.transform.position,
                    from.transform.forward);
            }

            public SetHitMode ToMousePositionInWorld()
            {
                Ray ray = from.ScreenPointToRay(Input.mousePosition);

                return new SetHitMode(
                    ray.origin,
                    ray.direction);
            }

            public SetHitMode ToPointInWorld(Vector3 point)
            {
                Vector3 cameraPosition = from.transform.position;
                Vector3 direction = point - cameraPosition;

                return new SetHitMode(
                    cameraPosition,
                    direction);
            }

            public SetHitMode ToGameObjectInWorld(Transform point)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position);
            }

            public SetHitMode ToGameObjectInWorld(GameObject point)
            {
                return ToGameObjectInWorld(point.transform);
            }
        }

        public struct SetHitMode
        {
            public struct SetMaxDistance
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
                internal readonly bool singleHit;

                public SetMaxDistance(Vector3 from, Vector3 direction, bool singleHit)
                {
                    this.from = from;
                    this.direction = direction;
                    this.singleHit = singleHit;
                }

                public PhysicsCastResult ContinueWithDefaultParams()
                {
                    return WithMaxDistance().ContinueWithDefaultParams();
                }

                public SetLayer WithMaxDistance()
                {
                    return WithDistance(float.PositiveInfinity);
                }

                public SetLayer WithDistance(float distance)
                {
                    return new SetLayer(from, direction, distance, singleHit);
                }
            }
            

            internal readonly Vector3 from;
            internal readonly Vector3 direction;





            public SetHitMode(
                Vector3 from,
                Vector3 direction)
            {
                this.from = from;
                this.direction = direction;
            }

            public PhysicsCastResult ContinueWithDefaultParams()
            {
                return SingleHit().ContinueWithDefaultParams();
            }

            public SetMaxDistance SingleHit()
            {
                return new SetMaxDistance(
                    from,
                    direction,
                    true);
            }

            public SetMaxDistance MultiHits()
            {
                return new SetMaxDistance(
                    from,
                    direction,
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

