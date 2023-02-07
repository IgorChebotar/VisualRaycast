using SimpleMan.VisualRaycast;
using SimpleMan.Utilities;
using System.Linq;
using UnityEngine;

namespace SimpleMan.VisualRaycastDemo
{
    public class CastFromCameraExample : MonoBehaviour
    {
        private const float JUMP_FORCE = 4f;
        
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0))
                return;

            var result = VisualPhysicsAPI.
                Raycast().
                FromMainCamera().
                ToMousePositionInWorld().
                ContinueWithDefaultParams();

            if (!result)
                return;

            if (result.hits.First().collider.attachedRigidbody.NotExist())
                return;

            result.hits.First().collider.attachedRigidbody.AddForce(Vector3.up * JUMP_FORCE, ForceMode.Impulse);
        }
    }
}

