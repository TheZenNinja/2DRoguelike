using UnityEngine;
using System.Collections;
using ZenUtil;

namespace WeaponSystem
{
    public class BasicThrowingWeapon : BasicGun
    {
        public override GameObject SpawnBullet(Vector3 pos, float angle)
        {
            var g = base.SpawnBullet(pos, angle);
            var proj = g.GetComponent<ProjectileScript>();

            if (projectileType == ProjectileType.sticking)
            {
                RecallDamageComponent recall = proj.gameObject.AddComponent<RecallDamageComponent>();
                recall.SetupData(recallDmg);
                onReloadAction += recall.RecallEvent;
            }
            return g;
        }
    }
}