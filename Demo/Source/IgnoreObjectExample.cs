using SimpleMan.VisualRaycast;
using SimpleMan.Utilities;
using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class IgnoreObjectExample : MonoBehaviour
    {
        private const float JUMP_FORCE = 4f;

        [SerializeField] private GameObject[] _ignoreObjects;

        private void Update()
        {
            //No input received? -> Ignore next code
            if (!Input.GetMouseButtonDown(0))
                return;

            //Make raycast
            var result = VisualPhysicsAPI.
                Raycast().
                FromMainCamera().
                ToMousePositionInWorld().
                SingleHit().
                UseDefaultLayerMask().
                IgnoreObjects(_ignoreObjects);

            //No hits? -> Ignore next code
            if (!result)
                return;

            //No rigidbody on hit object? -> Ignore next code
            if (result.hits.First().collider.attachedRigidbody.NotExist())
                return;

            //Add force to rigidbody
            result.hits.First().collider.attachedRigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
        }
    }
}

