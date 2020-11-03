using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecallDamageComponent : MonoBehaviour
{
    [SerializeField] int dmg;
    [SerializeField] StandardEntity e;

    public void SetupData(int dmg) => this.dmg = dmg;
    public void SetEntity(StandardEntity entity) => e = entity;
    public void RecallEvent()
    {
        if (!this)
            return;

        if (gameObject)
        {
            e?.Hit(dmg);
            GetComponent<ProjectileScript>().Destroy();
        }
    }
}
