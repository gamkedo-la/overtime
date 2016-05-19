using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class TripwireDropper : WeaponBase {

	// WEAPON NAME //
	[SerializeField] string weaponName;

	// Animator anim;
	[SerializeField] string bombPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	public GameObject ammoCount;

	public GameObject tripwireInHand;

	Vector3 origPos;

	// PNM FOR NAMING //
	public PlayerNetworkMover playerNetworkMover;


	// Use this for initialization
	void Start () 
	{
		ammo = 2;
		maxAmmo = 2;

		//anim = GetComponentInChildren<Animator> ();
		ammoCount = PlayerManager.instance.AmmoCount;
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
		origPos = tripwireInHand.transform.localPosition;
	}

	void OnEnable()
	{
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
	}
	
	// Update is called once per frame
	void Update (){

		// weapon bob during run
		tripwireInHand.transform.localPosition = origPos
			+ Mathf.Cos(transform.position.x) * Vector3.up * 0.05f
				+ Mathf.Cos(transform.position.z) * Vector3.right * 0.05f;
		// raise weapon when reloading
		if(loaded == false) {
			tripwireInHand.transform.localPosition -=
				0.15f*Vector3.up * ((shotTime + loadTime) - Time.time);
		}

		// No Aiming

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

		if(ammo <= 0 && tripwireInHand.activeSelf) {
			tripwireInHand.SetActive(false);
		}
		if(ammo > 0 && tripwireInHand.activeSelf == false) {
			tripwireInHand.SetActive(true);
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
			//instantiate the dart
			Debug.Log("trying to drop tripwire");
			GameObject bombInstance;
			bombInstance = PhotonNetwork.Instantiate(bombPrefab, tripwireInHand.transform.position, tripwireInHand.transform.rotation, 0) as GameObject;
			bombInstance.GetComponent<PhotonView>().RPC ("NameTripwireRPC", PhotonTargets.All, playerNetworkMover.myName);
		}

		
	}

	void FixedUpdate ()
	{
		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}

}