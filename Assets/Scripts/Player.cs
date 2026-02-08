using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 1f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Read Horizontal movement (A/D)
        moveInput.x = 0;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) moveInput.x = -1;
            if (Keyboard.current.dKey.isPressed) moveInput.x = 1;

            // Jump only if touching the ground and W is pressed
            if (Keyboard.current.wKey.wasPressedThisFrame && isGrounded)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        // Apply horizontal speed, and leave vertical speed to gravity/physics
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    // Simple way to detect ground without extra objects or layers
    private void OnCollisionStay2D(Collision2D collision)
    {
        // If we are colliding with something, assume we can jump
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
