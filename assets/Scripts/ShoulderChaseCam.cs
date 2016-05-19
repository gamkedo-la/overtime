using UnityEngine;
using System.Collections;

public class ShoulderChaseCam : MonoBehaviour {

	Transform chaseThis;

	// Use this for initialization
	void Start () {
		chaseThis = transform.parent.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = chaseThis.position - transform.forward * 1.5f;
		transform.LookAt (chaseThis);
	}
}
