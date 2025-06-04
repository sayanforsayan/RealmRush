using UnityEngine;

/// <summary>
/// Takes damage and dies after reaching 0 health.
/// Raises an event when killed.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private GameEvent enemyKilledEvent;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            enemyKilledEvent.Raise();
            Destroy(gameObject);
        }
    }
}
