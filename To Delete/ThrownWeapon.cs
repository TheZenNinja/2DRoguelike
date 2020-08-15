using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZenClasses;

public class ThrownWeapon : EquipmentBase
{
    public int damage = 1;
    public int recallDmg = 1;
    public float speed = 10;

    public int maxAmmo = 4;
    public int currentAmmo = 4;
    public int currentReseve = 12;
    public int maxReseve = 12;

    public List<ThrownDagger> daggers;

    public float accuracyAngle;
    public Vector3 spawnPos;

    public int RPM = 600;
    public Timer fireRate;
    public TimedInput reload;

    public GameObject daggerPref;

    public bool reloading = false;

    public float reloadDuration = 1;

    void Start()
    {
        System.Action baseReload = () =>
        {
            if (!reloading)
                StartCoroutine(Reload()); 
            Debug.Log("Reload");
        };
        System.Action recallReload = () =>
        {
            if (!reloading)
                StartCoroutine(Reload(true));
            Debug.Log("Recall");
        };
        reload.Setup(baseReload, recallReload);

        reloading = false;
        currentAmmo = maxAmmo;
    }
    private void SetRPM()
    {
        float fireDelay = 1f / ((float)RPM / 60);
        fireRate.timerLength = fireDelay;
    }
    public override void HandleInput()
    {
        reload.Update(Time.deltaTime);
        fireRate.Update(Time.deltaTime);

        if (reloading)
            return;

        if (fireRate.finished && currentAmmo > 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                Shoot();
        }
        //else if (Input.GetKeyDown(KeyCode.Mouse0))
        //    StartCoroutine(Reload());
    }
    public void Shoot()
    {
        /*fireRate.Restart();
        Vector3 dir;
        if (currentAmmo < maxAmmo)
        {
            float randomAngle = UnityEngine.Random.Range(-accuracyAngle / 2, accuracyAngle / 2);
            float newAngle = FindObjectOfType<PlayerControl>().GetMouseAngle() + randomAngle;
            dir = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }
        else
            dir = FindObjectOfType<PlayerControl>().GetMouseDir();

        ThrownDagger d = Instantiate(daggerPref, transform.position + transform.TransformVector(spawnPos), Quaternion.identity).GetComponent<ThrownDagger>();
        d.Setup(damage, recallDmg, speed, dir);
        daggers.Add(d);

        currentAmmo--;*/
    }
    private IEnumerator Reload(bool recall = false)
    {
        reloading = true;
        yield return new WaitForSeconds(reloadDuration);

        if (recall)
        {
            currentAmmo = maxAmmo;
            currentReseve = maxReseve;

            foreach (var d in daggers)
                d.Recall();
            daggers.Clear();
        }
        else
        {
            int ammoNeeded = maxAmmo - currentAmmo;
            if (currentReseve >= ammoNeeded)
            {
                currentAmmo = maxAmmo;
            }
        }
        reloading = false;
    }

    public override EquipmentUIData GetUIData()
    {
        return EquipmentUIData.NewThrownData(currentAmmo, currentReseve);
    }
}
