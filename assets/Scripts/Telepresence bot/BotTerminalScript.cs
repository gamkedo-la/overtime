using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BotTerminalScript : MonoBehaviour {

	public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
	[SerializeField] float refillTimer;
	[SerializeField] bool playerNearby;
	private FirstPersonController nearbyPlayerController;
	private Camera nearbyPlayerCamera;
	private bool actionButtonDown;
	[SerializeField] bool refillAvailable = true;
	public bool randomizeSpawn;

	void Start () {
		refillTimer = 0.0f;
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
		//Cooldown timer
		
		
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
			// CHECK IF AMMO ALREADY FULL AND REFILL IF NOT //
			//bool full = soaker.GiveAmmo();
			
			BotManager.instance.TakeRandomBotControl(nearbyPlayerController, nearbyPlayerCamera);
			SetEmpty();
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
	
	}
	
	void OnTriggerEnter(Collider other)
	{


		if (other.tag == "Player")
		{
			nearbyPlayerController = other.GetComponentInParent<FirstPersonController>();
			nearbyPlayerCamera = other.GetComponentInParent<PlayerNetworkMover>().GetPlayerCamera();
			playerNearby = true;
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
		{
			playerNearby = false;
			nearbyPlayerController = null;
			nearbyPlayerCamera = null;
		}
		
	}
	
	void SetEmpty()
	{
		refillAvailable = false;
		refillTimer = Random.Range(minRefillTime, maxRefillTime);
		StartCoroutine(RefillCooldown(refillTimer));
	}
	
	private void actionButtonPressed()
	{
		Debug.Log("ActionButtonEventMethod");
		actionButtonDown = true;
	}
}
