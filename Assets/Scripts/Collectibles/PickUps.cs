using UnityEngine;

public class PickUps : MonoBehaviour
{

    public enum PickUpType
    {
        Life = 1,
        Score = 2,
        PowerUp = 3
    }

    public PickUpType pickUpType = PickUpType.Life;
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

            switch (pickUpType)
            {
                case PickUpType.Life:
                    pc.lives++;
                    Debug.Log("Player Lives: " + pc.lives);
                    break;
                case PickUpType.Score:
                    pc.score++;
                    Debug.Log("Player Score: " + pc.score);
                    break;
                case PickUpType.PowerUp:
                    pc.ActivateJumpForceChange();
                    break;
               
            }
            
            Destroy(gameObject); // Destroy the power-up on collision with the player
        }
    }
}

