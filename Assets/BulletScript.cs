using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    Rigidbody2D rb;
    public LayerMask targetLayer;
    public bool piercing;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    public void Setup(int damage, float speed, float angle)
    {
        this.damage = damage;
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.velocity = transform.right * speed;
    }
    public void Setup(int damage, float speed, Vector3 dir)
    {
        this.damage = damage;
        rb.velocity = dir.normalized * speed;

        transform.right = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //learn this
        if (targetLayer == (targetLayer | (1<< collision.gameObject.layer)))
        {
            StandardEntity e = collision.GetComponent<StandardEntity>();
            if (e)
                e.Hit(damage);
            if (!piercing)
                Destroy(gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            Destroy(gameObject);
    }
}
