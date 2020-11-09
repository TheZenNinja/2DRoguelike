using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ProjectileScript : MonoBehaviour
{
    public int damage;
    public float rotationOffset;

    public int piercing = 0;
    public bool stickInTarget;

    protected Rigidbody2D rb;
    protected new Collider2D collider;
    protected SpriteRenderer spriteRenderer;

    public LayerMask targetLayer;
    public LayerMask groundLayer;

    public bool useGrav;
    public float gravity = 0;

    public AudioClip hitSound;
    public ParticleSystem hitParticle;
    public ParticleSystem destroyParticle;

    public bool willDespawn = true;
    public float lifetime = -1;

    public void AddComponents()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        collider = GetComponent<Collider2D>();
        if (!collider)
            collider = gameObject.AddComponent<BoxCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
            rb = gameObject.AddComponent<Rigidbody2D>();
        
    }
    protected void FixedUpdate()
    {
        if (useGrav)
            rb.velocity -= new Vector2(0, gravity * Time.fixedDeltaTime);

        transform.eulerAngles = new Vector3(0,0, rb.velocity.GetAngle() + rotationOffset);

        if (willDespawn)
        {
            lifetime -= Time.fixedDeltaTime;
            if (lifetime < 0)
            {
                Destroy(gameObject);
                Debug.Log($"Killed projectile: {gameObject.name}");
            }
        }
    }
    public static ProjectileScript Spawn(Vector2 position, Sprite sprite, Vector2 vel, bool stickInTarget = false, int pierce = 0, float gravity = 0, float lifetime = 30, bool isEnemy = false, float rotationOffset = 0, float glow = 0)
    {
        var g = new GameObject();
        g.AddComponent<Rigidbody2D>();
        g.AddComponent<BoxCollider2D>();
        g.AddComponent<SpriteRenderer>();
        ProjectileScript p = g.AddComponent<ProjectileScript>();

        //Debug.Log(p);

        #region Position, Rotation and velocity
        p.transform.position = position;
        p.rotationOffset = rotationOffset;

        p.AddComponents();
        p.rb.velocity = vel;
        p.transform.eulerAngles = new Vector3(0, 0, p.rb.velocity.GetAngle() + rotationOffset);

        p.useGrav = gravity != 0;
        p.gravity = gravity;
        #endregion

        #region collision Setup
        if (isEnemy)
        {
            p.targetLayer =  LayerMask.NameToLayer("Player");
            p.gameObject.layer =  LayerMask.NameToLayer("Enemy");
        }
        else
        {
            p.gameObject.layer =  LayerMask.NameToLayer("Player");
            p.targetLayer = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("Enemy"));
        }

        p.groundLayer = 1 << LayerMask.NameToLayer("Terrain");

        p.willDespawn = lifetime != -1;
        p.lifetime = lifetime;

        p.rb.freezeRotation = true;
        p.rb.velocity = vel;

        p.collider.isTrigger = true;
        (p.collider as BoxCollider2D).size = sprite.rect.size / sprite.pixelsPerUnit;
        #endregion

        #region Visuals
        p.spriteRenderer.sprite = sprite;
        Material m = new Material(Resources.Load<Material>("Basic Material"));
        m.SetTexture("_MainText", sprite.texture);
        m.SetColor("_Color", new Vector4(1,1,1,1) * (glow+1));

        p.spriteRenderer.material = m;
        p.spriteRenderer.sortingLayerName = "Effects";
        #endregion

        return p;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger)
            return;
        //learn this

        if (targetLayer == (targetLayer | (1 << collision.gameObject.layer)))
        {
            EntityBase e = collision.GetComponent<EntityBase>();
            if (e)
                e.Hit(damage);
            if (hitSound)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);


            if (piercing > 0)
                piercing--;
            else if (stickInTarget)
            {
                if (!collision.GetComponent<ProjectileScript>())
                {
                    if (e)
                        GetComponent<RecallDamageComponent>().SetEntity(e);

                    StickProjectile(collision.gameObject.transform);
                }
            }
            else if (piercing <= 0)
                    Destroy();
        }
        else if (groundLayer == (groundLayer | (1 << collision.gameObject.layer)))
        {
            if (hitSound)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
            if (!stickInTarget)
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
            var obj = Instantiate(destroyParticle, transform.position, transform.rotation, null);
            obj.gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
}
