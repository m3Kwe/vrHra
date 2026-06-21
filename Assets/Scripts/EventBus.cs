using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<string, Delegate> events = new Dictionary<string, Delegate>();

    /// <summary>
    /// Subscribe to an event with no parameters
    /// </summary>
    public static void On(string eventName, Action listener)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = listener;
        }
        else
        {
            events[eventName] = (Action)events[eventName] + listener;
        }
    }

    /// <summary>
    /// Subscribe to an event with one parameter
    /// </summary>
    public static void On<T>(string eventName, Action<T> listener)
    {
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = listener;
        }
        else
        {
            events[eventName] = (Action<T>)events[eventName] + listener;
        }
    }

    /// <summary>
    /// Subscribe to an event by type
    /// </summary>
    public static void On<T>(Action<T> listener) where T : class
    {
        string eventName = typeof(T).Name;
        if (!events.ContainsKey(eventName))
        {
            events[eventName] = listener;
        }
        else
        {
            events[eventName] = (Action<T>)events[eventName] + listener;
        }
    }

    /// <summary>
    /// Unsubscribe from an event
    /// </summary>
    public static void Off(string eventName, Action listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action)events[eventName] - listener;
        }
    }

    /// <summary>
    /// Unsubscribe from an event with one parameter
    /// </summary>
    public static void Off<T>(string eventName, Action<T> listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T>)events[eventName] - listener;
        }
    }

    /// <summary>
    /// Unsubscribe from an event by type
    /// </summary>
    public static void Off<T>(Action<T> listener) where T : class
    {
        string eventName = typeof(T).Name;
        if (events.ContainsKey(eventName))
        {
            events[eventName] = (Action<T>)events[eventName] - listener;
        }
    }

    /// <summary>
    /// Raise an event with no parameters
    /// </summary>
    public static void Emit(string eventName)
    {
        if (events.ContainsKey(eventName) && events[eventName] != null)
        {
            ((Action)events[eventName])?.Invoke();
        }
    }

    /// <summary>
    /// Raise an event with one parameter
    /// </summary>
    public static void Emit<T>(string eventName, T data)
    {
        if (events.ContainsKey(eventName) && events[eventName] != null)
        {
            ((Action<T>)events[eventName])?.Invoke(data);
        }
    }

    /// <summary>
    /// Raise an event by type
    /// </summary>
    public static void Emit<T>(T eventData) where T : class
    {
        string eventName = typeof(T).Name;
        if (events.ContainsKey(eventName) && events[eventName] != null)
        {
            ((Action<T>)events[eventName])?.Invoke(eventData);
        }
    }

    /// <summary>
    /// Clear all events
    /// </summary>
    public static void Clear()
    {
        events.Clear();
    }
}
