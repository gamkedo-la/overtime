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

	[SerializeField] float selfdestructTime;
	[SerializeField] float hitTime = 0;

	void Start() 
	{
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
						hit.transform.GetComponent<PhotonView>().RPC ("GetShot", PhotonTargets.All, damage, owner);
						//Destroy(gameObject);
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
				
						//Stop Physics and stick to what we hit
						if (killMe == false)
						{
							SoundCenter.instance.PlayClipOn(
								SoundCenter.instance.dartStick,transform.position);
							//hitTime = (Time.time + selfdestructTime);
							recoverable = true;
							rb.useGravity = false;
							rb.isKinematic = true;
							col.GetComponent<Collider>().enabled = false;
							rb.transform.parent = hit.transform;
							
						}


						
					}
					
				}

		}

		if (killMe == true && Time.time >= hitTime)
		{
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter (Collider other)
	{
		if (recoverable = true)
		{
			if (other.transform.tag == "Player")
			{
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