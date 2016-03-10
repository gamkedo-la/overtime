using UnityEngine;
using System.Collections;

public class HalfTimeToggle : MonoBehaviour {

	// Use this for initialization
	public void ToggleHalfTime () {
		{
            if (Time.timeScale == 1.0F)
                {Time.timeScale = 0.5F;
                	Debug.Log("Time is half");}
            else
                {Time.timeScale = 1.0F;
                	Debug.Log("Time is normal");}

            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
	}
}
