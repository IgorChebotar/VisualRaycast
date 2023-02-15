using UnityEngine;

namespace SimpleMan.VisibleRaycast
{
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Example: Raycast->FromMainCamera->ToMousePositionInWorld->SingleHit->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static RaycastDeclaration Raycast(this MonoBehaviour source)
        {
            return VisualPhysicsAPI.Raycast();
        }

        /// <summary>
        /// Example: Spherecast->FromMainCamera->ToMousePositionInWorld->SingleHit->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SpherecastDeclaration Spherecast(this MonoBehaviour source)
        {
            return VisualPhysicsAPI.Spherecast();
        }

        /// <summary>
        /// Example: Boxcast->FromMainCamera->ToMousePositionInWorld->SingleHit->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BoxcastDeclaration Boxcast(this MonoBehaviour source)
        {
            return VisualPhysicsAPI.Boxcast();
        }

        /// <summary>
        /// Example: SphereOverlap->FromGameObjectInWorld->WithRadius->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static SphereOverlapDeclaration SphereOverlap(this MonoBehaviour source)
        {
            return VisualPhysicsAPI.SphereOverlap();
        }

        /// <summary>
        /// Example: BoxOverlap->FromGameObjectInWorld->WithSize->WithRotation->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BoxOverlapDeclaration BoxOverlap(this MonoBehaviour source)
        {
            return VisualPhysicsAPI.BoxOverlap();
        }
    }
}

