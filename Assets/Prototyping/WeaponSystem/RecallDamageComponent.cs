using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallDamageComponent : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] Entity e;

    public void SetupData(int dmg) => this.dmg = dmg;
    public void SetEntity(Entity entity) => e = entity;
    public void RecallEvent()
    {
        if (!this)
            return;

        if (gameObject)
        {
            e?.Damage(dmg);
            GetComponent<ProjectileScript>().Destroy();
        }
    }
}
