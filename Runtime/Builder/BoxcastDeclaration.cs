using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    public struct BoxcastDeclaration
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
            public struct SetSize
            {
                public struct SetOrientation
                {
                    public struct SetMaxDistance
                    {
                        public struct SetLayerMask
                        {
                            public struct SetIgnoredColliders
                            {
                                internal Vector3 originPoint;
                                internal Vector3 direction;
                                internal Vector3 size;
                                internal Quaternion orientation;
                                internal LayerMask mask;
                                internal bool singleHit;
                                internal float maxDistance;

                                public SetIgnoredColliders(Vector3 originPoint, Vector3 direction, Vector3 size, Quaternion orientation, LayerMask mask, bool singleHit, float maxDistance)
                                {
                                    this.originPoint = originPoint;
                                    this.direction = direction;
                                    this.size = size;
                                    this.orientation = orientation;
                                    this.mask = mask;
                                    this.singleHit = singleHit;
                                    this.maxDistance = maxDistance;
                                }

                                public PhysicsCastResult DontIgnoreAnything()
                                {
                                    return InternalPhysicsCast.Boxcast(
                                        originPoint,
                                        direction,
                                        size,
                                        orientation,
                                        maxDistance,
                                        singleHit,
                                        null,
                                        mask);

                                }

                                public PhysicsCastResult IgnoreColliders(params Collider[] collidersToIgnore)
                                {
                                    return InternalPhysicsCast.Boxcast(
                                        originPoint,
                                        direction,
                                        size,
                                        orientation,
                                        maxDistance,
                                        singleHit,
                                        collidersToIgnore,
                                        mask);
                                }

                                public PhysicsCastResult IgnoreObjects(params GameObject[] objectsToIgnore)
                                {
                                    return InternalPhysicsCast.Boxcast(
                                        originPoint,
                                        direction,
                                        size,
                                        orientation,
                                        maxDistance,
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
                            internal Vector3 size;
                            internal Quaternion orientation;
                            internal bool singleHit;
                            internal float maxDistance;

                            public SetLayerMask(Vector3 originPoint, Vector3 direction, Vector3 size, Quaternion orientation, bool singleHit, float maxDistance)
                            {
                                this.originPoint = originPoint;
                                this.direction = direction;
                                this.size = size;
                                this.orientation = orientation;
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
                                    size,
                                    orientation,
                                    Physics.DefaultRaycastLayers,
                                    singleHit,
                                    maxDistance);
                            }

                            public SetIgnoredColliders UseCustomLayerMask(LayerMask mask)
                            {
                                return new SetIgnoredColliders(
                                    originPoint,
                                    direction,
                                    size,
                                    orientation,
                                    mask,
                                    singleHit,
                                    maxDistance);
                            }
                        }




                        internal Vector3 originPoint;
                        internal Vector3 direction;
                        internal Vector3 size;
                        internal Quaternion orientation;
                        internal bool singleHit;

                        public SetMaxDistance(Vector3 originPoint, Vector3 direction, Vector3 size, Quaternion orientation, bool singleHit)
                        {
                            this.originPoint = originPoint;
                            this.direction = direction;
                            this.size = size;
                            this.orientation = orientation;
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
                                size,
                                orientation,
                                singleHit,
                                float.PositiveInfinity);
                        }

                        public SetLayerMask WithDistance(float value)
                        {
                            return new SetLayerMask(
                                originPoint,
                                direction,
                                size,
                                orientation,
                                singleHit,
                                value);
                        }
                    }



                    internal Vector3 originPoint;
                    internal Vector3 direction;
                    internal Vector3 size;
                    internal bool singleHit;

                    public SetOrientation(Vector3 originPoint, Vector3 direction, Vector3 size, bool singleHit)
                    {
                        this.originPoint = originPoint;
                        this.direction = direction;
                        this.size = size;
                        this.singleHit = singleHit;
                    }

                    public PhysicsCastResult ContinueWithDefaultParams()
                    {
                        return WithDefaultRotation().ContinueWithDefaultParams();
                    }

                    public SetMaxDistance WithDefaultRotation()
                    {
                        return new SetMaxDistance(
                            originPoint,
                            direction,
                            size,
                            Quaternion.identity,
                            singleHit);
                    }

                    public SetMaxDistance WithRotation(Quaternion value)
                    {
                        return new SetMaxDistance(
                            originPoint,
                            direction,
                            size,
                            value,
                            singleHit);
                    }

                    public SetMaxDistance WithRotationOf(Transform target)
                    {
                        return new SetMaxDistance(
                            originPoint,
                            direction,
                            size,
                            GetTargetRotationOrDefault(),
                            singleHit);



                        Quaternion GetTargetRotationOrDefault()
                        {
                            if (target.NotExist())
                            {
                                PrintToConsole.Warning(
                                    Constants.PLUGIN_DISPLAYED_NAME,
                                    $"Argument of function '{nameof(WithRotationOf)}' doesn't exist. " +
                                    $"The process will continue with default rotation.");

                                return Quaternion.identity;
                            }

                            return target.rotation;
                        }
                    }

                    public SetMaxDistance WithRotationOf(GameObject target)
                    {
                        return new SetMaxDistance(
                            originPoint,
                            direction,
                            size,
                            GetTargetRotationOrDefault(),
                            singleHit);




                        Quaternion GetTargetRotationOrDefault()
                        {
                            if (target.NotExist())
                            {
                                PrintToConsole.Warning(
                                    Constants.PLUGIN_DISPLAYED_NAME,
                                    $"Argument of function '{nameof(WithRotationOf)}' doesn't exist. " +
                                    $"The process will continue with default rotation.");

                                return Quaternion.identity;
                            }

                            return target.transform.rotation;
                        }
                    }
                }



                internal Vector3 originPoint;
                internal Vector3 direction;
                internal bool singleHit;

                public SetSize(Vector3 originPoint, Vector3 direction, bool singleHit)
                {
                    this.originPoint = originPoint;
                    this.direction = direction;
                    this.singleHit = singleHit;
                }

                public PhysicsCastResult ContinueWithDefaultParams()
                {
                    return WithSize(Vector3.one).ContinueWithDefaultParams();
                }

                public SetOrientation WithSize(Vector3 size)
                {
                    return new SetOrientation(
                        originPoint,
                        direction,
                        size,
                        singleHit);
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

            public SetSize SingleHit()
            {
                return new SetSize(originPoint, direction, true);
            }

            public SetSize MultiHit()
            {
                return new SetSize(originPoint, direction, false);
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

