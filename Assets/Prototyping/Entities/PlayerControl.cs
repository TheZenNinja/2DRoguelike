using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenUtil;

public class PlayerControl : EntityBase
{
    public static PlayerControl instance;
    public PlayerControl() => instance = this;

    public Vector2 inputDir;
    public float speed = 5;
    public float acceleration = 10;
    public bool sprinting;
    [Header("Jumping & Gravity")]
    public bool useGrav = true;
    public float gravity = 20;
    public float jumpForce = 20;
    public float fallMulti = 1.5f;
    [Range(0,1)]
    public float jumpReleaseMulti = 1;
    public Counter doubleJump = new Counter(1);
    public bool grounded;
    public bool dropThruPlatform;
    public bool canDropThruRelease;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    public bool canJump;
    public Timer dropThru;
    public Timer cyoteTime;
    public Timer airStallTimer;
    [Header("Dashing")]
    public float dashDistance = 5;
    public bool canDash;
    public float dashDuration = 0.5f;
    public Timer dashCooldown;

    public static event Action onDodge;

    public Transform model;
    public Transform handAnim;

    public bool isFlipped;
    [SerializeField] Collider2D footCol;

    Vector2 additionalVel;
    Vector2 dashVel;

    public override void Start()
    {
        base.Start();
        canDash = true;
        airStallTimer.AttachHookToObj(gameObject);
        dashCooldown.AttachHookToObj(gameObject);
        cyoteTime.AttachHookToObj(gameObject);
        dropThru.AttachHookToObj(gameObject);
        dashCooldown.onTimeEnd = () => canDash = true;
        cyoteTime.onTimeEnd = () => canJump = false;
        dropThru.onTimeEnd = () => canDropThruRelease = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        Move();
    }
    public void HandleInput()
    {
        isFlipped = transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        transform.localScale = isFlipped ? new Vector3(-1,1,1) : new Vector3(1, 1, 1);
        //model.localScale = isFlipped ? new Vector3(-1,1,1) : new Vector3(1, 1, 1);
        //handAnim.localScale = isFlipped ? new Vector3(1, -1, 1) : new Vector3(1, 1, 1);

        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        sprinting = Input.GetKey(KeyCode.LeftShift);

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());

        //platform drop
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            if (!dropThruPlatform)
            {
                Platform.enableDropThru?.Invoke();
                dropThruPlatform = true;
                canDropThruRelease = false;
                dropThru.Restart();
            }
        }
        else
        {
            //getcomponent is to prevent the platforms from reactivating while dropping thru
            if (dropThruPlatform && canDropThruRelease)// && !GetComponent<CapsuleCollider2D>().IsTouchingLayers(platformLayer))
            {
                Platform.disableDropThru?.Invoke();
                dropThruPlatform = false;
                canDropThruRelease = false;
            }
        }
        //jump
        if (!Input.GetKey(KeyCode.S))
            if (Input.GetKeyDown(KeyCode.Space) && (canJump || doubleJump > 0))
                Jump(!canJump);
    }

    public void Move()
    {
        if (!dropThruPlatform)
            grounded = footCol.IsTouchingLayers(groundLayer);
        else
            grounded = footCol.IsTouchingLayers(groundLayer - platformLayer);

        float spd = speed;
        if (!airStallTimer.finished)
            spd *= 0.25f;

        velocity.x = Mathf.Lerp(velocity.x, inputDir.x * spd, Time.deltaTime * acceleration);

        if (grounded)
        {
            if (velocity.y < -0.1f)
                velocity.y = -0.1f;
            canJump = true;
            if (!doubleJump.atMax)
                doubleJump.SetToMax();
        }
        else
        {
            if (canJump && !cyoteTime.testing)
                cyoteTime.Restart();

        if (!airStallTimer.finished)
                    velocity.y = 0;
            else if (useGrav)
            {
                if (velocity.y > 0)
                {
                    velocity.y -= gravity * Time.deltaTime;

                    if (Input.GetKeyUp(KeyCode.Space))
                        velocity.y *= jumpReleaseMulti;
                }
                else
                    velocity.y -= gravity * fallMulti * Time.deltaTime;
            }
        }

        rb.velocity = velocity + dashVel + additionalVel;
    }

    public void AirStall(bool ignoreIfGoingUp = false)
    {
        if (grounded) return;

        if (!ignoreIfGoingUp || (ignoreIfGoingUp && velocity.y <= 0) )
            airStallTimer.Restart();
    }

    private void Jump(bool isDoubleJump)
    {
        if (isDoubleJump)
            doubleJump--;
        else
            doubleJump.SetToMax();

        canJump = false;
        velocity.y = jumpForce;
    }
    private IEnumerator Dash()
    {
        bool spotDodge = inputDir.x == 0;

        canDash = false;

        onDodge?.Invoke();

        if (spotDodge)
        {
            velocity.y = 0;
            useGrav = false;

            yield return new WaitForSeconds(0.5f);

            dashCooldown.Restart();
        }
        else
        {
            dashVel = new Vector2(inputDir.x, 0) * dashDistance / dashDuration;
            velocity.y = 0;
            useGrav = false;

            yield return new WaitForSeconds(dashDuration);

            dashCooldown.Restart();
            dashVel = Vector2.zero;
        }
        
        useGrav = true;
    }

    public void AddForce(Vector2 force) => velocity += force;
    public void AddForce(float force, Vector2 dir) => velocity += force * dir;
}
