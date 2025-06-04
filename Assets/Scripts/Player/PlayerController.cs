using UnityEngine;

/// <summary>
/// Player movement using CharacterController.
/// Supports horizontal/vertical input.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Horizontal (A/D or Left/Right) and Vertical (W/S or Up/Down) input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 move = transform.right * h + transform.forward * v;

        // Movement (X and Z)
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravity to vertical velocity (Y)
        velocity.y += gravity * Time.deltaTime;

        // Vertical movement
        controller.Move(velocity * Time.deltaTime);
    }
}
