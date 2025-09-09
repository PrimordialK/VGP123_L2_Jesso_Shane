using UnityEngine;

public abstract class PickUp : MonoBehaviour
{

    abstract public void OnPickUp();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPickUp();
            Destroy(gameObject); // Destroy the power-up on collision with the player
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnPickUp();
                Destroy(gameObject); // Destroy the power-up on collision with the player
        }
    }
}
