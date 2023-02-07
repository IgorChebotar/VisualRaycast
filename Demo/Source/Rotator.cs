using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;

        private void Update()
        {
            transform.Rotate(Vector3.up, _speed * Time.deltaTime);
        }
    }
}

