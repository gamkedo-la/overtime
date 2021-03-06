﻿/*using UnityEngine;
using System.Collections;


public class RoundTimerScript : MonoBehaviour {

	public float roundTimerInMinutes = 5f;
	private float roundTimerInSeconds;
	public bool timeStarted = false;
	
	

	void Start () {
	
		
		roundTimerInSeconds = (roundTimerInMinutes * 60F);
	}


	void Update()
	{
		if(timeStarted == true)
		{
			roundTimerInSeconds -= Time.deltaTime;
		}
		
	}*/
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Simple script that uses a property to sync a start time for a multiplayer game.
/// </summary>
/// <remarks>
/// When entering a room, the first player will store the synchronized timestamp. 
/// You can't set the room's synchronized time in CreateRoom, because the clock on the Master Server
/// and those on the Game Servers are not in sync. We use many servers and each has it's own timer.
/// 
/// Everyone else will join the room and check the property to calculate how much time passed since start.
/// You can start a new round whenever you like.
/// 
/// Based on this, you should be able to implement a synchronized timer for turns between players.
/// </remarks>
public class RoundTimerScript : MonoBehaviour
{
    public int MinutesPerRound = 5;
    public int MinutesPerPostRound = 1;
	bool roundOverCallSwitch = true;
	public bool postRoundOverCallSwitch = false;
    int SecondsPerTurn;                 // time per round/turn
    [SerializeField] double StartTime;                        // this should could also be a private. i just like to see this in inspector

    private bool startRoundWhenTimeIsSynced;        // used in an edge-case when we wanted to set a start time but don't know it yet.
    private const string StartTimeKey = "st";       // the name of our "start time" custom property.

    // CUSTOM ADDITIONS //
    public GameObject timeCount;
	private Text text;
	[SerializeField] string minutes;
	[SerializeField] string seconds;
	int currentLevel = 0; // Prevents double round over


    private void StartRoundNow()
    {
        // in some cases, when you enter a room, the server time is not available immediately.
        // time should be 0.0f but to make sure we detect it correctly, check for a very low value.
        if (PhotonNetwork.time < 0.0001f)
        {
            // we can only start the round when the time is available. let's check that in Update()
            startRoundWhenTimeIsSynced = true;
            return;
        }
        startRoundWhenTimeIsSynced = false;

        

        ExitGames.Client.Photon.Hashtable startTimeProp = new Hashtable();  // only use ExitGames.Client.Photon.Hashtable for Photon
        startTimeProp[StartTimeKey] = PhotonNetwork.time;
        PhotonNetwork.room.SetCustomProperties(startTimeProp);              // implement OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged) to get this change everywhere
    }

    void Start ()
    {
    	text = timeCount.transform.GetComponent<Text> ();
    	SecondsPerTurn = (MinutesPerRound * 60);
    }
    

    /// <summary>Called by PUN when this client entered a room (no matter if joined or created).</summary>
    public void OnJoinedRoom()
    {
        if (PhotonNetwork.isMasterClient)
        {
            this.StartRoundNow();
        }
        else
        {
            // as the creator of the room sets the start time after entering the room, we may enter a room that has no timer started yet
            Debug.Log("StartTime already set: " + PhotonNetwork.room.customProperties.ContainsKey(StartTimeKey));
        }
    }

    /// <summary>Called by PUN when new properties for the room were set (by any client in the room).</summary>
    public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(StartTimeKey))
        {
            StartTime = (double)propertiesThatChanged[StartTimeKey];
        }
    }

    /// <remarks>
    /// In theory, the client which created the room might crash/close before it sets the start time.
    /// Just to make extremely sure this never happens, a new masterClient will check if it has to
    /// start a new round.
    /// </remarks>
    public void OnMasterClientSwitched(PhotonPlayer newMasterClient)
    {
        if (!PhotonNetwork.room.customProperties.ContainsKey(StartTimeKey))
        {
            Debug.Log("The new master starts a new round, cause we didn't start yet.");
            this.StartRoundNow();
        }
    }

	public void OnLevelWasLoaded(int level)
	{
		if (level == 1)
		{
			// Get TimeCount after load for the
			currentLevel = level;
			timeCount = GameObject.Find ("TimeCount");
			text = timeCount.transform.GetComponent<Text> ();
			Debug.Log ("Level " + level + " was loaded.", gameObject);
		}
	}


    void Update()
    {
        if (startRoundWhenTimeIsSynced)
        {
            this.StartRoundNow();   // the "time is known" check is done inside the method.
        }

        float elapsedTime = (float)(PhotonNetwork.time - StartTime);
        float remainingTime = SecondsPerTurn - (elapsedTime % SecondsPerTurn);

        minutes = (Mathf.Floor(remainingTime / 60).ToString("00"));
		seconds = (Mathf.Floor(remainingTime % 60).ToString("00"));

		if (text) {
			text.text = (minutes + ":" + seconds);
		}

		if (remainingTime <= 1) {
			if (currentLevel == 0 && roundOverCallSwitch) {
				roundOverCallSwitch = false;
				GameManager.instance.RoundOver ();

			}
			if (remainingTime <= 1 && postRoundOverCallSwitch) {
				GameManager.instance.PostRoundOver ();
			}
		}
    }

    /*
    public void OnGUI()
    {
        // alternatively to doing this calculation here:
        // calculate these values in Update() and make them publicly available to all other scripts
        


        // simple gui for output
        GUILayout.BeginArea(TextPos);
        GUILayout.Label(string.Format("elapsed: {0:0.000}", elapsedTime));
        GUILayout.Label(string.Format("remaining: {0:0.000}", remainingTime));
        GUILayout.Label(string.Format("turn: {0:0}", turn));
        if (GUILayout.Button("new round"))
        {
            this.StartRoundNow();
        }
        GUILayout.EndArea();
    }
    */
}

	