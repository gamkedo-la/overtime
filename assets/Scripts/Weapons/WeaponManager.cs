using UnityEngine;
using System.Collections;
using System.Collections.Generic; // for List<>
using UnityEngine.UI;

public class WeaponManager : Photon.MonoBehaviour {
	public GameObject dartGunModel;
	public GameObject waterGunModel;
	public GameObject canThrowerModel;
	public GameObject nukeThrowerModel;

	private DartGun dartGun;
	private SoakerGun soakerGun;
	private SodaGrenadeThrower sodaGrenadeThrower;
	private NukeThrower nukeThrower;

	public List<WeaponBase> allWep = new List<WeaponBase>();
	
	void Start() {
		if(photonView.isMine){
			dartGun = GetComponentInChildren<DartGun>();
			allWep.Add((WeaponBase)dartGun);

			soakerGun = GetComponentInChildren<SoakerGun>();
			allWep.Add((WeaponBase)soakerGun);

			sodaGrenadeThrower = GetComponentInChildren<SodaGrenadeThrower>();
			allWep.Add((WeaponBase)sodaGrenadeThrower);

			nukeThrower = GetComponentInChildren<NukeThrower>();
			allWep.Add((WeaponBase)nukeThrower);

			ChangeWep(dartGun);
		}
	}

	void ChangeWep(WeaponBase toWep) {
		//SoundCenter.instance.PlayClipOn(
			//SoundCenter.instance.playerWepSwitch,transform.position);

		foreach(WeaponBase eachWep in allWep) {
			eachWep.enabled = (eachWep == toWep);
		}

		dartGunModel.SetActive(toWep == dartGun);
		waterGunModel.SetActive(toWep == soakerGun);
		canThrowerModel.SetActive(toWep == sodaGrenadeThrower);
		nukeThrowerModel.SetActive(toWep == nukeThrower);
	}
	
	void Update () {
		if(Input.GetButtonDown ("WeaponSlot1")){
			ChangeWep(dartGun);
		}
		if(Input.GetButtonDown ("WeaponSlot2")){
			ChangeWep(soakerGun);
		}
		if(Input.GetButtonDown ("WeaponSlot3")){
			ChangeWep(sodaGrenadeThrower);
		}
		if(Input.GetButtonDown ("WeaponSlot4")){
			ChangeWep(nukeThrower);
		}
	}
}