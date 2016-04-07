using UnityEngine;
using System.Collections;

public class NukeScript : WeaponBase {
	
	[SerializeField] string burstPrefab;
	[SerializeField] string dartPrefab;

	private float ceilingY = 3.03f;

	public string owner;
	public float moveSpeed;
	private Rigidbody rb;
	private Collider col;
	public bool shot = false;
	private bool airborne = false;
	public float damage = 100f;
	PhotonView photonView;
	public int dartsToSpray = 55;
	
	[SerializeField] float selfdestructTime;
	[SerializeField] float hitTime = 0;
	
	void Start() 
	{		
		photonView = transform.GetComponent<PhotonView>();
		Debug.Log("Nuke ID " + photonView.viewID);
		rb = GetComponent<Rigidbody>();
		col = GetComponent<Collider>();
		shot = true;
		hitTime = (Time.time + selfdestructTime);
		rb.velocity = (transform.forward * moveSpeed + transform.up * moveSpeed * 0.3f);
		rb.angularVelocity = Random.onUnitSphere * 100.0f;
		airborne = true;
		shot = false;
		StartCoroutine(AutoBurstTimer());
	}

	IEnumerator AutoBurstTimer() {
		yield return new WaitForSeconds(selfdestructTime);
		NukeBurst();
	}
	
	void NukeBurst() {
		/* SoundCenter.instance.PlayClipOn(
			SoundCenter.instance.sodaSplash,transform.position); */
		PhotonNetwork.Instantiate(burstPrefab, transform.position,
			Random.rotationUniform, 0);
		for(int i = 0; i < dartsToSpray; i++) {
			PhotonNetwork.Instantiate(dartPrefab, transform.position,
				Random.rotationUniform, 0);
		}
		Destroy(gameObject);
	}
	
	[PunRPC]
	public void NameNukeRPC (string playerName)
	{
		owner = playerName;
	}
	
}