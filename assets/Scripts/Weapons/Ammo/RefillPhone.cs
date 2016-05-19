using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillPhone : RefillBase {
	
	void Start () {
		reloadsWepName = "tripwire";
		ChildGONameForVisual = "PhoneLight";
		ammoToGive = 1;
		base.RefillInit();
	}
}
