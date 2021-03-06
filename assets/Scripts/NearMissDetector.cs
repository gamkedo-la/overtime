﻿using UnityEngine;
using System.Collections;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class NearMissDetector : MonoBehaviour {

	public float bufferTime = 0.5F;

	// Use this for initialization
	void OnTriggerEnter (Collider other)
	{
		DartRecoverableScript dart = other.transform.GetComponent<DartRecoverableScript> ();
		if (dart != null && dart.owner != PhotonNetwork.player.name) 
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
