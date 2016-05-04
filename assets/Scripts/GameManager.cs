using UnityEngine;
using System.Collections;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public bool postRound = false;
	public ScoreManager scoreManager;
	public NetworkManagerScript networkManager;
	public PlayerStats playerStats;
	public GameObject vitalsCanvas;
	public GameObject playerManager;
	public GameObject roundUI;
	public GameObject postRoundUI;
	public PlayerScoreList postPlayerScoreList;
	public int LevelThatWasLoadedTest = 0;
	public RoundTimerScript roundTimer;

	// Use this for initialization
	void Start ()
	{
		instance = this;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void RoundOver ()
	{
			// THINGS TO KEEP ACROSS THE LOAD //
			//DontDestroyOnLoad(networkManager.transform.gameObject);
		instance.networkManager.enabled = false;


		instance.postRoundUI.SetActive (true);
		instance.roundUI.SetActive(false);
		instance.postPlayerScoreList.ForceScoreboardUpdate();
		StartCoroutine(EnableReset());
		PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.player);
	}

	public void PostRoundOver ()
	{
		Application.LoadLevel ("Main");
	}

	IEnumerator EnableReset ()
	{
		yield return new WaitForSeconds (10);
		roundTimer.postRoundOverCallSwitch = true;
	}


	/*public void OnLevelWasLoaded(int level) { 
		if (level == 1) { // Set to the index of Post Round in Build Settings, UPDATE if changed 
			//playersToDestroy = GameObject.FindGameObjectsWithTag("Player");



			// Get the Network Manager after load
			//GameObject netMan = GameObject.Find ("NetworkManager");

			// Relink all the needed variables
			instance.playerScoreList = netMan.transform.GetComponent<PlayerScoreList> ();
			instance.scoreManager = netMan.transform.GetComponent<ScoreManager> ();
			instance.playerScoreList.scoreManager = instance.scoreManager;
			instance.postRound = true;
			//StartCoroutine(SelfDestructSequence(15));
		
		}
	}*/

	/*// Destory self in Post Round once Scoreboard has had time to update. This is to counter DontDestroyOnLoad
	IEnumerator SelfDestructSequence (float destructTimer)
	{
		yield return new WaitForSeconds(destructTimer);
		instance.playerScoreList.FreezeUpdate ();
		Destroy (this.gameObject);
	}*/

}
