using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillNuke : RefillBase {
	
	void Start () {
		pickupWhenTouched = true;
		reloadsWepName = "nuke";
		ChildGONameForVisual = "Nuke";
		ammoToGive = 1;
		base.RefillInit();
	}
}