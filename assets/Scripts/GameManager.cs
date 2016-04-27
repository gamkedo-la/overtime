using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool postRound = false;
	public PlayerScoreList playerScoreList;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		if (postRound)
		{
			playerScoreList.ForceScoreboardUpdate();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void RoundOver ()
	{
		if(!postRound) // Load the Post-Round Stats Scene
		{
			Application.LoadLevel("Main_PostRound");
		}
		else // This is the Post-Round, so restart the game
		{

		}

	}
}
