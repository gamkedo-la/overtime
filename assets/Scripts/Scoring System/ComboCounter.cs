using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class ComboCounter : MonoBehaviour {

	public static ComboCounter instance;

    [SerializeField] int comboTotal;
    [SerializeField] int comboMultiplier;
	bool[] comboUnique;
	public ScoreHolder scoreHolder;
    public ComboHolder comboHolder;
    public NetworkManagerScript networkManager;
    public string myPlayerName;

    string hitPlayerName;
    string targetName;

	[SerializeField] int score;
    
	//public ScoreHolder scoreHolder;
//    Dictionary<string, ComboData> comboValues = new Dictionary<string, ComboData>();

    //This class is used to track value an uniqueness of each combo the player can
    //perform at a given moment. It is initialized with the score value of the combo
    private class ComboData
    {
        public int value;
        public bool unique;

        public ComboData(int valueIn)
        {
            value = valueIn;
        }
    }

    void Start () {
		instance = this;
		//scoreHolder = transform.parent.parent.transform.Find ("VitalsCanvas/ScoreBar/ScoreCount").GetComponent<ScoreHolder> ();
        targetName = "ScoringTarget"; //TODO get the actual target name
        comboUnique = new bool[ComboList.count()];
        resetCombo();
    }

    public void GetMyPlayerName()
    {
        myPlayerName = PhotonNetwork.player.name;
    }

    //Reset the combo. Used on spawn and death
    public void resetCombo()
    {
		instance.comboTotal = 0;
		instance.comboMultiplier = 0;

        for(int i = 0; i < comboUnique.Length; i++)
        {
			instance.comboUnique[i] = true;
        }
    }

    //Method called by specific combos when a combo is pulled off
    public static void addCombo(ComboList.Combos comboName, string hitPlayerName)
    {
		if (/*playerName == targetName &&*/ instance.comboUnique[(int)comboName]) // ADD BACK TARGET REQUIREMENT LATER
        {
            instance.comboTotal += ComboList.getComboValue(comboName);
			instance.comboMultiplier++;
			instance.comboUnique[(int)comboName] = false;
        }
        else
        {
			instance.comboTotal += ComboList.getComboValue(comboName);
        }
        instance.UpdateUI();

    }

    //Add the combo points to the player's score
    public static void addComboToScore()
    {
		int scoreToAdd = (instance.comboTotal * instance.comboMultiplier);
		instance.score += scoreToAdd;
		//score.add(playerName, comboTotal* comboMultiplier);
        //target = newTargetName;
		instance.scoreHolder.score = instance.score;
        instance.UpdateUITag();
        instance.resetCombo();
        instance.UpdateUI();
        instance.networkManager.AddScoreTag_RPC(instance.score, instance.myPlayerName);

    }

    //Call the RPC
   

    public void UpdateUI() 
    {
        instance.comboHolder.comboDisplayUpdate(instance.comboTotal, instance.comboMultiplier);
    }

    public void UpdateUITag() 
    {
        instance.comboHolder.comboDisplayUpdateTag(instance.comboTotal, instance.comboMultiplier);
    }
}
