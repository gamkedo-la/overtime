using UnityEngine;
using System.Collections;


/// <summary>
/// Use standard event names for events that don't need parameters sent to them
/// </summary>
public enum StandardEventName
{
    None,

    StopSpinning,
}


/// <summary>
/// Use boolean event names for events that need a True or False sent to them
/// </summary>
public enum BooleanEventName
{
    None,
   
    Visible,
}


/// <summary>
/// Use integer event names for events that need an integger sent to them
/// </summary>
public enum IntegerEventName
{
    None,
}


/// <summary>
/// Use string event names that need a string sent to them
/// </summary>
public enum StringEventName
{
    None,

    HelloWorld,
}


/// <summary>
/// Use float event names that need a float value sent to them
/// </summary>
public enum FloatEventName
{
    None,

    Spin,
}