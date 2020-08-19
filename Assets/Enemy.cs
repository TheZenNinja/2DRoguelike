using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathParticle;
    Entity e;

    public float attackRange;
    public float attackDelay;
    public int attackDmg;

    public float detectRange = 10;

    public bool canAttack;

    public Entity player;

    private void Start()
    {
        e = GetComponent<Entity>();
        e.onDie += Die;
    }

    private void Update()
    {
        AttackPlayer();
    }
    private void FixedUpdate()
    {
        DetectPlayer();
    }
    private void DetectPlayer()
    {
        if (!player)
        {
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, attackRange);

            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].GetComponent<PlayerControl>())
                {
                    player = cols[i].GetComponent<Entity>();
                    break;
                }
            }
        }
    }

    private void AttackPlayer()
    {
        if (player)
        {
            if (player.InRangeMin(transform.position, attackRange) && canAttack)
            {
                Debug.Log("In Range");
            }
        }
    }

    public void Die()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
