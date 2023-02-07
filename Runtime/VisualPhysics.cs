using UnityEngine;

namespace SimpleMan.VisualRaycast
{
    /// <summary>
    /// Contains methods for performing and visualizing physics casting operations in Unity, 
    /// such as raycasting, sphere overlap, and box overlap.
    /// </summary>
    public static class VisualPhysics
    {
        /// <summary>
        /// Example: PhysicsCast->Raycast->FromMainCamera->ToMousePositionInWorld->Single->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <returns></returns>
        public static RaycastDeclaration Raycast()
        {
            return new RaycastDeclaration();
        }

        /// <summary>
        /// Example: PhysicsCast->SphereOverlap->FromGameObjectInWorld->WithRadius->UseCustomLayerMask->IgnoreObjects
        /// </summary>
        /// <returns></returns>
        public static SphereOverlapDeclaration SphereOverlap()
        {
            return new SphereOverlapDeclaration();
        }

        /// <summary>
        /// Example: PhysicsCast->BoxOverlap->FromGameObjectInWorld->WithSize->UseCustomLayerMask->UseRotationFrom->IgnoreObjects
        /// </summary>
        /// <returns></returns>
        public static BoxOverlapDeclaration BoxOverlap()
        {
            return new BoxOverlapDeclaration();
        }

        /// <summary>
        /// Performs a classic raycast using the specified parameters.
        /// </summary>
        /// <param name="from">The starting position of the raycast.</param>
        /// <param name="direction">The direction of the raycast.</param>
        /// <param name="distance">The maximum distance of the raycast.</param>
        /// <param name="singleCast">If true, only the first object hit by the raycast will be returned. If false, all objects hit by the raycast will be returned.</param>
        /// <param name="collidersToIgnore">An array of colliders to ignore during the raycast.</param>
        /// <param name="layerMask">A mask used to filter which objects the raycast will hit.</param>
        /// <returns>A PhysicsCastResult instance containing information about the objects hit by the raycast.</returns>
        public static PhysicsCastResult ClassicRaycast(
            Vector3 from,
            Vector3 direction,
            float distance,
            bool singleCast,
            Collider[] collidersToIgnore,
            LayerMask layerMask)
        {
            return InternalPhysicsCast.Raycast(from, direction, distance, singleCast, collidersToIgnore, layerMask);
        }

        /// <summary>
        /// Performs a classic 3D sphere overlap using the specified parameters.
        /// </summary>
        /// <param name="from">The center position of the sphere.</param>
        /// <param name="radius">The radius of the sphere.</param>
        /// <param name="mask">A mask used to filter which objects the sphere overlap will check for.</param>
        /// <param name="collidersToIgnore">An array of colliders to ignore during the sphere overlap.</param>
        /// <returns>A PhysicsOverlapResult instance containing information about the objects overlapped by the sphere.</returns>
        public static PhysicsOverlapResult ClassicSphereOverlap(
            Vector3 from,
            float radius,
            LayerMask mask,
            Collider[] collidersToIgnore)
        {
            return InternalPhysicsCast.SphereOverlap(from, radius, mask, collidersToIgnore);
        }

        /// <summary>
        /// Performs a classic 3D box overlap using the specified parameters.
        /// </summary>
        /// <param name="from">The center position of the box.</param>
        /// <param name="size">The size of the box.</param>
        /// <param name="rotation">The rotation of the box.</param>
        /// <param name="mask">A mask used to filter which objects the box overlap will check for.</param>
        /// <param name="collidersToIgnore">An array of colliders to ignore during the box overlap.</param>
        /// <returns>A PhysicsOverlapResult instance containing information about the objects overlapped by the box.</returns>
        public static PhysicsOverlapResult ClassicBoxOverlap(
            Vector3 from,
            Vector3 size,
            Quaternion rotation,
            LayerMask mask,
            Collider[] collidersToIgnore)
        {
            return InternalPhysicsCast.BoxOverlap(from, size, rotation, mask, collidersToIgnore);
        }
        
    }
}

