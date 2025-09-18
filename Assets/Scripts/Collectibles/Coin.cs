using UnityEngine;

public class Coin : PickUp
{

    Rigidbody2D rb;

    public override void OnPickup() => GameManager.Instance.score++;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();


        // Update is called once per frame
    }
}
        

