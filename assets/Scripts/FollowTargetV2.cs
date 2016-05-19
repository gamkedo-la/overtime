using System;
using UnityEngine;

//Standard Follow Target with Rotation
namespace UnityStandardAssets.Utility
{
    public class FollowTargetV2 : MonoBehaviour
    {
        public Transform target;
        public Vector3 posOffset = new Vector3(0f, 0f, 0f);
       
		private Vector3 lookFocus;

        private void LateUpdate()
        {
			RaycastHit rhInfo;
			int LayerFilter = ~LayerMask.GetMask ("AmmoBox","Player");
			float raycastCamDist = 1.2f;
			Vector3 camGoal;
			Vector3 startPt = target.position+Vector3.up;
			Vector3 endPt = target.transform.TransformVector(posOffset).normalized * raycastCamDist;
			if (Physics.Raycast (startPt,
			                   endPt,
			                     out rhInfo,
			                     raycastCamDist,
			                   LayerFilter)) {
				float goalDist = Vector3.Distance(startPt, endPt);
				camGoal = rhInfo.point + rhInfo.normal*0.3f +
							Vector3.up * (raycastCamDist - 
					              Vector3.Distance(rhInfo.point,startPt)) * 1.2f;
				Debug.Log (rhInfo.collider.name);
			} else {
				camGoal = target.position+Vector3.up + 
					target.transform.TransformVector(posOffset).normalized * raycastCamDist;
			}

			// sink back slower than it rises
			float kVal = (camGoal.y < transform.position.y ? 0.98f : 0.75f);
			transform.position = kVal * transform.position +
				(1.0f - kVal) * camGoal;
					
			/*transform.rotation = Quaternion.Slerp(transform.rotation,
			                                      target.rotation,
			                                      Time.deltaTime*3.5f);*/
			float lookTargetDist = (camGoal.y < transform.position.y ? 5.0f : 6.0f);
			float lookKVal = 0.35f;
			lookFocus = lookKVal * lookFocus +
				(1.0f-lookKVal) * (target.position + target.forward * lookTargetDist);
			transform.LookAt ( lookFocus );
        }
    }

    // Extra code for smoothing
    //  But it made Camera Jerky
    //   transform.position = Vector3.Lerp (transform.position, (target.position + posOffset), smoothing * Time.deltaTime);
    //    public float smoothing = 5f;
}