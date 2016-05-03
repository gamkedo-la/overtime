using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScoreList : MonoBehaviour {

	public GameObject playerScoreEntryPrefab;

	public ScoreManager scoreManager;
	bool postRoundUpdated = false;
	public bool freezeUpdate = false;

	int lastChangeCounter;

	// Use this for initialization
	void Start () {
		
		lastChangeCounter = scoreManager.GetChangeCounter();
	}
	
	// Update is called once per frame
	void Update () {
		if(scoreManager == null) {
			Debug.LogError("You forgot to add the score manager component to a game object!");
			return;
		}

		if(scoreManager.GetChangeCounter() == lastChangeCounter) {
			// No change since last update!
			return;
		}

		lastChangeCounter = scoreManager.GetChangeCounter();

		if(!freezeUpdate)
		{
			while(this.transform.childCount > 0) {
				Transform c = this.transform.GetChild(0);
				c.SetParent(null);  // Become Batman
				Destroy (c.gameObject);
			}

			string[] names = scoreManager.GetPlayerNames("score");
		
			foreach(string name in names)
			{
					GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
					go.transform.SetParent(this.transform);
					go.transform.Find ("Username").GetComponent<Text>().text = name;
					go.transform.Find ("Score").GetComponent<Text>().text = scoreManager.GetScore(name,"score").ToString();
			}
		}
	}

	public void ForceScoreboardUpdate () // Trigger an update in the Post Round when no updates are naturally called
	{
		if (postRoundUpdated == false) {
			string[] names = scoreManager.GetPlayerNames ("score");

			if(this.transform.childCount > 0) {
				Transform c = this.transform.GetChild(0);
				c.SetParent(null);  // Become Batman
				Destroy (c.gameObject);
			}
				
			foreach (string name in names) {
				GameObject go = (GameObject)Instantiate (playerScoreEntryPrefab);
				go.transform.SetParent (this.transform);
				go.transform.Find ("Username").GetComponent<Text> ().text = name;
				go.transform.Find ("Score").GetComponent<Text> ().text = scoreManager.GetScore (name, "score").ToString ();
			}
			postRoundUpdated = true;
			Debug.Log("Forced Scoreboard Update Ran");
		}
	}

	public void OnLevelWasLoaded(int level) { 
			if (level == 1) // Set to the index of Post Round in Build Settings, UPDATE if changed
			{
			// 	
			scoreManager = GameObject.Find ("NetworkManager").GetComponent<ScoreManager>();
				ForceScoreboardUpdate();
			}
	}

	public void FreezeUpdate()
	{
		freezeUpdate = true;
	}
}