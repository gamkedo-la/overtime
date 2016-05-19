using UnityEngine;
using System.Collections;

public class LightFlash : MonoBehaviour {

	private Light light;

	// Use this for initialization
	void Start () {
		light = GetComponent<Light> ();
		StartCoroutine ( FlashIt() );
	}
	
	IEnumerator FlashIt () {
		for(;;) {
			light.enabled = !light.enabled;
			yield return new WaitForSeconds(0.3f);
		}
	}
}
