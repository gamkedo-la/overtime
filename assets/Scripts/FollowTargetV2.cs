using System;
using UnityEngine;

//Standard Follow Target with Rotation
namespace UnityStandardAssets.Utility
{
    public class FollowTargetV2 : MonoBehaviour
    {
        public Transform target;
        public Vector3 posOffset = new Vector3(0f, 0f, 0f);
       


        private void LateUpdate()
        {
            transform.position = target.position + posOffset;
            transform.rotation = target.rotation;
        }
    }

    // Extra code for smoothing
    //  But it made Camera Jerky
    //   transform.position = Vector3.Lerp (transform.position, (target.position + posOffset), smoothing * Time.deltaTime);
    //    public float smoothing = 5f;
}