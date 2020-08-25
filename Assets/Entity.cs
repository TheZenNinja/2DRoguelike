using UnityEngine;
using System.Collections;
using System;

public enum EntityStat
{
    health,
    atkSpeed,
    atkDamage,
    moveSpeed,
}
public class Entity : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    public float healthPercent { get { return (float)currentHealth / maxHealth; } }


    public SpriteRenderer sprite;
    public bool beingHit;
    public float hitStun = 0.1f;
    private float hitStunCounter;

    protected Rigidbody2D rb;

    public Action onDie;
    public Action onDamage;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    /*private void Update()
    {
        if (hitStunCounter > 0)
        {
            beingHit = true;
            hitStunCounter -= Time.deltaTime;
            sprite.color = Color.red;
        }
        else if (hitStunCounter < 0)
        {
            beingHit = false;
            hitStunCounter = 0;
            sprite.color = Color.white;
        }
    }*/
    public void Hit(Vector2 hitForce, int damage = 1)
    {
        if (!beingHit)
            Damage(damage);

        hitStunCounter = hitStun;

        rb.velocity = hitForce;
    }

    public bool InRangeMin(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) <= range;
    public bool InRangeMax(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) >= range;
    public void Damage(int dmg = 1)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= dmg;

        onDamage?.Invoke();

        if (currentHealth <= 0)
            Die();
    }
    public void Die()
    {
        currentHealth = 0;
        onDie?.Invoke();
    }
    public void ZeroYVelocity()
    {
        Vector2 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
    }
}
