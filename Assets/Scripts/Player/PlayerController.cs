using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    //private Transform groundCheckPos;//
    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check

   // [SerializeField] private bool isGrounded = false;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    private GroundCheck groundCheck;

    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount = 1;

    private float initialGroundCheckRadius;
    // private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        groundCheck = new GroundCheck(col, LayerMask.GetMask("Ground"), groundCheckRadius);

        groundLayer = LayerMask.GetMask("Ground");

        if (groundLayer == 0)
        {
            Debug.LogError("Ground layer not set. Please set the Ground layer in the LayerMask.");
        }
        groundCheck = new GroundCheck(col, groundLayer, groundCheckRadius);
        initialGroundCheckRadius = groundCheckRadius;
    }

    // Update is called once per frame
    void Update()
    {
        float vValue = Input.GetAxis("Vertical");
        float hValue = Input.GetAxis("Horizontal");
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        SpriteFlip(hValue);
        //isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        bool isCrouching = vValue < 0;
        float moveSpeed = isCrouching ? 0f : 5f;
        rb.linearVelocityX = hValue * moveSpeed;
        groundCheck.CheckIsGrounded();

        if (!currentState.IsName("Fire") && (Input.GetButtonDown("Fire1")))
        {
            anim.SetTrigger("Fire");
        }
        if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;   
        }
       
        
        if (!isCrouching && Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log("jumpCout: " + jumpCount);
        }
        
        
        if (groundCheck.IsGrounded)
            jumpCount = 1;

    

            // Update the animator parameters
            anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", groundCheck.IsGrounded);
        anim.SetFloat("vValue", Mathf.Abs(vValue));
        anim.SetBool("isCrouching", vValue < 0);
        Debug.Log ($"Ground Check Radius from Player Object: {groundCheckRadius}");
        if (initialGroundCheckRadius != groundCheckRadius)
            groundCheck.UpdateGroundCheckRadius(groundCheckRadius);





    }

    void SpriteFlip(float hValue)
    {
        /*   if (hValue > 0)
           {
               sr.flipX = false;
           }
           else if (hValue < 0)
           {
               sr.flipX = true;
           }*/
       if (hValue !=0) sr.flipX = (hValue < 0); // Simplified sprite flipping logic
    }
    //public void CheckIsGrounded()
    //{
    //    if (!isGrounded && rb.linearVelocityY < 0)
    //    { 
    //        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer); 
    //    }
    //     else if (isGrounded) isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
}
