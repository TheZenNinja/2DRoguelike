using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class EquipmentManager : MonoBehaviour
{
    public PrototypeWeaponItem[] weaponItems = new PrototypeWeaponItem[3];
    public int weaponIndex;
    public IWeaponInterface[] weaponObjs = new IWeaponInterface[3];
    private IWeaponInterface currentWeapon => weaponObjs[weaponIndex];

    public Transform playerHands;
    public PlayerControl playerControl;
    public EquipmentUI ui;
    public Animator anim;
    public Transform weaponRoot;

    void Start()
    {
        for (int i = 0; i < weaponItems.Length; i++)
        {
            var e = Instantiate(weaponItems[i].prefab, weaponRoot).GetComponent<IWeaponInterface>();
            Debug.Log(e);
            weaponObjs[i] = e;
        }

        SwapWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwapWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwapWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwapWeapon(2);


        currentWeapon.HandleInput();
    }
    public void SwapWeapon(int index)
    {
        weaponIndex = index;
        for (int i = 0; i < weaponObjs.Length; i++)
        {
            if (i == index)
                weaponObjs[i].Equip();
            else
                weaponObjs[i].Unequip();
        }
        ui.SetWeapon(currentWeapon);
    }
}
