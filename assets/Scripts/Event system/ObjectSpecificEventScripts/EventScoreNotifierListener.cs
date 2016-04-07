using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using UnityEngine.UI;


/// <summary>
/// This class goes on the objects you want to respond to the events
/// </summary>

public class EventScoreNotifierListener : MonoBehaviour 
{
    [Header("Select a float event to trigger")]
    [SerializeField] FloatEventName m_floatEventName = FloatEventName.Spin;     // This can be used to select an event name from the inspector 
    [SerializeField] StringEventName m_stringEventName = StringEventName.DummyKill;

    [SerializeField] Text notifierText;
  



    void Awake()
    {
        notifierText = transform.GetComponent<Text>();
    }


    #region Methods called by triggered events

    private void DummyKill (string dummyName){
        notifierText.text = dummyName;

    }

   /* private void StartSpinning(float torque)
    {
        m_rigidBody.AddTorque(Vector3.up * torque, ForceMode.Impulse);
    }


    private void StopSpinning()
    {
        m_rigidBody.angularVelocity = Vector3.zero;
    }


    private void PrintMessage(string message)
    {
        print(message + " from " + name);   // Append the game object name to the message sent by the event
    }


    private void Visible(bool visible)
    {
        m_meshRenderer.enabled = visible;
    }


    private void VisibleAndPrintMessage(EventArgs args)
    {
        var stringBool = args as StringBoolEventArgs;

        // Catch the situation where we accidentally send the wrong type of event arguments
        if (stringBool == null)
        {
            print("The event arguments weren't of type StringBoolEventArgs");
            return;
        }

        // If the event arguments are OK, i.e. stringBool isn't null, we can use them
        m_meshRenderer.enabled = stringBool.Bool;
        print(stringBool.String + " from " + name);   // Append the game object name to the message sent by the event
    }
*/
    #endregion


    /// <summary>
    /// This is a MonoBehaviour method called automatically by Unity when the script is enabled.
    /// Use it to register all the events you want this game object to respond to.
    /// Each event calls a method within this class as the second parameter (e.g. StartSpinning)
    /// </summary>
    void OnEnable ()
	{

        //EventManager.StartListening(m_floatEventName, StartSpinning);               // A float event sends a float value to the StartSpinning method when it's triggered
        EventManager.StartListening(m_stringEventName, DummyKill);   
    }


    /// <summary>
    /// This is a MonoBehaviour method called automatically by Unity when the script is disabled
    /// Unregister all the events you registered in OnEnable()
    /// </summary>
    void OnDisable ()
	{
		//EventManager.StopListening(m_floatEventName, StartSpinning);
        EventManager.StopListening(m_stringEventName, DummyKill); 
    }
}
