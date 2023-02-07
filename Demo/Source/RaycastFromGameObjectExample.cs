using SimpleMan.AsyncOperations;
using SimpleMan.VisualRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class RaycastFromGameObjectExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }

        private void Tick()
        {
            VisualRaycast.VisualPhysics.
                Raycast().
                FromGameObjectInWorld(gameObject).
                ToDirection(transform.forward).
                ContinueWithDefaultParams();
        }
    }
}
