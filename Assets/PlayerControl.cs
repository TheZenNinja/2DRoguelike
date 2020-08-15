using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenClasses;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public PlayerControl() => instance = this;

    public Vector2 inputDir;
    public bool flipped;
    public SpriteRenderer sprite;
    public float speed = 5;
    public float sprintSpeed = 10;
    public float acceleration = 10;
    public bool sprinting; 

    [Header("Dashing")]
    public float dashSpeed = 20;
    public bool canDash;
    public TimedInput dashInput;
    public float dashDuration = 0.5f;
    public Timer dashCooldown;


    public static event System.Action onDash;

    Vector2 velocity;
    Vector2 dashVelocity;
    Rigidbody2D rb;

    void Start()
    {
        canDash = true;
        dashInput.SetupTapOnly(CheckDash);
        dashCooldown.onTimeEnd = () => canDash = true;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        CheckForFlipped();
        dashInput.Update(Time.deltaTime);
        dashCooldown.Update(Time.deltaTime);
    }
    public void Move()
    {
        inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        sprinting = Input.GetKey(KeyCode.LeftShift);

        Vector2 goalVel = sprinting ? inputDir * sprintSpeed : inputDir * speed;
        velocity = Vector2.Lerp(velocity, goalVel, Time.deltaTime * acceleration);

        rb.velocity = velocity + dashVelocity;
    }
    public void CheckForFlipped()
    {
        if (Input.mousePosition.x - Screen.width / 2 > 0)
            flipped = false; 
        else if (Input.mousePosition.x - Screen.width / 2 < 0)
            flipped = true;
        sprite.flipX = flipped;
    }
    private void CheckDash()
    {
        if (dashCooldown.finished)
            StartCoroutine(Dash());
    }
    private IEnumerator Dash()
    {
        canDash = false;
        dashVelocity = inputDir * dashSpeed;
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
