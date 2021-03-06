﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboStats : MonoBehaviour {

// Variables for determining if Scoring Plays have occured.
// ComboGenerator references and updates these.
// This script functions as the holder/menu of options.
   public class playerTag
    {
        public string playerName;
        public float time;

        public playerTag(string nameIn)
        {
            playerName = nameIn;
            time = Time.time;
        }
    }

	public static ComboStats instance;

    
	[Header("General")]
	public string[] players;
	public string targetName;

	[Header("Generic Assist")]
	public string[] mostRecentAssistHit;

	[Header("Incoming Attacks")]
	public float[] lastTimeDartHitMe;
	public float lastDeathTime;
	public int totalNearMisses;
    
    
	[Header("Tags")]
	//public static List<playerTag> playersDartTagged; // IMPLEMENT PLAYERTAG LATER - How do I feed that through the variable update?
	public List<string> playersDartTagged;
	public int totalDartTags = 0;
	public float lastTagTime = 0;
	float passUpProwessTimer;
    public float passUpProwessTimeWindow;
	public float longestTag = 0;
    

	[Header("Multi-Tag")]// - to eliminate repeats
	public bool doubleTag = false;
	public bool tripleTag = false;

	[Header("Soda Grenade")]
	public List<string> playersSodaGrenaded;
	public int totalSodaHits = 0;

	[Header("Trip Wire")]
	public List<string> playersTripwired;
	public List<Tripwire> activeTripwires;
	public int totalTripwireHits = 0;

	[Header("This Life Stats")] // ADD ALL OF THESE TO RespawnClear()
	public int sodaHitsTL = 0;
	public int dartTagsTL = 0;
	public int tripwireHitsTL = 0;
	public int nearMissesTL = 0;



	void Start(){
		instance = this;
		players = new string[]{"ScoringTarget"}; //Don't include self?
		mostRecentAssistHit = new string[players.Length];
		activeTripwires = new List<Tripwire>();
		lastTimeDartHitMe = new float[players.Length];
		//playersDartTagged = new List<playerTag>();
		playersDartTagged = new List<string>();
	}

	void Update ()
    {
        //Decrement timers
        if (passUpProwessTimer > 0)
        {
            passUpProwessTimer -= Time.deltaTime;
        }
	}

	public void RespawnClear () {

		// Resets temporary stats when player is Tagged Out

		sodaHitsTL = 0;
		dartTagsTL = 0;
		tripwireHitsTL = 0;
		nearMissesTL = 0;
	}

	// ADD STATS FUNCTIONS //
	// Called by the Generator to Add Stats


	public void AddDartTag (string playerHit, float distanceTraveled) 
	{
		lastTagTime = Time.time;
		totalDartTags ++;
		dartTagsTL ++;
		playersDartTagged.Add(playerHit);
		if (distanceTraveled > longestTag) { longestTag = distanceTraveled;}

	}

	public void AddSodaHit (string playerHit) 
	{
		totalSodaHits ++;
		sodaHitsTL ++;
		playersSodaGrenaded.Add(playerHit);
	}

	public void AddTripwireHit (string playerHit) 
	{
		totalTripwireHits ++;
		tripwireHitsTL ++;
		playersTripwired.Add(playerHit);
	}

	public void AddNearMiss ()
	{
		totalNearMisses ++;
		nearMissesTL ++;
	}


}
