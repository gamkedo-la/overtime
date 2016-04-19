using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboCounter : MonoBehaviour {

	public static ComboCounter instance;

    [SerializeField] int comboTotal;
    [SerializeField] int comboMultiplier;
	bool[] comboUnique;


    string playerName;
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
    public static void addCombo(ComboList.Combos comboName, string playerName)
    {
		if (/*playerName == targetName &&*/ instance.comboUnique[(int)comboName]) // ADD BACK TARGET REQUIREMENT LATER
        {
            instance.comboTotal += ComboList.getComboValue(comboName) * 2;
			instance.comboMultiplier++;
			instance.comboUnique[(int)comboName] = false;
        }
        else
        {
			instance.comboTotal += ComboList.getComboValue(comboName);
        }
    }

    //Add the combo points to the player's score
    public static void addComboToScore()
    {
		int scoreToAdd = (instance.comboTotal * instance.comboMultiplier);
		instance.score += scoreToAdd;
		//score.add(playerName, comboTotal* comboMultiplier);
        //target = newTargetName;

		instance.resetCombo();
    }
}
