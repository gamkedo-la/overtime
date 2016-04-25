using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoundTimerScript : MonoBehaviour {

	public float roundTimerInMinutes = 5f;
	private float roundTimerInSeconds;
	public bool timeStarted = false;
	[SerializeField] string minutes;
	[SerializeField] string seconds;
	public GameObject timeCount;
	private Text text;

	void Start () {
	
		text = timeCount.transform.GetComponent<Text> ();
		roundTimerInSeconds = (roundTimerInMinutes * 60F);
	}


	void Update()
	{
		if(timeStarted == true)
		{
			roundTimerInSeconds -= Time.deltaTime;
		}
		minutes = (Mathf.Floor(roundTimerInSeconds / 60).ToString("00"));
		seconds = (Mathf.Floor(roundTimerInSeconds % 60).ToString("00"));

		text.text = (minutes + ":" + seconds);
	}
}

	