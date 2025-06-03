using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 3;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            GameEvents.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
