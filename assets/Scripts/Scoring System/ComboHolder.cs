using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboHolder : MonoBehaviour {


	public int comboValue = 0;
	public int comboMultiplier = 0;
	public bool resetFromTag = false;
	public float resetWaitTime = 2F;
	private Text text;


	// Use this for initialization
	void Start () {
		text = transform.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (!resetFromTag)
		{
		text.text = (comboValue.ToString() + " x " + comboMultiplier.ToString());
		}
		else
		{
		text.text = (comboValue.ToString() + " x " + comboMultiplier.ToString() + " Nice Combo!");
		StartCoroutine(ResetComboUI());
		}
	}

	IEnumerator ResetComboUI()
	{
		yield return new WaitForSeconds(resetWaitTime);
		resetFromTag = false;
	}

	public void comboDisplayUpdate (int newComboValue, int newComboMultiplier)
	{
		comboValue = newComboValue;
		comboMultiplier = newComboMultiplier;
	}

	public void comboDisplayUpdateTag (int newComboValue, int newComboMultiplier)
	{
		resetFromTag = true;
		comboValue = newComboValue;
		comboMultiplier = newComboMultiplier;
	}

}
