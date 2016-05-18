
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;



public class Tripwire : WeaponBase 
{

	// STATES //
	private bool deployable = true; 		// TRUE if there is room to place trap
	private bool deployed = false; 			// TRUE if on the ground and extended
	public bool activated = false; 		// TRUE if a player has set off the trap
	//
	// COMPONENTS // 
	public GameObject receiver;  			// The telephone receiver
	public GameObject deployedReceiver;		// Invisible, used as a positioning reference
	public GameObject static_cord; 			// Curly cord, just as a visual decoration
	public GameObject cord_base_connect;	// Invisible positioning reference: extended cord
	public GameObject cord_receiver_connect;// Invisible positioning reference: extended cord
	public SelfDestruct selfDestruct;

	public WeaponManager wm; // For auto switching weapons after deployment
	private Rigidbody rb;
	private LineRenderer lr;

	// BLINKING RED LIGHT //
	public Light redLight;
	public int pulseCluster;
    public int pulseClusterDelay;
    public float pulseSpeed; 
    public int totalCycles;
    
  	private int clusterCtr = 0;
	private int delayCtr = 0;
	private int cycleCtr;
	private float timer;

    // AUDIO //
    public AudioSource soundPlayer;
    public AudioClip deploymentSound;
    public AudioClip activationSound;

	// ID //
	public string owner;
	PhotonView photonView;


    //--------------------
	//
	// 	  SETUP
	//
	//----------------
	//
	//
	void Start () 
	{
		rb = GetComponent<Rigidbody>();
		lr = GetComponent<LineRenderer>();
		photonView =  transform.GetComponent<PhotonView> ();

		// cdeleon changed: deploys on instantiate, isn't same as model in hand
		deployed = true;
		rb.isKinematic = false; 	// Manifest as a physical object
		transform.parent = null; 	// Leave player space
		rb.useGravity = true; 		// Start falling
		soundPlayer.PlayOneShot(deploymentSound);
	}



    //-------------------------
	//
	// 	  WIRE COLLIDER
	//		TRIGGERING
	//
	//---------------------
	//
	//
	void OnTriggerEnter(Collider other)
	{
		// Check for various reasons to ignore the trigger event
		if(activated || !deployed || other.gameObject.tag != "Player") {
			return;
		}
		string hitName = other.transform.parent.parent.GetComponent<PlayerNetworkMover> ().myName;	
		photonView.RPC ("tripwireActivated", PhotonTargets.All, hitName);
			
			

		
	}
	//
	void OnTriggerStay(Collider other)
	{
		// If walking around with it and there is no room to plant it, block any tries to deploy
		if(!deployed) {
			deployable = false;
		}
	}
	//
	void OnTriggerExit(Collider other)
	{
		// Start allowing deployment again (might be overridden at any point by OnTriggerStay...)
		if(!deployed && other.gameObject.tag != "Player") {
			deployable = true;
		}
	}
	

	
	//-----------------------
	//
	// 	  UPDATE CYCLE
	//
	//-------------------
	//
	//
	void Update ()
	{
		//
		// Update the dynamic cord drawing start/end points
		lr.SetPosition(0, cord_base_connect.transform.position);
		lr.SetPosition(1, cord_receiver_connect.transform.position);

		// Update deployment animation
		if(deployed) {
			static_cord.transform.localScale = Vector3.Lerp(static_cord.transform.localScale, Vector3.zero, 0.1f);
			receiver.transform.position = Vector3.Lerp(receiver.transform.position, deployedReceiver.transform.position, 0.1f);
			receiver.transform.rotation = Quaternion.Slerp(receiver.transform.rotation, deployedReceiver.transform.rotation, 0.1f);
			// wm.ReleaseTripwire();

		}
		//
		// Update blinking light
		if(activated) {
			timer += Time.deltaTime;
			if(timer > pulseSpeed) {
				timer = 0;
				if(clusterCtr++ >= pulseCluster) {
					if(delayCtr++ >= pulseClusterDelay) {
						clusterCtr = 0;
						delayCtr = 0;
						if(cycleCtr++ >= totalCycles) {
							cycleCtr = 0;
							activated = false;
							redLight.enabled = false;
						} else {
							redLight.enabled =  !redLight.enabled;	
						}
					} 
				} else {
					redLight.enabled =  !redLight.enabled;
				} 
			}	
		}
	}

	[PunRPC]
	public void NameTripwireRPC (string playerName)
	{
		owner = playerName;
	}

	[PunRPC]
	public void tripwireActivated (string hitName)
	{
		activated = true;
		ComboGenerator.ActionWireTrip (owner, hitName);
		soundPlayer.PlayOneShot (activationSound);
		selfDestruct.enabled = true;
	}

}