using SimpleMan.AsyncOperations;
using SimpleMan.VisibleRaycast;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class SphereOverlapExample : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float _raycastFrequency = 0.4f;

        [Range(1f, 15f)]
        [SerializeField] private float _radius = 6;


        
        
        private void Start()
        {
            this.RepeatForever(Tick, _raycastFrequency);
        }
        
        private void Tick()
        {
            this.
                SphereOverlap().
                FromGameObjectInWorld(gameObject).
                WithRadius(_radius).
                ContinueWithDefaultParams();
        }
    }
}

