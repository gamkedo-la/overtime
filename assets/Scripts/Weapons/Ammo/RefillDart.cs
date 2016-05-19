using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillDart : RefillBase {
	
	void Start () {
		pickupWhenTouched = true;
		reloadsWepName = "dart";
		ChildGONameForVisual = "dartbox";
		ammoToGive = 3;
		base.RefillInit();
	}
}