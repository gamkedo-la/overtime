using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillNuke : MonoBehaviour {

    public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
    [SerializeField] float refillTimer;
    private GameObject LightEffect;
    private NukeThrower nukeThrow;
    [SerializeField] bool playerNearby;
    private FirstPersonController nearbyPlayerController;
	[SerializeField] bool refillAvailable = true;
	public bool randomizeSpawn;

	void Start () {
        refillTimer = 0.0f;
		LightEffect = transform.Find("Nuke").gameObject;
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
            // Debug.Log("Player nearby");
        }

		if(playerNearby && refillAvailable && nukeThrow)
        {
			// CHECK IF AMMO ALREADY FULL AND REFILL IF NOT //
			bool full = nukeThrow.GiveAmmo(1);

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
		LightEffect.SetActive(true);
	}

    void OnTriggerEnter(Collider other)
    {
		nearbyPlayerController = other.GetComponent<FirstPersonController>();
		WeaponManager wepMan = other.GetComponent<WeaponManager>();
		if (wepMan) {
			nukeThrow = wepMan.GetNukeThrower ();
		}
        if (other.tag == "Player")
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
		nukeThrow = null;
        nearbyPlayerController = null;
        if(other.tag == "Player")
        {
            playerNearby = false;
        }

    }

	void SetEmpty()
	{
		refillAvailable = false;
		refillTimer = Random.Range(minRefillTime, maxRefillTime);
		StartCoroutine(RefillCooldown(refillTimer));
		LightEffect.SetActive(false);
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
