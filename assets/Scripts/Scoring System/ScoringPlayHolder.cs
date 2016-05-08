using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoringPlayHolder : MonoBehaviour {

	public string play = " ";
	public float resetDisplayTime = 2F;
	private Text text;
	private List<string> msgQueue;

	
	void Start () {
	
		text = transform.GetComponent<Text> ();
		msgQueue = new List<string> ();
		StartCoroutine (MessageQueuePop());
		
	}
	//void Update () {
	
		//text.text = play.ToString ();

	//}

	public void DisplayScoringPlay (string playName)
	{
		msgQueue.Add (playName);
		//play = playName;
		//StartCoroutine(ResetDisplay());

	}

	IEnumerator ResetDisplay()
	{
		yield return new WaitForSeconds(resetDisplayTime);
		play = " ";
	}

	IEnumerator MessageQueuePop()
	{
		while (true) {

			if (msgQueue.Count > 0)
			{
				text.text = msgQueue[0];
				msgQueue.RemoveAt(0);
				yield return new WaitForSeconds(resetDisplayTime);
			}
			else
			{
				text.text = "";
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}
