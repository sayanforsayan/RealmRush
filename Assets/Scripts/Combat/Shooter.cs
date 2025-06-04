using UnityEngine;

/// <summary>
/// When tap then Ray will through with given range , if hit then give damage
/// </summary>
public class Shooter : MonoBehaviour
{
    [SerializeField] private float range = 5f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
            {
                var enemy = hit.collider.transform.parent.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        }
    }
}
