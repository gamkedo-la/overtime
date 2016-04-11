using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHolder : MonoBehaviour {

	public int score = 0;
	private Text text;
	
	void Start () {
	
		text = transform.GetComponent<Text> ();
	}
	void Update () {
	
		text.text = score.ToString ();

	}
}
