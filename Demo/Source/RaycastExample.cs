using SimpleMan.AsyncOperations;
using SimpleMan.VisibleRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class RaycastExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }

        private void Tick()
        {
            //The entry point - your class. You can aslo use 'VisualRaycastAPI' instead of 'this'
            this.

            //The type of the operation
            Raycast().

            //The origin of the raycast
            FromGameObjectInWorld(gameObject).

            //The direction of the raycast
            ToDirection(transform.forward).

            //The parameters of the operation (could be extended)
            ContinueWithDefaultParams();
        }
    }
}

