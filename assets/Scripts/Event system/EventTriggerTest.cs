using UnityEngine;
using System.Collections;


/// <summary>
/// This class is used as a test harness for triggering events that any other game object can listen out for
/// </summary>
public class EventTriggerTest : MonoBehaviour 
{
    [Header("Float event parameters")]
    [SerializeField] float m_spinTorque = 90f;

    [Header("String event parameters")]
    [SerializeField] string m_messageToPrint = "Hello world";

    private bool m_visible = true;


    void Update () 
	{
        if (Input.GetKeyDown(KeyCode.S))
            EventManager.TriggerEvent(FloatEventName.Spin, m_spinTorque);   // Send the spin torque to all methods triggered by the Spin float event

        if (Input.GetKeyDown(KeyCode.R))
            EventManager.TriggerEvent(StandardEventName.StopSpinning);

        if (Input.GetKeyDown(KeyCode.H))
            EventManager.TriggerEvent(StringEventName.HelloWorld, m_messageToPrint);

        if (Input.GetKeyDown(KeyCode.V))
        {
            m_visible = !m_visible;
            EventManager.TriggerEvent(BooleanEventName.Visible, m_visible);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            m_visible = !m_visible;
            EventManager.TriggerEvent(GeneralEventName.None, new StringBoolEventArgs(m_messageToPrint, m_visible));
        }
    }
}
