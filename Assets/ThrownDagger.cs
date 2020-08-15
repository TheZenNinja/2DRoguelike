using UnityEngine;
using System.Collections;
using System;

public class ThrownDagger : MonoBehaviour
{
    public int initalDamage;
    public Entity stuckEntity;
    public int recallDamage;

    public LayerMask targetLayer;

    Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(int damage, int recallDmg, float speed, Vector3 dir)
    {
        initalDamage = damage;
        recallDamage = recallDmg;

        rb.velocity = dir * speed;
        transform.right = dir;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //learn this
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            Entity e = collision.GetComponent<Entity>();
            if (e)
            {
                e.Damage(initalDamage);
                stuckEntity = e;
            }
            rb.velocity = Vector3.zero;
            GetComponent<Collider2D>().enabled = false;
            transform.parent = collision.transform;
        }

    }
    public void Recall()
    {
        if (stuckEntity)
            stuckEntity.Damage(recallDamage);
        Destroy(gameObject);
    }
}
