using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using WeaponSystem;
using ZenUtil;

public class EnemyAIBase : Entity
{
    public enum MoveState
    { 
        walk,
        run,
        stand,
    }

    public Timer hitStunTimer = new Timer(0.75f);
    public float hitstunForce = 3;
    public float hitstunGrav = 10;

    public MoveState moveState;
    public int lookDir = 1;
    public Transform mesh;
    
    public LayerMask playerLayer;
    public LayerMask terrainLayer;
    public Collider2D footCol;
    public bool useGrav;
    public bool grounded;
    public float gravity = 20;

    public Collider2D wallDetect;
    public Collider2D edgeDetect;

    public bool isTurning;
    public float turnSpeed = 1;
    public Timer turnCooldown = new Timer(1);


    public float walkSpeed = 6;
    public float drag = 10;

    public FOV lineOfSight;
    public float loseTargetDist = 12;
    public override void Start()
    {
        base.Start();
        turnCooldown.AttachHookToObj(gameObject);
        hitStunTimer.AttachHookToObj(gameObject);
    }
    
    public void Update()
    {
        grounded = footCol.IsTouchingLayers(terrainLayer);
        if (!isTurning && turnCooldown.finished)
        {
            if (wallDetect.IsTouchingLayers(terrainLayer) || !edgeDetect.IsTouchingLayers(terrainLayer))
                Turn();
        }

        if (grounded)
        {
            if (velocity.y < -0.1f)
                velocity.y = -0.1f;
            velocity.x = Mathf.Lerp(velocity.x, 0, drag * Time.deltaTime);

            /*switch (moveState)
            {
                case MoveState.walk:
                    velocity.x = walkSpeed * lookDir;
                    break;
                case MoveState.run:
                    break;
                default:
                case MoveState.stand:
                    velocity.x = 0;
                    break;
            }*/
        }
        else
        {
            if (useGrav)
            {
                if (hitStunTimer.finished)
                    velocity.y -= gravity * Time.deltaTime;
                else if (velocity.y > 0)
                    velocity.y -= hitstunGrav * Time.deltaTime;
            }
        }

        rb.velocity = velocity;
    }
    public override DamageEvent Damage(int dmg = 1)
    {
        DamageEvent e =base.Damage(dmg);
        if (!grounded)
        {
            velocity.y = hitstunForce;
            hitStunTimer.Start();
        }
        return e;
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        mesh.localScale = new Vector3(lookDir, 1, 1);
    }
    public void Turn()
    {
        isTurning = true;
        StartCoroutine(turn());

        IEnumerator turn()
        {
            moveState = MoveState.stand;
            yield return new WaitForSeconds(turnSpeed);
            lookDir *= -1;
            moveState = MoveState.walk;
            turnCooldown.Start();
            isTurning = false;
        }
    }
    public void OnDrawGizmosSelected()
    {
        lineOfSight.DrawDebug(transform);
    }
}
