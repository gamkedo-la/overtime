﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class DartGun : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// WEAPON NAME //
	[SerializeField] string weaponName;


	// Animator anim;
	[SerializeField] string dartPrefab;
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true;
	[SerializeField] float shotTime;
	public float loadTime = 2;	

	public GameObject gunObj;
	GameObject ammoCount;

	// DARTS DISAPPEARING //
	public GameObject dartAmmoGOFirst;
	public GameObject dartAmmoGOSecond;
	public GameObject dartAmmoGOLast;

	public ScoringPlayHolder scoringPlayHolder;

	// AIMING VARIABLES //
	public GameObject firingPointObj;

	// PNM FOR NAMING //
	public PlayerNetworkMover playerNetworkMover;
	



	void UpdateAmmoModelVis() {
		dartAmmoGOLast.SetActive(ammo>0);
		dartAmmoGOSecond.SetActive(ammo>1);
		dartAmmoGOFirst.SetActive(ammo>2);
	}

	public override bool GiveAmmo(int amt) {
		bool wasFull = base.GiveAmmo (amt);

		// Prompt gun to display its new ammo
		if (wasFull == false) {
			loaded = false;
			shotTime = Time.time;
			UpdateAmmoModelVis ();
		}
		return wasFull;
	}

	// Use this for initialization
	void Start () 
	{
		ammo = 3;
		maxAmmo = 5;
		//anim = GetComponentInChildren<Animator> ();

		// Grab the Ammo Count Object

		ammoCount = PlayerManager.instance.AmmoCount;
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
		UpdateAmmoModelVis();
	}

	void OnEnable()
	{
		PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
	}
	
	// Update is called once per frame
	void Update (){

		// Variables for Dart Aim Raycast
		RaycastHit hitInfo;
		Vector3 shootToward;
		Ray centerRay = Camera.main.ScreenPointToRay(new Vector3((Camera.main.pixelWidth/2), (Camera.main.pixelHeight/2), 0f));

		// Dart Gun Aiming
		if(loaded == true & ammo > 0) {
			int filterLayers = ~LayerMask.GetMask("AmmoBox");
			
			if(Physics.Raycast(centerRay, out hitInfo, 150.0f, filterLayers) &&
			   	transform.InverseTransformPoint(hitInfo.point).z > 0.5f) {
				shootToward = hitInfo.point;
			} else {
				shootToward = centerRay.origin + centerRay.direction * 50.0f;
			}
			
			gunObj.transform.LookAt(shootToward);
		}


		// Shoot if we hit fire, aren't running, have ammo, and round is loaded
		if(Input.GetButtonDown ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){
			if(loaded == true && ammo > 0){
				
				/*SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.dartShoot,transform.position); */

				shooting = true;
				shotTime = Time.time;
				Debug.Log ("It's firing");
			} else {

				/*SoundCenter.instance.PlayClipOn(
					SoundCenter.instance.playerNoAmmoTriedToFire,transform.position); */
			}
		}

		// Reload if the cooldown has happened
		if(loaded == false && Time.time >= (shotTime + loadTime)){ 
			loaded = true;
		}

		// Stop shooting, unload, and instantiate the dart
		if (shooting){ 
			shooting = false;
			loaded = false;
			ammo -= 1;
			UpdateAmmoModelVis();
			GameObject dartInstance;
			dartInstance = PhotonNetwork.Instantiate(dartPrefab, firingPointObj.transform.position, firingPointObj.transform.rotation, 0) as GameObject;
			dartInstance.GetComponent<PhotonView>().RPC ("NameDartRPC", PhotonTargets.All, playerNetworkMover.myName);
			/*scoringPlayHolder.DisplayScoringPlay ("THIS IS ONE" + "! " + Random.Range(5,100) + "Pts");
			scoringPlayHolder.DisplayScoringPlay ("THIS IS TWO" + "! " + Random.Range(5,100) + "Pts");
			scoringPlayHolder.DisplayScoringPlay ("THIS IS THREE" + "! " + Random.Range(5,100) + "Pts");*/
		}

		
	}

	void FixedUpdate ()
	{
		Quaternion gunRotGoal = transform.rotation * Quaternion.AngleAxis(180.0f,Vector3.up);
		// Point the gun up and away if out of ammo
		if(loaded == false || ammo == 0) {
			gunRotGoal *= Quaternion.AngleAxis(68.0f,Vector3.up);
			gunRotGoal *= Quaternion.AngleAxis(-38.0f,Vector3.right);
		}
		
		gunObj.transform.rotation = Quaternion.Slerp(
			gunObj.transform.rotation,
			gunRotGoal, 0.3f);


		ammoCount.GetComponent<Text>().text = ammo.ToString();
	}



}