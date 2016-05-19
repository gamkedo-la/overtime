using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	public GameObject AmmoCount;
	public GameObject AmmoName;

	// Use this for initialization
	void Awake () {
		Debug.Log ("PlayerManager singleton getting set up");
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
