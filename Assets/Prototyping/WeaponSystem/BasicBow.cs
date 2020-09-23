using UnityEngine;
using System.Collections;
using ZenUtil;
namespace WeaponSystem
{
    public class BasicBow : RangedWeaponBase
    {
        public override void HandleInput()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (fireRate.finished)
                    Shoot();
                else if (!fireRate.testing)
                    fireRate.Start();
            }
        }
        public override void Shoot()
        {
            audioSource.Play();
            fireRate.Restart();

            float angle = GetHandAngle() + GetRandomSpread();

            SpawnProjectile(GetBarrelPos(), angle);
        }
        public override GameObject SpawnProjectile(Vector3 pos, float angle)
        {
            GameObject g = Instantiate(projectilePref, GetBarrelPos(), Quaternion.identity);
            ProjectileScript proj = g.GetComponent<ProjectileScript>();

            proj.Setup(damage, speed, angle, projectileType);

            return g;
        }
        public override void Equip(Animator anim, bool suppressSwapEvent = false)
        {
            base.Equip(anim, suppressSwapEvent);
        }
        public override void Unequip()
        {
            base.Unequip();
        }
        public override string GetUIInfo()
        {
            return (fireRate.percent).ToString();
        }
        public override void SwapAbility()
        {
            SpawnProjectileSpread(GetBarrelPos(), 60, 5);
        }
        protected float Remap(float min, float max, float percent)
        {
            float diff = max - min;
            return min + diff * percent;
        }
    }
}