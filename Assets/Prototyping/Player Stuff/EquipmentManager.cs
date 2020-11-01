using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;
using ZenUtil;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public EquipmentManager() => instance = this;

    public PrototypeWeaponItem[] weaponItems = new PrototypeWeaponItem[3];
    public int weaponIndex;
    public WeaponBase[] weaponObjs = new WeaponBase[3];
    public WeaponBase currentWeapon => weaponObjs[weaponIndex];

    public Transform[] holsterPos;

    public Transform playerHands;
    public PlayerControl playerControl;
    public EquipmentUI ui;
    public Animator anim;
    public Transform weaponRoot;

    public Timer swapCooldown = new Timer(1);
    void Start()
    {
        swapCooldown.AttachHookToObj(gameObject);

        for (int i = 0; i < weaponItems.Length; i++)
        {
            var e = Instantiate(weaponItems[i].prefab, weaponRoot).GetComponent<WeaponBase>();
            Debug.Log(e);
            weaponObjs[i] = e;
        }

        SwapWeapon(0, true);
        ui.SetWeapon(currentWeapon);
    }

    void Update()
    {
        currentWeapon.HandleInput();

        if (swapCooldown.finished)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwapWeapon(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                SwapWeapon(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                SwapWeapon(2);
        }
        for (int i = 0; i < weaponItems.Length; i++)
        {
            ui.UpdateCooldown(i, weaponObjs[i].swapAbilityCooldown);
        }
    }
    public void SwapWeapon(int index, bool setup = false)
    {
        swapCooldown.Start();
        weaponIndex = index;
        int holsterIndex = 0;
        for (int i = 0; i < weaponObjs.Length; i++)
        {
            if (i == index)
                weaponObjs[i].Equip(weaponRoot, anim, setup);
            else
            {
                weaponObjs[i].Unequip(holsterPos[holsterIndex]);
                holsterIndex++;
            }
        }
        ui.SetWeapon(currentWeapon);
    }
}
