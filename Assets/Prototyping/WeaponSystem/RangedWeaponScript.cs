using UnityEngine;
using System.Collections;
using ZenClasses;
using System;
namespace WeaponSystem
{
    public class RangedWeaponScript : MonoBehaviour, IWeaponInterface
    {
        protected enum TriggerType
        {
            semi,
            auto,
            charge,
            //burst,
        }
        [SerializeField] protected TriggerType trigger;
        public int damage = 1;
        public int recallDmg = 1;
        public float speed = 10;
        public Vector2Int clipAmmo = Vector2Int.one * 10;
        public Vector2Int reserveAmmo = Vector2Int.one * 200;

        [Range(0, 360)]
        public float accuracyAngle;
        public Vector3 barrelPos;
        public SpriteRenderer shootFX;
        public float flashDecay = 0.1f;

        public int RPM = 600;
        public Timer fireRate;

        public ProjectileType projectileType;
        public GameObject projectilePref;

        public bool reloading = false;
        private bool canReload { get { return clipAmmo.x < clipAmmo.y && reserveAmmo.x > 0; } }
        public float reloadDuration = 1;

        public Action onReloadAction;

        void Start()
        {
            SetRPM();
            shootFX.enabled = false;
            reloading = false;
            clipAmmo.x = clipAmmo.y;
        }
        private void SetRPM()
        {
            float fireDelay = 1f / ((float)RPM / 60);
            fireRate.timerLength = fireDelay;
        }
        public void HandleInput()
        {
            fireRate.Update(Time.deltaTime);

            if (reloading)
                return;

            if (Input.GetKeyDown(KeyCode.R) && canReload)
                StartCoroutine(Reload());

            if (fireRate.finished && clipAmmo.x > 0)
            {
                switch (trigger)
                {
                    case TriggerType.semi:
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                            Shoot();
                        break;
                    case TriggerType.auto:
                        if (Input.GetKey(KeyCode.Mouse0))
                            Shoot();
                        break;
                    default:
                        throw new NotImplementedException("Not A valid trigger type");
                }
            }
        }
        public void Shoot()
        {
            fireRate.Restart();

            GameObject g = Instantiate(projectilePref, GetBarrelPos(), Quaternion.identity);
            Debug.Log(g);
            ProjectileScript proj = g.GetComponent<ProjectileScript>();
            Debug.Log(proj);

            float angle = CursorControl.instance.GetAngleFromHand();
            if (clipAmmo.x < clipAmmo.y)
                angle += UnityEngine.Random.Range(-accuracyAngle / 2, accuracyAngle / 2);

            if (shootFX != null)
            {
                StopCoroutine(FlashEffect());
                StartCoroutine(FlashEffect());
            }


            proj.Setup(damage, speed, angle, projectileType);

            if (projectileType == ProjectileType.sticking)
            {
                RecallDamageComponent recall = proj.gameObject.AddComponent<RecallDamageComponent>();
                recall.SetupData(recallDmg);
                onReloadAction += recall.RecallEvent;
            }

            clipAmmo.x--;
        }
        private IEnumerator FlashEffect()
        {
            shootFX.enabled = true;
            yield return new WaitForSeconds(flashDecay);
            shootFX.enabled = false;
        }
        private IEnumerator Reload()
        {
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
            onReloadAction = null;
        }
        public Vector3 GetBarrelPos()
        {
            return transform.position + transform.TransformVector(barrelPos);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(GetBarrelPos(), GetBarrelPos() + transform.right);
        }

        public string GetUIInfo() => clipAmmo.x + "/" + clipAmmo.y;

        public void Equip()
        {
            gameObject.SetActive(true);
            reloading = false;
        }

        public void Unequip()
        {
            reloading = false;
            gameObject.SetActive(false);
        }
    }
}