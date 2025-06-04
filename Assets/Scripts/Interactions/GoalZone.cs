using UnityEngine;

/// <summary>
/// Visit area and get point. Used color indicator as completed.
/// Raise an event when visited.
/// </summary>
public class GoalZone : MonoBehaviour
{
    [SerializeField] private GameEvent areaReachedEvent;
    BoxCollider coll;
    Renderer renderer;
    void Start()
    {
        coll = GetComponent<BoxCollider>();
        renderer = GetComponent<Renderer>();
        coll.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coll.isTrigger = false;
            renderer.material.color = Color.grey;
            areaReachedEvent.Raise();
        }
    }
}
