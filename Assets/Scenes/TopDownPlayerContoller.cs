using UnityEngine;
using UnityEngine.InputSystem; // needed for InputValue

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;        // movement speed
    private Vector2 moveInput;          // stores WASD input
    private Rigidbody2D rb;             // reference to Rigidbody2D

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // This function is automatically called by the PlayerInput component
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void FixedUpdate()
    {
        // Apply movement every physics frame
        rb.velocity = moveInput * moveSpeed;
    }
}
