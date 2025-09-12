using UnityEngine;

public class GrowMushroom : PickUp
{
    Rigidbody2D rb;

    private int xVel = -4;
    public override void OnPickup() => GameManager.Instance.lives++;

    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(xVel, 4); // Set the initial velocity to move downwards
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(xVel, rb.linearVelocityY);
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Edge"))
        {
            xVel *= -1; // Reverse the horizontal velocity when hitting a wall
        }
    }
}
