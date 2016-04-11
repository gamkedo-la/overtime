using UnityEngine;
using System.Collections;

public class OtherPlayerScoreTriggers : MonoBehaviour {
    public GameObject dart;

    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.ToLower().Contains("Dart"))
        {
//            EventManager.TriggerEvent(StringEventName.DartHit, name);
        }
        else if(collision.collider.name.ToLower().Contains("stapl"))
        {
//            if(collision.transform.position.)
        }
    }
}
