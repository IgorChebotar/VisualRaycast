using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using static SimpleMan.VisualRaycast.SpherecastDeclaration.SetRadius.SetDirection.SetLayerMask;

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



                internal readonly Vector3 from;
                internal readonly Vector3 direction;
                internal readonly float distance;
                internal readonly bool singleHit;

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

            public ComplexityData MultiHits()
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

    public struct SpherecastDeclaration
    {
        public struct SetOriginPosition
        {
            internal readonly Vector3 from;

            public SetOriginPosition(Vector3 from)
            {
                this.from = from;
            }

            public SetRadius ToDirection(Vector3 direction, float castDistance = float.PositiveInfinity)
            {
                return new SetRadius(from, direction, castDistance);
            }

            public SetRadius ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                return new SetRadius(from, point - from, castDistance);
            }

            public SetRadius ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public SetRadius ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
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

            public SetRadius ToForward(float castDistance = float.PositiveInfinity)
            {
                return new SetRadius(
                    from.transform.position,
                    from.transform.forward,
                    castDistance.ClampPositive());
            }

            public SetRadius ToMousePositionInWorld(float castDistance = float.PositiveInfinity)
            {
                Ray ray = from.ScreenPointToRay(Input.mousePosition);

                return new SetRadius(
                    ray.origin,
                    ray.direction,
                    castDistance.ClampPositive());
            }

            public SetRadius ToPointInWorld(Vector3 point, float castDistance = float.PositiveInfinity)
            {
                Vector3 cameraPosition = from.transform.position;
                Vector3 direction = point - cameraPosition;

                return new SetRadius(
                    cameraPosition,
                    direction,
                    castDistance.ClampPositive());
            }

            public SetRadius ToGameObjectInWorld(Transform point, float castDistance = float.PositiveInfinity)
            {
                if (point.NotExist())
                {
                    throw new ArgumentNullException(nameof(point));
                }

                return ToPointInWorld(point.position, castDistance);
            }

            public SetRadius ToGameObjectInWorld(GameObject point, float castDistance = float.PositiveInfinity)
            {
                return ToGameObjectInWorld(point.transform, castDistance);
            }
        }

        public struct SetRadius
        {
            public struct SetDirection
            {
                public struct SetLayerMask
                {
                    public struct SetIgnoreColliders
                    {
                        internal readonly Vector3 from;
                        internal readonly Vector3 direction;
                        internal readonly float distance;
                        internal readonly float radius;
                        internal readonly bool singleHit;
                        internal readonly LayerMask mask;

                        public SetIgnoreColliders(
                            Vector3 from,
                            Vector3 direction,
                            float distance,
                            float radius,
                            bool singleHit,
                            LayerMask mask)
                        {
                            this.from = from;
                            this.direction = direction;
                            this.distance = distance;
                            this.radius = radius;
                            this.singleHit = singleHit;
                            this.mask = mask;
                        }

                        public PhysicsCastResult DontIgnoreAnything()
                        {
                            return InternalPhysicsCast.Spherecast(
                                from,
                                direction,
                                distance,
                                radius,
                                singleHit,
                                null,
                                mask);
                        }

                        public PhysicsCastResult IgnoreColliders(params Collider[] collidersToIgnore)
                        {
                            return InternalPhysicsCast.Spherecast(
                               from,
                               direction,
                               distance,
                               radius,
                               singleHit,
                               collidersToIgnore,
                               mask);
                        }

                        public PhysicsCastResult IgnoreObjects(params GameObject[] objectsToIgnore)
                        {
                            return InternalPhysicsCast.Spherecast(
                               from,
                               direction,
                               distance,
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



                    internal readonly Vector3 from;
                    internal readonly Vector3 direction;
                    internal readonly float radius;
                    internal readonly float distance;
                    internal readonly bool singleHit;

                    public SetLayerMask(
                            Vector3 from,
                            Vector3 direction,
                            float distance,
                            float radius,
                            bool singleHit)
                    {
                        this.from = from;
                        this.direction = direction;
                        this.radius = radius;
                        this.distance = distance;
                        this.singleHit = singleHit;
                    }

                    public PhysicsCastResult ContinueWithDefaultParams()
                    {
                        SetIgnoreColliders layerMaskData = new SetIgnoreColliders(
                            from,
                            direction,
                            distance,
                            radius,
                            singleHit,
                            Physics.DefaultRaycastLayers);

                        return layerMaskData.DontIgnoreAnything();
                    }

                    public SetIgnoreColliders UseDefaultLayerMask()
                    {
                        return UseCustomLayerMask(UnityEngine.Physics.DefaultRaycastLayers);
                    }

                    public SetIgnoreColliders UseCustomLayerMask(LayerMask mask)
                    {
                        return new SetIgnoreColliders(
                            from,
                            direction,
                            distance,
                            radius,
                            singleHit,
                            mask);
                    }
                }

                internal readonly Vector3 from;
                internal readonly Vector3 direction;
                internal readonly float distance;
                internal readonly float radius;

                public SetDirection(
                    Vector3 from,
                    Vector3 direction,
                    float distance,
                    float radius)
                {
                    this.from = from;
                    this.direction = direction;
                    this.distance = distance;
                    this.radius = radius;
                }

                public PhysicsCastResult ContinueWithDefaultParams()
                {
                    SetLayerMask complexityData = new SetLayerMask(
                        from,
                        direction,
                        distance,
                        radius,
                        true);

                    return complexityData.ContinueWithDefaultParams();
                }

                public SetLayerMask SingleHit()
                {
                    return new SetLayerMask(
                        from,
                        direction,
                        distance,
                        radius,
                        true);
                }

                public SetLayerMask MultiHits()
                {
                    return new SetLayerMask(
                        from,
                        direction,
                        distance,
                        radius,
                        false);
                }
            }

            internal readonly Vector3 from;
            internal readonly Vector3 direction;
            internal readonly float distance;

            public SetRadius(
                Vector3 from,
                Vector3 direction,
                float distance)
            {
                this.from = from;
                this.direction = direction;
                this.distance = distance;
            }

            public SetDirection WithRadius(float radius)
            {
                return new SetDirection(from, direction, distance, radius);
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

        public SetOriginPosition FromPointInWorld(Vector3 point)
        {
            return new SetOriginPosition(point);
        }

        public SetOriginPosition FromGameObjectInWorld(Transform point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromPointInWorld(point.position);
        }

        public SetOriginPosition FromGameObjectInWorld(GameObject point)
        {
            return FromGameObjectInWorld(point.transform);
        }
    }

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

                        public PhysicsCastResult ContinueWithDefaultParameters()
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

                    public PhysicsCastResult ContinueWithDefaultParamters()
                    {
                        return WithDefaultRotation().ContinueWithDefaultParameters();
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

