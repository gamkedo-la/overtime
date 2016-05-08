using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class TripwireDropper : WeaponBase {

	// Animator anim;
	[SerializeField] string bombPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	
	public float ammo = 3;
	public GameObject ammoCount;

	public GameObject tripwireInHand;

	Vector3 origPos;

	// PNM FOR NAMING //
	public PlayerNetworkMover playerNetworkMover;


	// Use this for initialization
	void Start () 
	{
		//anim = GetComponentInChildren<Animator> ();
		ammoCount = this.transform.parent.parent.parent.transform.Find("VitalsCanvas/VitalsBar/AmmoCount").gameObject;
		origPos = tripwireInHand.transform.localPosition;
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
			tripwireInHand.transform.LookAt(shootToward);
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

		if(ammo <= 0 && tripwireInHand.activeSelf) {
			tripwireInHand.SetActive(false);
		}

		if (shooting){
			//stop shooting and unload
			shooting = false;
			loaded = false;
			//instantiate the dart
			Debug.Log("trying to drop tripwire");
			GameObject bombInstance;
			bombInstance = PhotonNetwork.Instantiate(bombPrefab, tripwireInHand.transform.position, tripwireInHand.transform.rotation, 0) as GameObject;
			bombInstance.GetComponent<Tripwire>().owner = playerNetworkMover.myName;
		}

		
	}

	void FixedUpdate ()
	{
		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}

}