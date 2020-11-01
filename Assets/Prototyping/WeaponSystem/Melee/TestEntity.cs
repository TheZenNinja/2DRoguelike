using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEntity : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        velocity -= Vector2.up * 10 * Time.deltaTime;
    }
    void FixedUpdate()
    {
        rb.velocity = velocity;
    }
    public void Hit()
    { 
    
    }
    public void HitWithStun()
    { 
    
    }
    public void HitWithForce(Vector2 force)
    {
        velocity = force;
    }
}
