using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ZenUtil;
using WeaponSystem;

public class Entity : MonoBehaviour
{
    [Range(0,1)]
    [Tooltip("0 = no resist, 1 = cant be stunned")]
    public float stunResist = 0;
    public bool stunned;

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
    public virtual void Hit(int damage = 1)
    {
        currentHealth -= damage;
    }
    public void HitWithStun()
    {

    }
    public virtual void HitWithForce(int damage, Vector2 force, float stunAmt = 0)
    {
        currentHealth -= damage;

        if (stunResist == 0 || stunAmt > stunResist)
            Stun(0.5f);
        velocity = force;
    }
    public bool InRangeMin(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) <= range;
    public bool InRangeMax(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) >= range;
    public virtual DamageEvent Damage(int dmg = 1)
    {
        if (currentHealth <= 0)
            return new DamageEvent(this, 0, true);

        currentHealth -= dmg;

        onDamage?.Invoke();

        bool dead = currentHealth <= 0;
        
        if (dead)
            Die();
        return new DamageEvent(this, dmg, dead);
    }
    public void Die()
    {
        currentHealth = 0;
        onDie?.Invoke();
    }
    public void ZeroYVelocity()
    {
        velocity.y = 0;
    }

    protected virtual void Stun(float length)
    {
        StopCoroutine(stunTimer());
        StartCoroutine(stunTimer(length));
    }
    protected IEnumerator stunTimer(float length = 1)
    {
        stunned = true;
        yield return new WaitForSeconds(length);
        stunned = false;
    }
}
