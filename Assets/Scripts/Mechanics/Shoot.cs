using UnityEngine;


public class Shoot : MonoBehaviour
{
    public AudioClip shootSound;

    private SpriteRenderer sr;
    private AudioSource audioSource;
    [SerializeField] private Vector2 initShotVelocity = Vector2.zero;
    [SerializeField] private Transform leftSpawn;
    [SerializeField] private Transform rightSpawn;
    [SerializeField] private Projectile projectilePrefab = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (shootSound != null)
        {

            TryGetComponent(out audioSource);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                Debug.Log("AudioSource component was missing. Added one dynamically.");
            }
        }

        if (initShotVelocity == Vector2.zero)
        {
            initShotVelocity = new Vector2(10.0f, 0.0f);
            Debug.LogWarning("Initial shot velocity is not set. Using default value: " + initShotVelocity);
        }
        if (leftSpawn == null || rightSpawn == null || projectilePrefab == null)
        {
            Debug.LogError("Spawn points not set. Please assign leftspawn and rightspawn in the inspector");
        }
    }


    public void Fire()

    {
        Projectile curProjectile;
        if (!sr.flipX)
        {
            curProjectile = Instantiate(projectilePrefab, rightSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(initShotVelocity);
        }
        else
        {
            curProjectile = Instantiate(projectilePrefab, leftSpawn.position, Quaternion.identity);
            curProjectile.SetVelocity(-initShotVelocity);
        }
        audioSource?.PlayOneShot(shootSound);
    }
}


