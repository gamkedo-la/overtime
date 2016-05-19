using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	public static PlayerManager instance;

	public GameObject AmmoCount;
	public GameObject AmmoName;
	public GameObject ToolTipGrabAmmo;
	public Image WeaponIcon;

	// Use this for initialization
	void Awake () {
		Debug.Log ("PlayerManager singleton getting set up");
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
