using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ZenUtil;
using WeaponSystem;

public class Entity : MonoBehaviour
{
    //[Range(0,1)]
    //[Tooltip("0 = no resist, 1 = cant be stunned")]
    //public float stunResist = 0;
    //public bool stunned;

    public int maxHealth = 10;
    public int currentHealth;

    public Timer hitStunTimer = new Timer(0.75f);
    public LayerMask groundLayer;
    public bool grounded;
    public float groundRayDistance;

    public float gravity;
    protected float currentGrav;

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
        currentGrav = gravity;
        hitStunTimer.AttachHookToObj(gameObject);
    }

    public virtual void FixedUpdate()
    {
        FilterStatusEffects();
        DetectGround();
        ApplyGravity();
    }
    protected void FilterStatusEffects()
    { 
        statusEffects.RemoveAll(x => x.duration >= 0);
    }
    protected void DetectGround() => grounded = Physics2D.Raycast(transform.position, -Vector2.up, groundRayDistance, groundLayer);
    protected void ApplyGravity()
    {
        if (grounded)
        {
            if (velocity.y < -0.1f)
                velocity.y = -0.1f;
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
    }

    public void InflictStatus(StatusEffect effect)
    {
        StatusEffect e = statusEffects.Find(x => x.type == effect.type);
        if (e != null)
            e += effect;
        else
            statusEffects.Add(effect);
    }

    #region On Hit Methods
    public virtual DamageEvent Hit(int damage) => this.Hit(new Damage(damage));
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

    public virtual DamageEvent Hit(Damage damage, Vector2 force)
    {
        velocity = force;
        hitStunTimer.Restart();
        return this.Hit(damage);
    }
    public virtual DamageEvent Hit(Damage damage, float airHitStunForce)
    {
        if (!grounded)
        {
            velocity.y = airHitStunForce;
            hitStunTimer.Restart();
        }
        return this.Hit(damage);
    }
#endregion

    public bool InRangeMin(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) <= range;
    public bool InRangeMax(Vector3 position, float range) => Vector3.SqrMagnitude(transform.position - position) >= range;

    public void Die()
    {
        currentHealth = 0;
        onDie?.Invoke();
    }
}
