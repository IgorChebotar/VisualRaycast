using UnityEngine;
using System.Linq;

namespace SimpleMan.VisibleRaycast.Presentation
{
    internal class BoxcastMultiDrawTask : CastDrawTask
    {
        private readonly Vector3 _size;
        private readonly Quaternion _orientation;

        public BoxcastMultiDrawTask(
            Vector3 from,
            Vector3 direction,
            Vector3 size,
            Quaternion orientation,
            float distance,
            float lifeTime,
            float hitPointRadius,
            Color hitColor,
            Color missColor,
            PhysicsCastResult castResult) : base(from, direction, distance, castResult, hitPointRadius, lifeTime, hitColor, missColor)
        {
            _size = size;
            _orientation = orientation;
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
            ComplexGizmos.DrawWireBox(_from + _direction * _distance, _size, _orientation, _missColor);
        }

        private void DrawHitRay()
        {
            RaycastHit lastHit = _castResult.hits.Last();

            foreach (var hit in _castResult.hits)
            {
                ComplexGizmos.DrawWireBox(_from + _direction * hit.distance, _size, _orientation, _hitColor);
                ComplexGizmos.DrawSphere(hit.point, _hitPointRadius, _hitColor);
            }

            ComplexGizmos.DrawRay(_from, _direction, lastHit.distance, _hitColor);
            ComplexGizmos.DrawRay(_from + _direction * lastHit.distance, _direction, _distance - lastHit.distance, _missColor);
            ComplexGizmos.DrawWireBox(_from + _direction * _distance, _size, _orientation, _missColor);
        }
    }
}
