using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType
{
    none,
    piercing,
    sticking,
}

public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public ProjectileType type;

    Rigidbody2D rb;
    public LayerMask targetLayer;
    public LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    private void Start()
    {
    }
    public void Setup(int damage, float speed, float angle, ProjectileType type)
    {
        this.type = type;

        this.damage = damage;
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.velocity = transform.right * speed;
        if (type != ProjectileType.sticking)
            Destroy(gameObject, 10f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //learn this
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            Entity e = collision.GetComponent<Entity>();
            if (e)
                e.Damage(damage);

            switch (type)
            {
                default:
                case ProjectileType.none:
                Destroy(gameObject);
                    break;
                case ProjectileType.piercing:
                    break;
                case ProjectileType.sticking:
                    if (!collision.GetComponent<ProjectileScript>())
                    {
                        if (e)
                            GetComponent<RecallDamageComponent>().SetEntity(e);

                        rb.velocity = Vector2.zero;
                        transform.parent = collision.transform;
                        this.enabled = false;
                    }
                    break;
            }
        }
        if (groundLayer == (groundLayer | (1 << collision.gameObject.layer)))
        {
            if (type != ProjectileType.sticking)
                Destroy(gameObject);
        }
    }
}
