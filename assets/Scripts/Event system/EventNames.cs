using UnityEngine;
using System.Collections;


/// <summary>
/// Use standard event names for events that don't need parameters sent to them
/// </summary>
public enum StandardEventName
{
    StopSpinning = -1,
    None = 0,
	ActionButton = 1,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use general event names for events that need general purpose EventArgs sent to them
/// </summary>
public enum GeneralEventName
{
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use boolean event names for events that need a True or False sent to them
/// </summary>
public enum BooleanEventName
{
    Visible = -1,
    None = 0,
 
    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use integer event names for events that need an integger sent to them
/// </summary>
public enum IntegerEventName
{
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use string event names that need a string sent to them
/// </summary>
public enum StringEventName
{
    HelloWorld = -1,
    None = 0,
    DummyKill = 1,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use float event names that need a float value sent to them
/// </summary>
public enum FloatEventName
{
    Spin = -1,
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use string-string event names that need two strings sent to them
/// </summary>
public enum StringStringEventName
{
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use string-bool event names that need both a string and bool sent to them
/// </summary>
public enum StringBoolEventName
{
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}


/// <summary>
/// Use string-string-float event names that need two strings and a float sent to them
/// </summary>
public enum StringStringFloatEventName
{
    None = 0,

    // Add new event names here as required (add a number too, to prevent serialization problems if they get reordered later)
}
