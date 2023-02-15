using SimpleMan.AsyncOperations;
using SimpleMan.VisibleRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class SpherecastMultiHitsExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        [Range(0.1f, 3f)]
        [SerializeField] private float _radius = 0.5f;

        [Range(1, 100)]
        [SerializeField] private float _distance = 15f;


        

        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }
        
        private void Tick()
        {
            this.
                Spherecast().
                FromGameObjectInWorld(gameObject).
                ToDirection(transform.forward).
                MultiHit().
                WithRadius(_radius).
                WithDistance(_distance).
                ContinueWithDefaultParams();
        }
    }
}

