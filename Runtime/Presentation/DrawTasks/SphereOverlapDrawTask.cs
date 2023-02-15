using UnityEngine;

namespace SimpleMan.VisibleRaycast.Presentation
{
    internal class SphereOverlapDrawTask : DrawTask
    {
        private readonly Vector3 _from;
        private readonly float _radius;
        private readonly PhysicsOverlapResult _result;

        public SphereOverlapDrawTask(
            Vector3 from,
            float radius,
            PhysicsOverlapResult result,
            float hitPointRadius,
            float lifeTime,
            Color hitColor,
            Color missColor) : base(hitPointRadius, lifeTime, hitColor, missColor)
        {
            _from = from;
            _radius = radius;
            _result = result;
        }

        public override void Draw()
        {
            Color color = _result ? _hitColor : _missColor;
            ComplexGizmos.DrawWireSphere(_from, _radius, color);

            if (!_result)
                return;

            foreach (var hit in _result.detectedColliders)
            {
                ComplexGizmos.DrawSphere(hit.ClosestPoint(_from), _hitPointRadius, color);
            }
        }
    }
}
