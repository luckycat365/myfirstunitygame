
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Playerinputcontroller InputController;
    public Rigidbody2D rigid;
    public float moveSpeed = 200f;
    public float jumpForce = 5f;
    public SpriteRenderer Sprite;


    public bool isGrounded;
    public Animator animator;
    public bool OnJump;
    public bool OnFall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputController = GetComponent<Playerinputcontroller>();
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
        jump();



    }

    private void movement()
    {
        // Horizontal Movement
        rigid.linearVelocityX = InputController.MoveData * moveSpeed * Time.deltaTime;//deltaTime is used to avoid jitter.

        if (InputController.MoveData == 1)
        {
            Sprite.flipX = false;
            animator.SetInteger("Movement", 1);
        }
        else if (InputController.MoveData == -1)
        {
            Sprite.flipX = true;
            animator.SetInteger("Movement", 1);
        }
        else
        {
            animator.SetInteger("Movement", 0);
        }
    }

    private void jump()
    {
        // Jump Logic
        if (InputController.Isjumping && isGrounded)
        {
            // Set Y velocity for jump, but keep X velocity (so you can jump diagonally)
            rigid.linearVelocityY = jumpForce;

            // To allow only jumping once per land, we reset the input flag
            InputController.Isjumping = false;

            animator.SetBool("jump", true);
            OnJump = true;

        }
        if (rigid.linearVelocityY < 0 && !isGrounded)
        {
            OnFall = true;
            animator.SetBool("fall", true);
            animator.SetBool("jump", false);
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            animator.SetBool("fall", false);
            animator.SetBool("jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
