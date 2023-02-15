using SimpleMan.AsyncOperations;
using SimpleMan.VisibleRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class BoxcastMultiHitsExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        [Range(1, 100)]
        [SerializeField] private float _distance = 15f;
        [SerializeField] private Vector3 _size = Vector3.one;

        

        
        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }

        private void Tick()
        {
                this.
                Boxcast().
                FromGameObjectInWorld(gameObject).
                ToDirection(transform.forward).
                MultiHit().
                WithSize(_size).
                WithRotationOf(gameObject).
                WithDistance(_distance).
                ContinueWithDefaultParams();
        }
    }
}

