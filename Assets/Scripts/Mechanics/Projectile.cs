using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Range(0, 20)] private float lifetime = 1.0f;
    [SerializeField] private string[] ignoreTags = { "Colliders" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() => Destroy(gameObject, lifetime);

    public void SetVelocity(Vector2 velocity) => GetComponent<Rigidbody2D>().linearVelocity = velocity;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var tag in ignoreTags)
        {
            if (collision.gameObject.CompareTag("Colliders"))
            {
                return; // Ignore collision with specified tags
            }

            Destroy(gameObject); // Destroy the projectile on collision


        }
    }
}
