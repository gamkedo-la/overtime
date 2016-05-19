using UnityEngine;
using System.Collections;

public class StickySlowsMe : MonoBehaviour {
	private const float stickyTime = 5.0f;

	protected float m_StickyEffectMult = 1.0f;

	protected float stickySafetyBreak = stickyTime;

	// hack, likely redundant with the coroutine, but ensuring never stuck in... stuck state
	protected void StickySafeFix() {
		if (m_StickyEffectMult < 1.0f) {
			stickySafetyBreak -= Time.deltaTime;
			if(stickySafetyBreak < 0.0f) {
				m_StickyEffectMult = 1.0f;
			}
		} else {
			stickySafetyBreak = stickyTime;
		}
	}

	public void SpeedStickyZone() {
		m_StickyEffectMult = 0.12f;
		StartCoroutine (UnstickySpeed());
	}
	
	public IEnumerator UnstickySpeed() {
		yield return new WaitForSeconds (stickyTime);
		m_StickyEffectMult = 1.0f;
	}
}
