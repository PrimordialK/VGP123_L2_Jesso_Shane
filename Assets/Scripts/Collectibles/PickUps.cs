using UnityEngine;

public class PickUps : MonoBehaviour
{

    public enum PickUpType
    {
        Life = 0,
        Score = 1,
        PowerUp = 2
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PickUp collected: " + pickUpType);

            switch (pickUpType)
            {
                case PickUpType.Life:
                    GameManager.Instance.lives++;
                    Debug.Log("Player Lives: " + GameManager.Instance.lives);
                    break;
                case PickUpType.Score:
                    GameManager.Instance.score++;
                    Debug.Log("Score collected! Current score: " + GameManager.Instance.score);
                    break;
                case PickUpType.PowerUp:
                   // pc.ActivateJumpForceChange();
                    break;
               
            }
            
            Destroy(gameObject); // Destroy the power-up on collision with the player
        }
    }
}

