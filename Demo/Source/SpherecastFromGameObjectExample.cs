using SimpleMan.AsyncOperations;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class SpherecastFromGameObjectExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        [Range(0.1f, 3f)]
        [SerializeField] private float _radius = 0.5f;

        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }

        private void Tick()
        {
            VisualRaycast.VisualPhysics.
                SphereCast().
                FromGameObjectInWorld(gameObject).
                ToDirection(transform.forward).
                WithRadius(_radius).
                ContinueWithDefaultParams();
        }
    }
}

