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
    public float jumpForce = 20;
    public bool grounded;
    public LayerMask groundLayer;

    [Header("Dashing")]
    public float dashDistance = 5;
    public bool canDash;
    public float dashDuration = 0.5f;
    public Timer dashCooldown;

    public static event System.Action onDash;

    [SerializeField] Collider2D footCol;

    Vector2 velocity;
    Vector2 dashVelocity;
    Rigidbody2D rb;

    void Start()
    {
        canDash = true;
        dashCooldown.onTimeEnd = () => canDash = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        dashCooldown.Update(Time.deltaTime);
    }
    public void Move()
    {
        grounded = footCol.IsTouchingLayers(groundLayer);

        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && dashCooldown.finished)
            StartCoroutine(Dash());

        sprinting = Input.GetKey(KeyCode.LeftShift);

        Vector2 goalVel = Vector2.zero;
        goalVel.x = sprinting ? inputDir.x * sprintSpeed : inputDir.x * speed;
        if (!grounded)
            goalVel.y -= gravity * Time.deltaTime;
        velocity = Vector2.Lerp(velocity, goalVel, Time.deltaTime * acceleration);

        rb.velocity = velocity + dashVelocity;
    }
    private IEnumerator Dash()
    {
        canDash = false;
        dashVelocity = inputDir * dashDistance/dashDuration;
        sprite.color =  new Color(0.75f, 0.75f, 0.75f, 0.75f);
        onDash?.Invoke();
        yield return new WaitForSeconds(dashDuration);

        dashCooldown.Restart();
        dashVelocity = Vector2.zero;
        sprite.color =  new Color(1, 1, 1, 1);
    }
        
    public void AddForce(Vector2 force) => velocity += force;
    public void AddForce(float force, Vector2 dir) => velocity += force * dir;
}
