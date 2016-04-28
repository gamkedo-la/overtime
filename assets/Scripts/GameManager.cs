using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool postRound = false;
	public PlayerScoreList playerScoreList;
	public ScoreManager scoreManager;
	public NetworkManagerScript networkManager;
	public PlayerStats playerStats;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		DontDestroyOnLoad(transform.gameObject);

		if (postRound)
		{

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
			// THINGS TO KEEP ACROSS THE LOAD //
			DontDestroyOnLoad(networkManager.transform.gameObject);
			instance.networkManager.enabled = false;

			Application.LoadLevel("Main_PostRound");
		}
		else // This is the Post-Round, so restart the game
		{

		}

	}

	void OnLevelWasLoaded(int level) { 
		if (level == 1) // Set to the index of Post Round in Build Settings, UPDATE if changed
		{ 
			// Get the Network Manager after load
			GameObject netMan = GameObject.Find("NetworkManager");
			// Relink all the needed variables
			instance.playerScoreList = netMan.transform.GetComponent<PlayerScoreList>();
			instance.scoreManager = netMan.transform.GetComponent<ScoreManager>();
			instance.playerScoreList.scoreManager = instance.scoreManager;

			// Run the score update
			playerScoreList.ForceScoreboardUpdate();

		}	
		
	}

}
