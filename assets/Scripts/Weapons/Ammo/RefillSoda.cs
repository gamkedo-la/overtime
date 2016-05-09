using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class RefillSoda : MonoBehaviour {
	
	public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
	[SerializeField] float refillTimer;
	[SerializeField] GameObject Can;
	private SodaGrenadeThrower grenadeThrower;
	[SerializeField] bool playerNearby;
	private FirstPersonController nearbyPlayerController;
	private bool actionButtonDown;
	private bool refillAvailable = true;
	
	void Start () {
		refillTimer = 0.0f;
	}
	
	void Update () {
		
		if(playerNearby)
		{
			Debug.Log("Player nearby");
		}
		
		if (actionButtonDown)
		{
			Debug.Log("ActionButtonDown");
		}
		
		if(playerNearby && actionButtonDown && refillAvailable)
		{
			grenadeThrower.GiveAmmo(3);
			refillAvailable = false;
			refillTimer = Random.Range(minRefillTime, maxRefillTime);
			StartCoroutine(RefillCooldown(refillTimer));
			Can.SetActive(false);
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
		Can.SetActive(true);
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
