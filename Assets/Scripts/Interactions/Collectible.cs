using UnityEngine;

/// <summary>
/// Collect cube and destroy.
/// Raise an event when collected.
/// </summary>
public class Collectible : MonoBehaviour
{
    [SerializeField] private GameEvent itemCollectedEvent;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            itemCollectedEvent.Raise();
            Destroy(gameObject);
        }
    }
}
