using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class RefillSoda : MonoBehaviour {
	
	public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
	[SerializeField] float refillTimer;
	[SerializeField] GameObject Cans;
	[SerializeField] GameObject Lights;
	private SodaGrenadeThrower grenadeThrower;
	[SerializeField] bool playerNearby;
	private FirstPersonController nearbyPlayerController;
	private bool actionButtonDown;
	[SerializeField] bool refillAvailable = true;
	public bool randomizeSpawn = false;
	
	void Start () {
		if (randomizeSpawn) {
		int randomNumber = Random.Range(1,21);
			if (randomNumber % 2 == 0)
			{
				SetEmpty();
			}
			else{
				return;
			}
		}
	}
	
	void Update () {
		
		if(playerNearby)
		{
			Debug.Log("Player nearby");
		}
		
		if (actionButtonDown)
		{
			Debug.Log("ActionButtonDown", gameObject);
		}
		
		if(playerNearby && actionButtonDown && refillAvailable)
		{
			// ADD AMMO ATTEMPT
			bool full = grenadeThrower.GiveAmmo(3);

			// THESE SHOULD NOT RUN IF THE PLAYER IS FULL OF AMMO
			if (!full)
			{
				SetEmpty();
			}
		}
		
		if(playerNearby && nearbyPlayerController != null && nearbyPlayerController.useButton)
		{
			actionButtonDown = true;
		}
		else
		{
			actionButtonDown = false;
		}
		
		
	}

	IEnumerator RefillCooldown (float cooldownTime)
	{
		yield return new WaitForSeconds (cooldownTime);
		refillAvailable = true;
		Cans.SetActive(true);
		Lights.SetActive(true);
	}
	
	void OnTriggerEnter(Collider other)
	{
		grenadeThrower = other.GetComponentInChildren<SodaGrenadeThrower>();
		nearbyPlayerController = other.GetComponent<FirstPersonController>();
		if (other.tag == "Player")
		{
			playerNearby = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		grenadeThrower = null;
		nearbyPlayerController = null;
		if(other.tag == "Player")
		{
			playerNearby = false;
		}
		
	}

	void SetEmpty ()
	{
		refillAvailable = false;
		refillTimer = Random.Range(minRefillTime, maxRefillTime);
		StartCoroutine(RefillCooldown(refillTimer));
		Cans.SetActive(false);
		Lights.SetActive(false);
	}
	
	private void actionButtonPressed()
	{
		Debug.Log("ActionButtonEventMethod");
		actionButtonDown = true;
	}
	
	/* void OnEnable()
    {
        EventManager.StartListening(StandardEventName.ActionButton, actionButtonPressed);
    }

    void OnDisable()
    {
        EventManager.StopListening(StandardEventName.ActionButton, actionButtonPressed);
    }*/
}
