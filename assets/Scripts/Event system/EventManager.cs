using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


public class StringBoolEventArgs : EventArgs
{
    public StringBoolEventArgs() { }

    public StringBoolEventArgs(string _string, bool _bool)
    {
        String = _string;
        Bool = _bool;
    }

    public string String { get; set; }
    public bool Bool { get; set; }
}


public class EventManager : MonoBehaviour 
{
    #region Fields

    private Dictionary<StandardEventName, UnityEvent> m_eventDictionary;
    private Dictionary<GeneralEventName, UnityEvent<EventArgs>> m_eventWithArgsDictionary;
    private Dictionary<BooleanEventName, UnityEvent<bool>> m_eventWithBoolDictionary;
	private Dictionary<FloatEventName, UnityEvent<float>> m_eventWithFloatDictionary;
    private Dictionary<StringEventName, UnityEvent<string>> m_eventWithStringDictionary;
    private Dictionary<IntegerEventName, UnityEvent<int>> m_eventWithIntDictionary;
    private Dictionary<StringStringEventName, UnityEvent<string, string>> m_eventWithTwoStringsDictionary;
    private Dictionary<StringBoolEventName, UnityEvent<string, bool>> m_eventWithStringAndBoolDictionary;
    private Dictionary<StringStringFloatEventName, UnityEvent<string, string, float>> m_eventWithTwoStringsAndFloatDictionary;

    private static EventManager m_eventManager;

    #endregion


    #region Initialisation

    public static EventManager Instance
	{
		get
		{
			if (!m_eventManager)
			{
				m_eventManager = FindObjectOfType(typeof (EventManager)) as EventManager;
				
				if (!m_eventManager)
				{
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				}
				else
				{
					m_eventManager.Initialise(); 
				}
			}
			
			return m_eventManager;
		}
	}


	void Initialise()
	{
		if (m_eventDictionary == null)
			m_eventDictionary = new Dictionary<StandardEventName, UnityEvent>();

        if (m_eventWithArgsDictionary == null)
            m_eventWithArgsDictionary = new Dictionary<GeneralEventName, UnityEvent<EventArgs>>();

        if (m_eventWithBoolDictionary == null)
			m_eventWithBoolDictionary = new Dictionary<BooleanEventName, UnityEvent<bool>>();

		if (m_eventWithFloatDictionary == null)
			m_eventWithFloatDictionary = new Dictionary<FloatEventName, UnityEvent<float>>();

        if (m_eventWithStringDictionary == null)
            m_eventWithStringDictionary = new Dictionary<StringEventName, UnityEvent<string>>();

        if (m_eventWithIntDictionary == null)
            m_eventWithIntDictionary = new Dictionary<IntegerEventName, UnityEvent<int>>();

        if (m_eventWithTwoStringsDictionary == null)
            m_eventWithTwoStringsDictionary = new Dictionary<StringStringEventName, UnityEvent<string, string>>();

        if (m_eventWithStringAndBoolDictionary == null)
            m_eventWithStringAndBoolDictionary = new Dictionary<StringBoolEventName, UnityEvent<string, bool>>();

        if (m_eventWithTwoStringsAndFloatDictionary == null)
            m_eventWithTwoStringsAndFloatDictionary = new Dictionary<StringStringFloatEventName, UnityEvent<string, string, float>>();
    }

    #endregion


    #region Standard event

    public static void StartListening (StandardEventName eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (Instance.m_eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener (listener);
		} 
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener (listener);
			Instance.m_eventDictionary.Add (eventName, thisEvent);
		}
	}


	public static void StopListening (StandardEventName eventName, UnityAction listener)
	{
		if (m_eventManager == null) return;

		UnityEvent thisEvent = null;
		if (Instance.m_eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener (listener);
		}
	}


	public static void TriggerEvent (StandardEventName eventName)
	{
		UnityEvent thisEvent = null;
		if (Instance.m_eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke ();
		}
	}

    #endregion


    #region General event

    public static void StartListening(GeneralEventName eventName, UnityAction<EventArgs> listener)
    {
        UnityEvent<EventArgs> thisEvent = null;
        if (Instance.m_eventWithArgsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GeneralEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithArgsDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(GeneralEventName eventName, UnityAction<EventArgs> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<EventArgs> thisEvent = null;
        if (Instance.m_eventWithArgsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(GeneralEventName eventName, EventArgs args)
    {
        UnityEvent<EventArgs> thisEvent = null;
        if (Instance.m_eventWithArgsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(args);
        }
    }


    public class GeneralEvent : UnityEvent<EventArgs>
    {

    }

    #endregion


    #region Boolean event

    public static void StartListening(BooleanEventName eventName, UnityAction<bool> listener)
	{
		UnityEvent<bool> thisEvent = null;
		if (Instance.m_eventWithBoolDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		} 
		else
		{
			thisEvent = new BoolEvent();
			thisEvent.AddListener(listener);
			Instance.m_eventWithBoolDictionary.Add(eventName, thisEvent);
		}
	}


	public static void StopListening(BooleanEventName eventName, UnityAction<bool> listener)
	{
		if (m_eventManager == null) return;
		
		UnityEvent<bool> thisEvent = null;
		if (Instance.m_eventWithBoolDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}


	public static void TriggerEvent(BooleanEventName eventName, bool argument)
	{
		UnityEvent<bool> thisEvent = null;
		if (Instance.m_eventWithBoolDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(argument);
		}
	}


    public class BoolEvent : UnityEvent<bool>
    {

    }

    #endregion


    #region Float event

    public static void StartListening(FloatEventName eventName, UnityAction<float> listener)
	{
		UnityEvent<float> thisEvent = null;
		if (Instance.m_eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		} 
		else
		{
			thisEvent = new FloatEvent();
			thisEvent.AddListener(listener);
			Instance.m_eventWithFloatDictionary.Add(eventName, thisEvent);
		}
	}
	
	
	public static void StopListening(FloatEventName eventName, UnityAction<float> listener)
	{
		if (m_eventManager == null) return;
		
		UnityEvent<float> thisEvent = null;
		if (Instance.m_eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}


	public static void TriggerEvent(FloatEventName eventName, float argument)
	{
		UnityEvent<float> thisEvent = null;
		if (Instance.m_eventWithFloatDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke(argument);
		}
	}


    public class FloatEvent : UnityEvent<float>
    {

    }

    #endregion


    #region String event

    public static void StartListening(StringEventName eventName, UnityAction<string> listener)
    {
        UnityEvent<string> thisEvent = null;
        if (Instance.m_eventWithStringDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithStringDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(StringEventName eventName, UnityAction<string> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<string> thisEvent = null;
        if (Instance.m_eventWithStringDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(StringEventName eventName, string argument)
    {
        UnityEvent<string> thisEvent = null;
        if (Instance.m_eventWithStringDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(argument);
        }
    }


    public class StringEvent : UnityEvent<string>
    {

    }

    #endregion


    #region Integer event

    public static void StartListening(IntegerEventName eventName, UnityAction<int> listener)
    {
        UnityEvent<int> thisEvent = null;
        if (Instance.m_eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new IntEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithIntDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(IntegerEventName eventName, UnityAction<int> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<int> thisEvent = null;
        if (Instance.m_eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(IntegerEventName eventName, int argument)
    {
        UnityEvent<int> thisEvent = null;
        if (Instance.m_eventWithIntDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(argument);
        }
    }


    public class IntEvent : UnityEvent<int>
    {

    }

    #endregion


    #region String-string event

    public static void StartListening(StringStringEventName eventName, UnityAction<string, string> listener)
    {
        UnityEvent<string, string> thisEvent = null;
        if (Instance.m_eventWithTwoStringsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringStringEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithTwoStringsDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(StringStringEventName eventName, UnityAction<string, string> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<string, string> thisEvent = null;
        if (Instance.m_eventWithTwoStringsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(StringStringEventName eventName, string argument1, string argument2)
    {
        UnityEvent<string, string> thisEvent = null;
        if (Instance.m_eventWithTwoStringsDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(argument1, argument2);
        }
    }


    public class StringStringEvent : UnityEvent<string, string>
    {

    }

    #endregion


    #region String-bool event

    public static void StartListening(StringBoolEventName eventName, UnityAction<string, bool> listener)
    {
        UnityEvent<string, bool> thisEvent = null;
        if (Instance.m_eventWithStringAndBoolDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringBoolEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithStringAndBoolDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(StringBoolEventName eventName, UnityAction<string, bool> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<string, bool> thisEvent = null;
        if (Instance.m_eventWithStringAndBoolDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(StringBoolEventName eventName, string argument1, bool argument2)
    {
        UnityEvent<string, bool> thisEvent = null;
        if (Instance.m_eventWithStringAndBoolDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(argument1, argument2);
        }
    }


    public class StringBoolEvent : UnityEvent<string, bool>
    {

    }

    #endregion


    #region String-string-float event

    public static void StartListening(StringStringFloatEventName eventName, UnityAction<string, string, float> listener)
    {
        UnityEvent<string, string, float> thisEvent = null;
        if (Instance.m_eventWithTwoStringsAndFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringStringFloatEvent();
            thisEvent.AddListener(listener);
            Instance.m_eventWithTwoStringsAndFloatDictionary.Add(eventName, thisEvent);
        }
    }


    public static void StopListening(StringStringFloatEventName eventName, UnityAction<string, string, float> listener)
    {
        if (m_eventManager == null) return;

        UnityEvent<string, string, float> thisEvent = null;
        if (Instance.m_eventWithTwoStringsAndFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }


    public static void TriggerEvent(StringStringFloatEventName eventName, string argument1, string argument2, float argument3)
    {
        UnityEvent<string, string, float> thisEvent = null;
        if (Instance.m_eventWithTwoStringsAndFloatDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(argument1, argument2, argument3);
        }
    }


    public class StringStringFloatEvent : UnityEvent<string, string, float>
    {

    }

    #endregion
}
