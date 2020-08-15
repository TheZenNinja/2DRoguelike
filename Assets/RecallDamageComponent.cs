using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallDamageComponent : MonoBehaviour
{
    [SerializeField] int dmg;
    Entity e;

    public void SetupData(int dmg) => this.dmg = dmg;
    public void SetEntity(Entity entity) => e = entity;
    public void RecallEvent()
    {
        if (gameObject && e)
        {
            e?.Damage(dmg);
            Destroy(gameObject);
        }
    }
}
