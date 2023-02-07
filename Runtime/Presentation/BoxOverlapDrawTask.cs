using SimpleMan.Utilities;
using UnityEngine;

namespace SimpleMan.VisualRaycast.Presentation
{
    internal class BoxOverlapDrawTask : DrawTask
    {
        private readonly Vector3 _from;
        private readonly Vector3 _size;
        private readonly Quaternion _rotation;
        private readonly PhysicsOverlapResult _result;

        public BoxOverlapDrawTask(
            Vector3 from,
            Vector3 size,
            Quaternion rotation,
            PhysicsOverlapResult result,
            float hitPointRadius,
            float lifeTime,
            Color hitColor,
            Color missColor) : base(hitPointRadius, lifeTime, hitColor, missColor)
        {
            _from = from;
            _size = size;
            _rotation = rotation;
            _result = result;
        }

        public override void Draw()
        {
            Color color = _result ? _hitColor : _missColor;
            ComplexGizmos.DrawBoxOLD(_from, _size, _rotation, color);

            if (!_result)
                return;

            foreach (var hit in _result.detectedColliders)
            {
                if (hit.NotExist())
                    continue;

                ComplexGizmos.DrawHitSphere(hit.ClosestPoint(_from), _hitPointRadius, color);
            }
        }
    }
}
