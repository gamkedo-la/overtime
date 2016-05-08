using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillWater : MonoBehaviour {

    public float minRefillTime = 15.0f, maxRefillTime = 45.0f;
    [SerializeField] float refillTimer;
    private GameObject Carboy;
    private SoakerGun soaker;
    [SerializeField] bool playerNearby;
    private FirstPersonController nearbyPlayerController;
    private bool actionButtonDown;

	void Start () {
        refillTimer = 0.0f;
        Carboy = transform.parent.Find("Cylinder_001").gameObject;
	}
	
	void Update () {
        //Cooldown timer
        if (refillTimer > 0)
        {
            refillTimer -= Time.deltaTime;
        }
        else if(!Carboy.GetActive())
        {
            Carboy.SetActive(true);
        }

        if(playerNearby)
        {
            Debug.Log("Player nearby");
        }

        if (actionButtonDown)
        {
            Debug.Log("ActionButtonDown");
        }

        if(playerNearby && actionButtonDown)
        {
			soaker.GiveAmmo();
            refillTimer = Random.Range(minRefillTime, maxRefillTime);
            Carboy.SetActive(false);
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

    void OnTriggerEnter(Collider other)
    {
        soaker = other.GetComponentInChildren<SoakerGun>();
        nearbyPlayerController = other.GetComponent<FirstPersonController>();
        if (other.tag == "Player")
        {
            playerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        soaker = null;
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
