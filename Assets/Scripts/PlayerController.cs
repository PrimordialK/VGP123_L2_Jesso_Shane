using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerScript : MonoBehaviour
{
    //private Transform groundCheckPos;//
    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check

    [SerializeField] private bool isGrounded = false;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;
    [SerializeField] private int maxJumpCount = 2;
    private int jumpCount = 1;
    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        groundLayer = LayerMask.GetMask("Ground");

        if (groundLayer == 0)
        {
            Debug.LogError("Ground layer not set. Please set the Ground layer in the LayerMask.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float vValue = Input.GetAxis("Vertical");
        float hValue = Input.GetAxis("Horizontal");
        SpriteFlip(hValue);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        bool isCrouching = vValue < 0;
        float moveSpeed = isCrouching ? 0f : 5f;
        rb.linearVelocityX = hValue * moveSpeed;


        if (!isCrouching && Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log("jumpCout: " + jumpCount);
        }
        if (isGrounded)
            jumpCount = 1;

    

            // Update the animator parameters
            anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("vValue", Mathf.Abs(vValue));
        anim.SetBool("isCrouching", vValue < 0);
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
}
