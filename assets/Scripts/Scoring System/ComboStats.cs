using UnityEngine;
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
    
	[Header("General")]
	public string[] players;
	public string targetName;

	[Header("Generic Assist")]
	public string[] mostRecentAssistHit;

	[Header("Deaths")]
	public float[] lastTimeDartHitMe;
	public float lastDeathTime;
    
    
	[Header("Tags")]
	public List<playerTag> playersTagged;
    

	[Header("Multi-Tag")]// - to eliminate repeats
	public bool doubleTag = false;
	public bool tripleTag = false;

	[Header("Trip Wire")]
	public List<Tripwire> activeTripwires;

	void Start(){
		players = new string[]{"ScoringTarget"}; //Don't include self?
		mostRecentAssistHit = new string[players.Length];
		activeTripwires = new List<Tripwire>();
		lastTimeDartHitMe = new float[players.Length];
		playersTagged = new List<playerTag>();
	}


}
