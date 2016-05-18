using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerNetworkMover : Photon.MonoBehaviour {


	public delegate void Respawn(float time);
	public event Respawn RespawnMe;
	public delegate void SendMessage(string MessageOverlay);
	public event SendMessage SendNetworkMessage;
	public delegate void SendScore(string fragger, string fragged);
	public event SendScore SendNetworkScore;

	// CAMERA //

	[SerializeField] GameObject playerCamera;


	// ID //
	public string myName;
	
	// MOVEMENT //
	Vector3 position;
	Quaternion rotation;
	float smoothing = 10f;

	// HEALTH //
	public float health = 100f;
	public float maxHealth = 100f;
	public GameObject healthCount;
	bool myHealth = false;
	bool respawnSwitch = false; // Prevents multiple respawns from one kill

	// Are these obsolete? //
	float myPlayerFrag;
	float myPlayerDeath;

	// ANIMATION SYNC // 
	bool aim = false;
	bool sprint = false;
	bool initialLoad = true;

	// SOAKER PUSH //
	//[SerializeField] bool pushable = false;
	[SerializeField] bool soakerPushing = false;
	[SerializeField] Vector3 soakerPushForce;
	Vector3 positionToPush;
	int pushSteps = 0;




	// COLLIDERS //
	[SerializeField] GameObject colliderMaster;
	[SerializeField] Collider[] colliders;


	Rigidbody rigidbody;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		//Get animator for syncing
		anim = GetComponentInChildren<Animator> ();
		rigidbody = transform.GetComponent<Rigidbody>();
		colliders = colliderMaster.GetComponentsInChildren<Collider>();

		if(photonView.isMine)  // Activate player scripts if my character
		{
			GetComponent<Rigidbody>().useGravity = true;
			GetComponent<CharacterController>().enabled = true;
			GetComponent<WeaponManager>().enabled = true;
			(GetComponent("FirstPersonController") as MonoBehaviour).enabled = true;
			GetComponentInChildren<DartGun>().enabled = true;
			playerCamera.GetComponentInChildren<Camera>().enabled = true;
			gameObject.tag =  "Player";
			gameObject.layer = 14;
			colliderMaster.tag = "Player";
			foreach(Collider col in colliders)
			{
				col.transform.tag = "Player";
			}

			//GetComponentInChildren<Melee>().enabled = true;
			//GetComponentInChildren<AudioListener>().enabled = true;
			/*foreach(Camera cam in GetComponentsInChildren<Camera>())
			{
			cam.enabled = true;
			}*/
			healthCount = this.transform.parent.parent.transform.Find("VitalsCanvas/HealthBar/HealthCount").gameObject;
			myHealth = true;
			ComboGenerator.ActionRespawn();
			myName = PhotonNetwork.player.name;



		}
		else
		{
			StartCoroutine("UpdateData");
		}
	}

	public void HealPowerUp(){
			health += (maxHealth * 0.20f);
			if (health > maxHealth) {
				health = maxHealth;
			} 
		}


	IEnumerator UpdateData()
	{

		//Set transform for each clone if this is the first time we're loading. Should fix jitter
		if(initialLoad)
		{
			initialLoad = false;
			transform.position = position;
			transform.rotation = rotation; 

		}
		while(true)
		{
			transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * smoothing);
			transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * smoothing);
			anim.SetBool ("Aim", aim);
			anim.SetBool ("Sprint", sprint);
			yield return null;
		}
	}

	void FixedUpdate ()
	{
		if (myHealth){
			healthCount.GetComponent<Text>().text = health.ToString();
		}
		/*if (pushSteps > 0)
		{
			rigidbody.MovePosition (positionToPush * Time.deltaTime);
			pushSteps --;
		}

		/*while (soakerPushing) {
			r;
		}*/

		
	}


	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		// DL - Stream Input
		if(stream.isWriting)
		{
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(health);
			stream.SendNext(anim.GetBool ("Aim"));
            stream.SendNext(anim.GetBool ("Sprint"));		
			stream.SendNext(myName);
		}
		// DL - Stream Output. Read/Write order must be the same
		else
		{
			position = (Vector3) stream.ReceiveNext();
			rotation = (Quaternion) stream.ReceiveNext();
			health = (float)stream.ReceiveNext();
			aim = (bool)stream.ReceiveNext();
			sprint = (bool)stream.ReceiveNext();
			myName = (string)stream.ReceiveNext();
		}
	}



	[PunRPC]
	public void GetShot(float damage, string enemyName)
	{
		//health -= damage;

		if (photonView.isMine && !respawnSwitch) {


			if (SendNetworkMessage != null) // send messaging of the frag event
				SendNetworkMessage (myName + " was tagged by " + enemyName);
			if (RespawnMe != null)
				RespawnMe (3f);
			respawnSwitch = true;
			PhotonNetwork.Destroy (transform.parent.gameObject);
			/*SoundCenter.instance.PlayClipOn(
				//SoundCenter.instance.playerDie,transform.position);
				
			// DEATHMATCH LEGACY //
			if(SendNetworkScore != null) // Update the scoreboard data
			{
				SendNetworkScore(enemyName, myName);	}
		} else {
			//SoundCenter.instance.PlayClipOn(
				//SoundCenter.instance.playerHurt,transform.position);
		}*/
		}
	}

	[PunRPC]
	public void GetSoaked(Vector3 pushDirection, float pushForce)
	{
		if (photonView.isMine) {
			rigidbody.isKinematic = false;
			rigidbody.AddForce(pushDirection * pushForce);
			rigidbody.isKinematic = true;


			//positionToPush = (transform.position + (pushDirection * pushForce));
			//pushSteps = 3;

			//soakerPushForce = pushForce;
			//soakerPushing = true;
		}
	}



}
