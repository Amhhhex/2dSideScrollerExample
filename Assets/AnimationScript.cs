using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    public Rigidbody2D rigidBody;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public float moveSpeed = 4f;
    public float jumpVelocity = 10f;

    private float inputX = 0;
    private float facing = 1; // 1 = right, -1 = left
    private bool isGrounded;

    public Transform groundCheckPosition;
    float groundCheckLength = 0.25f;
    public LayerMask groundCheckLayerMask;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {

        inputX = Input.GetAxisRaw("Horizontal");

        //facing direction

        if(inputX > 0 )
        {
            facing = 1;
        }
        else if (inputX < 0 )
        {
            facing = -1;
        }

        if (facing >= 0.01)
        {
            spriteRenderer.flipX = false;
        }

        else if (facing < 0.01f)
        {
            spriteRenderer.flipX = true;
        }

        ////This is a way of doing the same code as below
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //}

        RaycastHit2D hit = (Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckLength, groundCheckLayerMask));

        if(hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rigidBody.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
        }

        if(animator)
        {
            animator.SetFloat("moveX", Mathf.Abs(rigidBody.linearVelocityX));
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocityX = inputX * moveSpeed;
    }
}
