﻿using System;
using System.Collections;
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
        public virtual GameObject SpawnBullet(Vector3 pos, float angle)
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

        public override void Equip(Animator anim)
        {
            base.Equip(anim);
            gameObject.SetActive(true);
        }
        public override void Unequip()
        {
            throw new NotImplementedException();
        }
        protected virtual void OnDestroy()
        {
            fireRate.DestroyHook();
        }
    }
}
