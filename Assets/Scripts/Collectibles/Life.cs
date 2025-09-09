using UnityEngine;

public class Life : PickUp
{
    Rigidbody2D rb;
    public int XVel = -4;

   

    public override void OnPickUp () => GameManager.Instance.lives++;
        
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(XVel, 4);
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(XVel, rb.linearVelocity.y);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Edge"))
        {
            XVel *= -1;
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // ensures pickup logic runs
    }
}
