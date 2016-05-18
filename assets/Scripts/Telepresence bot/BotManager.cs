using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class BotManager : MonoBehaviour {

	[SerializeField] TelepresenceControlChanger[] tpBots;

	public static BotManager instance;

	// Use this for initialization
	void Start () {
		instance = this;
		tpBots = transform.GetComponentsInChildren<TelepresenceControlChanger> ();
	
	}

	public void TakeRandomBotControl(FirstPersonController playerController, Camera playerCamera)
	{
		int botNumber = Random.Range (1, tpBots.Length);
		tpBots[botNumber].TakePlayerControl(playerController, playerCamera);
	}
	
	// Update is called once per frame
	public void UpdateBotList () {
	
	}
}
