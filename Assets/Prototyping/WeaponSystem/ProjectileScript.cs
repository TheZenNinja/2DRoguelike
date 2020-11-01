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

    public bool useGrav;
    public float gravity = 0;

    public AudioClip hitSound;
    public ParticleSystem hitParticle;
    public ParticleSystem destroyParticle;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    private void Update()
    {
        if (useGrav)
        {
            rb.velocity -= new Vector2(0, gravity * Time.deltaTime);
            transform.right = rb.velocity.normalized;
        }
    }
    public void Setup(int damage, float speed, float angle, ProjectileType type)
    {
        this.type = type;

        this.damage = damage;
        transform.eulerAngles = new Vector3(0, 0, angle);
        rb.velocity = transform.right * speed;
        if (type != ProjectileType.sticking)
            Destroy(gameObject, 30);

        useGrav = gravity != 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
            //learn this
        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            Entity e = collision.GetComponent<Entity>();
            if (e)
                e.Hit(damage);


            AudioSource.PlayClipAtPoint(hitSound, transform.position);

            switch (type)
            {
                default:
                case ProjectileType.none:
                    Destroy();
                    break;
                case ProjectileType.piercing:
                    break;
                case ProjectileType.sticking:
                    if (!collision.GetComponent<ProjectileScript>())
                    {
                        if (e)
                            GetComponent<RecallDamageComponent>().SetEntity(e);

                        StickProjectile(collision.gameObject.transform);
                    }
                    break;
            }
        }
        else if (groundLayer == (groundLayer | (1 << collision.gameObject.layer)))
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            if (type != ProjectileType.sticking)
                Destroy();
            StickProjectile(collision.transform);
        }
    }
    public void StickProjectile(Transform obj)
    {
        rb.velocity = Vector2.zero;
        transform.SetParent(obj);
        useGrav = false;
        this.enabled = false;
        rb.simulated = false;
    }
    public void Destroy()
    {
        if (destroyParticle != null)
        {
            var obj = Instantiate<ParticleSystem>(destroyParticle, transform.position, transform.rotation, null);
            obj.gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
}
