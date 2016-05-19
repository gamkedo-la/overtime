using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class RefillSoda : RefillBase {
	
	void Start () {
		reloadsWepName = "cans";
		ChildGONameForVisual = "LightsAndCans";
		ammoToGive = 3;
		base.RefillInit();
	}
}
