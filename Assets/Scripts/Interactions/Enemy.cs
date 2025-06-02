using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            QuestManager.Instance.EnemyKilled();
            Destroy(gameObject);
        }
    }
}
