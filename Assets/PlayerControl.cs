using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenUtil;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public PlayerControl() => instance = this;

    public Vector2 inputDir;
    public float speed = 5;
    public float sprintSpeed = 10;
    public float acceleration = 10;
    public bool sprinting;
    [Header("Jumping & Gravity")]
    public bool useGrav = true;
    public float gravity = 20;
    public float fallMulti = 1.5f;
    public float jumpForce = 20;
    public Counter doubleJump = new Counter(1);
    public bool grounded;
    public bool dropThruPlatform;
    public bool canDropThruRelease;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    public Timer dropThru;
    public bool canJump;
    public Timer cyoteTime;
    [Header("Dashing")]
    public TimedInput dashInput;
    public float dashDistance = 5;
    public bool canDash;
    public float dashDuration = 0.5f;
    public Timer dashCooldown;

    public static event System.Action onDodge;

    [SerializeField] Collider2D footCol;

    float xVelSmoothing = 0;
    Vector2 moveVel;
    Vector2 additionalVel;
    Vector2 dashVel;
    Rigidbody2D rb;

    void Start()
    {
        canDash = true;
        dashInput.SetupTapOnly(TryDash);
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
        //add dashing


        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        sprinting = Input.GetKey(KeyCode.LeftShift);
        //platform drop
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            if (!dropThruPlatform)
            {
                Platform.enableDropThru?.Invoke();
                dropThruPlatform = true;
                canDropThruRelease = false;
                dropThru.Start();
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

        float spd = sprinting ? inputDir.x * sprintSpeed : inputDir.x * speed;
        moveVel.x = Mathf.Lerp(moveVel.x, inputDir.x * speed, Time.deltaTime * acceleration);

        if (grounded)
        {
            if (moveVel.y < -0.1f)
                moveVel.y = -0.1f;
            canJump = true;
            if (!doubleJump.maxed)
                doubleJump.SetToMax();
        }
        else
        {
            if (canJump && !cyoteTime.testing)
                cyoteTime.Start();

            if (useGrav)
            {
                if (moveVel.y > 0)
                    moveVel.y -= gravity * Time.deltaTime;
                else
                    moveVel.y -= gravity * fallMulti * Time.deltaTime;
            }
        }

        rb.velocity = moveVel + dashVel + additionalVel;
    }
    private void Jump(bool isDoubleJump)
    {
        if (isDoubleJump)
            doubleJump--;
        else
            doubleJump.SetToMax();

        canJump = false;
        moveVel.y = jumpForce;
    }
    public void TryDash()
    {
        if (canDash)
            StartCoroutine(Dash());
    }
    private IEnumerator Dash()
    {
        bool spotDodge = inputDir.x == 0;

        canDash = false;

        onDodge?.Invoke();

        if (spotDodge)
        {
            moveVel.y = 0;
            useGrav = false;

            yield return new WaitForSeconds(0.5f);

            dashCooldown.Restart();
        }
        else
        {
            dashVel = new Vector2(inputDir.x, 0) * dashDistance / dashDuration;
            moveVel.y = 0;
            useGrav = false;

            yield return new WaitForSeconds(dashDuration);

            dashCooldown.Restart();
            dashVel = Vector2.zero;
        }
        
        useGrav = true;
    }

    public void AddForce(Vector2 force) => moveVel += force;
    public void AddForce(float force, Vector2 dir) => moveVel += force * dir;
}
