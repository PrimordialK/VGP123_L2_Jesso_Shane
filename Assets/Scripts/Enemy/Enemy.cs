using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sr;
    protected Animator anim;
    protected int health;

    [SerializeField] private int maxHealth = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (maxHealth <= 0)
        {
            Debug.Log("maxHealth must be greater than 0. Setting to 5.");
            maxHealth = 5;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    public virtual void TakeDamage(int damageValue, DamageType damagetype = DamageType.Default)
    {
        health -= damageValue;

        if (health <= 0)
        {
            anim.SetTrigger("Death");

            if (transform.parent != null)
                Destroy(transform.parent.gameObject, 0.5f);
            else
                Destroy(gameObject, 0.5f);
        }
    }
}

    public enum DamageType
    {
        Default,
        JumpedOn
    }

