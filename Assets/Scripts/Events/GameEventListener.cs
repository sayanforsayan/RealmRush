using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listens to a GameEvent and triggers a UnityEvent when it is raised.
/// </summary>
public class GameEventListener : MonoBehaviour
{
    [Tooltip("GameEvent - listen")]
    public GameEvent gameEvent;

    [Tooltip("Response to call when the event is raised.")]
    public UnityEvent response;

    /// <summary>
    /// Register listener
    /// </summary>
    private void OnEnable()
    {
        gameEvent.Register(OnEventRaised);
    }

    /// <summary>
    /// Unregister listener
    /// </summary>
    private void OnDisable()
    {
        gameEvent.Unregister(OnEventRaised);
    }

    /// <summary>
    /// Called when GameEvent is raised
    /// </summary>
    private void OnEventRaised()
    {
        response?.Invoke();
    }
}
