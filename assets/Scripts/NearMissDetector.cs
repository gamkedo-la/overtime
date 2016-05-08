using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class NearMissDetector : MonoBehaviour {

	public float bufferTime = 0.25F;

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.GetComponent<DartRecoverableScript> () != null) 
		{
			StartCoroutine("NearMissBuffer", bufferTime);
		}
	}

	IEnumerator NearMissBuffer (float bufferTime)
	{
		yield return new WaitForSeconds (bufferTime);
		ComboGenerator.ActionNearMiss ();

	}
}
