using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ScriptableObject that broadcasts to listeners.
/// Listeners can register and respond when the event is raised.
/// </summary>
[CreateAssetMenu(menuName = "Events/GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<Action> listeners = new List<Action>(); // List of listeners

    /// <summary>
    /// Call this to raise the event & it will notify all registered listeners.
    /// </summary>
    public void Raise()
    {
        foreach (var listener in listeners)
        {
            listener?.Invoke();
        }
    }

    /// <summary>
    /// Add a listener
    /// </summary>
    public void Register(Action listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    /// <summary>
    /// Remove a listener
    /// </summary>
    public void Unregister(Action listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}
