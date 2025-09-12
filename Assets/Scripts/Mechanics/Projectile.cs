using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof (Animator))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType = ProjectileType.Player;
    [SerializeField] private float gravityScale = 0.0f;
    [SerializeField, Range(0, 20)] private float lifetime = 1.0f;
    
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
            if (enemy != null)
            {
                enemy.TakeDamage(10);
                Destroy(gameObject);
            }
        }

        if (projectileType == ProjectileType.Enemy && collision.gameObject.CompareTag("Player"))
        {
                GameManager.Instance.lives--;
                Debug.Log("Player hit! Lives left: " + GameManager.Instance.lives);
                Destroy(gameObject);
            
        }
    }
    public enum ProjectileType
    { 
        Player,
        Enemy    
    }
}
