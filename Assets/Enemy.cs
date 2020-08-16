using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject deathParticle;
    Entity e;
    private void Start()
    {
        e = GetComponent<Entity>();
        e.onDie += Die;
    }
    public void Die()
    {
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
