using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;


/// <summary>
/// This class goes on the objects you want to respond to the events
/// </summary>
[RequireComponent(typeof(Rigidbody))]   // This ensures that when you add this script to a game object a Rigidbody will automatically get added too
[RequireComponent(typeof(MeshRenderer))]
public class EventTester : MonoBehaviour 
{
    [Header("Select a float event to trigger")]
    [SerializeField] FloatEventName m_floatEventName = FloatEventName.Spin;     // This can be used to select an event name from the inspector 
  
    private Rigidbody m_rigidBody;
    private MeshRenderer m_meshRenderer;


    void Awake()
    {
        m_rigidBody = GetComponent<Rigidbody>();    // We know this is never null because of the RequireComponent attribute on the class
        m_meshRenderer = GetComponent<MeshRenderer>();
    }


    #region Methods called by triggered events

    private void StartSpinning(float torque)
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

    #endregion


    /// <summary>
    /// This is a MonoBehaviour method called automatically by Unity when the script is enabled.
    /// Use it to register all the events you want this game object to respond to.
    /// Each event calls a method within this class as the second parameter (e.g. StartSpinning)
    /// </summary>
    void OnEnable ()
	{
        EventManager.StartListening(m_floatEventName, StartSpinning);               // A float event sends a float value to the StartSpinning method when it's triggered
        EventManager.StartListening(StandardEventName.StopSpinning, StopSpinning);
        EventManager.StartListening(StringEventName.HelloWorld, PrintMessage);
        EventManager.StartListening(BooleanEventName.Visible, Visible);
        EventManager.StartListening(GeneralEventName.None, VisibleAndPrintMessage);
    }


    /// <summary>
    /// This is a MonoBehaviour method called automatically by Unity when the script is disabled
    /// Unregister all the events you registered in OnEnable()
    /// </summary>
    void OnDisable ()
	{
		EventManager.StopListening(m_floatEventName, StartSpinning);
        EventManager.StopListening(StandardEventName.StopSpinning, StopSpinning);
        EventManager.StopListening(StringEventName.HelloWorld, PrintMessage);
        EventManager.StopListening(BooleanEventName.Visible, Visible);
        EventManager.StopListening(GeneralEventName.None, VisibleAndPrintMessage);
    }
}
