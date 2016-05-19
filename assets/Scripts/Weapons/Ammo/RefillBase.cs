using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillBase : MonoBehaviour {

	protected string ChildGONameForVisual = "Cylinder_001";
	protected int ammoToGive = -1; // -1 restocks in full
	protected string reloadsWepName = "soaker"; // see OnTriggerEnter for valid strings

	protected bool pickupWhenTouched = false;

	public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
    [SerializeField] float refillTimer;
    
	private GameObject restockedVisual;
    
	private WeaponBase playerWepToRecharge;

    [SerializeField] bool playerNearby;
    private FirstPersonController nearbyPlayerController;
	[SerializeField] bool refillAvailable = true;
	public bool randomizeSpawn;

	protected void RefillInit () {
        refillTimer = 0.0f;
		restockedVisual = transform.Find(ChildGONameForVisual).gameObject;
		if (randomizeSpawn) {
			int randomNumber = Random.Range(1,21);
			if (randomNumber % 2 == 0)
			{
				SetEmpty();
			}
		}
	}
	
	void Update () {
        //Cooldown timer

		/*if(nearbyPlayerController != null)
        {
			Debug.Log("playerNearby" + playerNearby + " useButt"+
			          nearbyPlayerController.useButton);
        }*/

		bool actionButtonDown = (playerNearby && nearbyPlayerController != null && nearbyPlayerController.useButton);
		
		if(playerNearby && (actionButtonDown || pickupWhenTouched) && refillAvailable)
        {
			// CHECK IF AMMO ALREADY FULL AND REFILL IF NOT //
			bool full = playerWepToRecharge.GiveAmmo(ammoToGive);

			if (!full)
			{
				SetEmpty();
			}
        }

    }

	IEnumerator RefillCooldown (float cooldownTime)
	{
		yield return new WaitForSeconds (cooldownTime);
		refillAvailable = true;
		restockedVisual.SetActive(true);
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.GetComponent<FirstPersonController>()) // other.tag == "Player")
        {
			playerNearby = true;
			nearbyPlayerController = other.GetComponent<FirstPersonController>();
			WeaponManager wepMan = other.GetComponent<WeaponManager>();
			if (wepMan) {
				switch(reloadsWepName) {
				case "soaker":
					playerWepToRecharge = wepMan.GetSoakerGun ();
					break;
				case "cans":
					playerWepToRecharge = wepMan.GetSodaGrenadeThrower ();
					break;
				case "nuke":
					playerWepToRecharge = wepMan.GetNukeThrower ();
					break;
				case "tripwire":
					playerWepToRecharge = wepMan.GetTripwire ();
					break;
				default:
					Debug.LogError ("Invalid ammo type " + reloadsWepName);
					break;
				}
			}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerNearby = false;
			playerWepToRecharge = null;
			nearbyPlayerController = null;
		}

    }

	void SetEmpty()
	{
		refillAvailable = false;
		refillTimer = Random.Range(minRefillTime, maxRefillTime);
		StartCoroutine(RefillCooldown(refillTimer));
		restockedVisual.SetActive(false);
	}

}
