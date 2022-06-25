using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private static Dictionary<string, Action<EventParam>> eventDictionary = new Dictionary<string, Action<EventParam>>();
    public void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void StartListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Add(eventName, listener);
        }
    }

    public static void StopListening(string eventName, Action<EventParam> listener)
    {
        Action<EventParam> thisEvent;
        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary.Remove(eventName);
        }
    }

    public static void TriggerEvent(string eventName, EventParam eventParam)
    {
        Action<EventParam> thisEvent;

        if (eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke(eventParam);
        }
    }
}
public struct EventParam
{
    public string stringParam;
    public int intParam;
    public float floatParam;
    public bool boolParam;
    public Vector3 vectorParam;
}

