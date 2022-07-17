 using System;
using UnityEngine;


namespace SimpleMan.VisualRaycast
{
    public static class VisualCast
    {
        #region RAYCAST
        [Obsolete("Use 'this.Raycast' instead")]
        /// <summary>
        /// Make raycast 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo">Should the gizmo be drawn?</param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Raycast(Vector3 originPoint, Vector3 direction, float maxDistance, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.Raycast(originPoint, direction, out hit, maxDistance))
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new RaycastInfo(new Ray(originPoint, direction), maxDistance, false, castResult));

            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Raycast' instead")]
        /// <summary>
        /// Make raycast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <param name="layerMask"></param>
        /// <param name="drawGizmo">Should the gizmo be drawn?</param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Raycast(Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask layerMask, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.Raycast(originPoint, direction, out hit, maxDistance, layerMask))
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new RaycastInfo(new Ray(originPoint, direction), maxDistance, false, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Raycast' instead")]
        /// <summary>
        /// Make raycast 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo">Should the gizmo be drawn?</param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult RaycastAll(Vector3 originPoint, Vector3 direction, float maxDistance, bool drawGizmo = true)
        {
            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.RaycastAll(originPoint, direction, maxDistance);
            CastResult castResult = new CastResult(hits);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new RaycastInfo(new Ray(originPoint, direction), maxDistance, true, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Raycast' instead")]
        /// <summary>
        /// Make raycast 
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo">Should the gizmo be drawn?</param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult RaycastAll(Vector3 originPoint, Vector3 direction, float maxDistance, LayerMask layerMask, bool drawGizmo = true)
        {
            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.RaycastAll(originPoint, direction, maxDistance, layerMask);
            CastResult castResult = new CastResult(hits);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new RaycastInfo(new Ray(originPoint, direction), maxDistance, true, castResult));


            //Return empty cast result
            return castResult;
        }

        
        #endregion

        #region BOXCAST
        [Obsolete("Use 'this.Boxcast' instead")]
        /// <summary>
        /// Make boxcast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="maxDistance"></param>
        /// <param name="rotation"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Boxcast(Vector3 originPoint, Vector3 direction, Vector3 size, float maxDistance, Quaternion rotation, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.BoxCast(originPoint, size * 0.5f, direction, out hit, rotation, maxDistance)) 
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new BoxcastInfo(new Ray(originPoint, direction), size, rotation, maxDistance, false, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Boxcast' instead")]
        /// <summary>
        /// Make boxcast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="maxDistance"></param>
        /// <param name="rotation"></param>
        /// <param name="layerMask"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Boxcast(Vector3 originPoint, Vector3 direction, Vector3 size, float maxDistance, Quaternion rotation, LayerMask layerMask, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.BoxCast(originPoint, size * 0.5f, direction, out hit, rotation, maxDistance, layerMask))
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new BoxcastInfo(new Ray(originPoint, direction), size, rotation, maxDistance, false, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Boxcast' instead")]
        /// <summary>
        /// Make boxcast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="maxDistance"></param>
        /// <param name="rotation"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult BoxcastAll(Vector3 originPoint, Vector3 direction, Vector3 size, float maxDistance, Quaternion rotation, bool drawGizmo = true)
        {
            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.BoxCastAll(originPoint, size * 0.5f, direction, rotation, maxDistance);
            CastResult castResult = new CastResult(hits);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new BoxcastInfo(new Ray(originPoint, direction), size, rotation, maxDistance, true, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Boxcast' instead")]
        /// <summary>
        /// Make boxcast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="size"></param>
        /// <param name="maxDistance"></param>
        /// <param name="rotation"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult BoxcastAll(Vector3 originPoint, Vector3 direction, Vector3 size, float maxDistance, Quaternion rotation, LayerMask layerMask, bool drawGizmo = true)
        {
            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.BoxCastAll(originPoint, size * 0.5f, direction, rotation, maxDistance, layerMask);
            CastResult castResult = new CastResult(hits);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new BoxcastInfo(new Ray(originPoint, direction), size, rotation, maxDistance, true, castResult));


            //Return empty cast result
            return castResult;
        }
        #endregion

        #region SPHERECAST
        [Obsolete("Use 'this.Spherecast' instead")]
        /// <summary>
        /// Make spherecast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Spherecast(Vector3 originPoint, Vector3 direction, float radius, float maxDistance, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.SphereCast(originPoint, radius, direction, out hit, maxDistance))
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new SpherecastInfo(new Ray(originPoint, direction), radius, maxDistance, false, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Spherecast' instead")]
        /// <summary>
        /// Make spherecast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="maxDistance"></param>
        /// <param name="layerMask"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult Spherecast(Vector3 originPoint, Vector3 direction, float radius, float maxDistance, LayerMask layerMask, bool drawGizmo = true)
        {
            CastResult castResult = new CastResult(null);
            RaycastHit hit;


            //Hit something? => Add hit as result
            if (Physics.SphereCast(originPoint, radius, direction, out hit, maxDistance, layerMask))
                castResult = new CastResult(hit);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new SpherecastInfo(new Ray(originPoint, direction), radius, maxDistance, false, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Spherecast' instead")]
        /// <summary>
        /// Make spherecast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult SpherecastAll(Vector3 originPoint, Vector3 direction, float radius, float maxDistance, bool drawGizmo = true)
        {

            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.SphereCastAll(originPoint, radius, direction, maxDistance);
            CastResult castResult = new CastResult(hits);


            //Draw gizmo if need
            VisualCastDrawer.Instance.DrawGizmo(new SpherecastInfo(new Ray(originPoint, direction), radius, maxDistance, true, castResult));


            //Return empty cast result
            return castResult;
        }

        [Obsolete("Use 'this.Spherecast' instead")]
        /// <summary>
        /// Make spherecast
        /// </summary>
        /// <param name="originPoint"></param>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="maxDistance"></param>
        /// <param name="drawGizmo"></param>
        /// <param name="customParams">Custom draw parameters. If this value is null, the gizmo will be drawn with default parameters</param>
        /// <returns></returns>
        public static CastResult SpherecastAll(Vector3 originPoint, Vector3 direction, float radius, float maxDistance, LayerMask layerMask, bool drawGizmo = true)
        {

            //Hit something? => Add hit as result
            RaycastHit[] hits = Physics.SphereCastAll(originPoint, radius, direction, maxDistance, layerMask);
            CastResult castResult = new CastResult(hits);

            VisualCastDrawer.Instance.DrawGizmo(new SpherecastInfo(new Ray(originPoint, direction), radius, maxDistance, true, castResult));

            //Return empty cast result
            return castResult;
        }
        #endregion

        private static void UpdateDrawerOnScene()
        {
            //if (VisualCastDrawerDelayed.Instance)
            //    return;


            //GameObject drawer = new GameObject("Visual cast drawer", typeof(VisualCastDrawerDelayed));
        }
    }
}