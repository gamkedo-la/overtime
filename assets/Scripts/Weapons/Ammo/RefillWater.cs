using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class RefillWater : RefillBase {

	void Start () {
		reloadsWepName = "soaker";
		ChildGONameForVisual = "Cylinder_001";
		ammoToGive = -1;
		base.RefillInit();
	}

}
