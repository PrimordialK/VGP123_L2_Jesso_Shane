using UnityEngine;

public class Life : PickUp
{
    Rigidbody2D rb;

    public int XVel = -4;
    //When Gameobject is picked up it will increase the players lives by 1
    public override void OnPickUp(GameObject player) => player.GetComponent<PlayerController>().lives++;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = new Vector2(XVel, 4);
    }

    // Update is called once per frame
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
}
