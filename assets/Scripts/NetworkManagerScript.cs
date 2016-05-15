using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;



public class NetworkManagerScript : MonoBehaviour {

	[SerializeField] Text connectionText;

	// SPAWNING // 
	[SerializeField] Transform[] spawnPoints;
	[SerializeField] GameObject SpawnPointManager;

	// SCENE CAMERA // 
	[SerializeField] Camera sceneCamera;

	[SerializeField] string version;
	[SerializeField] float respawnTime;
	[SerializeField] string playerObject;

	//Game window things
	[SerializeField] GameObject serverWindow;
	[SerializeField] InputField username;
	[SerializeField] InputField roomName;
	[SerializeField] InputField roomList;
	[SerializeField] InputField messageWindow;
	[SerializeField] PhotonHashtable PlayerCustomProps = new PhotonHashtable();
	[SerializeField] GameObject playerManager;

	// UI ELEMENTS // 

	[SerializeField] GameObject PlayerUI;
	[SerializeField] GameObject InfoUI;
	

	GameObject player;

	//Add Queue for messaging, going to add front delete back
	Queue<string> messages;
	const int messagesCount = 6;
	PhotonView photonView;
	public ScoreManager scoreManager;



	void Start () 
	{
		if (PhotonNetwork.offlineMode == true){
			PhotonNetwork.CreateRoom("Offline Room");
		}

		GetSpawnPoints ();

		//Get Photon view and messsage queue
		photonView = GetComponent<PhotonView> ();
		messages = new Queue<string> (messagesCount);

		// DL - here we tell Photon to use more verbose logging for our testing phase
		PhotonNetwork.logLevel = PhotonLogLevel.Full;
		// DL - We identify the version number of the build so same versions can connect, I made this a publi variable
		PhotonNetwork.ConnectUsingSettings (version);
		//Start the connection dialogue
		StartCoroutine ("UpdateConnectionString");


	
	}



	// Update is called once per frame
	IEnumerator UpdateConnectionString () 
	{
		while(true)
		{
			connectionText.text = PhotonNetwork.connectionStateDetailed.ToString ();
			yield return null;
		}

	}

	void OnJoinedLobby ()
	{
		//Turn on the room join panel
		serverWindow.SetActive (true);
	}

	void OnReceivedRoomListUpdate ()
	{
		//after OnJoinedLobby, room list needs a minute to populate
		
		//Clear room list
		roomList.text = "";
		//Get the Room list and populate it
		RoomInfo[] rooms = PhotonNetwork.GetRoomList ();
		foreach(RoomInfo room in rooms)
			roomList.text += room.name + "\n";
		
	}

	public void JoinRoom()
	{
		PhotonNetwork.player.name = username.text;
		scoreManager.SetScore(username.text, "score", 0);

		// DL - join room or create one
		RoomOptions roomOptions = new RoomOptions(){ isVisible = true, maxPlayers = 10 };
		PhotonNetwork.JoinOrCreateRoom(roomName.text, roomOptions, TypedLobby.Default);

		// DUPLICATE NAME DETECTION - Not yet Working //
		/*bool collisionFound = true;
		do {
			collisionFound = false;
			foreach (PhotonPlayer pl in PhotonNetwork.playerList) {
				if (username.text == pl.name) {
					username.text = (username.text + "_");
					Debug.Log ("There was a name conflict and the rename is " + username.text);
					collisionFound = true;
				}
			}
		} while (collisionFound);*/
		

    
	}





	void OnJoinedRoom ()
	{
		//Turn off the room join panel
		serverWindow.SetActive (false);
		//Stop Connection Dialogue and clear out the text
		StopCoroutine ("UpdateConnectionString");
		connectionText.text = "";
		//Set CustomProperties
		PhotonHashtable PlayerCustomProps = new PhotonHashtable();
		PlayerCustomProps["score"] = 0;
		PhotonNetwork.player.SetCustomProperties(PlayerCustomProps);
		ComboCounter.instance.GetMyPlayerName();
		//Start spawn, 0 cooldown
		StartSpawnProcess (0f);
	}


	void StartSpawnProcess (float respawnTime)
	{
		// DL - This starts the coroutine
		sceneCamera.enabled = true;
		PlayerUI.SetActive (false);
		InfoUI.SetActive (false);
		StartCoroutine ("SpawnPlayer", respawnTime);
	}

	IEnumerator SpawnPlayer(float respawnTime)
	{
		yield return new WaitForSeconds(respawnTime);
		// DL - pick a random spawn point
		int index = Random.Range (0, spawnPoints.Length);
		// DL - use photon to instantiate the player object
		player = PhotonNetwork.Instantiate (playerObject,
		                                    spawnPoints[index].position,
		                                    spawnPoints[index].rotation,
		                                    0);
		player.transform.SetParent(playerManager.transform);
		player.GetComponentInChildren<PlayerNetworkMover>().RespawnMe += StartSpawnProcess;
		//send PNM the kill/spawn message
		player.GetComponentInChildren<PlayerNetworkMover>().SendNetworkMessage += AddMessage;

	

		//Turn off top down view when we spawn
		sceneCamera.enabled = false;
		PlayerUI.SetActive (true);
		InfoUI.SetActive (true);

		AddMessage (PhotonNetwork.player.name + " has entered the office.");
	}

	void GetSpawnPoints()
	{
		spawnPoints = new Transform[SpawnPointManager.transform.childCount];
		for (int i = 0; i < SpawnPointManager.transform.childCount; i++)
		{
			spawnPoints[i] = SpawnPointManager.transform.GetChild(i).transform;
		}
	}

	
// DEATHMATCH LEGACY //
/*
	//Call the RPC
	void AddScore(string fragger, string fragged)
	{
		//               what we call       who sent to       parameter (what passes)
		photonView.RPC ("AddScore_RPC", PhotonTargets.All, fragger, fragged);
	}
*/	



	public void AddScoreTag_RPC(int scoreToSend, string scoringPlayer)
    {
        //               what we call       who sent to       parameter (what passes)
        photonView.RPC ("AddScoreTag_RPC", PhotonTargets.All, scoreToSend, scoringPlayer);
    }

	//Call the RPC
	void AddMessage(string message)
	{
		//               what we call       who sent to       parameter (what passes)
		photonView.RPC ("AddMessage_RPC", PhotonTargets.All, message);
	}


	[PunRPC]
	void AddMessage_RPC(string message)
	{
		//Update the list, Enqueue = front, Dequeue = back
		messages.Enqueue(message);
		if(messages.Count > messagesCount)
			messages.Dequeue();

		//Write to the box, clear first
		messageWindow.text = "";
		foreach(string m in messages)
			messageWindow.text += m + "\n";
	}
}
