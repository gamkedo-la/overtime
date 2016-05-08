using UnityEngine;
using System.Collections;

public class DartRecoverableScript : WeaponBase {

	public string owner;
	public float moveSpeed;
	public Rigidbody rb;
	public Collider col;
	public float dartRange;
	public bool shot = false;
	public bool airborne = false;
	private bool killMe = false;
	public float damage = 100f;
	public bool recoverable = false;
	public bool recovered = false;
	public EventDartTrigger eventDartTrigger;
	public string lastHit;

	PhotonView photonView;

	// DISTANCE TRAVELED //
	[SerializeField] Vector3 initialPosition;
	[SerializeField] float distanceTravelled;

	[SerializeField] float selfdestructTime;
	[SerializeField] float hitTime = 0;

	void Start() 
	{
		photonView = transform.GetComponent<PhotonView> ();
		initialPosition = transform.position;
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		shot = true;
	}

	void FixedUpdate() 
	{
		
		if (shot)
		{
			//Initiate velocity forward
			rb.velocity = (transform.forward * moveSpeed);

			//Turn on gravity, make "airborne" for interaction/physics, stop shot so velocity isn't continously applied
			rb.useGravity = true;
			airborne = true;
			shot = false;
		}

		if (airborne)
		{
			//Rotate towards velocity
			RaycastHit hit;
			rb.transform.LookAt( rb.transform.position + rb.velocity);
			if(Physics.Raycast(transform.position, transform.forward, out hit, dartRange))
				{
					
					if(hit.transform.tag == "Enemy")
					{
						distanceTravelled = Vector3.Distance (transform.position, initialPosition);
						GameObject tempGO = hit.transform.gameObject;
						string hitName = hit.transform.GetComponent<PlayerNetworkMover>().myName;
						if(hitName != lastHit)
						{
						hit.transform.GetComponent<PhotonView>().RPC ("GetShot", PhotonTargets.All, damage, owner);
						ComboGenerator.ActionDartTag(hitName, distanceTravelled);
						hitName = lastHit;
						}
						StartCoroutine ("LastHitReset");
						//Destroy(gameObject);
					}
					if(hit.transform.tag == "Dummy")
					{
						GameObject tempGO = hit.transform.gameObject;
						string hitName = tempGO.transform.name;
						hit.transform.gameObject.SetActive(false);
						//ComboGenerator.ActionDartTag(hitName);
						//eventDartTrigger.EventDummyKill(hitName);		
					}
					//if(hit.transform.tag == "Ground")
					//{
					//	Destroy(gameObject);
					//}
					if(hit.transform.tag == "Player")
					{
						return;
					}
					if(hit.transform.tag == "Friendly")
					{
						return;
					}
					
					//COME BACK TO THIS. Currently happens too far from the collision
					if((hit.transform.tag == "Map") || (hit.transform.tag == "Ground"))
					{
					distanceTravelled = Vector3.Distance (transform.position, initialPosition);
						//Stop Physics and stick to what we hit
						if (killMe == false)
						{
							//SoundCenter.instance.PlayClipOn(
								//SoundCenter.instance.dartStick,transform.position);
							//hitTime = (Time.time + selfdestructTime);
							recoverable = true;
							rb.useGravity = false;
							rb.isKinematic = true;
							col.GetComponent<Collider>().isTrigger = true;
							//rb.transform.parent = hit.transform;
							
						}


						
					}
					
				}

		}

		if (killMe == true && Time.time >= hitTime)
		{
			Destroy(gameObject);
		}

	}

	IEnumerator LastHitReset ()
	{
		yield return new WaitForSeconds(2);
		lastHit = "";
	}

	void OnTriggerEnter (Collider other)
	{
		if (recoverable = true)
		{
			Debug.Log("Attempting Recovery");
			if (other.transform.tag == "Player")
			{
				Debug.Log("Attempting Player Recovery");
				other.GetComponentInChildren<DartGun>().GiveAmmo(1);
				Destroy(gameObject);
			}
		}
	}


	[PunRPC]
	public void NameDartRPC (string playerName)
	{
		owner = playerName;
	}

}