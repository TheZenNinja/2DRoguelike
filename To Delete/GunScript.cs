using UnityEngine;
using System.Collections;
using System;
using ZenClasses;

public class GunScript : EquipmentBase
{
    private enum TriggerType
    {
        semi,
        auto,
        //burst,
    }

    [SerializeField] TriggerType trigger;
    public int damage = 1;
    public float speed = 10;

    public int maxBullets = 10;
    public int currentBullets = 10;

    public float accuracyAngle;
    public Vector3 barrelPos;
    public SpriteRenderer muzzleFlash;
    public float flashDecay = 0.1f;

    public int RPM = 600;
    public Timer fireRate;

    public GameObject bulletPref;

    public bool reloading = false;

    

    public float reloadDuration = 1;

    void Start()
    {
        SetRPM();
        muzzleFlash.enabled = false;
        reloading = false;
        currentBullets = maxBullets;
    }
    private void SetRPM()
    {
        float fireDelay = 1f / ((float)RPM / 60);
        fireRate.timerLength = fireDelay;
    }
    public override void HandleInput()
    {
        fireRate.Update(Time.deltaTime);

        if (reloading)
            return;

        if (Input.GetKeyDown(KeyCode.R) && currentBullets < maxBullets)
            StartCoroutine(Reload());

        if (fireRate.finished && currentBullets > 0)
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
        //else if (Input.GetKeyDown(KeyCode.Mouse0))
        //    StartCoroutine(Reload());
    }
    public void Shoot()
    {
        /*fireRate.Restart();
        BulletScript b = Instantiate(bulletPref, GetBarrelPos(), Quaternion.identity).GetComponent<BulletScript>();
        Vector3 dir;
        if (currentBullets < maxBullets)
        {
            float randomAngle = UnityEngine.Random.Range(-accuracyAngle / 2, accuracyAngle / 2);
            float newAngle = FindObjectOfType<PlayerControl>().GetMouseAngle() + randomAngle;
            dir = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }
        else
            dir = FindObjectOfType<PlayerControl>().GetMouseDir();

        muzzleFlash.enabled = true;
        Invoke(nameof(DisableFlash), flashDecay);
        b.Setup(damage, speed, dir);
        currentBullets--;*/
    }
    private void DisableFlash()
    {
        muzzleFlash.enabled = false;
    }
    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadDuration);
        currentBullets = maxBullets;
        reloading = false;
    }
    public Vector3 GetBarrelPos()
    {
        return transform.position + transform.TransformVector(barrelPos);
    }

    public override EquipmentUIData GetUIData()
    {
        return EquipmentUIData.NewGunData(currentBullets, maxBullets);
    }
}
