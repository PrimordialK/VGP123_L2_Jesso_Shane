using UnityEditor.Animations;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalkRange : Enemy
{
    private Rigidbody2D rb;
    [SerializeField, Range(1f, 10f)] private float xVelocity = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;



    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        
        if (stateInfo.IsName("Walk"))
            rb.linearVelocityX = (sr.flipX) ? -xVelocity : xVelocity;
    }

    public override void TakeDamage(int damageValue, DamageType damageType = DamageType.Default)
    {
        Debug.Log($"TakeDamage called with type: {damageType}");
        if (damageType == DamageType.JumpedOn)
        {
            anim.SetTrigger("Squish");
            Destroy(transform.parent.gameObject, 0.5f);
            return;
        }
   
        
            base.TakeDamage(damageValue, damageType);
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Triggered with: {collision.gameObject.name}, Tag: {collision.tag}");
        if (collision.CompareTag("Barriers"))
        {
            anim.SetTrigger("Turn");
            sr.flipX = !sr.flipX;
        }
    }
}
