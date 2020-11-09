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
                    fireRate.Restart();
            }
        }
        public override void Shoot()
        {
            base.Shoot();
            audioSource.Play();
            fireRate.Restart();

            float angle = GetHandAngle() + GetRandomSpread();

            SpawnProjectile(GetBarrelPos(), angle);
        }
        public override GameObject SpawnProjectile(Vector3 pos, float angle)
        {
            GameObject g = Instantiate(projectilePref, GetBarrelPos(), Quaternion.identity);
            ProjectileScript proj = g.GetComponent<ProjectileScript>();

            return g;
        }
        public override void Equip(Transform root, Animator anim, bool suppressSwapEvent = false)
        {
            base.Equip(root, anim, suppressSwapEvent);
        }
        public override void Unequip(Transform root)
        {
            base.Unequip(root);
        }
        public override string GetUIInfo()
        {
            return ((1-fireRate.percent)* 100).ToString("N0") + "%";
        }
        public override void SwapAbility()
        {
            SpawnProjectileSpread(GetBarrelPos(), 60, 5);
            audioSource.Play();
        }
        protected float Remap(float min, float max, float percent)
        {
            float diff = max - min;
            return min + diff * percent;
        }
    }
}