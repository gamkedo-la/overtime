using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class TelepresenceControlChanger : MonoBehaviour {

	[SerializeField] TelepresenceBotAiPatrol aiControl;
	[SerializeField] TelepresenceBotPlayerControl playerControl;
	public FirstPersonController characterControl;
	public Camera playerCharacterCamera;


	// Use this for initialization
	public void TakePlayerControl(FirstPersonController playerController, Camera playerCamera)
	{
		characterControl = playerController;
		playerCharacterCamera = playerCamera;
		aiControl.enabled = false;
		playerControl.enabled = true;
		RemovePlayerCharacterControl ();
	}

	public void TakeAiControl()
	{
		playerControl.enabled = false;
		aiControl.enabled = true;
	}

	public void RestorePlayerCharacterControl ()
	{
		
		if (playerCharacterCamera != null)
		{
			playerCharacterCamera.enabled = true;
		}
		if (characterControl != null)
		{
			characterControl.enabled = true;
		}
		TakeAiControl ();
	}

	void RemovePlayerCharacterControl()
	{
		characterControl.enabled = true;
		playerCharacterCamera.enabled = false;

	}

	void OnDisable()
	{
		BotManager.instance.UpdateBotList ();
	}
}
