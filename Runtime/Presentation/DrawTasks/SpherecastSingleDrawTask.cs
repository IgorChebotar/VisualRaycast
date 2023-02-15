using UnityEngine;
using System.Linq;

namespace SimpleMan.VisibleRaycast.Presentation
{
    internal class SpherecastSingleDrawTask : CastDrawTask
    {
        private readonly float _radius;

        public SpherecastSingleDrawTask(
            Vector3 from,
            Vector3 direction,
            float radius,
            float distance,
            float lifeTime,
            float hitPointRadius,
            Color hitColor,
            Color missColor,
            PhysicsCastResult castResult) : base(from, direction, distance, castResult, hitPointRadius, lifeTime, hitColor, missColor)
        {
            _radius = radius;
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
            ComplexGizmos.DrawWireSphere(_from + _direction * _distance, _radius, _missColor);
        }

        private void DrawHitRay()
        {
            ComplexGizmos.DrawLine(_from, _from + _direction * _castResult.hits[0].distance, _hitColor);
            ComplexGizmos.DrawWireSphere(_from + _direction * _castResult.hits[0].distance, _radius, _hitColor);
            ComplexGizmos.DrawSphere(_castResult.hits.First().point, _hitPointRadius, _hitColor);
        }
    }
}
