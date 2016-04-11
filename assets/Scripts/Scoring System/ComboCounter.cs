using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboCounter : MonoBehaviour {
    [SerializeField] int comboTotal;
    [SerializeField] int comboMultiplier;
    string playerName;
    string targetName;
    bool[] comboUnique;
	public ScoreHolder scoreHolder;
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
		scoreHolder = transform.parent.parent.transform.Find ("VitalsCanvas/ScoreBar/ScoreCount").GetComponent<ScoreHolder> ();
        targetName = "ScoringTarget"; //TODO get the actual target name
        comboUnique = new bool[ComboList.count()];

        resetCombo();
    }

    //Reset the combo. Used on spawn and death
    public void resetCombo()
    {
        comboTotal = 0;
        comboMultiplier = 1;

        for(int i = 0; i < comboUnique.Length; i++)
        {
            comboUnique[i] = true;
        }
    }

    //Method called by specific combos when a combo is pulled off
    public void addCombo(ComboList.Combos comboName, string playerName)
    {
        if (playerName == targetName && comboUnique[(int)comboName])
        {
            comboTotal += ComboList.getComboValue(comboName) * 2;
            comboMultiplier++;
            comboUnique[(int)comboName] = false;
        }
        else
        {
            comboTotal += ComboList.getComboValue(comboName);
        }
    }

    //Add the combo points to the player's score
    public void addComboToScore()
    {
		int scoreToAdd = (comboTotal * comboMultiplier);
		scoreHolder.score += scoreToAdd;
		//score.add(playerName, comboTotal* comboMultiplier);
        //target = newTargetName;

        resetCombo();
    }
}
