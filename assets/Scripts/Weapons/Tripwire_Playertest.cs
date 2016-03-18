using UnityEngine;
using System.Collections;

public class Tripwire_Playertest : MonoBehaviour {

	public GameObject tripwirePrefab;
	public GameObject tripwireInstance;




	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.R) && tripwireInstance.transform.parent != this.transform) {
			tripwireInstance = Instantiate(tripwirePrefab);
			tripwireInstance.transform.parent = this.transform;
			tripwireInstance.transform.localPosition = tripwirePrefab.transform.localPosition;
			tripwireInstance.transform.localRotation = tripwirePrefab.transform.localRotation;
		}
	}
}
