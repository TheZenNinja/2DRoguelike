using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public float range = 1;
    public float angle = 45;
    public LayerMask mask;

    public void HitEnemiesInHitbox(int damage, float range = 0, float angle = 0)
    {
        if (range > 0)
            this.range = range;
        if (angle > 0)
            this.angle = angle;

        foreach (var e in GetEntitiesInHitbox())
        {
            e.Damage(damage);
            Debug.Log(e.name);
        }
    }

    public List<Entity> GetEntitiesInHitbox()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range, mask);
        List<Entity> entities = new List<Entity>();

        foreach (var c in cols)
        {
            Entity e = c.GetComponentInParent<Entity>();
            if (e && !entities.Contains(e))
            {
                Vector3 dirToTarget = (e.transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.right, dirToTarget) <= angle / 2)
                {
                    entities.Add(e);
                }
            }
        }

        return entities;
    }

    public Vector3 GetDir()
    {
        float a = transform.eulerAngles.z + angle;
        return new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad), 0);
    }
    public Vector3 GetHalfDir()
    {
        float a = transform.eulerAngles.z + angle/2;
        return new Vector3(Mathf.Cos(a * Mathf.Deg2Rad), Mathf.Sin(a * Mathf.Deg2Rad), 0);
    }
}
