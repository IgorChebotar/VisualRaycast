using UnityEngine;
using System.Linq;

namespace SimpleMan.VisualRaycast.Presentation
{
    internal class RaycastMultiDrawTask : CastDrawTask
    {
        public RaycastMultiDrawTask(
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
            RaycastHit lastHit = _castResult.hits.Last();
            foreach (var hit in _castResult.hits)
            {
                ComplexGizmos.DrawHitSphere(hit.point, _hitPointRadius, _hitColor);
            }
            
            ComplexGizmos.DrawRay(_from, _direction, lastHit.distance, _hitColor);
            ComplexGizmos.DrawRay(lastHit.point, _direction, _distance - lastHit.distance, _missColor);
        }
    }
}
