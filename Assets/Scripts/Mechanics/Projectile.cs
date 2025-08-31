using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof (Animator))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType = ProjectileType.Player;
    [SerializeField] private float gravityScale = 0.0f;
    [SerializeField, Range(0, 20)] private float lifetime = 1.0f;
    [SerializeField] private string[] ignoreTags = { "Colliders" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = gravityScale; // Apply gravity scale
        Destroy(gameObject, lifetime);
    }

    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (projectileType == ProjectileType.Player && collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy!= null)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
        }
        
        
        
        
        
        
        
        
        
        foreach (var tag in ignoreTags)
        {
            if (collision.gameObject.CompareTag("Colliders"))
            {
                return; // Ignore collision with specified tags
            }

            Destroy(gameObject); // Destroy the projectile on collision


        }

        
    }
    public enum ProjectileType
    { 
        Player,
        Enemy    
    }
}
