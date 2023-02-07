using UnityEngine;
using System.Linq;

namespace SimpleMan.VisualRaycast.Presentation
{
    internal abstract class CastDrawTask : DrawTask
    {
        protected readonly Vector3 _from;
        protected readonly Vector3 _direction;
        protected readonly float _distance;
        protected readonly PhysicsCastResult _castResult;

        protected CastDrawTask(
            Vector3 from,
            Vector3 direction,
            float distance,
            PhysicsCastResult result,
            float hitPointRadius,
            float lifeTime,
            Color hitColor,
            Color missColor) : base(hitPointRadius, lifeTime, hitColor, missColor)
        {
            _from = from;
            _direction = direction;
            _distance = distance;
            _castResult = result;
        }

        protected void OrderHitsByDistance(RaycastHit[] hits)
        {
            hits = hits.OrderBy(x => Vector3.Distance(_from, x.point)).ToArray();
        }
    }
}
