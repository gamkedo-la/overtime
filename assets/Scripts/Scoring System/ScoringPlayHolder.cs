using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoringPlayHolder : MonoBehaviour {

	public string play = " ";
	public float resetDisplayTime = 2F;
	private Text text;

	
	void Start () {
	
		text = transform.GetComponent<Text> ();
	}
	void Update () {
	
		text.text = play.ToString ();

	}

	public void DisplayScoringPlay (string playName)
	{
		play = playName;
		StartCoroutine(ResetDisplay());
	}

	IEnumerator ResetDisplay()
	{
		yield return new WaitForSeconds(resetDisplayTime);
		play = " ";
	}
}
