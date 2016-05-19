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
			RaycastHit rhInfo;
			int LayerFilter = ~LayerMask.GetMask ("AmmoBox","Player");
			if (Physics.Raycast (target.position+Vector3.up,
			                   target.transform.TransformVector(posOffset).normalized,
			                     out rhInfo,
			                     1.2f,
			                   LayerFilter)) {
				transform.position = rhInfo.point + rhInfo.normal*0.5f;
				Debug.Log (rhInfo.collider.name);
			} else {
				transform.position = target.position+Vector3.up + 
					target.transform.TransformVector(posOffset).normalized * 1.2f;
			}

			transform.rotation = Quaternion.Slerp(transform.rotation,
			                                      target.rotation,
			                                      Time.deltaTime*3.5f);
        }
    }

    // Extra code for smoothing
    //  But it made Camera Jerky
    //   transform.position = Vector3.Lerp (transform.position, (target.position + posOffset), smoothing * Time.deltaTime);
    //    public float smoothing = 5f;
}