using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class SodaGrenadeThrower : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// WEAPON NAME //
	[SerializeField] string weaponName;
	GameObject ammoName;

	// Animator anim;
	[SerializeField] string canPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	public GameObject ammoCount;
	public GameObject canInHand;
	string myName;

	Vector3 origPos;


	// Use this for initialization
	void Start () 
	{
		ammo = 3;
		maxAmmo = 6;
		//anim = GetComponentInChildren<Animator> ();
		ammoCount = PlayerManager.instance.AmmoCount;
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
		origPos = canInHand.transform.localPosition;
		myName = PhotonNetwork.player.name;
	}

	void OnEnable()
	{
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
	}
	
	// Update is called once per frame
	void Update (){

		// weapon bob during run
		canInHand.transform.localPosition = origPos /*
			+ Mathf.Cos(transform.position.x) * Vector3.up * 0.05f
				+ Mathf.Cos(transform.position.z) * Vector3.right * 0.05f*/;
		// raise weapon when reloading
		if(loaded == false) {
			canInHand.transform.localPosition -=
				0.15f*Vector3.forward * ((shotTime + loadTime) - Time.time);
		}

	
		// Variables for Aim Raycast
		RaycastHit hitInfo;
		Vector3 shootToward;
		Ray centerRay = Camera.main.ScreenPointToRay(new Vector3((Camera.main.pixelWidth/2), (Camera.main.pixelHeight/2), 0f));

		// Aiming
		if(loaded == true & ammo > 0) {
			int filterLayers = ~LayerMask.GetMask("AmmoBox");
			
			if(Physics.Raycast(centerRay, out hitInfo, 150.0f, filterLayers) &&
			   transform.InverseTransformPoint(hitInfo.point).z > 0.5f) {

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
		if(ammo > 0 && canInHand.activeSelf == false) {
			canInHand.SetActive(true);
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
			//instantiate the dart
			GameObject canInstance;
			canInstance = PhotonNetwork.Instantiate(canPrefab, canInHand.transform.position, canInHand.transform.rotation, 0) as GameObject;
			canInstance.GetComponent<SodaGrenScript>().owner = PhotonNetwork.player.name;
			
		}

		
	}

	public bool GiveAmmo(int amt) {
		if (ammo >= maxAmmo) {
			return true;
		} else {
			ammo += amt;
			if (ammo > maxAmmo) {
				ammo = maxAmmo;
			}
			// Prompt gun to display its new ammo
			loaded = false;
			shotTime = Time.time;
			return false;
		}
	}

	void FixedUpdate ()
	{
		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}



}