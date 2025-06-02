using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float range = 50f;
    public int damage = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
            {
                var enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    Debug.Log("Enemy Destroy");
                }
            }
        }
    }
}
