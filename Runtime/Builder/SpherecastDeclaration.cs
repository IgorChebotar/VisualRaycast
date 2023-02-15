using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    public struct SpherecastDeclaration
    {
        public struct SetDirectionFromWorld
        {
            internal readonly Vector3 originPosition;

            public SetDirectionFromWorld(Vector3 value)
            {
                originPosition = value;
            }

            public SetHitMode ToDirection(Vector3 direction)
            {
                return new SetHitMode(originPosition, direction);
            }

            public SetHitMode ToPointInWorld(Vector3 point)
            {
                return new SetHitMode(originPosition, point - originPosition);
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
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToGameObjectInWorld(point.transform);
            }
        }

        public struct SetDirectionFromCamera
        {
            internal readonly Camera camera;

            public SetDirectionFromCamera(Camera from)
            {
                if (from.NotExist())
                {
                    throw new ArgumentNullException(nameof(from));
                }
                this.camera = from;
            }

            public SetHitMode ToForward()
            {
                return new SetHitMode(
                    camera.transform.position,
                    camera.transform.forward);
            }

            public SetHitMode ToMousePositionInWorld()
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                return new SetHitMode(
                    ray.origin,
                    ray.direction);
            }

            public SetHitMode ToPointInWorld(Vector3 point)
            {
                Vector3 cameraPosition = camera.transform.position;
                Vector3 direction = point - cameraPosition;

                return new SetHitMode(cameraPosition, direction);
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
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToGameObjectInWorld(point.transform);
            }
        }

        public struct SetHitMode
        {
            public struct SetRadius
            {
                public struct SetMaxDistance
                {
                    public struct SetLayerMask
                    {
                        public struct SetIgnoredColliders
                        {
                            internal Vector3 originPoint;
                            internal Vector3 direction;
                            internal LayerMask mask;
                            internal float radius;
                            internal bool singleHit;
                            internal float maxDistance;

                            public SetIgnoredColliders(Vector3 originPoint, Vector3 direction, float radius, LayerMask mask, bool singleHit, float maxDistance)
                            {
                                this.originPoint = originPoint;
                                this.direction = direction;
                                this.radius = radius;
                                this.mask = mask;
                                this.singleHit = singleHit;
                                this.maxDistance = maxDistance;
                            }

                            public PhysicsCastResult DontIgnoreAnything()
                            {
                                return InternalPhysicsCast.Spherecast(
                                    originPoint,
                                    direction,
                                    maxDistance,
                                    radius,
                                    singleHit,
                                    null,
                                    mask);

                            }

                            public PhysicsCastResult IgnoreColliders(params Collider[] collidersToIgnore)
                            {
                                return InternalPhysicsCast.Spherecast(
                                    originPoint,
                                    direction,
                                    maxDistance,
                                    radius,
                                    singleHit,
                                    collidersToIgnore,
                                    mask);
                            }

                            public PhysicsCastResult IgnoreObjects(params GameObject[] objectsToIgnore)
                            {
                                return InternalPhysicsCast.Spherecast(
                                    originPoint,
                                    direction,
                                    maxDistance,
                                    radius,
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




                        internal Vector3 originPoint;
                        internal Vector3 direction;
                        internal float radius;
                        internal bool singleHit;
                        internal float maxDistance;

                        public SetLayerMask(Vector3 originPoint, Vector3 direction, float radius, bool singleHit, float maxDistance)
                        {
                            this.originPoint = originPoint;
                            this.direction = direction;
                            this.radius = radius;
                            this.singleHit = singleHit;
                            this.maxDistance = maxDistance;
                        }

                        public PhysicsCastResult ContinueWithDefaultParams()
                        {
                            return UseDefaultLayerMask().DontIgnoreAnything();
                        }

                        public SetIgnoredColliders UseDefaultLayerMask()
                        {
                            return new SetIgnoredColliders(
                                originPoint,
                                direction,
                                radius,
                                Physics.DefaultRaycastLayers,
                                singleHit,
                                maxDistance);
                        }

                        public SetIgnoredColliders UseCustomLayerMask(LayerMask mask)
                        {
                            return new SetIgnoredColliders(
                                originPoint,
                                direction,
                                radius,
                                mask,
                                singleHit,
                                maxDistance);
                        }
                    }




                    internal Vector3 originPoint;
                    internal Vector3 direction;
                    internal float radius;
                    internal bool singleHit;

                    public SetMaxDistance(Vector3 originPoint, Vector3 direction, float radius, bool singleHit)
                    {
                        this.originPoint = originPoint;
                        this.direction = direction;
                        this.radius = radius;
                        this.singleHit = singleHit;
                    }

                    public PhysicsCastResult ContinueWithDefaultParams()
                    {
                        return WithMaxDistace().ContinueWithDefaultParams();
                    }

                    public SetLayerMask WithMaxDistace()
                    {
                        return new SetLayerMask(
                            originPoint,
                            direction,
                            radius,
                            singleHit,
                            float.PositiveInfinity);
                    }

                    public SetLayerMask WithDistance(float value)
                    {
                        return new SetLayerMask(
                            originPoint,
                            direction,
                            radius,
                            singleHit,
                            value);
                    }
                }



                internal Vector3 originPoint;
                internal Vector3 direction;
                internal bool singleHit;

                public SetRadius(Vector3 originPoint, Vector3 direction, bool singleHit)
                {
                    this.originPoint = originPoint;
                    this.direction = direction;
                    this.singleHit = singleHit;
                }

                public PhysicsCastResult ContinueWithDefaultParams()
                {
                    return WithRadius(0.5f).ContinueWithDefaultParams();
                }

                public SetMaxDistance WithRadius(float radius)
                {
                    return new SetMaxDistance(originPoint, direction, radius, singleHit);
                }
            }



            internal Vector3 originPoint;
            internal Vector3 direction;

            public SetHitMode(Vector3 originPoint, Vector3 direction)
            {
                this.originPoint = originPoint;
                this.direction = direction;
            }

            public PhysicsCastResult ContinueWithDefaultParams()
            {
                return SingleHit().ContinueWithDefaultParams();
            }

            public SetRadius SingleHit()
            {
                return new SetRadius(originPoint, direction, true);
            }

            public SetRadius MultiHit()
            {
                return new SetRadius(originPoint, direction, false);
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

