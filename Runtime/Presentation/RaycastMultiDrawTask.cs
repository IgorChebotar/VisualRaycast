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
            OrderHitsByDistance(_castResult.hits);
            Vector3 lastPoint = _castResult.hits.Last().point;

            foreach (var hit in _castResult.hits)
            {
                ComplexGizmos.DrawLine(_from, hit.point, _hitColor);
                ComplexGizmos.DrawHitSphere(hit.point, _hitPointRadius, _hitColor);
            }

            float distanceFromOriginToLast = Vector3.Distance(_from, lastPoint);
            float distanceFromLastPointToEndOfRay = _distance - distanceFromOriginToLast;

            ComplexGizmos.DrawRay(_castResult.hits.Last().point, _direction, distanceFromLastPointToEndOfRay, _missColor);
        }
    }
}
