using UnityEngine;
using System.Collections;
using ZenUtil;
namespace WeaponSystem
{
    public class BasicBow : RangedWeaponBase
    {
        public bool charging;
        public float chargeTime = 1; 
        public float currentCharge;
        float chargePercent => Mathf.Clamp01(currentCharge / chargeTime);

        public float minSpeed;
        public int minDamage;
        public override void HandleInput()
        {
            if (!fireRate.finished)
                return;
            if (charging)
                currentCharge += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                charging = true;
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
        public void CancelCharge()
        {
            charging = false;
            currentCharge = 0;
        }
        public override void Shoot()
        {
            charging = false;

            audioSource.Play();
            fireRate.Restart();

            float angle = GetHandAngle();
            angle += GetRandomSpread() * (1 - chargePercent);

            SpawnProjectile(GetBarrelPos(), angle);
            currentCharge = 0;
        }
        public override GameObject SpawnProjectile(Vector3 pos, float angle)
        {
            GameObject g = Instantiate(projectilePref, GetBarrelPos(), Quaternion.identity);
            ProjectileScript proj = g.GetComponent<ProjectileScript>();

            int dmg = Mathf.CeilToInt(Remap(minDamage, damage, chargePercent));
            float spd = Remap(minSpeed, speed, chargePercent);

            proj.Setup(dmg, spd, angle, projectileType);

            return g;
        }
        public override void Equip(Animator anim)
        {
            base.Equip(anim);
            CancelCharge();
        }
        public override void Unequip()
        {
            base.Unequip();
            if (charging)
                CancelCharge();
        }
        public override string GetUIInfo()
        {
            return (currentCharge / chargeTime).ToString();
        }
        protected float Remap(float min, float max, float percent)
        {
            float diff = max - min;
            return min + diff * percent;
        }
    }
}