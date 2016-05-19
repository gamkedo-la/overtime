using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class NukeThrower : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// WEAPON NAME //
	[SerializeField] string weaponName;

	// Animator anim;
	[SerializeField] string dartPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	public GameObject ammoCount;

	public GameObject canInHand;

	Vector3 origPos;

	[SerializeField] PlayerNetworkMover playerNetworkMover;


	// Use this for initialization
	void Start () 
	{
		ammo = 0;
		maxAmmo = 1;

		//anim = GetComponentInChildren<Animator> ();
		ammoCount = PlayerManager.instance.AmmoCount;
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
		origPos = canInHand.transform.localPosition;
	}

	void OnEnable()
	{
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
	}
	
	// Update is called once per frame
	void Update (){

		// weapon bob during run
		canInHand.transform.localPosition = origPos
			+ Mathf.Cos(transform.position.x) * Vector3.up * 0.05f
				+ Mathf.Cos(transform.position.z) * Vector3.right * 0.05f;
		// raise weapon when reloading
		if(loaded == false) {
			canInHand.transform.localPosition -=
				0.15f*Vector3.up * ((shotTime + loadTime) - Time.time);
		}

	
		// Variables for Aim Raycast
		RaycastHit hitInfo;
		Vector3 shootToward;
		Ray centerRay = Camera.main.ScreenPointToRay(new Vector3((Camera.main.pixelWidth/2), (Camera.main.pixelHeight/2), 0f));

		// Aiming
		if(loaded == true & ammo > 0) {
			if(Physics.Raycast(centerRay, out hitInfo, 150.0f)) {
				shootToward = hitInfo.point;
			} else {
				shootToward = centerRay.origin + centerRay.direction * 50.0f;
			}
			canInHand.transform.LookAt(shootToward);
		}

		//Shoot if we hit fire, aren't running, have ammo, and round is loaded
		if(Input.GetButtonDown ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){
			if(loaded == true && ammo > 0){
				shooting = true;
				ammo--;
				shotTime = Time.time;
				Debug.Log ("It's firing");
				/* SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.sodaThrow,transform.position); */
			} else {
				/* SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.playerNoAmmoTriedToFire,transform.position); */
			}
		}

		//Reload if the cooldown has happened
		if(loaded == false && Time.time >= (shotTime + loadTime)){
			loaded = true;
		}

		if(ammo <= 0 && canInHand.activeSelf) {
			canInHand.SetActive(false);
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
			//instantiate the dart
			GameObject nukeInstance;
			nukeInstance = PhotonNetwork.Instantiate(dartPrefab, canInHand.transform.position, canInHand.transform.rotation, 0) as GameObject;
			nukeInstance.GetComponent<PhotonView>().RPC ("NameNukeRPC", PhotonTargets.All, playerNetworkMover.myName);

		}

		
	}

	void FixedUpdate ()
	{
		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}



}