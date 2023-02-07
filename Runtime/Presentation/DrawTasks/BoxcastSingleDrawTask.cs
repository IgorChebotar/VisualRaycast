using UnityEngine;
using System.Linq;

namespace SimpleMan.VisualRaycast.Presentation
{
    internal class BoxcastSingleDrawTask : CastDrawTask
    {
        private readonly Vector3 _size;
        private readonly Quaternion _orientation;

        public BoxcastSingleDrawTask(
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
            ComplexGizmos.DrawBox(_from + _direction * _distance, _size, _orientation, _missColor);
        }

        private void DrawHitRay()
        {
            ComplexGizmos.DrawLine(_from, _from + _direction * _castResult.hits[0].distance, _hitColor);
            ComplexGizmos.DrawBox(_from + _direction * _castResult.hits[0].distance, _size, _orientation, _hitColor);
            ComplexGizmos.DrawHitSphere(_castResult.hits.First().point, _hitPointRadius, _hitColor);
        }
    }
}
