using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using ZenUtil;

public class EnemyAIBase : MonoBehaviour
{
    public enum MoveState
    { 
        walk,
        run,
        stand,
    }

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
    public Vector2 velocity;
    
    public FOV lineOfSight;
    public float loseTargetDist = 12;
    Rigidbody2D rb;
    public void Start()
    {
        turnCooldown.AttachHookToObj(gameObject);
        rb = GetComponent<Rigidbody2D>();
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
            switch (moveState)
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
            }


            if (velocity.y < -0.1f)
                velocity.y = -0.1f;

        }
        else
        {
            if (useGrav)
            {
                velocity.y -= gravity * Time.deltaTime;
            }
        }

        rb.velocity = velocity;
    }
    public void FixedUpdate()
    {
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
