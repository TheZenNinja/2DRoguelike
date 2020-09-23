using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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

    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    protected Rigidbody2D rb;

    public Action onDie;
    public Action onDamage;

    public virtual void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void FixedUpdate()
    {
        statusEffects.RemoveAll(x => x.duration >= 0);
    }
    public void InflictStatus(StatusEffect effect)
    {
        StatusEffect e = statusEffects.Find(x => x.type == effect.type);
        if (e != null)
            e += effect;
        else
            statusEffects.Add(effect);
    }
    public void Hit(Vector2 hitForce, int damage = 1)
    {
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
