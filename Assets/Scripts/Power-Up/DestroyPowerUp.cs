using UnityEngine;

public class DestroyPowerUp : MonoBehaviour
{
    void Start()
    {
        // Optional: You can initialize anything here if needed
    }

    void Update()
    {
        // Optional: You can add any update logic here if needed
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            pc.SetLives(pc.GetLives() + 1);
            Destroy(gameObject); // Destroy the power-up on collision with the player
        }
    }
}

