using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum PickupType
    {
        Life = 0,
        Score = 1,
        Powerup = 2,
            Grow= 3
    }

    public AudioClip lifeSound;
    public AudioClip coinSound;
    private AudioSource audioSource;

    public PickupType pickupType = PickupType.Life; // Type of the pickup

    void Start()
    {
        if (lifeSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = GameManager.Instance.sfxMixerGroup;
                Debug.LogWarning("AudioSource component missing. Added one dynamically.");
            }
        }
        if (coinSound != null)
        {
            TryGetComponent(out audioSource);
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = GameManager.Instance.sfxMixerGroup;
                Debug.LogWarning("AudioSource component missing. Added one dynamically.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            

            switch (pickupType)
            {
                case PickupType.Life:
                    GameManager.Instance.lives++;
                    
                    //Debug.Log("Lives: ");
                    break;
                case PickupType.Score:
                    GameManager.Instance.score++;
                    
                    Debug.Log("Score collected! Current score: " + GameManager.Instance.score);
                    break;
                case PickupType.Powerup:
                    PlayerController pc = collision.GetComponent<PlayerController>();
                    pc.ActivateJumpForceChange();
                    Debug.Log("Powerup collected! Jump force increased temporarily.");
                    
                    break;
                
            }
           
            Destroy(gameObject); // Destroy the pickup after collection
        }
    }
}
