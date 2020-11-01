using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class MeleeComponent : MonoBehaviour
{
    public List<MeleeHitbox> hitboxes;

    public Vector2 hitVelocity; 
    void Start()
    {
        SetHitVelocity(hitVelocity);
    }
    public void SetHitVelocity(Vector2[] forces)
    {
        for (int i = 0; i < hitboxes.Count; i++)
            hitboxes[i].hitForce = forces[i];
    }
    public void SetHitVelocity(Vector2 force)
    {
        hitVelocity = force;
        foreach (var h in hitboxes)
            h.hitForce = force;
    }
}
