using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoakerGun : WeaponBase {


	// DL - This script is responsible for: 1) Graphical Effects of Shooting 2) Physics of shooting 3) Impacts, etc.

	// WEAPON NAME //
	[SerializeField] string weaponName;

	Animator anim;
	// DL - Collection (Matrix? Array?) of impacts that are moved when shooting. Since they already exist and are not repeatedly called, this is more resource friendly.
	[SerializeField] bool shooting = false;
	[SerializeField] bool loaded = true; // when unloaded, gun kills itself
	[SerializeField] float shotTime;
	[SerializeField] bool shotCooldown = true;
	[SerializeField] float shotCooldownTime = 0.5f;

    // OLD STREAM VERSION //
	/*public int ammo = 1200;
    public int maxAmmo = 1200;
	public int ammoDecay = 1; // how fast we burn ammo*/

	public float range = 10; 
	public float knockbackForce = 35; // how much force hit pushes with
	public GameObject ammoCount;  // referenced for UI readout
	public GameObject waterPrefab;
	private ParticleSystem waterSprayer; // grabbed off waterPrefab in Start()

	public GameObject waterGunModel;
	public ParticleSystem waterInTank;
	Vector3 wepOrig;

	// AIMING VARIABLES //
	public GameObject firingPointObj;

		// Use this for initialization
		void Start () 
		{
			ammo = 6;
			maxAmmo = 6;

			ammoCount = PlayerManager.instance.AmmoCount;
			PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
			waterPrefab.SetActive(true);
			waterSprayer = waterPrefab.GetComponent<ParticleSystem>();
			waterSprayer.enableEmission = false;
			wepOrig = waterGunModel.transform.localPosition;
		}

		void OnEnable()
		{
			PlayerManager.instance.AmmoName.GetComponent<Text> ().text = weaponName;
		}
		
		// Update is called once per frame
		void Update (){

		waterGunModel.transform.localPosition = wepOrig;
				/*+ Mathf.Cos(transform.position.x+transform.position.z) * Vector3.up * 0.03f
					+ Mathf.Cos(transform.position.x*0.7f+transform.position.z*0.4f) * Vector3.right * 0.03f
					+ Mathf.Cos(transform.position.x*0.3f+transform.position.z*0.5f) * Vector3.forward * 0.01f*/;
		/*waterGunModel.transform.localRotation =
				Quaternion.AngleAxis(-90.0f + Mathf.Cos (Time.time*0.1f)*3.0f,Vector3.right) *
					Quaternion.AngleAxis(Mathf.Cos (Time.time*0.4f*0.1f)*3.0f,Vector3.up)*/;

			if(Input.GetButton ("Fire1") && !Input.GetKey(KeyCode.LeftShift)){ // While pressing fire, we aren't running, and we have ammo, we are shooting.
				if(loaded == true && ammo > 0){
					shooting = true;
					// Debug.Log ("It's shooting");
					waterSprayer.enableEmission = true;
					/* SoundCenter.instance.PlayClipOn(
						SoundCenter.instance.watergunSquirt,transform.position); */
				}		
				else{
					waterSprayer.enableEmission = false;
					shooting = false;
					/* SoundCenter.instance.PlayClipOn(
						SoundCenter.instance.playerNoAmmoTriedToFire,transform.position); */
				}		
			}
			if(Input.GetButtonUp ("Fire1")){
					shooting = false;
					waterSprayer.enableEmission = false;
				}

			RaycastHit hitInfo;
			Vector3 shootToward;
			Ray centerRay = Camera.main.ScreenPointToRay(new Vector3((Camera.main.pixelWidth/2), (Camera.main.pixelHeight/2), 0f));

			if(loaded == true & ammo > 0) {
				int filterLayers = ~LayerMask.GetMask("AmmoBox");
				
				if(Physics.Raycast(centerRay, out hitInfo, 150.0f, filterLayers) &&
				   transform.InverseTransformPoint(hitInfo.point).z > 0.5f) {

					shootToward = hitInfo.point;
				} else {
					shootToward = centerRay.origin + centerRay.direction * 50.0f;
				}
				
				waterGunModel.transform.LookAt(shootToward);
			}
			


		if (shooting && shotCooldown)
		{ // how we shoot
				RaycastHit hit;
				Vector3 fwd = transform.TransformDirection (Vector3.forward);
				if (Physics.Raycast (firingPointObj.transform.position, firingPointObj.transform.forward, out hit, range))
				{
					Debug.Log ("SOAKER: Firing");
					//Debug.DrawLine(transform.position, hit.point, Color.blue, 2);  // TEMP display of raycast
					if (hit.transform.tag == "Enemy") { // if we hit a rigidbody, apply force
						hit.transform.GetComponent<PhotonView> ().RPC ("GetSoaked", PhotonTargets.All, fwd, knockbackForce);
						Debug.Log ("SOAKER: Hit");
					}
				Debug.Log("SOAKER: Fwd = " + fwd);
				Debug.Log("SOAKER: Position to add = " + (fwd * knockbackForce));
				}
				
				// Delay The Next Shot //
				shotCooldown = false;
				StartCoroutine(Cooldown());

				// Lose Ammo When We Shoot //
				ammo --;
				// OLD STREAM VERSION //
				// lose ammo gradually as we shoot
				/*int DecayRate = (Mathf.RoundToInt(Time.deltaTime * ammoDecay));
				ammo -= DecayRate;
				Debug.Log ("SOAKER: Decay Rate = " + DecayRate);*/	
		}

		if (ammo <= 0) { // if we run out of ammo, we aren't loaded
			waterInTank.gameObject.SetActive (false);
			loaded = false;
		}

		if (loaded == false && ammo > 0) { // if we run out of ammo, we aren't loaded
			waterInTank.gameObject.SetActive (true);
			loaded = true;
		}
}

		IEnumerator Cooldown ()
		{
			yield return new WaitForSeconds (shotCooldownTime);
			shotCooldown = true;
		}

		void FixedUpdate ()
		{
			// Update Ammo UI //
			ammoCount.GetComponent<Text>().text = (ammo).ToString();
			// OLD STREAM VERSION //
			//ammoCount.GetComponent<Text>().text = (ammo/6).ToString();  
		}

}