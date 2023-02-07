using UnityEngine;
using System.Linq;

namespace SimpleMan.VisualRaycast.Presentation
{
    internal class RaycastSingleDrawTask : CastDrawTask
    {
        public RaycastSingleDrawTask(
            Vector3 from,
            Vector3 direction,
            float distance,
            float lifeTime,
            float hitPointRadius,
            Color hitColor,
            Color missColor,
            PhysicsCastResult castResult) : base(from, direction, distance, castResult, hitPointRadius, lifeTime, hitColor, missColor)
        {
        }

        public override void Draw()
        {
            if (_castResult)
                DrawHitRay();

            else
                DrawMissedRay();
        }

        private void DrawMissedRay()
        {
            ComplexGizmos.DrawRay(_from, _direction, _distance, _missColor);
        }

        private void DrawHitRay()
        {
            ComplexGizmos.DrawLine(_from, _castResult.hits.First().point, _hitColor);
            ComplexGizmos.DrawHitSphere(_castResult.hits.First().point, _hitPointRadius, _hitColor);
        }
    }
}
