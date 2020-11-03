using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using ZenUtil;
using WeaponSystem;

public class StandardEntity : EntityBase
{
    //[Range(0,1)]
    //[Tooltip("0 = no resist, 1 = cant be stunned")]
    //public float stunResist = 0;
    //public bool stunned;

    public float gravity;
    protected float currentGrav;

    public Timer hitStunTimer = new Timer(0.75f);
    public LayerMask groundLayer;
    public bool grounded;
    public float groundRayDistance;

    public override void Start()
    {
        base.Start();
        hitStunTimer.AttachHookToObj(gameObject);
        currentGrav = gravity;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        ApplyGravity();
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
}
