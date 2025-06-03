using UnityEngine;

public class GoalZone : MonoBehaviour
{
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
            GameEvents.AreaReached();
        }
    }
}
