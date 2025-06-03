using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    private CharacterController controller;
    private Vector3 velocity;
    private bool isStartGame;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void OnEnable()
    {
        GameEvents.OnResetCall += ResetToDefault;
    }

    void OnDisable()
    {
        GameEvents.OnResetCall -= ResetToDefault;
    }

    void Update()
    {
        if (!isStartGame) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        controller.Move(move * moveSpeed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void ResetToDefault()
    {

    }
}
