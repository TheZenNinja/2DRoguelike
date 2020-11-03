using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ZenUtil;
using WeaponSystem;

public class EntityBase : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;

    

    public float healthPercent { get { return (float)currentHealth / maxHealth; } }

    public List<StatusEffect> statusEffects = new List<StatusEffect>();

    public Vector2 velocity;
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
        FilterStatusEffects();
    }
    protected void FilterStatusEffects()
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

    public virtual DamageEvent Hit(Damage damage)
    {
        if (currentHealth <= 0)
            return new DamageEvent(this, 0, true);

        currentHealth -= damage;

        onDamage?.Invoke();

        bool dead = currentHealth <= 0;

        if (dead)
            Die();

        return new DamageEvent(this, damage, dead);
    }

    public bool InRangeMin(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) <= range;
    public bool InRangeMax(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) >= range;

    public void Die()
    {
        currentHealth = 0;
        onDie?.Invoke();
    }
}