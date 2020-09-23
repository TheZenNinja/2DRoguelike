using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenUtil;

namespace WeaponSystem
{
    public abstract class RangedWeaponBase : WeaponBase// MonoBehaviour, IWeaponInterface
    {
        public bool auto;
        public int damage = 1;
        public float speed = 10;

        [Range(0, 360)]
        public float accuracyAngle;
        public Vector3 spawnPos;

        public int RPM = 600;
        public Timer fireRate;

        public ProjectileType projectileType;
        public GameObject projectilePref;

        public float reloadDuration = 1;


        protected virtual void Start()
        {
            SetRPM();
            fireRate.AttachHookToObj(gameObject);
        }
        protected void SetRPM()
        {
            float fireDelay = 1f / ((float)RPM / 60);
            fireRate.timerLength = fireDelay;
        }
        public override void HandleInput()
        {
            if (fireRate.finished)
                if (Input.GetKeyDown(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse0) && auto))
                    Shoot();
        }
        public abstract void Shoot();
        public float GetHandAngle()
        {
            return CursorControl.instance.GetAngleFromHand();
        }
        public float GetRandomSpread()
        {
            return UnityEngine.Random.Range(-accuracyAngle / 2, accuracyAngle / 2);
        }
        public virtual List<GameObject> SpawnProjectileSpread(Vector3 pos, float spreadAngle, int count)
        {
            List<GameObject> projs = new List<GameObject>();
            float angleSubdiv = spreadAngle / count;
            int offset = count % 2 == 0 ? count/2 : (count-1)/2 ;
            
            for (int i = -offset; i < count-offset; i++)
            {
                projs.Add(SpawnProjectile(pos, GetHandAngle() + angleSubdiv * i));
            }
            return projs;
        }
        public virtual GameObject SpawnProjectile(Vector3 pos, float angle)
        {
            GameObject g = Instantiate(projectilePref, GetBarrelPos(), Quaternion.identity);
            ProjectileScript proj = g.GetComponent<ProjectileScript>();

            proj.Setup(damage, speed, angle, projectileType);

            return g;
        }

        public Vector3 GetBarrelPos()
        {
            return transform.position + transform.TransformVector(spawnPos);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GetBarrelPos(), GetBarrelPos() + transform.right);
        }

        public override string GetUIInfo() => throw new System.NotImplementedException("Override the GetUIInfo method");
        public override void Equip(Animator anim, bool suppressSwapEvent = false)
        {
            gameObject.SetActive(true);
            base.Equip(anim, suppressSwapEvent);
    }
        public override void Unequip()
        {
            gameObject.SetActive(false);
        }
        protected virtual void OnDestroy()
        {
            fireRate.DestroyHook();
        }
    }
}
