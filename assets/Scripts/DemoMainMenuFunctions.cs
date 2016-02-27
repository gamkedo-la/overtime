using UnityEngine;
using System.Collections;

public class DemoMainMenuFunctions : MonoBehaviour {
	public GameObject creditsCanvas;
	public GameObject mainButtonsCanvas;

	public void StartSingleplayer() {
		PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.OfflineMode;
		PhotonNetwork.offlineMode = true;
		PhotonNetwork.CreateRoom("OCRSinglePlayer");
		Debug.Log("Single Player Attempted");
		mainButtonsCanvas.SetActive(false);
	
		
	}

	public void StartMultiplayer() {
		PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.PhotonCloud;
		
	}

	public void CreditsToggle() {
		creditsCanvas.SetActive( mainButtonsCanvas.activeSelf );
		mainButtonsCanvas.SetActive( mainButtonsCanvas.activeSelf == false );
	}
}
