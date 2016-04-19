using UnityEngine;
using System.Collections;

public class ComboHolder : MonoBehaviour {

	public static ComboHolder instance;
	public static List<string> activeComboList = new List<string>();
	


	// Use this for initialization
	void Start () {
		instance = this;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
