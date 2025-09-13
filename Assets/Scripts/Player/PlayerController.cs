using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{

    

    private float jumpForce = 6f;

    private Coroutine jumpForceChange = null;
    public void ActivateJumpForceChange()
    {
        //start  couroutine to change jump force for 5 seconds
        if (jumpForceChange != null)
        {
            StopCoroutine(jumpForceChange);
            jumpForceChange = null;
            jumpForce = 6f; // Reset to default before starting again
        }
        jumpForceChange = StartCoroutine(JumpForceChangeRoutine());

    }
    private IEnumerator JumpForceChangeRoutine()
    {
        jumpForce = 12f;
        Debug.Log($"Jump Force changed to {jumpForce} at {Time.time}");
        yield return new WaitForSeconds(5f);
        jumpForce = 6f;
        Debug.Log($"Jump Force changed to {jumpForce} at {Time.time}");
        jumpForceChange = null;
    }

    public void ActivateGrow()
    {
        playerTransform.localScale = new Vector3(10f, 10f, 1f);
    }
    public void DeactivateGrow()
    {
        playerTransform.localScale = new Vector3(1f, 1f, 1f);
    }
   

   


        public int maxLives = 9;    
   

    //private Transform groundCheckPos;//
    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground check

   // [SerializeField] private bool isGrounded = false;
    private LayerMask groundLayer;
    public Transform playerTransform;
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
        playerTransform = GetComponent<Transform>();
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

        // Hold S to crouch, release S to stand
        bool isCrouching = Input.GetKey(KeyCode.S) && groundCheck.IsGrounded;
        float moveSpeed = isCrouching ? 0f : 5f;
        rb.linearVelocityX = hValue * moveSpeed;
        groundCheck.CheckIsGrounded();
        
        
                    

     
        

        if (!currentState.IsName("Fire") && Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Fire");
        }
        else if (currentState.IsName("Fire"))
        {
            rb.linearVelocity = Vector2.zero;
        }
         if (currentState.IsName("Jump") && Input.GetButton("Fire2") && vValue > 0.1)
        {
            anim.SetTrigger("JumpAttack");
        }

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            Debug.Log("jumpCout: " + jumpCount);
        }
        if (groundCheck.IsGrounded)
            jumpCount = 1;

        // Set crouch/stand triggers based on S key
        if (isCrouching && !currentState.IsName("Crouch"))
        {
            anim.SetTrigger("Crouch");
        }
        else if (!isCrouching && currentState.IsName("Crouch"))
        {
            anim.SetTrigger("Stand");
        }

        // Update the animator parameters
        anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", groundCheck.IsGrounded);
        anim.SetFloat("vValue", Mathf.Abs(vValue));
        anim.SetBool("isCrouching", isCrouching);
        
        if (initialGroundCheckRadius != groundCheckRadius)
            groundCheck.UpdateGroundCheckRadius(groundCheckRadius);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Barriers")) // corrected: Barriers
        {
            Physics2D.IgnoreCollision(col, collision.collider);
        }
        
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

   

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Squish") && rb.linearVelocityY < 0)
        {
            collision.GetComponentInParent<Enemy>().TakeDamage(0, DamageType.JumpedOn);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

}

