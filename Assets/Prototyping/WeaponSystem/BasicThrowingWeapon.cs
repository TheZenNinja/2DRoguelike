using UnityEngine;
using System.Collections;
using ZenUtil;

namespace WeaponSystem
{
    public class BasicThrowingWeapon : BasicGun
    {
        public override GameObject SpawnProjectile(Vector3 pos, float angle)
        {
            var g = base.SpawnProjectile(pos, angle);
            var proj = g.GetComponent<ProjectileScript>();

            if (projectileType == ProjectileType.sticking)
            {
                RecallDamageComponent recall = proj.gameObject.AddComponent<RecallDamageComponent>();
                recall.SetupData(recallDmg);
                onReloadAction += recall.RecallEvent;
            }
            return g;
        }
        public override void SwapAbility()
        {
            anim.SetTrigger("Swap");
        }
    }
}