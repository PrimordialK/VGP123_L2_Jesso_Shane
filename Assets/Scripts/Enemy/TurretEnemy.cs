using UnityEngine;

public class TurretEnemy : Enemy
{
    [SerializeField] private float fireRate = 2.0f;
    private float timeSinceLastShot = 0.0f;

    [SerializeField] private float shootRange = 10f; // The distance within which the turret will shoot
    [SerializeField] private Transform playerTransform; // Assign in Inspector or find at runtime

    protected override void Start()
    {
        base.Start();

        if (fireRate <= 0)
        {
            Debug.Log("fireRate must be greater than 0. Setting to 2.0f.");
            fireRate = 2.0f;
        }

        // Find player if not assigned
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
        }
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Face the player based on position
        if (playerTransform != null)
        {
            sr.flipX = playerTransform.position.x < transform.position.x;

            // Only shoot if player is within range
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= shootRange && stateInfo.IsName("Idle"))
            {
                if (Time.time - timeSinceLastShot >= fireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastShot = Time.time;
                }
            }
        }
    }
}
