using UnityEngine;
using System.Collections;
using ZenClasses;

public class BowScript : EquipmentBase
{
    private enum BowType
    {
        normal,
        crossbow,
    }

    public float chargePercent { get { return Mathf.Clamp01(currentTime/drawTime); } }
    public bool overDrawn { get { return currentTime >= cautionTime; } }

    public float drawTime = 1f;
    public float cautionTime = 2f;
    public float releaseTime = 3f;
    public float currentTime;
    public bool bowDrawn;

    public float minSpeed = 5, maxSpeed = 20;
    public int minDamage = 2, maxDamage = 10;

    public float reloadDuration = 0.5f;
    public bool reloading;
    public Vector3 spawnPos;
    public GameObject projectilePref;
    private void Start()
    {
        reloading = false;
    }
    public override void HandleInput()
    {
        if (reloading)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bowDrawn = true;
        }
        else if (bowDrawn)
        {
            if ((currentTime >= releaseTime || Input.GetKeyUp(KeyCode.Mouse0)))
                Shoot();
            if (Input.GetKeyDown(KeyCode.R))
            {
                bowDrawn = false;
                currentTime = 0;
            }
        }

        if (bowDrawn)
        {
            currentTime += Time.deltaTime;
        }
    }
    public void Shoot()
    {
        /*bowDrawn = false;

        Vector3 dir;
        if (overDrawn)
        {
            float randomAngle = Random.Range(-20f, 20f);
            float newAngle = CursorControl.GetMouseAngle() + randomAngle;
            dir = new Vector3(Mathf.Cos(newAngle * Mathf.Deg2Rad), Mathf.Sin(newAngle * Mathf.Deg2Rad));
        }
        else
            dir = FindObjectOfType<PlayerControl>().GetMouseDir();

        int damage = Mathf.RoundToInt(UtilFunctions.Remap01(chargePercent, minDamage, maxDamage));
        if (overDrawn)
            damage = Mathf.RoundToInt(damage * 0.75f);

        float speed = UtilFunctions.Remap01(chargePercent, minSpeed, maxSpeed);

        BulletScript b = Instantiate(projectilePref, transform.position + transform.TransformVector(spawnPos), Quaternion.identity).GetComponent<BulletScript>();
        b.Setup(damage, speed, dir);

        currentTime = 0;
        StartCoroutine(Reload());*/
    }
    private IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(reloadDuration);
        reloading = false;
    }
    public override EquipmentUIData GetUIData()
    {
        return EquipmentUIData.NewBowData(chargePercent, overDrawn);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.TransformVector(spawnPos), 0.1f);
    }

    
}
