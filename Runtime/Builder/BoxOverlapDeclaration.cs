using SimpleMan.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    public struct BoxOverlapDeclaration
    {
        public struct SetSize
        {
            public struct SetOrientation
            {
                public struct SetLayerMask
                {
                    public struct SetIgnoredColliders
                    {
                        internal Vector3 originPoint;
                        internal Vector3 size;
                        internal Quaternion orientation;
                        internal LayerMask mask;

                        public SetIgnoredColliders(Vector3 originPoint, Vector3 size, Quaternion orientation, LayerMask mask)
                        {
                            this.originPoint = originPoint;
                            this.size = size;
                            this.orientation = orientation;
                            this.mask = mask;
                        }

                        public PhysicsOverlapResult DontIgnoreAnything()
                        {
                            return InternalPhysicsCast.BoxOverlap(
                                originPoint,
                                size,
                                orientation,
                                mask,
                                null);
                        }

                        public PhysicsOverlapResult IgnoreColliders(params Collider[] collidersToIgnore)
                        {
                            return InternalPhysicsCast.BoxOverlap(
                                originPoint,
                                size,
                                orientation,
                                mask,
                                collidersToIgnore);
                        }

                        public PhysicsOverlapResult IgnoreObjects(params GameObject[] objectsToIgnore)
                        {
                            return InternalPhysicsCast.BoxOverlap(
                                originPoint,
                                size,
                                orientation,
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
                    internal Quaternion orientation;
                    internal Vector3 size;

                    public SetLayerMask(Vector3 originPoint, Vector3 size, Quaternion orientation)
                    {
                        this.originPoint = originPoint;
                        this.size = size;
                        this.orientation = orientation;
                    }

                    public PhysicsOverlapResult ContinueWithDefaultParams()
                    {
                        return UseDefaultLayerMask().DontIgnoreAnything();
                    }

                    public SetIgnoredColliders UseDefaultLayerMask()
                    {
                        return new SetIgnoredColliders(originPoint, size, orientation, Physics.DefaultRaycastLayers);
                    }

                    public SetIgnoredColliders UseCustomLayerMask(LayerMask mask)
                    {
                        return new SetIgnoredColliders(originPoint, size, orientation, mask);
                    }
                }



                internal Vector3 originPoint;
                internal Vector3 size;

                public SetOrientation(Vector3 originPoint, Vector3 size)
                {
                    this.originPoint = originPoint;
                    this.size = size;
                }
              
                public SetLayerMask WithDefaultRotation()
                {
                    return WithRotation(Quaternion.identity);
                }

                public SetLayerMask WithRotation(Quaternion value)
                {
                    return new SetLayerMask(originPoint, size, value);
                }

                public SetLayerMask WithRotationOf(Transform target)
                {
                    return new SetLayerMask(originPoint, size, GetTargetRotationOrDefault());


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

                public SetLayerMask WithRotationOf(GameObject target)
                {
                    return new SetLayerMask(originPoint, size, GetTargetRotationOrDefault());


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

            public SetSize(Vector3 originPoint)
            {
                this.originPoint = originPoint;
            }

            public SetOrientation WithSize(Vector3 size)
            {
                return new SetOrientation(originPoint, size);
            }
        }




        public SetSize FromMainCamera()
        {
            return FromCamera(Camera.main);
        }

        public SetSize FromCamera(Camera point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return new SetSize(point.transform.position);
        }

        public SetSize FromPointInWorld(Vector3 point)
        {
            return new SetSize(point);
        }

        public SetSize FromGameObjectInWorld(Transform point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromPointInWorld(point.position);
        }

        public SetSize FromGameObjectInWorld(GameObject point)
        {
            if (point.NotExist())
            {
                throw new ArgumentNullException(nameof(point));
            }

            return FromGameObjectInWorld(point.transform);
        }
    }
}

