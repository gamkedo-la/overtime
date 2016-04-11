using UnityEngine;
using System.Collections;

public class NearMissTrigger : MonoBehaviour {
    public GameObject dart;
    public float nearMissTime;
    public float nearMissTimer;
/*
    void OnTriggerExit(Collider collider)
    {
        if(collider.name == dart.name + "(Clone)")
        {
            EventManager.TriggerEvent(StringEventName.NearMiss, collider.GetComponent<DartRecoverableScript>().owner);
            nearMissTimer = nearMissTime;
        }
    }

    void Update()
    {
        if(nearMissTimer > 0)
        {
            nearMissTimer -= Time.deltaTime;
        }
    }
    */
}
