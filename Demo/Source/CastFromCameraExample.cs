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
            //No input received? -> Ignore next code
            if (!Input.GetMouseButtonDown(0))
                return;


            
            //Declarate raycast operation and request to get result of it
            var result = this.

            //Type of operation
            Raycast().

            //Origin of ray is main camera
            FromMainCamera().

            //Direction is mouse position in world
            ToMousePositionInWorld().

            //The parameters of the operation (could be extended)
            ContinueWithDefaultParams();



            
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

