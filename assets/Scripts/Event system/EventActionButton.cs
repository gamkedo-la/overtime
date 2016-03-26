using UnityEngine;
using System.Collections;

public class EventActionButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventManager.TriggerEvent(StandardEventName.ActionButton);
        }
    }
}
