using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenClasses;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public PlayerControl() => instance = this;

    public Vector2 inputDir;
    public SpriteRenderer sprite;
    public float speed = 5;
    public float sprintSpeed = 10;
    public float acceleration = 10;
    public bool sprinting;
    public float gravity = 20;
    public float fallMulti = 1.5f;
    public float jumpForce = 20;
    public bool doubleJump = false;
    public bool grounded;
    public bool dropThruPlatform;
    public bool canDropThruRelease;
    public LayerMask groundLayer;
    public LayerMask platformLayer;
    public Timer dropThru;
    [Header("Dashing")]
    public float dashDistance = 5;
    public bool canDash;
    public float dashDuration = 0.5f;
    public Timer dashCooldown;

    public static event System.Action onDash;

    [SerializeField] Collider2D footCol;

    float xVelSmoothing = 0;
    Vector2 moveVel;
    Vector2 additionalVel;
    Vector2 dashVel;
    Rigidbody2D rb;

    void Start()
    {
        canDash = true;
        dashCooldown.onTimeEnd = () => canDash = true;
        dropThru.onTimeEnd = () => canDropThruRelease = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
        Move();
        dashCooldown.Update(Time.deltaTime);
        dropThru.Update(Time.deltaTime);
        Debug.Log(GetComponent<CapsuleCollider2D>().IsTouchingLayers(platformLayer));

    }
    public void HandleInput()
    {
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
            if (dropThruPlatform && canDropThruRelease && !GetComponent<CapsuleCollider2D>().IsTouchingLayers(platformLayer))
            {
                Platform.disableDropThru?.Invoke();
                dropThruPlatform = false;
                canDropThruRelease = false;
            }
        }
        //jump
        if (!Input.GetKey(KeyCode.S))
            if (Input.GetKeyDown(KeyCode.Space) && (grounded || doubleJump))
            {
                doubleJump = grounded;
                moveVel.y = jumpForce;
            }
    }

    public void Move()
    {
        if (!dropThruPlatform)
            grounded = footCol.IsTouchingLayers(groundLayer);
        else
            grounded = footCol.IsTouchingLayers(groundLayer-platformLayer);

        float spd = sprinting ? inputDir.x * sprintSpeed : inputDir.x * speed;
        moveVel.x = Mathf.Lerp(moveVel.x, inputDir.x * speed, Time.deltaTime * acceleration);

        if (grounded)
        {
            if (moveVel.y < -0.1f)
                moveVel.y = -0.1f;
        }
        else
        {
            if (moveVel.y > 0)
                moveVel.y -= gravity * Time.deltaTime;
            else
                moveVel.y -= gravity * fallMulti * Time.deltaTime;
        }

        rb.velocity = moveVel + dashVel + additionalVel;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        dashVel = new Vector2(inputDir.x,0) * dashDistance/dashDuration;
        sprite.color =  new Color(0.75f, 0.75f, 0.75f, 0.75f);
        onDash?.Invoke();
        yield return new WaitForSeconds(dashDuration);

        dashCooldown.Restart();
        dashVel = Vector2.zero;
        sprite.color =  new Color(1, 1, 1, 1);
    }
        
    public void AddForce(Vector2 force) => moveVel += force;
    public void AddForce(float force, Vector2 dir) => moveVel += force * dir;
}
