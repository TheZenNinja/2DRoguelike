using UnityEngine;
using System;
using System.Collections;
using ZenUtil;

namespace WeaponSystem
{
    public class BasicGun : RangedWeaponBase
    {
        public Sprite projectileSprite;
        public int recallDmg;
        public Action onReloadAction;
        public Counter clipAmmo = new Counter(10);
        public bool reloading
        {
            get { return _reloading; }
            set
            {
                _reloading = value;
                if (anim)
                    anim.SetBool("Reloading", value);
            }
        }
        [SerializeField]
        protected bool _reloading = false;
        protected bool canReload => !clipAmmo.atMax;

        protected override void Start()
        {
            base.Start();
            reloading = false;
            clipAmmo.SetToMax();
        }
        public override void HandleInput()
        {
            if (reloading)
                return;

            if (Input.GetKeyDown(KeyCode.Mouse0) && clipAmmo.atMin)
                StartCoroutine(Reload());
            if (Input.GetKeyDown(KeyCode.R) && canReload)
                StartCoroutine(Reload());

            if (!clipAmmo.atMin)
            base.HandleInput();
        }
        public override void Shoot()
        {
            base.Shoot();
            audioSource.Play();
            fireRate.Restart();
            //anim.SetTrigger("Throw");

            Vector2 dir = CursorControl.instance.GetDirFromHand();
            if (!clipAmmo.atMax)
                dir.RandomizeAngle(GetRandomSpread());

            SpawnProjectile(GetBarrelPos(), dir.normalized);

            clipAmmo--;
        }
        public GameObject SpawnProjectile(Vector2 pos, Vector2 dir)
        {
            return ProjectileScript.Spawn(pos, projectileSprite, speed * dir, rotationOffset: -90, glow: 3f).gameObject;
        }
        protected virtual IEnumerator Reload()
        {
            #region Old Reload
            /* Reload logic for using reserve ammo
            reloading = true;
            yield return new WaitForSeconds(reloadDuration);
            int ammoNeeded = clipAmmo.y - clipAmmo.x;

            if (reserveAmmo.x >= ammoNeeded)
            {
                clipAmmo.x = clipAmmo.y;
                reserveAmmo.x -= ammoNeeded;
            }
            else
            {
                clipAmmo.x = reserveAmmo.x;
                reserveAmmo.x = 0;
            }
            reloading = false;

            onReloadAction?.Invoke();
            onReloadAction = null;*/
            #endregion

            reloading = true;
            yield return new WaitForSeconds(reloadDuration);
            clipAmmo.SetToMax();

            onReloadAction?.Invoke();
            onReloadAction = (Action)Action.RemoveAll(onReloadAction, null);
            reloading = false;
        }
        public override string GetUIInfo() => clipAmmo.current + "/" + clipAmmo.max;
        public override void SwapAbility()
        {
            StartCoroutine(FireBurst());
        }
        IEnumerator FireBurst()
        {
            for (int i = 0; i < 4; i++)
            {
                Shoot();
                if (clipAmmo.atMin)
                    break;
                yield return new WaitForSeconds(0.1f);
            }
            clipAmmo.SetToMax();
        }
        public override void Equip(Transform root, Animator anim, bool swapEvent = false)
        {
            base.Equip(root, anim, swapEvent);
            reloading = false;
        }
        public override void Unequip(Transform root)
        {
            base.Unequip(root);
            reloading = false;
        }
    }
}