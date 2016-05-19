using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponBase : Photon.MonoBehaviour {
	// Ammo //
	protected int ammo = 6;
	protected int maxAmmo = 6;
	public Sprite weaponIcon;

	public virtual bool GiveAmmo(int amt)
	{
		if (ammo >= maxAmmo) {
			return false;
		}
		if (amt > 0) {
			ammo += amt;
		} else {
			ammo = maxAmmo;
		}
		if (ammo >= maxAmmo) {
			ammo = maxAmmo;
		}
		return true;
	}
}
